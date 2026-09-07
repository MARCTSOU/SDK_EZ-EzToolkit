using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public static class FREQSET
    {
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
        // 只計算/寫入 0x1998/0x199C/0x19A0/0x1994 (channel 0 的 PLL calibration 值)，
        // 緊接在 SetStandardParameter() 的 Set SYNTHESIZER 區塊後面呼叫。
        // 0x1380 不在這裡寫 —— 由 SetTx.TxCondition_1() 負責寫最終生效的值，避免重複。
        public static void SetFrequency(StreamWriter sw, double Frequency_MHz, double dataRate, double H)
        {
            //2025/09/08 暫時是只有 38.4 才要設
            bool isOK = false;
            UInt32 DIV_INT = 0;
            UInt32 DIV_FRAC = 0;
            UInt32 REG1998 = 0;
            UInt32 REG199C = 0;
            UInt32 REG19A0 = 0;
            UInt32 REG1994 = 0;
            double fDataRate = dataRate / 1000000;
            double hf32Channel = Frequency_MHz + (fDataRate * H) / 2;
            double RXmf32Channel = Frequency_MHz;
            double TXmf32Channel = Frequency_MHz;
            double lf32Channel = Frequency_MHz - (fDataRate * H) / 2;
            double temp = 0.0;
            //if (dataRate >= 1000000)    // 1M, 2025/06/12 >=1M改成 >=960K
            if (dataRate >= 960000)    // 1M, 2025/06/12 >=1M改成 >=960K
                RXmf32Channel = RXmf32Channel - 38.4 / 40 * 2;
            else
                RXmf32Channel = RXmf32Channel - 38.4 / 40;
          
            if ((Frequency_MHz<= 2040) && (Frequency_MHz)>= 1556)
            {
                isOK = true;
                temp = (hf32Channel * 2) / (38.4 * 2) - 32;
                DIV_INT = (UInt32) temp;
                DIV_FRAC = (UInt32) ((temp - DIV_INT) * 262144);
                REG1998 = (UInt32)((DIV_INT << 18) | DIV_FRAC); ;
                //-------------------------------------------
                temp = (lf32Channel * 2) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG199C = (UInt32)((DIV_INT << 18) | DIV_FRAC); ;
                //-------------------------------------------
                temp = (RXmf32Channel * 2) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG19A0 = (UInt32)((DIV_INT << 18) | DIV_FRAC); 
                //-------------------------------------------
                temp = (TXmf32Channel * 2) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);

                REG1994 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
            }
            else if ((Frequency_MHz <= 1020) && (Frequency_MHz) >= 778)
            {
                isOK = true;
                temp = (hf32Channel * 4) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1998 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (lf32Channel * 4) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG199C = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (RXmf32Channel * 4) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG19A0 = (UInt32)((DIV_INT << 18) | DIV_FRAC); 
                //-------------------------------------------
                temp = (TXmf32Channel * 4) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1994 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
            }
            else if ((Frequency_MHz <= 510) && (Frequency_MHz) >= 389)
            {
                isOK = true;
                temp = (hf32Channel * 8) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1998 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (lf32Channel * 8) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG199C = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (RXmf32Channel * 8) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG19A0 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (TXmf32Channel * 8) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1994 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
            }
            else if ((Frequency_MHz <= 255) && (Frequency_MHz) >= 194)
            {
                isOK = true;
                temp = (hf32Channel * 16) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1998 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (lf32Channel * 16) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG199C = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (RXmf32Channel * 16) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG19A0 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (TXmf32Channel * 16) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1994 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
            }
            else if ((Frequency_MHz <= 127) && (Frequency_MHz) >= 97)
            {
                isOK = true;
                temp = (hf32Channel * 32) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1998 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (lf32Channel * 32) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG199C = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (RXmf32Channel * 32) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG19A0 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = (TXmf32Channel * 32) / (38.4 * 2) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1994 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
            }
            if (isOK)
            {
                CodeOutHelper.CodeOut(sw, "0x1998, 0x" + REG1998.ToString("X").PadLeft(8, '0') + ",");
                CodeOutHelper.CodeOut(sw, "0x199C, 0x" + REG199C.ToString("X").PadLeft(8, '0') + ",");
                CodeOutHelper.CodeOut(sw, "0x19A0, 0x" + REG19A0.ToString("X").PadLeft(8, '0') + ",");
                CodeOutHelper.CodeOut(sw, "0x1994, 0x" + REG1994.ToString("X").PadLeft(8, '0') + ",");
            }
            else
            {
                ;
            }
        }

    }
}
