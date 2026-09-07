using System.IO;

namespace ER41xxTestTool
{
    /// <summary>
    /// TRx_Config_Array[] 開頭的固定 PA 對照表 (0x1F00 ~ 0x1F38, 共 15 個)。
    /// 這 15 個 register 是固定值, 不隨任何參數改變, 每次 GENERATE 都要輸出。
    ///
    /// 值以 32-bit 形式傳給 CodeOutHelper.CodeOut(), 由它依 8bits/32bits 選項決定輸出格式:
    ///   32bits -> 0x1f00, 0x03020101,
    ///   8bits  -> 0x1f00, {0x01, 0x01, 0x02, 0x03},     (LSB-first, reg_data[0] 是 bit7:0)
    /// </summary>
    public static class PaSetting
    {
        public static void Set(StreamWriter sw)
        {
            sw.WriteLine("    //====== " + RegCategories.Get(0x1F00) + " ======");
            CodeOutHelper.CodeOut(sw, "0x1f00, 0x03020101,");   // {0x01, 0x01, 0x02, 0x03}
            CodeOutHelper.CodeOut(sw, "0x1f04, 0x07060504,");   // {0x04, 0x05, 0x06, 0x07}
            CodeOutHelper.CodeOut(sw, "0x1f08, 0x0D0C0A08,");   // {0x08, 0x0a, 0x0c, 0x0d}
            CodeOutHelper.CodeOut(sw, "0x1f0c, 0x12100F0E,");   // {0x0e, 0x0f, 0x10, 0x12}
            CodeOutHelper.CodeOut(sw, "0x1f10, 0x19171513,");   // {0x13, 0x15, 0x17, 0x19}
            CodeOutHelper.CodeOut(sw, "0x1f14, 0x1F1E1D1B,");   // {0x1b, 0x1d, 0x1e, 0x1f}
            CodeOutHelper.CodeOut(sw, "0x1f18, 0x27252321,");   // {0x21, 0x23, 0x25, 0x27}
            CodeOutHelper.CodeOut(sw, "0x1f1c, 0x2F2D2B29,");   // {0x29, 0x2b, 0x2d, 0x2f}
            CodeOutHelper.CodeOut(sw, "0x1f20, 0x38363331,");   // {0x31, 0x33, 0x36, 0x38}
            CodeOutHelper.CodeOut(sw, "0x1f24, 0x3F3E3C3A,");   // {0x3a, 0x3c, 0x3e, 0x3f}
            CodeOutHelper.CodeOut(sw, "0x1f28, 0x46444241,");   // {0x41, 0x42, 0x44, 0x46}
            CodeOutHelper.CodeOut(sw, "0x1f2c, 0x4F4E4D49,");   // {0x49, 0x4d, 0x4e, 0x4f}
            CodeOutHelper.CodeOut(sw, "0x1f30, 0x5E595551,");   // {0x51, 0x55, 0x59, 0x5e}
            CodeOutHelper.CodeOut(sw, "0x1f34, 0x6B68625F,");   // {0x5f, 0x62, 0x68, 0x6b}
            CodeOutHelper.CodeOut(sw, "0x1f38, 0x7F7F7871,");   // {0x71, 0x78, 0x7f, 0x7f}
        }
    }
}
