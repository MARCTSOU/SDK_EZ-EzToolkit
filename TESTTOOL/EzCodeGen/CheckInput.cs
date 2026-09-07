using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    public class CheckInput
    {
        public static void Int(object sender, KeyPressEventArgs e, int lower, int upper)
        {
            // 1. 只允許數字、控制鍵 (例如 Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            // 2. 取得「按下按鍵後」預期會出現的完整字串
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;
            string currentText = tb.Text;

            // 考慮到使用者可能選取部分文字再覆蓋輸入，需處理選取範圍
            string nextText = currentText.Remove(tb.SelectionStart, tb.SelectionLength)
                                         .Insert(tb.SelectionStart, e.KeyChar.ToString());

            // 3. 判斷數值範圍 (0-127)
            if (!char.IsControl(e.KeyChar)) // 只有輸入數字時才檢查範圍，Backspace 不檢查
            {
                if (int.TryParse(nextText, out int value))
                {
                    if (value < lower || value > upper)
                    {
                        e.Handled = true; // 超出範圍，不允許輸入
                    }
                }

                // 處理特殊情況：防止輸入多個 0 (例如 "00")
                if (nextText.Length > 1 && nextText.StartsWith("0"))
                {
                    e.Handled = true;
                }
            }
        }
        public static bool Text(string text, int lower, int upper)
        {
            bool res = true;
            // 3. 判斷數值範圍 (0-127)
            if (int.TryParse(text, out int value))
            {
                if (value < lower || value > upper)
                {
                    res = false; // 超出範圍，不允許輸入
                }
            }
            return res;
        }
        public static void CheckNull(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            // 檢查是否為空字串或只包含空白字元
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                // 做法 A：跳出警告視窗
                GenMsg.Show("Value required!", "WARNING");

                // 做法 B：自動補回預設值 (例如 0)
                // tb.Text = "0";

                // 做法 C：強制讓游標回到該 TextBox (注意：這可能導致使用者無法關閉視窗，需謹慎使用)
                // tb.Focus(); 
            }
        }

        public static int CheckTxRxThr(string txt, int lower, int upper)
        {
            // 1. 嘗試將字串轉換為數字
            // TryParse 會回傳 bool，如果成功，數值會存入 result
            if (int.TryParse(txt, out int result))
            {
                // 2. 判斷數字是否在上限及下限之間 (包含邊界)
                if (result >= lower && result <= upper)
                {
                    return result;
                }
            }

            // 如果上述任一條件不成立，則回傳 9999 (或您指定的錯誤代碼)
            return 999;
        }

        public static string GetSyncword(string syncwordTxt)
        {
            string input = syncwordTxt.Trim();

            // (2) 長度必須為 8 個字元
            if (input.Length != 8)
            {
                GenMsg.Show("Syncword need 4 bytes (8 chars)", "WARNING");
                return null;
            }

            // (1) 判斷是否為 16 進位 (0-9, A-F)
            if (!Regex.IsMatch(input, @"\A[0-9A-Fa-f]{8}\z"))
            {
                GenMsg.Show("Syncword only 0-9, A-F (hex chars)", "WARNING");
                return null;
            }

            // 統一轉成大寫（通常 firmware / register 比較乾淨）
            return input.ToUpper();
        }

        public static uint GetAllowBitError(string bitErr)
        {
            string input = bitErr.Trim();

            // 1. 嘗試解析為 uint (這會自動擋掉負數與非數字字串)
            if (uint.TryParse(input, out uint result))
            {
                // 2. 判斷是否在 0 ~ 7 之間
                if (result <= 7 && result > -0)
                {
                    return result;
                }
            }
            // 3. 若不符合條件，顯示警告並回傳預設值 (或丟出 Exception)
            GenMsg.Show("WRONG SET: Value must be 0-7", "WARNING");
            return 9999; // 回傳預設值
        }

        public static uint GetPreambleContent(string input)
        {
            //string input = preambleContent; // 去除前後空白
            uint res = 0;
            // 1. 判斷字元長度是否合法 (1 或 2 個字元)
            if (input.Length < 1 || input.Length > 2)
            {
                GenMsg.Show("WRONG SET", "WARNING");
                return 9999;
            }

            // 2. 嘗試將 16 進位字串轉為 uint
            // HexNumber 樣式允許處理如 "AA", "07", "F" 等字串
            if (uint.TryParse(input, System.Globalization.NumberStyles.HexNumber, null, out uint result))
            {
                // 轉換成功，此時 result 最大就是 255 (0xFF)
                // 這裡可以接著執行 writeData(result) 或其他邏輯
                return result;
            }
            else
            {
                // 如果輸入了非 16 進位的字元 (例如 "G1")
                GenMsg.Show("Invalid Hex Character", "WARNING");
                return 9999;
            }
        }
        public static uint GetPreambleLength(string input)
        {
            //string input = preambleLen.Trim();
            // 1. 嘗試解析為 uint (這會自動擋掉負數與非數字字串)
            if (uint.TryParse(input, out uint result))
            {
                // 2. 判斷是否在 1 ~ 16 之間
                if (result <= 16 && result > 0)
                {
                    return result;
                }
            }
            // 3. 若不符合條件，顯示警告並回傳預設值 (或丟出 Exception)
            GenMsg.Show("WRONG SET: Value must be 1-16", "WARNING");
            return 9999; // 回傳預設值
        }
    }
}
