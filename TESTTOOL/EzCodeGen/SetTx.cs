using System;
using System.IO;

namespace ER41xxTestTool
{
    public static class SetTx
    {
        // After 2024/12/09     VER2.1
        public static void TxRegister(StreamWriter sw, UInt32 crystal_HZ, double dataRate, bool isGFSK)   // Version2 2024/12/09
        {
            UInt32 REG13ac = 0;
            UInt32 REGa030 = 0;
            uint tmp = 0;
            uint DR = (uint)dataRate;
            tmp = (uint)(crystal_HZ / (DR * 10));
            tmp = tmp & 0x000FFFFF;   // TPM_DMI_FS_PARM是bit[19:0]共20 bit(見暫存器規格表), 不是16 bit
            // if GFSK: 0x13ac = 0x3030_xxxx
            // else if FSK: 0x13ac=0x3020_xxxx
            if (isGFSK)
                REG13ac = 0x30300000 | tmp;
            else 
                REG13ac = 0x30200000 | tmp;
            //------------------------------------------

            REGa030 = (crystal_HZ / (uint)(20 * dataRate));
            REGa030 = REGa030 << 16;
            //REG1998.ToString("X").PadLeft(8, '0') + ",");
            sw.WriteLine("   //====== " + RegCategories.Get(0x13AC) + " ======");
            CodeOutHelper.CodeOut(sw, "0x13ac, 0x" + REG13ac.ToString("X").PadLeft(8, '0') + ",");
            CodeOutHelper.CodeOut(sw, "0xa030, 0x" + REGa030.ToString("X").PadLeft(8, '0') + ",");
        }

        public static void TxCondition_1(StreamWriter sw, double txdataRate, double Frequency_MHz, double txH)
        {
            //是否符合4種情形:
            if (((Frequency_MHz <= 1020 && Frequency_MHz >= 778) && (txdataRate == 1920000) && (txH == 1)) ||
                    ((Frequency_MHz <= 1020 && Frequency_MHz >= 778) && (txdataRate == 960000) && (txH == 2)) ||
                    ((Frequency_MHz <= 510 && Frequency_MHz >= 389) && (txdataRate == 1920000) && (txH == 0.5)) ||
                    ((Frequency_MHz <= 510 && Frequency_MHz >= 389) && (txdataRate == 960000) && (txH == 1)))
            {
                CodeOutHelper.CodeOut(sw, "0x1380, 0x0014F785,");
                CodeOutHelper.CodeOut(sw, "0x13a0, 0x001F41F4,");
            }
            else
            {
                // 0x1380頻段常數: 依Frequency_MHz所在頻段選值(對應Python ER4100_API_V405_1217.py
                // TRx_SetChannel()的5組頻段常數/channel_pll_table.cs的同一組頻段邊界)。
                // 修正前這裡不分頻段固定寫0x0014A785(只對389-510MHz頻段是對的), 778-1020MHz
                // (常用915/868MHz band)等其餘頻段都會被寫錯。
                string reg1380;
                if (Frequency_MHz <= 2040 && Frequency_MHz >= 1556)
                    reg1380 = "0x0018A785";
                else if (Frequency_MHz <= 1020 && Frequency_MHz >= 778)
                    reg1380 = "0x0016A785";
                else if (Frequency_MHz <= 510 && Frequency_MHz >= 389)
                    reg1380 = "0x0014A785";
                else if (Frequency_MHz <= 255 && Frequency_MHz >= 194)
                    reg1380 = "0x0012A785";
                else if (Frequency_MHz <= 127 && Frequency_MHz >= 97)
                    reg1380 = "0x0010A785";
                else
                    reg1380 = "0x0014A785";  // Frequency_MHz不在任何支援頻段內, 沿用修正前的預設值

                CodeOutHelper.CodeOut(sw, "0x1380, " + reg1380 + ",");
                CodeOutHelper.CodeOut(sw, "0x13a0, 0x00708708,");
            }
        }

    }
}
