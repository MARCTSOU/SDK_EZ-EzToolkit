using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ER41xxTestTool
{

    public static class TxPowerLv
    {
        public static void TRx_DCDC_LCurrent(StreamWriter sw)
        {
            CodeOutHelper.CodeOut(sw, "0x1204, 0x00000018,");
            CodeOutHelper.CodeOut(sw, "0x1e5c, 0x00000112,");
        }
        public static int GetCurrBand(int curr_ch)
        {
            int curr_band = 915;
            if ((899000000 <= curr_ch) && (curr_ch <= 1020000000))
            {//Band 915
                curr_band = 915;
            }
            else if (778000000 <= curr_ch)
            {//Band 868;
                curr_band = 868;
            }
            else if ((448000000 <= curr_ch) && (curr_ch <= 510000000))
            {//Band 490
                curr_band = 490;
            }
            else if (385000000 <= curr_ch)
            {//Band 433
                curr_band = 433;
            }
            else if ((194500000 <= curr_ch) && (curr_ch <= 255000000))
            {//Band 233
             //err = XT_API_ERR_TXPWR_BAND_NO_SUP | (1<<24);
                curr_band = 233;
            }
            else if ((97250000 <= curr_ch) && (curr_ch <= 127500000))
            {//Band 97~127, use 233 APIs?
                curr_band = 120;
            }
            else
            {
                curr_band = -1;
            }
            return curr_band;
        }
        public static void Set(StreamWriter sw, int freq, string paConfig, uint pwrlvl)
        {
            int curr_band = GetCurrBand(freq);
            int max_dbm = Convert.ToInt32(paConfig);
            sw.WriteLine("    //====== " + RegCategories.Get(0x1204) + " ======");
            if (curr_band > 0)
            {
                switch (curr_band)
                {
                    case 120:
                    case 233:
                    case 915:
                        switch (max_dbm)  // 10,14,16,20,23
                        {
                            case 10:
                                TRx_PAL915MHz10dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            case 14:
                                GenMsg.Show("No PA Config = 14 for 915 band", "WARNING");
                                break;
                            case 16:
                                GenMsg.Show("No PA Config = 16 for 915 band", "WARNING");
                                break;
                            case 20:
                                TRx_PAH915MHz20dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            case 23:
                                TRx_PAH915MHz23dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            default:
                                GenMsg.Show("Wrong PA Config", "WARNING");
                                break;

                        }
                        break;
                    case 868:   // 868 只有 low power
                        switch (max_dbm)  // 10,14,16,20,23
                        {
                            case 10:
                                TRx_PAL868MHz10dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            case 14:
                                TRx_PAL868MHz14dBm_FSK(sw, (uint)pwrlvl); // Low power IC
                                break;
                            case 16:
                                TRx_PAL868MHz16dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            case 20:
                                TRx_PAH868MHz20dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            case 23:
                                GenMsg.Show("No PA Config = 23 for 868 band", "WARNING");
                                break;
                            default:
                                GenMsg.Show("Wrong PA Config", "WARNING");
                                break;

                        }
                        break;

                    case 490:
                        switch (max_dbm)  // 10,14,16,20,23
                        {
                            case 10:
                                TRx_PAL490MHz10dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            case 14:
                                GenMsg.Show("No PA Config = 14 for 490 band", "WARNING");
                                break;
                            case 16:
                                GenMsg.Show("No PA Config = 16 for 490 band", "WARNING");
                                break;
                            case 20:
                                TRx_PAH490MHz20dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            case 23:
                                TRx_PAH490MHz23dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            default:
                                GenMsg.Show("Wrong PA Config", "WARNING");
                                break;
                        }
                        break;

                    case 433:
                        switch (max_dbm)  // 10,14,16,20,23
                        {
                            case 10:
                                TRx_PAL433MHz10dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            case 14:
                                GenMsg.Show("No PA Config = 14 for 433 band", "WARNING");
                                break;
                            case 16:
                                GenMsg.Show("No PA Config = 16 for 433 band", "WARNING");
                                break;
                            case 20:
                                TRx_PAH433MHz20dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            case 23:
                                TRx_PAH433MHz23dBm_FSK(sw, (uint)pwrlvl);
                                break;
                            default:
                                GenMsg.Show("Wrong PA Config", "WARNING");
                                break;
                        }
                        break;

                    default:
                        GenMsg.Show("Wrong Frequency Setting", "WARNING");
                        break;

                }
            }
        }

        public static void TRx_PAL915MHz10dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 27)
                pwrlvl = 27;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x006006c5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x00039200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000006,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000008,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }
        public static void TRx_PAH915MHz20dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 127)
                pwrlvl = 127;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x0060ffc5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x00129200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000000,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000000,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }

        public static void TRx_PAH915MHz23dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 127)
                pwrlvl = 127;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x00619fc5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x0013d200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000000,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000000,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }

        public static void TRx_PAL868MHz10dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 27)
                pwrlvl = 27;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x006006c5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x00029200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000006,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000008,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }

        public static void TRx_PAL868MHz14dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 27)
                pwrlvl = 27;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x00600505,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x00039200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000007,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x0000000c,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }

        public static void TRx_PAL868MHz16dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 27)
                pwrlvl = 27;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x006006c5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x0002d200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000002,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000008,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }

        public static void TRx_PAH868MHz20dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 127)
                pwrlvl = 127;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x0060ffc5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x00129200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000000,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000000,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }
        public static void TRx_PAL490MHz10dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 27)
                pwrlvl = 27;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x006006c5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x00029200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000006,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000008,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }
        public static void TRx_PAH490MHz20dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 127)
                pwrlvl = 127;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x00601fc5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x00139200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000006,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000008,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }
        public static void TRx_PAH490MHz23dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 127)
                pwrlvl = 127;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x00619fc5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x0013d200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000000,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000000,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }
        public static void TRx_PAL433MHz10dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 27)
                pwrlvl = 27;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x008006c5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x00029200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000006,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000008,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }

        public static void TRx_PAH433MHz20dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 127)
                pwrlvl = 127;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x00601fc5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x00131200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000003,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x0000000c,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }
        public static void TRx_PAH433MHz23dBm_FSK(StreamWriter sw, uint pwrlvl)
        {
            if (pwrlvl > 127)
                pwrlvl = 127;

            TRx_DCDC_LCurrent(sw);
            CodeOutHelper.CodeOut(sw, "0x1480, 0x00619fc5,");
            CodeOutHelper.CodeOut(sw, "0x1484, 0x0013d200,");
            CodeOutHelper.CodeOut(sw, "0x1488, 0x00000000,");
            CodeOutHelper.CodeOut(sw, "0x1e60, 0x00000000,");
            uint u32Data = (0x1000000 | ((uint)pwrlvl << 25));
            string hexString = "0x" + u32Data.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, "0x1e1c, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x10a8, " + hexString);
            CodeOutHelper.CodeOut(sw, "0x1e20, 0x13000027,");
        }

    }
}
