using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class StructItem
    {
        public static void WriteItem(StreamWriter sw)
        {
            sw.WriteLine("/* Struct --------------------------------------------------------*/");
            sw.WriteLine("typedef struct {");
            sw.WriteLine("    uint16_t addr;");
            if (MainForm.is8BitsFormat)
            {
                sw.WriteLine("    uint8_t reg_data[4];");
                sw.WriteLine("    //reg_data[0] is bit7:0 of uint32_t");
                sw.WriteLine("    //reg_data[1] is bit15:8 of uint32_t");
                sw.WriteLine("    //reg_data[2] is bit23:16 of uint32_t");
                sw.WriteLine("    //reg_data[3] is bit31:24 of uint32_t");
            }               
            else
                sw.WriteLine("    uint32_t reg_data;");

            sw.WriteLine("}t_reg_array;");
            sw.WriteLine("typedef struct {");

            if (MainForm.is8BitsFormat)
            {
                sw.WriteLine("    uint8_t freq[4];");
                sw.WriteLine("    uint8_t reg_1998[4];");
                sw.WriteLine("    uint8_t reg_199C[4];");
                sw.WriteLine("    uint8_t reg_19A0[4];");
                sw.WriteLine("    uint8_t reg_1994[4];");
            }
            else
            {
                sw.WriteLine("    uint32_t freq;");
                sw.WriteLine("    uint32_t reg_1998;");
                sw.WriteLine("    uint32_t reg_199C;");
                sw.WriteLine("    uint32_t reg_19A0;");
                sw.WriteLine("    uint32_t reg_1994;");
            }
            
            sw.WriteLine("}ch_pll_item_t;");
        }
    }
}
