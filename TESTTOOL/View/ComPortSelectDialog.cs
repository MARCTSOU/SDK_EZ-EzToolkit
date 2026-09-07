using System;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    // v1.21L: COM PORT 選擇改用獨立對話方塊(仿 Tera Term New Connection),
    // ComboBox 是一般 Form 上的控制項、不再巢狀在 ToolStrip 選單裡,
    // 徹底避開 ToolStripComboBox 巢狀彈出清單點擊失效的問題
    //
    // ShowDialog() 回傳值:
    //   OK      -> 使用者選好 port 按 OK, 呼叫端要執行連線
    //   Abort   -> 使用者按 DISCONNECT, 呼叫端要執行斷線
    //   其他     -> 只是關閉對話方塊, 不做任何事
    public partial class ComPortSelectDialog : Form
    {
        public string SelectedPort => portCombo.Text;

        public ComPortSelectDialog(string[] portNames, string currentPort, string baudRateText, bool isConnected)
        {
            InitializeComponent();

            baudLabel.Text = baudRateText;
            portCombo.Enabled = !isConnected;
            SetPortItems(portNames, currentPort);
            UpdateDeviceInfo();

            portCombo.SelectedIndexChanged += (s, e) => UpdateDeviceInfo();

            refreshButton.Enabled = !isConnected;
            refreshButton.Click += (s, e) =>
            {
                string selected = portCombo.Text;
                string[] refreshed = SerialPort.GetPortNames().Distinct().OrderBy(p => p).ToArray();
                SetPortItems(refreshed, selected);
                UpdateDeviceInfo();
                okButton.Enabled = refreshed.Length > 0 && !isConnected;
            };

            okButton.Enabled = portNames.Length > 0 && !isConnected;
            disconnectButton.Enabled = isConnected;
        }

        private void SetPortItems(string[] portNames, string preferredSelection)
        {
            portCombo.Items.Clear();
            portCombo.Items.AddRange(portNames);
            if (portNames.Length > 0)
            {
                int idx = Array.IndexOf(portNames, preferredSelection);
                portCombo.SelectedIndex = idx >= 0 ? idx : 0;
            }
        }

        /// <summary>依目前 portCombo 選的 port, 查 Windows PnP 裝置資訊填 deviceNameLabel/vidPidLabel; 查不到 (例如清單是空的、或裝置沒回報標準 USB VID/PID) 就顯示空白, 不讓 dialog 掛掉。</summary>
        private void UpdateDeviceInfo()
        {
            string portName = portCombo.Text;
            var info = GetPortDeviceInfo(portName);
            deviceNameLabel.Text = info.Name ?? "";
            vidPidLabel.Text = (info.Vid != null && info.Pid != null) ? "VID: " + info.Vid + "  PID: " + info.Pid : "";
        }

        /// <summary>
        /// 查 Win32_PnPEntity 找出目前這個 COM port 對應的 USB 裝置名稱跟 VID/PID。
        /// PNPDeviceID 格式例如 "USB\\VID_0403&amp;PID_6001\\XXXXXXXX", 用正則抓 VID_xxxx / PID_xxxx。
        /// 虛擬 port (非 USB 轉接的實體 port, 或驅動沒回報標準 PnP 資訊) 抓不到 VID/PID 是正常的, 不算錯誤。
        /// </summary>
        private static (string Name, string Vid, string Pid) GetPortDeviceInfo(string portName)
        {
            if (string.IsNullOrEmpty(portName))
                return (null, null, null);

            try
            {
                string query = "SELECT Name, PNPDeviceID FROM Win32_PnPEntity WHERE Name LIKE '%(" + portName + ")%'";
                using (var searcher = new ManagementObjectSearcher(query))
                using (ManagementObjectCollection results = searcher.Get())
                {
                    foreach (ManagementBaseObject device in results)
                    {
                        string name = device["Name"] as string;
                        string pnpId = device["PNPDeviceID"] as string;
                        string vid = null, pid = null;
                        if (!string.IsNullOrEmpty(pnpId))
                        {
                            Match vidMatch = Regex.Match(pnpId, "VID_([0-9A-Fa-f]{4})");
                            Match pidMatch = Regex.Match(pnpId, "PID_([0-9A-Fa-f]{4})");
                            if (vidMatch.Success) vid = vidMatch.Groups[1].Value.ToUpperInvariant();
                            if (pidMatch.Success) pid = pidMatch.Groups[1].Value.ToUpperInvariant();
                        }
                        return (name, vid, pid);
                    }
                }
            }
            catch
            {
                // WMI 查詢失敗 (例如系統沒有 WMI 服務、或權限問題) 時顯示空白就好, 不影響選 port/連線這個 dialog 的主要功能。
            }
            return (null, null, null);
        }
    }
}
