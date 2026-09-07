using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class StandardTitle
    {
        public static void WriteItem(StreamWriter sw, string apiVersion)
        {
            sw.WriteLine("/* **********************************************************************");
            sw.WriteLine(" * ");
            if (MainForm.is8BitsFormat)
                sw.WriteLine(" * @8bits setting  ");
            else
                sw.WriteLine(" * @32bits setting  ");
            sw.WriteLine(" * @file     SPI_ER41xx_config.h");
            sw.WriteLine(" * @version  " + apiVersion + " for other crystal");
            sw.WriteLine(" * @brief    ER41xx series SPI driver");
            sw.WriteLine(" * config file");
            sw.WriteLine(" *");
            sw.WriteLine(" * @copyright (C) COPYRIGHT 2020 ESMT");
            sw.WriteLine(" * Technology Corp. All rights reserved.");
            sw.WriteLine(" * ********************************************************************** */");
        }


    }
}
