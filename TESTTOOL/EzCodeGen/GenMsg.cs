using System.Windows.Forms;

namespace ER41xxTestTool
{
    /// <summary>
    /// Code Gen 路徑上的訊息框包裝。
    ///
    /// 平常 (Quiet = false) 行為與 MessageBox.Show 完全相同。
    /// 當 REG TABLE 因為使用者改了參數而自動重算時, 會把 Quiet 設成 true —— 那是背景計算,
    /// 不該因為「頻率不在頻率表內」之類的提示跳出對話框卡住使用者。此時一律回 DialogResult.OK
    /// (等同使用者按了確定/繼續), 由呼叫端原本的邏輯決定要不要繼續。
    /// </summary>
    public static class GenMsg
    {
        public static bool Quiet = false;

        public static DialogResult Show(string text)
        {
            return Quiet ? DialogResult.OK : MessageBox.Show(text);
        }

        public static DialogResult Show(string text, string caption)
        {
            return Quiet ? DialogResult.OK : MessageBox.Show(text, caption);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return Quiet ? DialogResult.OK : MessageBox.Show(text, caption, buttons);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Quiet ? DialogResult.OK : MessageBox.Show(text, caption, buttons, icon);
        }

        public static DialogResult Show(IWin32Window owner, string text)
        {
            return Quiet ? DialogResult.OK : MessageBox.Show(owner, text);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            return Quiet ? DialogResult.OK : MessageBox.Show(owner, text, caption);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            return Quiet ? DialogResult.OK : MessageBox.Show(owner, text, caption, buttons);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Quiet ? DialogResult.OK : MessageBox.Show(owner, text, caption, buttons, icon);
        }
    }
}
