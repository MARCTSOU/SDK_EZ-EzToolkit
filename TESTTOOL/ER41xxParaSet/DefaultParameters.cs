namespace ER41xxTestTool
{
    /// <summary>
    /// 官方預設參數規格 (2026-08-03 客戶 Marc 要求, 見專案根目錄
    /// 20260803_Marc要求\SPI_ER41xx_config_38C_output.h): 38.4MHz / 435MHz / 96000bps /
    /// 48000Hz / GFSK / PA 23dBm Level 127。
    /// 內容取自 TESTTOOL\bin\Debug\settings_0805.ini —— 已驗證這組參數用 GENERATE 產生的
    /// register 值跟現在編譯進 exe 的 REG TABLE 內建預設值 (ER41xxReg\RegTableItems.cs)
    /// 逐位元組完全相符,是唯一可信的預設值來源。
    /// 編譯進 exe, 執行期不讀外部檔案; 由 MainForm_Load 透過 LoadEzIniFile() 套用到 PARAMETER
    /// 等分頁的畫面控制項 (沿用「載入設定檔」既有的逐欄位解析/比對邏輯,不另寫一套)。
    /// 2026-08-13 修正: Marc 規格文件寫的 "Deviation: 48000Hz" 其實是 reg 0x1984 的原始值,
    /// 沒注意到 reg 0x1984 = 2 * Deviation(Hz) 這層關係; PARAMETER 分頁的 DEVIATION 欄位是
    /// 物理 fdev(kHz), 所以這裡要填 24 (0x1984=48000 才會對得上)。見 ER41xxParaSet\ParameterSet.cs
    /// 的 freqDevValue = 2*1000*freqDevDouble。
    /// </summary>
    public static class DefaultParameters
    {
        /*
         * Recommand default frquency :
         * 230                      : Meter, AMR
         * 230.50                   : Meter, AMR
         * 231.00                   : Meter, AMR
         * 433.92                   : TPMS, Key, Remote
         * 433.301                  : Remote door bell
         * 470.1                    : Meter, AMR
         * 868.10, 868.30, 868.50   : LoRa
         * 868.525                  : Smart Home
         * 915.501                  :
         * 923.2, 923.5             : Sub-G Mesh, RFID Reader
         */
        /// <summary>PARAMETER 分頁的預設頻率, 同時也是 FREQUENCYTABLE 的第一個頻點; 其餘 3 個頻點以此為基準往上加 FreqTableStepMHz。</summary>
        private const decimal DefaultFrequencyMHz = 433.301m;
        private const decimal FreqTableStepMHz = 0.1m;
        private const int CountOfFreqTable = 2;

        /// <summary>[FREQUENCYTABLE] 區塊內容: NUM 決定要印幾個 VALUEn, 頻點以 DefaultFrequencyMHz 為底, 每個往上加一次 FreqTableStepMHz。改 CountOfFreqTable 就會自動長出/減少對應的 VALUEn 行, 不用手動對齊。</summary>
        private static string BuildFrequencyTableSection()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("NUM=").Append(CountOfFreqTable);
            for (int i = 1; i <= CountOfFreqTable; i++)
                sb.Append("\r\nVALUE").Append(i).Append('=').Append(DefaultFrequencyMHz + FreqTableStepMHz * (i - 1));
            return sb.ToString();
        }

        public static readonly string IniContent =
$@"EZCODE

[PARAMETER]
CRYSTAL=40
CRYSTALGAIN=4
CAPIO=33
FREQUENCY={DefaultFrequencyMHz}
DATARATE=100.000
DEVIATION=50
MODULATION=GFSK
PACONFIG=23
POWERLEVEL=127

[PREAMBLE]
CONTENT=AA
LEN=4

[PAYLOADORDER]
MSB=false

[TXPAYLOADFORMAT]
VALUE=TRANSPARENTwithPRESYNC

[PCRMU]
ENABLE=true

[SYNCWORD]
TXSYNCWORD=5A0FBE66
TXINVERT=false
RXSYNCWORD=5A0FBE66
RXINVERT=false
ALLOWBITERROR=1

[INTERRUPT]
INT_ST_RX=true
INT_ST_RXERR=false
INT_ST_TX=true
INT_ST_TXERR=false
INT_ST_TMRTHD=false
INT_ST_WAKEUP=false
INT_ST_NFC=false
INT_ST_TX_FIFO_AEMPTY=false
INT_ST_RX_FIFO_AFULL=false
INT_ST_RX_SYNCW=false

[TX_AEMPTY_THR]
VALUE=64

[RX_AFULL_THR]
VALUE=64

[GPIO0]
MODE=Default_Mode
GIO_SEL=TR_BSY
PULL_CTRL=No_Pull
INV=false

[GPIO1]
MODE=Default_Mode
GIO_SEL=TR_BSY
PULL_CTRL=No_Pull
INV=false

[FREQUENCYTABLE]
{BuildFrequencyTableSection()}
";
    }
}
