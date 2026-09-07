using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class channel_pll_table
    {                                                               // uint: MHz (double)
        public static void WriteItem(StreamWriter sw, bool is384, double[] freqArray, int size, double dataRate, double H)
        {
            int i = 0;
            string hexString = "";
            uint[] REG_ARRAY = new uint[4];    // REG_ARRAY[0]=REG1998;[1]=REG199C;[2]=REG19A0;[3]=REG1994;
            uint tmp = 0;
            sw.WriteLine("/* Exported constants --------------------------------------------------------*/");
            sw.WriteLine("/* Channel Table*/");
            sw.WriteLine("code const ch_pll_item_t channel_pll_table[] = {");
            for (i=0; i<size; i++)
            {
                sw.WriteLine("    { // Freq"+i+" = "+ freqArray[i].ToString()+" MHz");                
                tmp = (uint)(freqArray[i] * 1000000);
                hexString = MainForm.is8BitsFormat ? TransferTo8BitsHexString(tmp) : TransferTo32BitsHexString(tmp);
                sw.WriteLine("        "+ hexString+",");
                if (is384)
                {
                    REG_ARRAY = GetRegArray(freqArray[i], dataRate, H);
                    hexString = MainForm.is8BitsFormat ? TransferTo8BitsHexString(REG_ARRAY[0]): TransferTo32BitsHexString(REG_ARRAY[0]); 
                    sw.WriteLine("        "+ hexString +", // 1998");
                    hexString = MainForm.is8BitsFormat ? TransferTo8BitsHexString(REG_ARRAY[1]) : TransferTo32BitsHexString(REG_ARRAY[1]); 
                    sw.WriteLine("        " + hexString + ", // 199C");
                    hexString = MainForm.is8BitsFormat ? TransferTo8BitsHexString(REG_ARRAY[2]) : TransferTo32BitsHexString(REG_ARRAY[2]); 
                    sw.WriteLine("        " + hexString + ", // 19A0");
                    hexString = MainForm.is8BitsFormat ? TransferTo8BitsHexString(REG_ARRAY[3]) : TransferTo32BitsHexString(REG_ARRAY[3]);
                    sw.WriteLine("        " + hexString + ", // 1994");
                }
                sw.WriteLine("    },");                
            }
            sw.WriteLine("};");
        }

        public static uint[] GetRegArray(double Frequency_MHz, double dataRate, double H)
        {
            //2025/09/08 暫時是只有 38.4 才要設 
            uint[] REG_ARRAY = new UInt32[4];
            UInt32 DIV_INT = 0;
            UInt32 DIV_FRAC = 0;
            UInt32 REG1998 = 0;
            UInt32 REG199C = 0;
            UInt32 REG19A0 = 0;
            UInt32 REG1994 = 0;
            double fDataRate = dataRate / 1000000;
            double hf32Channel = Frequency_MHz + ((fDataRate * H) / 2);
            double RXmf32Channel = Frequency_MHz;
            double TXmf32Channel = Frequency_MHz;
            double lf32Channel = Frequency_MHz - ((fDataRate * H) / 2);
            double temp = 0.0;
            //if (dataRate >= 1000000)    // 1M, 2025/06/12 >=1M改成 >=960K
            if (dataRate >= 960000)    // 1M, 2025/06/12 >=1M改成 >=960K
                RXmf32Channel = RXmf32Channel - (38.4 / 40) * 2;
            else
                RXmf32Channel = RXmf32Channel - (38.4 / 40);

            if ((Frequency_MHz <= 2040) && (Frequency_MHz) >= 1556)
            {
                temp = ((hf32Channel * 2) / (38.4 * 2)) - 32;
                DIV_INT = (UInt32)temp;
                DIV_FRAC = (UInt32)((temp - DIV_INT) * 262144);
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
                temp = ((hf32Channel * 4) / (38.4 * 2)) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1998 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = ((lf32Channel * 4) / (38.4 * 2)) - 32;
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
                temp = ((hf32Channel * 8) / (38.4 * 2)) - 32;
                DIV_INT = (uint)temp;
                DIV_FRAC = (uint)((temp - DIV_INT) * 262144);
                REG1998 = (UInt32)((DIV_INT << 18) | DIV_FRAC);
                //-------------------------------------------
                temp = ((lf32Channel * 8) / (38.4 * 2)) - 32;
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
            REG_ARRAY[0] = REG1998;
            REG_ARRAY[1] = REG199C;
            REG_ARRAY[2] = REG19A0;
            REG_ARRAY[3] = REG1994;

            return REG_ARRAY;

        }

        public static string TransferTo32BitsHexString(uint tmp)
        {
            return $"0x{tmp:X8}";
        }

        public static string TransferTo8BitsHexString(uint tmp)
        {
            string hexString = "";

            // 將 uint 轉換為 byte 陣列 (4 bytes)
            byte[] bytes = BitConverter.GetBytes(tmp);

            // 將陣列轉換為 0xXX 格式並用逗號隔開
            hexString = string.Join(", ", bytes.Select(b => $"0x{b:X2}"));  
            return hexString;
        }

    }
}
