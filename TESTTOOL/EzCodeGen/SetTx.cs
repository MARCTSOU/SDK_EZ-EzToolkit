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
            tmp = tmp & 0x0000FFFF;
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

            // 0x1E04 (ANCTL_CONFIG_1): 對照原廠 TRx_TX_SETUP() API, 這顆是跟 0x13AC 同一個
            // TX SETUP 步驟一起算的, 不是跟 Set Freq/Dev/DataRate(0x1980~0x1990) 那組一起算的,
            // 原本放在 SetStandardParameter() 裡是放錯地方, 移過來這裡。
            // bits[7:5] ANCTL_TX_MOD: 原廠 API 在 GFSK 分支下固定寫成 FSK 編碼(=3), 這個工具
            // 目前 isGFSK 恆為 true, 對應這裡固定的 0x60 (bits[7:5]=3, 其餘全 0, 皆為硬體預設值)。
            // bit23 ANCTL_TPM_CAL_EN: 原廠是 DataRate>=100000 才開啟, 之前這裡不分情況一律開啟,
            // 是bug, 這次一併修正。
            uint reg1E04 = 0x60;
            if (dataRate >= 100000)
                reg1E04 |= (1 << 23);
            CodeOutHelper.CodeOut(sw, "0x1e04, 0x" + reg1E04.ToString("X").PadLeft(8, '0') + ",");
        }

        //public static void TxCondition_1(StreamWriter sw, double txdataRate, double Frequency_MHz, double txH)
        //{
        //    //是否符合4種情形:
        //    if (((Frequency_MHz <= 1020 && Frequency_MHz >= 778) && (txdataRate == 1920000) && (txH == 1)) ||
        //            ((Frequency_MHz <= 1020 && Frequency_MHz >= 778) && (txdataRate == 960000) && (txH == 2)) ||
        //            ((Frequency_MHz <= 510 && Frequency_MHz >= 389) && (txdataRate == 1920000) && (txH == 0.5)) ||
        //            ((Frequency_MHz <= 510 && Frequency_MHz >= 389) && (txdataRate == 960000) && (txH == 1)))
        //    {
        //        CodeOutHelper.CodeOut(sw, "0x1380, 0x0014F785,");
        //        CodeOutHelper.CodeOut(sw, "0x13a0, 0x001F41F4,");
        //    }
        //    else
        //    {
        //        CodeOutHelper.CodeOut(sw, "0x1380, 0x0014A785,");
        //        CodeOutHelper.CodeOut(sw, "0x13a0, 0x00708708,");
        //    }
        //}

    }
}
