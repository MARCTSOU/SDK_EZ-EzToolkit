using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ER41xxTestTool
{
    public class DefineItem
    {
        public static void WriteItem(StreamWriter sw, uint dr, uint dev, string txsyncword, string rxsyncword, bool is384, int chTableNum)
        {
            string drHex = "0x" + dr.ToString("X8");
            string devHex = "0x" + dev.ToString("X8");
            byte[] drbytes = BitConverter.GetBytes(dr);
            byte[] devbytes = BitConverter.GetBytes(dev);
            string drArray = "{" + string.Join(",", drbytes.Select(b => $"0x{b:X2}")) + "}";
            string devArray = "{" + string.Join(",", devbytes.Select(b => $"0x{b:X2}")) + "}";
            sw.WriteLine("/* Define to prevent recursive inclusion -------------------------------------*/");
            sw.WriteLine("#ifndef __SPI_ER41XX_CONFIG_H__");
            sw.WriteLine("#define __SPI_ER41XX_CONFIG_H__");
            sw.WriteLine("/* Includes ------------------------------------------------------------------*/");
            sw.WriteLine("#include \"SPI_ER41xx.h\"");
            sw.WriteLine("/* Definition & Macro -------------------------------------------------------*/");
           // if (!MainForm.is8BitsFormat)
           //     sw.WriteLine("#include <stdint.h>");
           //sw.WriteLine("#include \"drv_defs.h\"");
           //sw.WriteLine("#include \"common.h\"");
            if (is384)
                sw.WriteLine("#define CRYSTAL_HZ          38400000");
            else
                sw.WriteLine("#define CRYSTAL_HZ          40000000");

            sw.WriteLine("#define NUM_OF_CH_TABLE     " + chTableNum);
            if (MainForm.is8BitsFormat)
            {
                sw.WriteLine("#define GLB_DATA_RATE "+ drArray+ "  //" +drHex+" = "+dr);
                sw.WriteLine("#define GLB_DEVIATION "+ devArray+ "  //" + devHex + " = " + dev);
            }
            else
            {
                sw.WriteLine("#define GLB_DATA_RATE " + drHex + "  //" + drHex + " = " + dr);
                sw.WriteLine("#define GLB_DEVIATION " + devHex + "  //" + devHex + " = " + dev);
            }
            sw.WriteLine("                        /* Predefined resources */");
            sw.WriteLine("#define PREDEFINED_RX_SZ                128");
            sw.WriteLine("#define PREDEFINED_RX_SYNCWORD          0x"+rxsyncword);
            sw.WriteLine("#define TABLE_END_ADDR      0xFFFF");
            if (MainForm.is8BitsFormat)
            {
                sw.WriteLine("#define TABLE_END_DATA_A    0xFF");
                sw.WriteLine("#define TABLE_END_DATA_B    0xFF");
                sw.WriteLine("#define TABLE_END_DATA_C    0xFF");
                sw.WriteLine("#define TABLE_END_DATA_D    0xFF");
            }
            else
                sw.WriteLine("#define TABLE_END_DATA    0xFFFFFFFF");

            sw.WriteLine("#define IS_TABLE_END(e) \\");
            sw.WriteLine("     ((e).addr == TABLE_END_ADDR && \\");

            if (MainForm.is8BitsFormat)
            {
                sw.WriteLine("     (e).reg_data[0] == TABLE_END_DATA_A && \\");
                sw.WriteLine("     (e).reg_data[1] == TABLE_END_DATA_B && \\");
                sw.WriteLine("     (e).reg_data[2] == TABLE_END_DATA_C && \\");
                sw.WriteLine("     (e).reg_data[3] == TABLE_END_DATA_D)");
                sw.WriteLine("     ");
            }
            else
            {
                sw.WriteLine("     (e).reg_data == TABLE_END_DATA");
                sw.WriteLine("     ");
            }
                
        }
    }
}
