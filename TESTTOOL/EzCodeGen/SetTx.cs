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
                CodeOutHelper.CodeOut(sw, "0x1380, 0x0014A785,");
                CodeOutHelper.CodeOut(sw, "0x13a0, 0x00708708,");
            }
        }

    }
}
