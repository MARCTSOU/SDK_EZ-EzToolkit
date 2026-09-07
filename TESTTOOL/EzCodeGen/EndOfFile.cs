using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class EndOfFile
    {
        public static void WriteItem(StreamWriter sw)
        {
            sw.WriteLine("    //======================INIT END===============================//");
            sw.WriteLine("#endif /*__SPI_ER41XX_CONFIG_H__*/");

            sw.WriteLine("/*** (C) COPYRIGHT 2020 ESMT Technology Corp. ***/");
        }
    }
}
