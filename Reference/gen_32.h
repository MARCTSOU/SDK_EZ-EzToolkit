/* **********************************************************************
 * 
 * @32bits setting  
 * @file     SPI_ER41xx_config.h
 * @version  V4.04 for other crystal
 * @brief    ER41xx series SPI driver
 * config file
 *
 * @copyright (C) COPYRIGHT 2020 ESMT
 * Technology Corp. All rights reserved.
 * ********************************************************************** */
/* Comments ------------------------------------------------------------------*/
// Crystal              : 40MHz
// CAP IO               : 33
/* RF Properties */
// Base frequency       : 915MHz
// Data Rate            : 100000bps
// Deviation            : 25000Hz
// Modulation           : GFSK
// TX Power - PA Type   : 10dBm
// TX Power - Level     : 127
/* Packet Format */
// TX Preamble          : 0xAA, 8byte(s)
// Tx Syncword          : 0x5A0FBE66
// Rx Syncword          : 0x5A0FBE66
// Syncword bit error   : 1bit(s)
/* Interrupt */
/* Define to prevent recursive inclusion -------------------------------------*/
#ifndef __SPI_ER41XX_CONFIG_H__
#define __SPI_ER41XX_CONFIG_H__
/* Includes ------------------------------------------------------------------*/
#include "SPI_ER41xx.h"
/* Definition & Macro -------------------------------------------------------*/
#define CRYSTAL_HZ          40000000
#define NUM_OF_CH_TABLE     1
#define GLB_DATA_RATE 0x000186A0  //0x000186A0 = 100000
#define GLB_DEVIATION 0x000061A8  //0x000061A8 = 25000
                        /* Predefined resources */
#define PREDEFINED_RX_SZ                128
#define PREDEFINED_RX_SYNCWORD          0x5A0FBE66
#define TABLE_END_ADDR      0xFFFF
#define TABLE_END_DATA    0xFFFFFFFF
#define IS_TABLE_END(e) \
     ((e).addr == TABLE_END_ADDR && \
     (e).reg_data == TABLE_END_DATA
     
/* Struct --------------------------------------------------------*/
typedef struct {
    uint16_t addr;
    uint32_t reg_data;
}t_reg_array;
typedef struct {
    uint32_t freq;
    uint32_t reg_1998;
    uint32_t reg_199C;
    uint32_t reg_19A0;
    uint32_t reg_1994;
}ch_pll_item_t;
/* Exported constants --------------------------------------------------------*/
/* Channel Table*/
code const ch_pll_item_t channel_pll_table[] = {
    { // Freq0 = 915 MHz
        0x3689CAC0,
    },
};
/* Config Table*/
code const t_reg_array Initial_Reg_Array[] = {
    0x0010, 0xFFFFFFFF, 0x0010, 0x00000000, 
    0x0000, 0x00000007, 0x0014, 0x00000001, 0x1100, 0x00003821, 0x1280, 0x00007890, 0x1284, 0x00200015,
    0x1380, 0x0016a785, 0x1384, 0x3fcc02a8, 0x1390, 0x00000708, 0x1580, 0x00c28548, 0x1620, 0x441405e8,
    0x1624, 0x02920135, 0x1700, 0x02000041, 0x4004, 0x00060000, 0x400C, 0x00060000, 0x1a14, 0x00000000,
    0xb0a8, 0x6f00370e, 0xb19c, 0x00015f03, 0xb208, 0x06068544, 0xb158, 0x00001200, 0xb15c, 0x33222222,
    0xb160, 0x33333333, 0xb164, 0x33333333, 0xb168, 0x33333333, 0xb16c, 0x00000000, 0xb170, 0x33222220,
    0xb174, 0x33333333, 0xb178, 0x33333333, 0xb17c, 0x54987654, 0xb180, 0x21432106, 0xb184, 0x43210543,
    0xb188, 0xcba98765, 0xb18c, 0x66555555, 0xb190, 0x66666666, 0xb194, 0x77777666, 0xb198, 0x77777777,
    0xb1bc, 0x0032001a, 0xb1c0, 0x0062004a, 0xb1c4, 0x0092007a, 0xb1c8, 0x00c600a8, 0xb1cc, 0x00f000da,
    0xb1d0, 0x011c0104, 0xb1d4, 0x014c0134, 0xb1d8, 0x017a0164, 0xb1dc, 0x01ac0192, 0xb1e0, 0x01d601c4,
    0xb1e4, 0x020201ea, 0xb1e8, 0x0236021a, 0xb1ec, 0x02600248, 0xb1f0, 0x0294027c, 0xb1f4, 0x02c402aa,
    0xb1f8, 0x02ec02da, 0xb250, 0x00000000, 0xb254, 0x76543210, 0xb258, 0x77777777, 0x1200, 0x8e1205a0,
    0x1204, 0x00000018, 0x1e50, 0x253124ed, 0x1e54, 0x24312431, 0x1e58, 0x122d122d, 0x1e5c, 0x00000112,
    0x1e18, 0x04041400, 0x1e00, 0x00010101, 0x1e3c, 0x0000000d, 0x1e30, 0xfdffff3f, 0x1e34, 0x00000001,
    0x1e64, 0x00000000,
    0xb30c, 0x00000201, 0x1488, 0x00000001, 0x1600, 0x000A0200, 0x1e60, 0x00000001, 0x1624, 0x13d20135,
    TABLE_END_ADDR,  TABLE_END_DATA};
//------------------------------------------------
code const t_reg_array code TRx_Config_Array[] = {
    //====== BELOW IS PA SETTING ======
    0x1f00, 0x03020101,
    0x1f04, 0x07060504,
    0x1f08, 0x0D0C0A08,
    0x1f0c, 0x12100F0E,
    0x1f10, 0x19171513,
    0x1f14, 0x1F1E1D1B,
    0x1f18, 0x27252321,
    0x1f1c, 0x2F2D2B29,
    0x1f20, 0x38363331,
    0x1f24, 0x3F3E3C3A,
    0x1f28, 0x46444241,
    0x1f2c, 0x4F4E4D49,
    0x1f30, 0x5E595551,
    0x1f34, 0x6B68625F,
    0x1f38, 0x7F7F7871,
    //====== Set Freq/Dev/DataRate ======
    0x1980, 0x3689CAC0,
    0x1984, 0x000061A8,
    0x1988, 0x000186A0,
    0x1990, 0x00000001,
    0x1e04, 0x00800060,
    //====== BELOW IS RX SETTING ======
    0x0004, 0x0001010a,
    0xb304, 0x00000000,
    0xb1b4, 0x0000fee8,
    0x1604, 0x1fc33b7d,
    0xb200, 0x000c0017,
    0xb400, 0x0000002a,
    0xb404, 0xfff6001c,
    0xb408, 0xff07ff9d,
    0xb40c, 0xfe5dfe74,
    0xb410, 0x0145ff40,
    0xb414, 0x05eb03e9,
    0xb418, 0x021405b1,
    0xb41c, 0xf373fb49,
    0xb420, 0xf065ee62,
    0xb424, 0x12e4fc87,
    0xb428, 0x4cfe2fe8,
    0xb42c, 0x6a796287,
    0xb108, 0x7feef7de,
    0xb10C, 0xf7bd6820,
    0xb110, 0x0000019e,
    0xb19c, 0x00015f04,
    0xb1a8, 0x00000050,
    0xb000, 0x00000906,
    0x1188, 0x00000002,
   //====== BELOW IS TX SETTING ======
    0x13ac, 0x30300028,
    0xa030, 0x00140000,
    0x1380, 0x0014A785,
    0x13a0, 0x00708708,
    //====== POWER CONFIG ======
    0x1204, 0x00000018,
    0x1e5c, 0x00000112,
    0x1480, 0x006006c5,
    0x1484, 0x00039200,
    0x1488, 0x00000006,
    0x1e60, 0x00000008,
    0x1e1c, 0x37000000,
    0x10a8, 0x37000000,
    0x1e20, 0x13000027,
    TABLE_END_ADDR,  TABLE_END_DATA};

code const t_reg_array code TRx_ConfigEx_Array[] = {
    //====== Set Cap Value ======
    0x1580, 0x00D0A188,
    //====== Set PCRMU ======
    0x0020, 0x00000004,
    0x0028, 0x00000000,
    0x1A00, 0xFFFFFFFF,
    0x1A08, 0xFFFFFFFF,
    //====== Enable Interrupt Mode ======
    0xF004, 0x00000000,
    //====== Preamble Setting ======
    0xA02C, 0x0000AA07,
    //====== Tx Payload Format ======
    0xA028, 0x00000003,
    //====== Tx Syncword Setting ======
    0xA034, 0x00000003,
    0xA038, 0x5A0FBE66,
    //====== Rx Syncword Setting ======
    0xB100, 0x5A0FBE66,
    //====== Rx Syncword Error bit ======
    0xB104, 0x0007CC01,
    //====== Payload Order: LSB ======
    0xA0A8, 0x00000000,
    TABLE_END_ADDR,  TABLE_END_DATA};
    //======================INIT END===============================//
#endif /*__SPI_ER41XX_CONFIG_H__*/
/*** (C) COPYRIGHT 2020 ESMT Technology Corp. ***/
