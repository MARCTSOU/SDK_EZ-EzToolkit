using System;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    /// <summary>
    /// v1.19L: GENERATE 按下後彈出的預覽視窗, 取代原本主畫面上常駐的 gentext。
    /// SAVE 鈕的存檔邏輯仍由 MainForm (CodeGenProcess.cs) 負責, 這裡只負責顯示內容並轉發按鍵事件。
    /// </summary>
    public partial class CodeGenPreviewForm : Form
    {
        public event EventHandler SaveClicked;

        public string PreviewText => previewText.Text;

        public CodeGenPreviewForm(string content)
        {
            InitializeComponent();
            previewText.Text = content;
            CancelButton = btnExit;
            ActiveControl = btnExit;   // 避免唯讀 previewText 取得初始焦點時整段文字被反白選取
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
