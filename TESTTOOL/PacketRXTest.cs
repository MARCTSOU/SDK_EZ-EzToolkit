using ER41xxTestTool;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ER41xxTestTool
{
    public partial class MainForm : Form
    {
        public int averageRssi = 0;
        public int totalRcv = 0;
        public int totalRssi = 0;
        public int rcvOK = 0;
        public int rcvErr = 0;
        int packetRxMaxRcvNum = 100;
        bool rxInfiniteMode = false;

        public void DisableParameterSetting()
        {
            paramDvbBandCombo.Enabled = false;
            paramPAConfigCombo.Enabled = false;
            paramBaseFreqText.Enabled = false;
            paramDataRateCombo.Enabled = false;
            paramDeviationText.Enabled = false;
            paramModulationCombo.Enabled = false;
            paramPwrLvText.Enabled = false;
            paramCapIoText.Enabled = false;
            paramRxSyncText.Enabled = false;              // RX 測試用 RX SYNCWORD
            paramRxSyncSoftInvertCheck.Enabled = false;
            paramRxSyncAllowBitErrText.Enabled = false;
        }

        public void EnableParameterSetting()
        {
            paramDvbBandCombo.Enabled = true;
            paramPAConfigCombo.Enabled = true;
            paramBaseFreqText.Enabled = true;
            paramDataRateCombo.Enabled = true;
            paramDeviationText.Enabled = true;
            paramModulationCombo.Enabled = true;
            paramPwrLvText.Enabled = true;
            paramCapIoText.Enabled = true;
            paramRxSyncText.Enabled = true;               // RX 測試用 RX SYNCWORD
            paramRxSyncSoftInvertCheck.Enabled = true;
            paramRxSyncAllowBitErrText.Enabled = true;

        }
        //const uint8_t pktHeader[] = { 0xA5, 0xB3, 0xC6, 0xD9 };
        public void PayloadAnalysisByte(byte[] packet) // 從MCU 以byte[]型式接收
        {
            try
            {
                if (packet.Length < 4)
                {
                    ShowInsLog("Packet too short\r\n");
                    return;
                }

                byte length = packet[4];                    // Length
                byte rssiByte = packet[packet.Length - 2];  // RSSI
                byte crcOk = packet[packet.Length - 1];     // CRC_OK

                int rssi = -rssiByte;                       // RSSI 轉負值
                if (crcOk == 1)
                {
                    totalRssi += rssi;
                    rcvOK++;
                    averageRssi = totalRssi / rcvOK;
                }
                else
                {
                    rcvErr++;
                }
                // ========== Timestamp ==========
                string timestamp = DateTime.Now.ToString("HH:mm:ss:fff");
                string status = crcOk == 1 ? "Ok" : "Er";

                string output = "";
                int index = 6;   // Payload 起始位置 (0xAA, 0xBB時是4)

                // --- 情況1: 有 Sequence Number ---
                if (dvbTstPktRxParseSeqCheck.Checked && packet.Length >= 6)  // 至少要 L, Seq_H, Seq_L, X, X, RSSI, CRC
                {
                    // 1️) 讀取序號
                    int seq = (packet[index] << 8) | packet[index + 1];
                    string seqText = seq.ToString("D4");  // 顯示為 0004

                    index += 2; // 往後跳過序號 2 Bytes

                    // 2️) Payload + Checksum (不含 RSSI & CRC)
                    byte[] payload = packet.Skip(index).Take(packet.Length - index - 2).ToArray();
                    string payloadStr = string.Join(" ", payload.Select(b => b.ToString("X2")));

                    // 3️) 組成輸出字串：加入序號
                    output = $"{timestamp} :{status}: LEN[{length}]: SEQ[{seqText}]: {payloadStr}\r\n";
                }
                else
                {
                    // --- 情況2: 無 Sequence Number  ---
                    byte[] payload = packet.Skip(index).Take(packet.Length - index - 2).ToArray();
                    //byte[] payload = packet.Skip(1).Take(packet.Length - 3).ToArray();
                    string payloadStr = string.Join(" ", payload.Select(b => b.ToString("X2")));

                    output = $"{timestamp} :{status}: LEN[{length}]: {payloadStr}\r\n";
                }

                // === 顯示封包 ===
                ShowRxPacket(output);

                // === 更新 RSSI ===
                this.Invoke((MethodInvoker)(() =>
                {
                    dvbTstPktRxRssiText.Text = averageRssi.ToString();
                    dvbTstPktRxOkCntText.Text = rcvOK.ToString();
                    dvbTstPktRxErrCntText.Text = rcvErr.ToString();
                }));
            }
            catch (Exception ex)
            {
                ShowInsLog("Parse error: " + ex.Message + "\r\n");
            }
        }
        public void PayloadAnalysis(string line) //從MCU送出printf格式
        {
            int crcOk = 0;
            int rssiVal = 0;

            try
            {
                string[] parts = line.Split(',');
                if (parts.Length < 3)
                {
                    ShowInsLog("Invalid packet format\r\n");
                    return;
                }

                // RSSI
                if (!int.TryParse(parts[parts.Length - 2], out rssiVal))
                {
                    ShowInsLog("RSSI parse error\r\n");
                    return;
                }
                int rssi = -rssiVal;

                // CRC
                if (!int.TryParse(parts[parts.Length - 1], out crcOk))
                {
                    ShowInsLog("CRC parse error\r\n");
                    return;
                }
                if (crcOk == 1)
                {
                    totalRssi += rssi;
                    rcvOK++;
                    averageRssi = totalRssi / rcvOK;
                }
                else
                {
                    rcvErr++;
                }

                // Header & Length
                if (!byte.TryParse(parts[1], System.Globalization.NumberStyles.HexNumber, null, out byte headerLen))
                {
                    ShowInsLog("Header parse error\r\n");
                    return;
                }

                string payloadStr = "";
                string seqStr = "";
                int displayLen = 0;

                if (dvbTstPktRxParseSeqCheck.Checked)
                {
                    // Sequence (2 bytes)
                    if (parts.Length < 5) // 至少要有 header + 2 bytes seq + payload
                    {
                        ShowInsLog("Seq packet too short\r\n");
                        return;
                    }

                    if (ushort.TryParse(parts[2] + parts[3], System.Globalization.NumberStyles.HexNumber, null, out ushort seq))
                    {
                        seqStr = seq.ToString("D4"); // 4位數字
                    }
                    else
                    {
                        // safer: 手動組合 (little endian)
                        if (byte.TryParse(parts[2], System.Globalization.NumberStyles.HexNumber, null, out byte lo) &&
                            byte.TryParse(parts[3], System.Globalization.NumberStyles.HexNumber, null, out byte hi))
                        {
                            seqStr = ((hi << 8) | lo).ToString("D4");
                        }
                    }

                    // Payload = 從 index 4 到 倒數第 3
                    var dataList = new List<byte>();
                    for (int i = 4; i < parts.Length - 2; i++)
                    {
                        if (byte.TryParse(parts[i], System.Globalization.NumberStyles.HexNumber, null, out byte b))
                            dataList.Add(b);
                    }

                    payloadStr = string.Join(" ", dataList.Select(b => b.ToString("X2")));
                    //displayLen = headerLen - 2; // 去掉 sequence 的 2 bytes // Owen 改
                    if (dvbTstPktRxCrcCombo.SelectedIndex==0)   // seq. CCIT
                        displayLen = headerLen - 2; 
                    else if (dvbTstPktRxCrcCombo.SelectedIndex == 1)   // seq. CRC16
                        displayLen = headerLen;
                }
                else
                {
                    // 無 sequence, payload = 從 index 2 到 倒數第 3
                    var dataList = new List<byte>();
                    for (int i = 2; i < parts.Length - 2; i++)
                    {
                        if (byte.TryParse(parts[i], System.Globalization.NumberStyles.HexNumber, null, out byte b))
                            dataList.Add(b);
                    }

                    payloadStr = string.Join(" ", dataList.Select(b => b.ToString("X2")));
                    //displayLen = headerLen; // headerLen 就是 payload+checksum 長度
                    if (dvbTstPktRxCrcCombo.SelectedIndex == 0)   // no seq. CCIT
                        displayLen = headerLen;
                    else if (dvbTstPktRxCrcCombo.SelectedIndex == 1)   // no seq. CRC16
                        displayLen = headerLen + 2;
                }

                // Timestamp
                string timestamp = DateTime.Now.ToString("HH:mm:ss:fff");
                string status = (crcOk == 1) ? "Ok" : "Er";

                if (dvbTstPktRxParseSeqCheck.Checked)
                    ShowRxPacket($"{timestamp} :{status}: {displayLen}: {seqStr}: {payloadStr}\r\n");
                else
                    ShowRxPacket($"{timestamp} :{status}: {displayLen}: {payloadStr}\r\n");

                // 更新 UI
                this.Invoke((MethodInvoker)(() =>
                {
                    dvbTstPktRxRssiText.Text = averageRssi.ToString();
                    dvbTstPktRxOkCntText.Text = rcvOK.ToString();
                    dvbTstPktRxErrCntText.Text = rcvErr.ToString();
                }));
            }
            catch (Exception ex2)
            {
                ShowInsLog($"Parse error: {ex2.Message}\r\n");
            }
        }
       
        public void ShowRxPacket(string str) //從MCU送出printf格式
        {
            if (dvbTstPktRxLogText.InvokeRequired)
            {
                // 如果在非 UI Thread 呼叫 (例如 DataReceived)，要用 Invoke 切回 UI Thread
                dvbTstPktRxLogText.BeginInvoke((MethodInvoker)(() => ShowRxPacket(str)));
            }
            else
            {
                // 限制文字大小，避免 TextBox 文字太多卡住
                const int MAX_LENGTH = 100000; // 例如只保留最近 100K 字
                if (dvbTstPktRxLogText.TextLength > MAX_LENGTH)
                {
                    dvbTstPktRxLogText.Text = dvbTstPktRxLogText.Text.Substring(dvbTstPktRxLogText.TextLength - MAX_LENGTH);
                }

                // 追加文字
                dvbTstPktRxLogText.AppendText(str); // AppendText 效率比 += 好

                // 自動捲到最後
                dvbTstPktRxLogText.SelectionStart = dvbTstPktRxLogText.Text.Length;
                dvbTstPktRxLogText.ScrollToCaret();
            }
        }
        
        private readonly byte[] HEADER = new byte[] { 0xA5, 0xB3, 0xC6, 0xD9 };
        private List<byte> recvBuffer = new List<byte>();
        private void UartReceiveDataByte(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytesToRead = serialPort1.BytesToRead;
                if (bytesToRead == 0) return;

                byte[] temp = new byte[bytesToRead];
                serialPort1.Read(temp, 0, bytesToRead);
                recvBuffer.AddRange(temp);  // 加到緩衝區
                
                // 開始切封包
                while (recvBuffer.Count >= 6) // 最小封包: Header(4) + Length(1) + RSSI+CRC(2)
                {
                    // 1. 找完整 4-byte Header
                    int startIndex = FindHeaderIndex(recvBuffer, HEADER);
                    if (startIndex < 0)
                    {
                        recvBuffer.Clear(); // 找不到任何 Header，就清空
                        break;
                    }

                    // 如果 Header 不是從 index 0 開始 → 清掉前方垃圾
                    if (startIndex > 0)
                    {
                        recvBuffer.RemoveRange(0, startIndex);
                    }

                    // 確保有 Length Byte
                    if (recvBuffer.Count < 5) break;

                    int payloadLength = recvBuffer[4];  // 取 Length
                    int fullPacketLength = 4 + 1 + payloadLength + 2; // Header(4) + Length + Payload+Checksum + RSSI+CRC

                    if (recvBuffer.Count < fullPacketLength) break;  // 等待更多資料

                    // 取出完整封包
                    byte[] packet = recvBuffer.Take(fullPacketLength).ToArray();
                    recvBuffer.RemoveRange(0, fullPacketLength);

                    //if (rxInfiniteMode)
                    totalRcv++;

                    // 呼叫封包解析
                    PayloadAnalysisByte(packet);
                }
            }
            catch (Exception ex)
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    ShowInsLog($"RX Error: {ex.Message}\r\n");
                }));
            }

            if ((!rxInfiniteMode) && (totalRcv >= packetRxMaxRcvNum))
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    PacketRxBtnStopProcess();
                }));
                return;
            }
        }
        //4-byte Header 的工具函式
        private int FindHeaderIndex(List<byte> buffer, byte[] header)
        {
            for (int i = 0; i <= buffer.Count - header.Length; i++)
            {
                if (buffer[i] == header[0] &&
                    buffer[i + 1] == header[1] &&
                    buffer[i + 2] == header[2] &&
                    buffer[i + 3] == header[3])
                {
                    return i;
                }
            }
            return -1;
        }

        /*  
         *  // Header 只接收 2bytes(0xAA, 0xBB) 版本
        private List<byte> recvBuffer = new List<byte>();
        private void UartReceiveDataByte(object sender, SerialDataReceivedEventArgs e)
        {
            //這是專門用在接收 PacketRx 的 byte arr[]
            //if (rxInfiniteMode)
            //    totalRcv++;

            try
            {
                int bytesToRead = serialPort1.BytesToRead;
                if (bytesToRead == 0) return;

                byte[] temp = new byte[bytesToRead];
                serialPort1.Read(temp, 0, bytesToRead);
                recvBuffer.AddRange(temp);  // 加入接收緩衝區

                // 封包解析
                while (recvBuffer.Count >= 4) // 最小封包長度: Header(2) + Length(1) + RSSI + CRC
                {
                    // 1. 找 Header
                    int startIndex = recvBuffer.IndexOf(0xAA);
                    if (startIndex < 0 || startIndex + 1 >= recvBuffer.Count)
                    {
                        recvBuffer.Clear(); // 沒找到完整 Header，清掉
                        break;
                    }
                    if (recvBuffer[startIndex + 1] != 0xBB)
                    {
                        recvBuffer.RemoveAt(startIndex);
                        continue;
                    }

                    // 2. 取得 Length
                    if (startIndex + 2 >= recvBuffer.Count) break;
                    int payloadLength = recvBuffer[startIndex + 2]; // Payload + Checksum 長度
                    int fullPacketLength = 2 + 1 + payloadLength + 2; // Header(2) + Length(1) + Payload+Checksum + RSSI + CRC

                    if (recvBuffer.Count < startIndex + fullPacketLength) break; // 不夠長，等待下一次

                    // 3. 抽出完整封包
                    byte[] packet = recvBuffer.Skip(startIndex).Take(fullPacketLength).ToArray();
                    recvBuffer.RemoveRange(0, startIndex + fullPacketLength); // 移除已處理部分
                    if (rxInfiniteMode)
                        totalRcv++; // 收到完整封包

                    // 4. 解析封包
                    PayloadAnalysisByte(packet);
                }
            }
            catch (Exception ex)
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    ShowInsLog($"RX Error: {ex.Message}\r\n");
                }));
            }
        }
        */

        // UART 資料接收事件（類似 IRQ）
        private void UartReceiveData(object sender, SerialDataReceivedEventArgs e)
        {
            if (rxInfiniteMode)
            {
                totalRcv++;
            }
            try
            {
                string line = serialPort1.ReadLine(); // 會等到 \n 才回傳
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    //ShowInsLog($"RX: {line}\r\n");
                    // 在這裡處理完整的一筆資料
                    PayloadAnalysis(line);
                }));
            }
            catch (TimeoutException)
            {
                // 可以忽略，表示等太久沒收到 \n
            }
            catch (Exception ex)
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    ;  //ShowInsLog($"RX Error: {ex.Message}\r\n");  // COM PORT stop error
                }));
            }
            if ((!rxInfiniteMode) && (totalRcv>=packetRxMaxRcvNum))
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    PacketRxBtnStopProcess();
                }));
                
            }
        }

        public bool PacketRxStartProcess()
        {
            bool res = false;
            string cmd = "";
            string stmp = "";
            int rxCRCSel = dvbTstPktRxCrcCombo.SelectedIndex;

           // cmd = "rfg 31 0\r";  // disable IRQ
           // serialPort1.Write(cmd);
            //Thread.Sleep(100);

            // 2026-09-03 暫時關閉: ER4100_SetParameter() 是呼叫DVB韌體自己的cfg API算RF組態,
            // 只支援Xtal=40MHz, 跟REG TABLE/Generate算出來的值可能對不起來。之後要改成先用
            // REG TABLE的「Write DVB Register」直接寫暫存器, 這裡先跳過, 使用者自己按過
            // Write DVB Register 才按 Start。
            // res = ER4100_SetParameter(2);
            res = true;
            if (!res)
            {
                EnableRxControlsAfterStop();
                ShowInsLog("Set Parameter Fail\r\n");
                return false;
            }
            res = RxTableSetup();
            if (!res)
            {
                EnableRxControlsAfterStop();
                ShowInsLog("Set Rx Parameter Fail\r\n");
                return false;
            }
            else
            {
                ShowInsLog("Set Parameter Finished\r\n");
                Thread.Sleep(100);
                // ---------------------------
                SetPayloadOrder();  // 0xA0A8: LSB=0 / MSB=1, 要在 RxTableSetup() 之後才不會被蓋掉
                Thread.Sleep(50);
                SetAllowBitError(); // 0xB104: RX syncword 允許的 bit error 數
                Thread.Sleep(50);
                // ---------------------------
                cmd = "rx 1 1 128 0\r";
                serialPort1.WriteLine(cmd);
                Thread.Sleep(100);
                // ---------------------------
                cmd = "rfg 27 " + rxCRCSel.ToString() + "\r";
                serialPort1.WriteLine(cmd);
                Thread.Sleep(30);
                // ---------------------------
                cmd = "settestst 1\r";     // 不印一般 LOG
                serialPort1.WriteLine(cmd);
                Thread.Sleep(30);
                // ---------------------------
                cmd = "setperst 5\r";     // per=5 (以printf方式收), per=6(以byte[]方式收)
                serialPort1.WriteLine(cmd);
                Thread.Sleep(30);

                //cmd = "rfg 32 1\r";     //收進來的byte hexReverse 一次
                //serialPort1.WriteLine(cmd);
                //Thread.Sleep(30);

                stmp = serialPort1.ReadExisting();
                Thread.Sleep(50);

                AttachUartRxHandler();
                
                if (dvbTstPktRxCntLimitRadioBtn.Checked)
                {
                    rxInfiniteMode = false;
                    packetRxMaxRcvNum = int.Parse(dvbTstPktRxCntText.Text);
                    if (packetRxMaxRcvNum <= 0)
                    {
                        ShowInsLog("Rx Rcv Num need to be > 0\r\n");
                        return false;
                    }
                }
                else
                {
                    rxInfiniteMode = true;
                }
                dvbTstPktRxCrcCombo.Enabled = false;
                dvbTstPktRxParseSeqCheck.Enabled = false;
                dvbTstPktRxCntText.Enabled = false;
                dvbTstPktRxCntLimitRadioBtn.Enabled = false;
                dvbTstPktRxCntNoLimitRadioBtn.Enabled = false;

                //cmd = "rfg 31 1\r";  // enable IRQ
                //serialPort1.Write(cmd);
                res = true;
            }
                
            return res;
        }


        public bool ConnectWithIRQ()
        {
            bool res = false;
            serialPort1.PortName = comPortToolStripComboBox.Text;   // COM Port
            serialPort1.BaudRate = baudRateNumber;    // Baudrate
            serialPort1.DataBits = 8;               
            serialPort1.StopBits = StopBits.One;
            serialPort1.Parity = Parity.None;
            serialPort1.Handshake = Handshake.None;
            serialPort1.Encoding = Encoding.ASCII;    // 視情況設定

            // 避免重複註冊事件 → 每次連線前要先移除
            //serialPort1.DataReceived -= UartReceiveData;
            //serialPort1.DataReceived += UartReceiveData;
            try
            {
                serialPort1.Open();             
                res = true;
            }
            catch (Exception ex)
            {
                ShowInsLog($"{comPortToolStripComboBox.Text} connect failed\r\n");
            }

            return res;
        }
    }
}
