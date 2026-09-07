using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    public class Comment
    {
        //(sw, xtal, capio, freqDouble.ToString(), drValue.ToString(), devValue.ToString(), mod, PAConfig, pwr, preambleContent, preambleLength, txsyncword, bitError, interruptList);
        public static void WriteItem(StreamWriter sw, string xtal, string capio, string freq, string dr, string dev, string mod, string PAConfig, string pwr, uint preambleContent, uint preambleLength, string txsyncword, string rxsyncword, uint bitError, List<string> interruptList)
        {
            sw.WriteLine("/* Comments ------------------------------------------------------------------*/");
            sw.WriteLine("// Crystal              : "+xtal+"MHz");
            sw.WriteLine("// CAP IO               : "+capio);
            sw.WriteLine("/* RF Properties */");
            sw.WriteLine("// Base frequency       : "+freq+"MHz");
            sw.WriteLine("// Data Rate            : "+dr+"bps");
            sw.WriteLine("// Deviation            : "+dev+"Hz");
            sw.WriteLine("// Modulation           : "+mod);
            sw.WriteLine("// TX Power - PA Type   : "+PAConfig+"dBm");
            sw.WriteLine("// TX Power - Level     : "+pwr);
            sw.WriteLine("/* Packet Format */");
            string hexString = "0x" + preambleContent.ToString("X2") + ", "+preambleLength.ToString()+"byte(s)";
            sw.WriteLine("// TX Preamble          : " + hexString); // 0xAA, 4bytes");
            sw.WriteLine("// Tx Syncword          : 0x"+ txsyncword.ToUpper());
            sw.WriteLine("// Rx Syncword          : 0x" + rxsyncword.ToUpper());
            sw.WriteLine("// Syncword bit error   : "+bitError.ToString()+"bit(s)");
            sw.WriteLine("/* Interrupt */");
            if (interruptList.Count > 0)
            {
                for (int i = 0; i < interruptList.Count; i++) {
                    sw.WriteLine("// " + interruptList[i]);
                }
            }
        }
    }
}
