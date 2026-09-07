/* **********************************************************************
 *
 * @8bits setting
 * @file     SPI_ER41xx_config.h
 * @version  V4.04 for other crystal
 * @brief    ER41xx series SPI driver
 * config file
 *
 * @copyright (C) COPYRIGHT 2020 ESMT
 * Technology Corp. All rights reserved.
 * ********************************************************************** */
/* Comments ------------------------------------------------------------------*/
// Crystal              : 38.4MHz
// CAP IO               : 33
/* RF Properties */
// Base frequency       : 435MHz
// Data Rate            : 96000bps
// Deviation            : 48000Hz
// Modulation           : GFSK
// TX Power - PA Type   : 23dBm
// TX Power - Level     : 127
/* Packet Format */
// TX Preamble          : 0xAA, 8byte(s)
// Tx Syncword          : 0x5A0FBE66
// Rx Syncword          : 0x5A0FBE66
// Syncword bit error   : 1bit(s)
/* Interrupt */
// INT_ST_RX
// INT_ST_TX
/* Define to prevent recursive inclusion -------------------------------------*/
#ifndef __SPI_ER41XX_CONFIG_H__
#define __SPI_ER41XX_CONFIG_H__
/* Includes ------------------------------------------------------------------*/
/* --------------------------------------------------------------------------*/
/* Predefined Parameter                                                      */
/* --------------------------------------------------------------------------*/
#define CRYSTAL_HZ          38400000
#define PREDEFINED_RX_SZ                128
#define PREDEFINED_RX_SYNCWORD          0x5A0FBE66
#define GLB_DATA_RATE {0x00,0x77,0x01,0x00}  //0x00017700 = 96000
#define GLB_DEVIATION {0x80,0xBB,0x00,0x00}  //0x0000BB80 = 48000

/* --------------------------------------------------------------------------*/
/* Configuration Register Data                                               */
/* --------------------------------------------------------------------------*/
//====== Initial_Reg_Array ======
#define INIT_SW_RST_REG_1ST  0x0010, {0xFF, 0xFF, 0xFF, 0xFF}
#define INIT_SW_RST_REG_2ND  0x0010, {0x00, 0x00, 0x00, 0x00}
#define INIT_CLK_GATING_EN_REG  0x0000, {0x07, 0x00, 0x00, 0x00}
#define INIT_MOD_RST_REG  0x0014, {0x01, 0x00, 0x00, 0x00}
#define INIT_BIAS_CONFIG1  0x1100, {0x21, 0x38, 0x00, 0x00}
#define INIT_IRC_ADPLL_CONFIG_1  0x1280, {0x90, 0x78, 0x00, 0x00}
#define INIT_IRC_ADPLL_CONFIG_2  0x1284, {0x15, 0x00, 0x20, 0x00}
#define INIT_FREQSYNTH_CONFIG_1  0x1380, {0x85, 0xA7, 0x16, 0x00}  // Freq: FREQSYNTH_CONFIG_1 開機預設值(對應778~1020MHz band的合成器常數)，會在下面依實際頻率被 FREQSYNTH_CONFIG_1 覆蓋
#define INIT_FREQSYNTH_CONFIG_2  0x1384, {0xA8, 0x02, 0xCC, 0x3F}
#define INIT_FREQSYNTH_CONFIG_5  0x1390, {0x08, 0x07, 0x00, 0x00}
#define INIT_XO_CONFIG_1  0x1580, {0x48, 0x85, 0xC2, 0x00}
#define INIT_RX_CONFIG_5  0x1620, {0xE8, 0x05, 0x14, 0x44}
#define INIT_RX_CONFIG_6_1ST  0x1624, {0x35, 0x01, 0x92, 0x02}
#define INIT_NFC_CONFIG  0x1700, {0x41, 0x00, 0x00, 0x02}
#define INIT_GPIO_1_CFG_REG  0x4004, {0x00, 0x00, 0x06, 0x00}
#define INIT_GPIO_DBG_CFG_REG  0x400C, {0x00, 0x00, 0x06, 0x00}
#define INIT_PMU_CONFIG_3  0x1A14, {0x00, 0x00, 0x00, 0x00}
#define INIT_BBP_RXGFSK_CNTL  0xB0A8, {0x0E, 0x37, 0x00, 0x6F}
#define INIT_BBP_AGC18  0xB19C, {0x03, 0x5F, 0x01, 0x00}  // DataRate/Dev: 開機預設值，會在下面依實際 data rate/h 被 BBP_AGC18 覆蓋
#define INIT_BBP_AGC45  0xB208, {0x44, 0x85, 0x06, 0x06}
#define INIT_BBP_AGC1  0xB158, {0x00, 0x12, 0x00, 0x00}
#define INIT_BBP_AGC2  0xB15C, {0x22, 0x22, 0x22, 0x33}
#define INIT_BBP_AGC3  0xB160, {0x33, 0x33, 0x33, 0x33}
#define INIT_BBP_AGC4  0xB164, {0x33, 0x33, 0x33, 0x33}
#define INIT_BBP_AGC5  0xB168, {0x33, 0x33, 0x33, 0x33}
#define INIT_BBP_AGC6  0xB16C, {0x00, 0x00, 0x00, 0x00}
#define INIT_BBP_AGC7  0xB170, {0x20, 0x22, 0x22, 0x33}
#define INIT_BBP_AGC8  0xB174, {0x33, 0x33, 0x33, 0x33}
#define INIT_BBP_AGC9  0xB178, {0x33, 0x33, 0x33, 0x33}
#define INIT_BBP_AGC10  0xB17C, {0x54, 0x76, 0x98, 0x54}
#define INIT_BBP_AGC11  0xB180, {0x06, 0x21, 0x43, 0x21}
#define INIT_BBP_AGC12  0xB184, {0x43, 0x05, 0x21, 0x43}
#define INIT_BBP_AGC13  0xB188, {0x65, 0x87, 0xA9, 0xCB}
#define INIT_BBP_AGC14  0xB18C, {0x55, 0x55, 0x55, 0x66}
#define INIT_BBP_AGC15  0xB190, {0x66, 0x66, 0x66, 0x66}
#define INIT_BBP_AGC16  0xB194, {0x66, 0x76, 0x77, 0x77}
#define INIT_BBP_AGC17  0xB198, {0x77, 0x77, 0x77, 0x77}
#define INIT_BBP_AGC26  0xB1BC, {0x1A, 0x00, 0x32, 0x00}
#define INIT_BBP_AGC27  0xB1C0, {0x4A, 0x00, 0x62, 0x00}
#define INIT_BBP_AGC28  0xB1C4, {0x7A, 0x00, 0x92, 0x00}
#define INIT_BBP_AGC29  0xB1C8, {0xA8, 0x00, 0xC6, 0x00}
#define INIT_BBP_AGC30  0xB1CC, {0xDA, 0x00, 0xF0, 0x00}
#define INIT_BBP_AGC31  0xB1D0, {0x04, 0x01, 0x1C, 0x01}
#define INIT_BBP_AGC32  0xB1D4, {0x34, 0x01, 0x4C, 0x01}
#define INIT_BBP_AGC33  0xB1D8, {0x64, 0x01, 0x7A, 0x01}
#define INIT_BBP_AGC34  0xB1DC, {0x92, 0x01, 0xAC, 0x01}
#define INIT_BBP_AGC35  0xB1E0, {0xC4, 0x01, 0xD6, 0x01}
#define INIT_BBP_AGC36  0xB1E4, {0xEA, 0x01, 0x02, 0x02}
#define INIT_BBP_AGC37  0xB1E8, {0x1A, 0x02, 0x36, 0x02}
#define INIT_BBP_AGC38  0xB1EC, {0x48, 0x02, 0x60, 0x02}
#define INIT_BBP_AGC39  0xB1F0, {0x7C, 0x02, 0x94, 0x02}
#define INIT_BBP_AGC40  0xB1F4, {0xAA, 0x02, 0xC4, 0x02}
#define INIT_BBP_AGC41  0xB1F8, {0xDA, 0x02, 0xEC, 0x02}
#define INIT_BBP_AGC48  0xB250, {0x00, 0x00, 0x00, 0x00}
#define INIT_BBP_AGC49  0xB254, {0x10, 0x32, 0x54, 0x76}
#define INIT_BBP_AGC50  0xB258, {0x77, 0x77, 0x77, 0x77}
#define INIT_DCDC_CONFIG_0  0x1200, {0xA0, 0x05, 0x12, 0x8E}
#define INIT_DCDC_CONFIG_1  0x1204, {0x18, 0x00, 0x00, 0x00}
#define INIT_ANCTL_DCDC_REG0  0x1E50, {0xED, 0x24, 0x31, 0x25}
#define INIT_ANCTL_DCDC_REG1  0x1E54, {0x31, 0x24, 0x31, 0x24}
#define INIT_ANCTL_DCDC_REG2  0x1E58, {0x2D, 0x12, 0x2D, 0x12}
#define INIT_ANCTL_DCDC_REG3  0x1E5C, {0x12, 0x01, 0x00, 0x00}
#define INIT_ANCTL_CONFIG_6  0x1E18, {0x00, 0x14, 0x04, 0x04}
#define INIT_ANCTL_CONFIG_0  0x1E00, {0x01, 0x01, 0x01, 0x00}
#define INIT_ANCTL_CONFIG_BBP  0x1E3C, {0x0D, 0x00, 0x00, 0x00}
#define INIT_ANCTL_CONFIG_7_5  0x1E30, {0x3F, 0xFF, 0xFF, 0xFD}
#define INIT_ANCTL_CONFIG_7_6  0x1E34, {0x01, 0x00, 0x00, 0x00}
#define INIT_ANCTL_FS_REG0  0x1E64, {0x00, 0x00, 0x00, 0x00}
#define INIT_BBP_POP_CLIPPING  0xB30C, {0x01, 0x02, 0x00, 0x00}
#define INIT_PA1G_CONFIG_2  0x1488, {0x01, 0x00, 0x00, 0x00}
#define INIT_RX_POWSET  0x1600, {0x00, 0x02, 0x0A, 0x00}
#define INIT_ANCTL_PA1G_REG0  0x1E60, {0x01, 0x00, 0x00, 0x00}
#define INIT_RX_CONFIG_6_2ND  0x1624, {0x35, 0x01, 0xD2, 0x13}

//====== TRx_Config_Array ======
// BELOW IS PA SETTING
#define ANCTL_CONFIG_40  0x1F00, {0x01, 0x01, 0x02, 0x03}
#define ANCTL_CONFIG_41  0x1F04, {0x04, 0x05, 0x06, 0x07}
#define ANCTL_CONFIG_42  0x1F08, {0x08, 0x0A, 0x0C, 0x0D}
#define ANCTL_CONFIG_43  0x1F0C, {0x0E, 0x0F, 0x10, 0x12}
#define ANCTL_CONFIG_44  0x1F10, {0x13, 0x15, 0x17, 0x19}
#define ANCTL_CONFIG_45  0x1F14, {0x1B, 0x1D, 0x1E, 0x1F}
#define ANCTL_CONFIG_46  0x1F18, {0x21, 0x23, 0x25, 0x27}
#define ANCTL_CONFIG_47  0x1F1C, {0x29, 0x2B, 0x2D, 0x2F}
#define ANCTL_CONFIG_48  0x1F20, {0x31, 0x33, 0x36, 0x38}
#define ANCTL_CONFIG_49  0x1F24, {0x3A, 0x3C, 0x3E, 0x3F}
#define ANCTL_CONFIG_50  0x1F28, {0x41, 0x42, 0x44, 0x46}
#define ANCTL_CONFIG_51  0x1F2C, {0x49, 0x4D, 0x4E, 0x4F}
#define ANCTL_CONFIG_52  0x1F30, {0x51, 0x55, 0x59, 0x5E}
#define ANCTL_CONFIG_53  0x1F34, {0x5F, 0x62, 0x68, 0x6B}
#define ANCTL_CONFIG_54  0x1F38, {0x71, 0x78, 0x7F, 0x7F}
// BELOW IS TX SETTING
#define TPM_DMI_CONFIG_1  0x13AC, {0x28, 0x00, 0x30, 0x30}  // DataRate: TX bit rate分頻值(低20bits)，公式 = ref_clk/(DataRate*10)，寫入前先讀出保留高12bits(調變設定)
#define ANCTL_CONFIG_1  0x1E04, {0x60, 0x00, 0x80, 0x00}  // DataRate: 主要是TX調變模式設定，但bit23會依 DataRate>=100000 額外被設為1
#define MAC_TX_DATDLV  0xA030, {0x00, 0x00, 0x14, 0x00}  // DataRate: TX FIFO delivery timing，公式 = (ref_clk/20/DataRate)<<16
#define FREQSYNTH_TPMCAL_CONFIG_3  0x13A0, {0x08, 0x87, 0x70, 0x00}  // Freq/DataRate/Dev: TPM modulation校正值(PGMH/PGML)，跟0x1380一起由EzGen依(頻率band, DataRate, h)四種特例組合判斷；本值0x00708708對應「不符合特例」的預設值，只有 band=389~510或778~1020 且 DataRate=1920000/960000 且 h 為特定值(1/2/0.5)時才會改用特例值(0x1380=0x0014F785, 0x13a0=0x001F41F4)。我們DataRate=96000不在特例範圍內，走預設值
// BELOW IS RX SETTING
#define BBP_CORE52  0xB304, {0x00, 0x00, 0x00, 0x00}  // DataRate: RX core設定，依 DataRate 分段(2Mbps一組值/其餘一組值)
#define BBP_AGC24  0xB1B4, {0xE8, 0xFE, 0x00, 0x00}  // DataRate: RX AGC設定，依 DataRate 分段取值
#define RX_GAIN_FILTSET  0x1604, {0x7D, 0x3B, 0xC3, 0x1F}  // DataRate/Dev: RX通道濾波器係數，依 DataRate 與調變指數 h(=2*Dev/DataRate) 選表(TRx_Filter_SETUP)
#define BBP_AGC43  0xB200, {0x17, 0x00, 0x0C, 0x00}  // DataRate/Dev: RX AGC/濾波設定，依 DataRate 分段、每段內再依 h 細分
// 以下為 DataRate/Dev: RX Channel Input Filter係數，來自 SPI_ER41xx_config.h(Lierda參考碼) 裡的 RXCHFILT_h{h}_OSR{osr} 表，依 h(=2*Dev/DataRate) 與 OSR(依DataRate分支) 選表。本組12筆數值跟 RXCHFILT_h0p5_OSR8[] 逐筆比對完全相同，對應本方案 h=2*24000/96000=0.5
#define BBP_CH_FILT0  0xB400, {0x2A, 0x00, 0x00, 0x00}
#define BBP_CH_FILT1  0xB404, {0x1C, 0x00, 0xF6, 0xFF}
#define BBP_CH_FILT2  0xB408, {0x9D, 0xFF, 0x07, 0xFF}
#define BBP_CH_FILT3  0xB40C, {0x74, 0xFE, 0x5D, 0xFE}
#define BBP_CH_FILT4  0xB410, {0x40, 0xFF, 0x45, 0x01}
#define BBP_CH_FILT5  0xB414, {0xE9, 0x03, 0xEB, 0x05}
#define BBP_CH_FILT6  0xB418, {0xB1, 0x05, 0x14, 0x02}
#define BBP_CH_FILT7  0xB41C, {0x49, 0xFB, 0x73, 0xF3}
#define BBP_CH_FILT8  0xB420, {0x62, 0xEE, 0x65, 0xF0}
#define BBP_CH_FILT9  0xB424, {0x87, 0xFC, 0xE4, 0x12}
#define BBP_CH_FILT10  0xB428, {0xE8, 0x2F, 0xFE, 0x4C}
#define BBP_CH_FILT11  0xB42C, {0x87, 0x62, 0x79, 0x6A}
// 以下為 DataRate/Dev: RX Low Pass Filter係数，對應 RXLPF_h0p5_OSR16[] (同一份參考檔案)，逐筆數值完全相同
#define BBP_RXLPF_1M_0  0xB108, {0xDE, 0xF7, 0xEE, 0x7F}
#define BBP_RXLPF_1M_1  0xB10C, {0x20, 0x68, 0xBD, 0xF7}
#define BBP_RXLPF_1M_2  0xB110, {0x9E, 0x01, 0x00, 0x00}
#define BBP_AGC18  0xB19C, {0x04, 0x5F, 0x01, 0x00}  // DataRate: RX AGC設定，依 DataRate 分段取值，覆蓋掉開機時的 INIT_BBP_AGC18 預設值
#define BBP_AGC21  0xB1A8, {0x50, 0x00, 0x00, 0x00}  // DataRate: RX AGC設定，依 DataRate 分段取值
#define BBP_RXIQCAL  0xB000, {0x06, 0x09, 0x00, 0x00}  // DataRate: RX_SETUP最後寫入，依 DataRate>=1Mbps 二選一，跟 CTQBPSD_CONFIG_3 同一段 if/else
#define CTQBPSD_CONFIG_3  0x1188, {0x02, 0x00, 0x00, 0x00}  // DataRate: RX_SETUP最後寫入，依 DataRate>=1Mbps 二選一(4或2)
// Set Freq/Dev/DataRate
#define SYNTH_CAL_CONFIG_0  0x1980, {0x80, 0x59, 0x1B, 0x1A}  // Freq: 目標頻率(Hz)原始值，對應 TRx_SetChannel(u32Frequency_Hz) 直接寫入
#define SYNTH_CAL_CONFIG_1  0x1984, {0xC0, 0x5D, 0x00, 0x00}  // Dev: 頻偏(Hz)原始值，對應 u32gTRx_FreqDev 直接寫入
#define SYNTH_CAL_CONFIG_2  0x1988, {0x00, 0x77, 0x01, 0x00}  // DataRate: 資料速率(bps)原始值，對應 u32gTRx_DataRate 直接寫入(若>960000會再OR上bit24)
#define SYNTH_CAL_TRIG  0x1990, {0x01, 0x00, 0x00, 0x00}  // Freq: 寫入1，觸發synth依0x1980(freq)/0x1984(dev)/0x1988(data rate)重新計算並鎖頻
#define FREQSYNTH_CONFIG_1  0x1380, {0x85, 0xA7, 0x14, 0x00}  // Freq/DataRate/Dev: 依頻率band選擇的合成器常數，實際是跟 0x13A0(FREQSYNTH_TPMCAL_CONFIG_3) 一起由 EzGen 依(頻率band, DataRate, h)四種特例組合判斷；本值0x0014A785對應389~510MHz band的預設值(不符合特例)，本方案435~438MHz落在此band、DataRate=96000不在特例範圍
#define SYNTH_CAL_CONFIG_5  0x1998, {0x8F, 0x82, 0x36, 0x00}  // Freq: PLL分頻比 DIV_INT<<18|DIV_FRAC，hf32Channel=freq+deviation，高頻角
#define SYNTH_CAL_CONFIG_6  0x199C, {0x70, 0x7D, 0x36, 0x00}  // Freq: PLL分頻比，lf32Channel=freq-deviation，低頻角
#define SYNTH_CAL_CONFIG_7  0x19A0, {0x99, 0x19, 0x36, 0x00}  // Freq: PLL分頻比，RXmf32Channel=freq(依data rate扣除RX時脈補償)，RX中心頻
#define SYNTH_CAL_CONFIG_4  0x1994, {0x00, 0x80, 0x36, 0x00}  // Freq: PLL分頻比，TXmf32Channel=freq(無補償)，TX中心頻
#define MODEM_CLK_REG  0x0004, {0x0A, 0x01, 0x01, 0x00}  // DataRate: BBP data rate分頻值(MODEM_RATE_R1/R2/R3)。晶片硬體本身以40MHz設計，只要填好freq/dev/datarate等關鍵reg，硬體就會自動算出這顆reg；但我們crystal是38.4MHz，硬體算出來的是40MHz版本的值，所以要靠fw在硬體自動算完後(即0x1980~0x1990觸發之後)重新算過再蓋掉，這也是為什麼0x0004要排在Set Freq/Dev/DataRate段落後面寫的原因
// POWER CONFIG
#define DCDC_CONFIG_1  0x1204, {0x18, 0x00, 0x00, 0x00}
#define ANCTL_DCDC_REG3  0x1E5C, {0x12, 0x01, 0x00, 0x00}
#define PA1G_CONFIG_0  0x1480, {0xC5, 0x9F, 0x61, 0x00}
#define PA1G_CONFIG_1  0x1484, {0x00, 0xD2, 0x13, 0x00}
#define PA1G_CONFIG_2  0x1488, {0x00, 0x00, 0x00, 0x00}
#define ANCTL_PA1G_REG0  0x1E60, {0x00, 0x00, 0x00, 0x00}
#define ANCTL_CONFIG_7  0x1E1C, {0x00, 0x00, 0x00, 0xFF}
#define ASARADC_CONFIG_11  0x10A8, {0x00, 0x00, 0x00, 0xFF}
#define ANCTL_CONFIG_7_2  0x1E20, {0x27, 0x00, 0x00, 0x13}

//====== TRx_ConfigEx_Array ======
// Set Cap Value
#define XO_CONFIG_1  0x1580, {0x88, 0xA1, 0xD0, 0x00}
// Set GPIO1
#define GPIO_1_CFG_REG  0x4004, {0x0D, 0x01, 0x16, 0x00}
// Set PCRMU
#define PMU_CTL_REG  0x0020, {0x04, 0x00, 0x00, 0x00}
#define PMU_TIM_REG  0x0028, {0x00, 0x00, 0x00, 0x00}
#define PMU_CTL  0x1A00, {0xFF, 0xFF, 0xFF, 0xFF}
#define PMU_CONFIG_0  0x1A08, {0xFF, 0xFF, 0xFF, 0xFF}
// Enable Interrupt Mode
#define INT_EN_REG  0xF004, {0x05, 0x01, 0x00, 0x00}
// Preamble Setting
#define MAC_TX_CFG2  0xA02C, {0x07, 0xAA, 0x00, 0x00}
// Tx Payload Format
#define MAC_TX_CFG  0xA028, {0x03, 0x00, 0x00, 0x00}
// Tx Syncword Setting
#define MAC_TX_CFG3  0xA034, {0x03, 0x00, 0x00, 0x00}
#define MAC_TX_CFG4  0xA038, {0x66, 0xBE, 0x0F, 0x5A}
// Rx Syncword Setting
#define BBP_CORR_CODE_BT_BLE  0xB100, {0x66, 0xBE, 0x0F, 0x5A}
// Rx Syncword Error bit
#define BBP_CORR_ERR_CNT  0xB104, {0x01, 0xCC, 0x07, 0x00}
// Payload Order: LSB
#define FIFO_FEA_REG  0xA0A8, {0x00, 0x00, 0x00, 0x00}

/* --------------------------------------------------------------------------*/
/* Configuration Register Array                                              */
/* --------------------------------------------------------------------------*/
#define END_REG_INT 0xFFFF
#define END_REG_CHAR 0xFF
#define END_REG_DATA END_REG_INT, {END_REG_CHAR, END_REG_CHAR, END_REG_CHAR, END_REG_CHAR}

#define RF_REG_INIT_ARRAY { \
          INIT_SW_RST_REG_1ST, \
          INIT_SW_RST_REG_2ND, \
          INIT_CLK_GATING_EN_REG, \
          INIT_MOD_RST_REG, \
          INIT_BIAS_CONFIG1, \
          INIT_IRC_ADPLL_CONFIG_1, \
          INIT_IRC_ADPLL_CONFIG_2, \
          INIT_FREQSYNTH_CONFIG_1, \
          INIT_FREQSYNTH_CONFIG_2, \
          INIT_FREQSYNTH_CONFIG_5, \
          INIT_XO_CONFIG_1, \
          INIT_RX_CONFIG_5, \
          INIT_RX_CONFIG_6_1ST, \
          INIT_NFC_CONFIG, \
          INIT_GPIO_1_CFG_REG, \
          INIT_GPIO_DBG_CFG_REG, \
          INIT_PMU_CONFIG_3, \
          INIT_BBP_RXGFSK_CNTL, \
          INIT_BBP_AGC18, \
          INIT_BBP_AGC45, \
          INIT_BBP_AGC1, \
          INIT_BBP_AGC2, \
          INIT_BBP_AGC3, \
          INIT_BBP_AGC4, \
          INIT_BBP_AGC5, \
          INIT_BBP_AGC6, \
          INIT_BBP_AGC7, \
          INIT_BBP_AGC8, \
          INIT_BBP_AGC9, \
          INIT_BBP_AGC10, \
          INIT_BBP_AGC11, \
          INIT_BBP_AGC12, \
          INIT_BBP_AGC13, \
          INIT_BBP_AGC14, \
          INIT_BBP_AGC15, \
          INIT_BBP_AGC16, \
          INIT_BBP_AGC17, \
          INIT_BBP_AGC26, \
          INIT_BBP_AGC27, \
          INIT_BBP_AGC28, \
          INIT_BBP_AGC29, \
          INIT_BBP_AGC30, \
          INIT_BBP_AGC31, \
          INIT_BBP_AGC32, \
          INIT_BBP_AGC33, \
          INIT_BBP_AGC34, \
          INIT_BBP_AGC35, \
          INIT_BBP_AGC36, \
          INIT_BBP_AGC37, \
          INIT_BBP_AGC38, \
          INIT_BBP_AGC39, \
          INIT_BBP_AGC40, \
          INIT_BBP_AGC41, \
          INIT_BBP_AGC48, \
          INIT_BBP_AGC49, \
          INIT_BBP_AGC50, \
          INIT_DCDC_CONFIG_0, \
          INIT_DCDC_CONFIG_1, \
          INIT_ANCTL_DCDC_REG0, \
          INIT_ANCTL_DCDC_REG1, \
          INIT_ANCTL_DCDC_REG2, \
          INIT_ANCTL_DCDC_REG3, \
          INIT_ANCTL_CONFIG_6, \
          INIT_ANCTL_CONFIG_0, \
          INIT_ANCTL_CONFIG_BBP, \
          INIT_ANCTL_CONFIG_7_5, \
          INIT_ANCTL_CONFIG_7_6, \
          INIT_ANCTL_FS_REG0, \
          INIT_BBP_POP_CLIPPING, \
          INIT_PA1G_CONFIG_2, \
          INIT_RX_POWSET, \
          INIT_ANCTL_PA1G_REG0, \
          INIT_RX_CONFIG_6_2ND, \
          END_REG_DATA \
}

#define RF_REG_CONFIG_ARRAY { \
          ANCTL_CONFIG_40, \
          ANCTL_CONFIG_41, \
          ANCTL_CONFIG_42, \
          ANCTL_CONFIG_43, \
          ANCTL_CONFIG_44, \
          ANCTL_CONFIG_45, \
          ANCTL_CONFIG_46, \
          ANCTL_CONFIG_47, \
          ANCTL_CONFIG_48, \
          ANCTL_CONFIG_49, \
          ANCTL_CONFIG_50, \
          ANCTL_CONFIG_51, \
          ANCTL_CONFIG_52, \
          ANCTL_CONFIG_53, \
          ANCTL_CONFIG_54, \
          TPM_DMI_CONFIG_1, \
          ANCTL_CONFIG_1, \
          MAC_TX_DATDLV, \
          FREQSYNTH_CONFIG_1, \
          FREQSYNTH_TPMCAL_CONFIG_3, \
          BBP_CORE52, \
          BBP_AGC24, \
          RX_GAIN_FILTSET, \
          BBP_AGC43, \
          BBP_CH_FILT0, \
          BBP_CH_FILT1, \
          BBP_CH_FILT2, \
          BBP_CH_FILT3, \
          BBP_CH_FILT4, \
          BBP_CH_FILT5, \
          BBP_CH_FILT6, \
          BBP_CH_FILT7, \
          BBP_CH_FILT8, \
          BBP_CH_FILT9, \
          BBP_CH_FILT10, \
          BBP_CH_FILT11, \
          BBP_RXLPF_1M_0, \
          BBP_RXLPF_1M_1, \
          BBP_RXLPF_1M_2, \
          BBP_AGC18, \
          BBP_AGC21, \
          BBP_RXIQCAL, \
          CTQBPSD_CONFIG_3, \
          SYNTH_CAL_CONFIG_0, \
          SYNTH_CAL_CONFIG_1, \
          SYNTH_CAL_CONFIG_2, \
          SYNTH_CAL_TRIG, \
          SYNTH_CAL_CONFIG_5, \
          SYNTH_CAL_CONFIG_6, \
          SYNTH_CAL_CONFIG_7, \
          SYNTH_CAL_CONFIG_4, \
          MODEM_CLK_REG, \
          DCDC_CONFIG_1, \
          ANCTL_DCDC_REG3, \
          PA1G_CONFIG_0, \
          PA1G_CONFIG_1, \
          PA1G_CONFIG_2, \
          ANCTL_PA1G_REG0, \
          ANCTL_CONFIG_7, \
          ASARADC_CONFIG_11, \
          ANCTL_CONFIG_7_2, \
          END_REG_DATA \
}

#define RF_REG_CONFIG_EX_ARRAY { \
          XO_CONFIG_1, \
          GPIO_1_CFG_REG, \
          PMU_CTL_REG, \
          PMU_TIM_REG, \
          PMU_CTL, \
          PMU_CONFIG_0, \
          INT_EN_REG, \
          MAC_TX_CFG2, \
          MAC_TX_CFG, \
          MAC_TX_CFG3, \
          MAC_TX_CFG4, \
          BBP_CORR_CODE_BT_BLE, \
          BBP_CORR_ERR_CNT, \
          FIFO_FEA_REG, \
          END_REG_DATA \
}

/* --------------------------------------------------------------------------*/
/* Hopping Register Data                                                     */
/* --------------------------------------------------------------------------*/
#define NUM_OF_CH_TABLE     4
//====== Register data of channel_pll_table ======
// Freq: 每個channel的 freq/1998/199C/19A0/1994 對應 TRx_SetChannel() 裡同一組公式(hi/lo/RX-mid/TX-mid)，這裡是針對每個跳頻channel預先算好的結果，格式跟上面 SYNTH_CAL_CONFIG_0/5/6/7 完全相同
#define CHANNEL_PLL_TABLE_CH0_FREQ {0x80, 0x59, 0x1B, 0x1A}
#define CHANNEL_PLL_TABLE_CH0_1998 {0x8F, 0x82, 0x36, 0x00}  // 1998
#define CHANNEL_PLL_TABLE_CH0_199C {0x70, 0x7D, 0x36, 0x00}  // 199C
#define CHANNEL_PLL_TABLE_CH0_19A0 {0x99, 0x19, 0x36, 0x00}  // 19A0
#define CHANNEL_PLL_TABLE_CH0_1994 {0x00, 0x80, 0x36, 0x00}  // 1994
#define CHANNEL_PLL_TABLE_CH1_FREQ {0xC0, 0x66, 0x1E, 0x1A}
#define CHANNEL_PLL_TABLE_CH1_1998 {0xE4, 0x97, 0x36, 0x00}  // 1998
#define CHANNEL_PLL_TABLE_CH1_199C {0xC5, 0x92, 0x36, 0x00}  // 199C
#define CHANNEL_PLL_TABLE_CH1_19A0 {0xEE, 0x2E, 0x36, 0x00}  // 19A0
#define CHANNEL_PLL_TABLE_CH1_1994 {0x55, 0x95, 0x36, 0x00}  // 1994
#define CHANNEL_PLL_TABLE_CH2_FREQ {0xA0, 0xFA, 0x22, 0x1A}
#define CHANNEL_PLL_TABLE_CH2_1998 {0xE4, 0xB7, 0x36, 0x00}  // 1998
#define CHANNEL_PLL_TABLE_CH2_199C {0xC5, 0xB2, 0x36, 0x00}  // 199C
#define CHANNEL_PLL_TABLE_CH2_19A0 {0xEE, 0x4E, 0x36, 0x00}  // 19A0
#define CHANNEL_PLL_TABLE_CH2_1994 {0x55, 0xB5, 0x36, 0x00}  // 1994
#define CHANNEL_PLL_TABLE_CH3_FREQ {0x20, 0x15, 0x29, 0x1A}
#define CHANNEL_PLL_TABLE_CH3_1998 {0x8F, 0xE2, 0x36, 0x00}  // 1998
#define CHANNEL_PLL_TABLE_CH3_199C {0x70, 0xDD, 0x36, 0x00}  // 199C
#define CHANNEL_PLL_TABLE_CH3_19A0 {0x99, 0x79, 0x36, 0x00}  // 19A0
#define CHANNEL_PLL_TABLE_CH3_1994 {0x00, 0xE0, 0x36, 0x00}  // 1994
/* --------------------------------------------------------------------------*/
/* Hopping Register Array                                                    */
/* --------------------------------------------------------------------------*/
//====== Array of channel_pll_table ======
#define CHANNEL_PLL_TABLE_ARRAY { \
          CHANNEL_PLL_TABLE_CH0_FREQ, \
          CHANNEL_PLL_TABLE_CH0_1998, \
          CHANNEL_PLL_TABLE_CH0_199C, \
          CHANNEL_PLL_TABLE_CH0_19A0, \
          CHANNEL_PLL_TABLE_CH0_1994, \
          CHANNEL_PLL_TABLE_CH1_FREQ, \
          CHANNEL_PLL_TABLE_CH1_1998, \
          CHANNEL_PLL_TABLE_CH1_199C, \
          CHANNEL_PLL_TABLE_CH1_19A0, \
          CHANNEL_PLL_TABLE_CH1_1994, \
          CHANNEL_PLL_TABLE_CH2_FREQ, \
          CHANNEL_PLL_TABLE_CH2_1998, \
          CHANNEL_PLL_TABLE_CH2_199C, \
          CHANNEL_PLL_TABLE_CH2_19A0, \
          CHANNEL_PLL_TABLE_CH2_1994, \
          CHANNEL_PLL_TABLE_CH3_FREQ, \
          CHANNEL_PLL_TABLE_CH3_1998, \
          CHANNEL_PLL_TABLE_CH3_199C, \
          CHANNEL_PLL_TABLE_CH3_19A0, \
          CHANNEL_PLL_TABLE_CH3_1994, \
}

#endif /*__SPI_ER41XX_CONFIG_H__*/
/*** (C) COPYRIGHT 2020 ESMT Technology Corp. ***/
