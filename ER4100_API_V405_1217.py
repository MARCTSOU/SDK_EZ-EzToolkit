"""
ER4100 API V4.05
Release date : 2024.12.17
"""


from pyftdi.spi import SpiController
import ER4100_config_V405
from ER4100_RegAccess_20230301 import RegAccess
import time
import sys
import numpy as np
# from smartcard.System import readers
# from smartcard.util import toHexString
# from smartcard.ATR import ATR


packetRx = 0
packetRxError = 0
packetTx = 0
packetTxError = 0
timeoutEvent = 0
wakeupEvent = 0
nfcEvent = 0
txFifoAEmpty = 0
rxFifoAFull = 0
rxSyncW = 0
spi_tx_buff = [0 for _ in range(300)]
RxLength = 256
TRx_ADCGain = 0
TRx_ADCOffset = 0

div_int = 0
div_frac = 0
lo_band_sel = 0
lfvco_en = 0
vtune_adc_hb = 0
vtune_adc_lb = 0
vtune_adc_target = 0
init_kvc = 0
kvc_opt = 0
vcoldo_lv = 0
vcoldo_lv_opt = 0
vtune_adc = 0
vtune_adc_opt = 0

TRx_DataRate = 0
TRx_FreqDev = 0
MAX_BANDPASS_FILTER_FREQ = 31

Crystal_Hz  = 40000000  # 40000000, 38400000
# Crystal_Hz  = 38400000  # 38400000
HW_Clk_MHz  = 40
Crystal_MHz = Crystal_Hz / 1000000
TRx_BPSOffset = Crystal_Hz / 1000000.0 / HW_Clk_MHz
Clk_BBP = Crystal_Hz / HW_Clk_MHz


class trx_driver:
    def __init__(self, **kwargs):
        # -------- trx RF Parameter -------------------------------
        self.freq =                     int(kwargs['freq'])
        self.dr =                       int(kwargs['dr'])
        self.mod_index =                float(kwargs['mod_index'])
        self.modulation =               int(kwargs['modulation'])
        self.ftdi_id =                  str(kwargs['id'])
        # -------- trx Interrupt ----------------------------------
        self.INT_ST_RX =                1 << 0
        self.INT_ST_RXERR =             1 << 1
        self.INT_ST_TX =                1 << 2
        self.INT_ST_TXERR =             1 << 3
        self.INT_ST_TMRTHD =            1 << 4
        self.INT_ST_WAKEUP =            1 << 8
        self.INT_ST_NFC =               1 << 9
        self.INT_ST_TX_FIFO_AEMPTY =    1 << 10
        self.INT_ST_RX_FIFO_AFULL =     1 << 11
        self.INT_ST_RX_SYNCW =          1 << 12
        # -------- trx TX Modulation ------------------------------
        self.TX_BPSK_MODE =             0x0  #  TRx Tx BPSK modulation
        self.TX_FSK_MODE =              0x3  #  TRx Tx FSK modulation
        self.TX_GFSK_MODE =             0x4  #  TRx Tx FSK modulation
        # -------- trx TX Type ------------------------------------
        self.TX_TRANSPARENT =           0x0  #  Transparent mode without preamble/SyncWord
        self.TX_TRANSPARENTwithPRESYNC =0x3  #  Transparent mode with preamble/SyncWord
        self.TX_802154withPRESYNC =     0xF  #  802.15.4 mode with preamble/SyncWord
        # -------- trx Command ------------------------------------
        self.COMM_NOP =                 0x00
        self.COMM_SETINT =              0x0B
        self.COMM_CLEARSTATUS =         0x0C
        self.COMM_GETSTATUSINT =        0x0D
        self.COMM_CHANGEMODE =          0x0E
        self.COMM_RSTFIFO =             0x18
        self.COMM_TXTrigger =           0x20
        self.COMM_RXTrigger =           0x21
        self.COMM_WRITEREG =            0x90
        self.COMM_READREG =             0x91
        self.COMM_TXFIFO =              0xC0
        self.COMM_RXFIFO =              0xD0
        # -------- trx Time Out counter -----------------------
        self.TimerOutCnt =              5000
        # -------- trx TX CCA RSSI Threshold -----------------------
        self.TX_CCA_RSSI_60dBmTHD =     0xE1F  # -60dBm RSSI threshold in TX CCA mode
        self.TX_CCA_RSSI_80dBmTHD =     0xD7F  # -80dBm RSSI threshold in TX CCA mode
        # -------- trx SARADC calibration voltage -------------------
        self.TRX_ADC_HIGH_VOLTAGE =     3.3
        self.TRX_ADC_LOW_VOLTAGE =      1.8
        # -------- trx NFC Config -------------------
        self.NFC_IGNORED =              1 << 0
        self.NFC_PWRGOOD =              1 << 1
        self.NFC_RX_CMD =               1 << 2
        self.NFC_TX_REPLY =             1 << 3
        self.NFC_USER_CFG4 =            1 << 4
        self.NFC_USER_CFG5 =            1 << 5
        self.NFC_USER_CFG6 =            1 << 6
        self.NFC_USER_CFG7 =            1 << 7
        self.TRx_STATUS_SUCESS = 0x0
        self.TRx_STATUS_FAIL = 0x2
        # -------- trx AGC mode -------------------
        self.AGC_MODE_DIG =             1
        self.AGC_MODE_RF_DIG =          2
        self.AGC_MODE_DEBUG =           3

        self.ftdi_init()

    def ftdi_init(self):
        global spi
        global gpio
        ctrl = SpiController()  # spi
        ctrl.configure(self.ftdi_id)  # Assuming there is only one FT232H.
        spi = ctrl.get_port(cs=0, freq=5E5, mode=0)  # Assuming D3 is used for chip select.
        gpio = ctrl.get_gpio()

    def spi_r_byte_tt(self):  # SPI Read

        write_buf = [0x08]
        a = list(reversed(spi.exchange(write_buf, 2)))
        # data = a[3] | a[2] << 8 | a[1] << 16 | a[0] << 24
        print(a)
        data = a[1] | a[0] << 8
        return data

    def spi_w_byte_tt(self):  # SPI Write

        write_buf = [0x02]
        spi.exchange(write_buf)

    def spi_r_byte(self, reg_a: int):  # SPI Read
        data = [0 for _ in range(4)]
        data[1] = reg_a & 0xff
        data[0] = (reg_a >> 8) & 0xff

        write_buf = [self.COMM_READREG, data[0], data[1], 0x00]
        a = list(reversed(spi.exchange(write_buf, 4)))
        data = a[3] | a[2] << 8 | a[1] << 16 | a[0] << 24
        return data

    def spi_w_byte(self,  reg_a: int, reg_v: int):  # SPI Write
        data_address = [0 for _ in range(4)]
        data_address[1] = reg_a & 0xff
        data_address[0] = (reg_a >> 8) & 0xff
        data = [0 for _ in range(4)]
        data[3] = reg_v & 0xff
        data[2] = (reg_v >> 8) & 0xff
        data[1] = (reg_v >> 16) & 0xff
        data[0] = (reg_v >> 24) & 0xff
        write_buf = [self.COMM_WRITEREG, data_address[0], data_address[1], data[3], data[2], data[1], data[0]]
        spi.exchange(write_buf)

        # print('0x%08x,0x%08x' % (reg_a, reg_v))

    def spi_w_field(self, write_reg_addr: int, write_field_mask: int, write_field_val: int):
        read_val = self.spi_r_byte(write_reg_addr)
        write_value = self.mask_update(read_val, write_field_val, write_field_mask)
        self.spi_w_byte(write_reg_addr, write_value)

    def spi_r_field(self, read_reg_addr: int, read_field_mask: int):
        read_reg_val = self.spi_r_byte(read_reg_addr)
        field_offset = self.find_mask_position(read_field_mask)
        read_field_val = (read_reg_val & read_field_mask) >> field_offset
        return read_field_val

    def mask_update(self, reg_v: int, comment: int, reg_m: int):  # Reg Mask
        start = self.find_mask_position(reg_m)
        reg_m_inverse = reg_m ^ 0b11111111111111111111111111111111
        data = reg_v & reg_m_inverse
        data1 = data | (comment << start)
        return data1

    def find_mask_position(self, n: int):  # Reg Mask position
        temp = n
        res = 0
        while temp % 2 == 0:
            temp >>= 1
            res += 1
        return res

    def TRx_NOP(self):
        value = 0
        write_buf = [self.COMM_NOP, value]
        spi.exchange(write_buf)

    def TRx_SW_Reset(self):
        data = 0xFFFFFFFF
        self.spi_w_byte(0x0010, data)
        time.sleep(0.001)
        data = 0
        self.spi_w_byte(0x0010, data)

    def TRx_Init(self):
        self.TRx_SW_Reset()

        a = ER4100_config_V405.ER4100_config()
        for reg_a, reg_v in a.Initial_Reg_Array.items():
            self.spi_w_byte(reg_a, reg_v)
        # print('TRx init Done')

    def TRx_EnableInt(self, mask: int):
        # print(hex(mask))
        data = [0 for _ in range(4)]
        data[3] = mask & 0xff
        data[2] = (mask >> 8) & 0xff
        data[1] = (mask >> 16) & 0xff
        data[0] = (mask >> 24) & 0xff
        write_buf = [self.COMM_SETINT, data[3], data[2], data[1], data[0]]
        spi.exchange(write_buf)

    def TRx_ClearIntFlag(self, trxdata: int):
        data = [0 for _ in range(4)]
        data[3] = trxdata & 0xff
        data[2] = (trxdata >> 8) & 0xff
        data[1] = (trxdata >> 16) & 0xff
        data[0] = (trxdata >> 24) & 0xff
        write_buf = [self.COMM_CLEARSTATUS, data[3], data[2], data[1], data[0]]
        spi.exchange(write_buf)

    def TRx_GetIntStatus(self):
        global packetRx
        global packetRxError
        global packetTx
        global packetTxError
        global timeoutEvent
        global wakeupEvent
        global nfcEvent
        global txFifoAEmpty
        global rxFifoAFull
        global rxSyncW

        data = 0x00015f03
        self.spi_w_byte(0xb19c, data)

        data = self.spi_r_byte(0xA004)
        write_buf = [self.COMM_GETSTATUSINT, 0x00]
        a = list(reversed(spi.exchange(write_buf, 4)))
        result = a[3] | a[2] << 8 | a[1] << 16 | a[0] << 24

        packetRx =      ((result >> 0) & 0x01)
        packetRxError = ((result >> 1) & 0x01)
        packetTx =      ((result >> 2) & 0x01)
        packetTxError = ((result >> 3) & 0x01)
        timeoutEvent =  ((result >> 4) & 0x01)
        wakeupEvent =   ((result >> 8) & 0x01)
        nfcEvent =      ((result >> 9) & 0x01)
        txFifoAEmpty =  ((result >> 10) & 0x01)
        rxFifoAFull =   ((result >> 11) & 0x01)
        rxSyncW =       ((result >> 12) & 0x01)
        return result, data

    def TRx_PowerDownMode(self):
        value = 3
        write_buf = [self.COMM_CHANGEMODE, value]
        spi.exchange(write_buf)

    def TRx_PCRMU_Enable_withoutNFC(self):
        data = 0x24
        self.spi_w_byte(0x0020, data)
        data = 0
        self.spi_w_byte(0x0028, data)
        data = 0xFFFFFFFF
        self.spi_w_byte(0x1A00, data)
        self.spi_w_byte(0x1A08, data)

    def TRx_PCRMU_Enable(self):
        data = 0x4
        self.spi_w_byte(0x0020, data)  # PMU_CTL_REG
        data = 0
        self.spi_w_byte(0x0028, data)  # PMU_TIM_REG
        data = 0xFFFFFFFF
        self.spi_w_byte(0x1A00, data)  # PMU_CTL
        self.spi_w_byte(0x1A08, data)  # PMU_CONFIG_0

    def TRx_PCRMU_Disable(self):
        data = 0xFFFFFFFF
        self.spi_w_byte(0x0020, data)  # PMU_CTL_REG
        data = 0
        self.spi_w_byte(0x1A00, data)  # PMU_CTL
        self.spi_w_byte(0x1A08, data)  # PMU_CONFIG_0

    def TRx_PCRMU_ShutDown_Enable(self):
        data = self.spi_r_byte(0x4004)
        data |= 0x00100000
        self.spi_w_byte(0x4004, data)

        # print('0x4004 : %s' % hex(data))
        data = self.spi_r_byte(0x0020)
        data &= 0x3B
        self.spi_w_byte(0x0020, data)
        # print('0x0020 : %s' % hex(data))

    def TRx_PCRMU_ShutDown_Disable(self):
        data = self.spi_r_byte(0x4004)
        data &= 0xffe7ffff
        self.spi_w_byte(0x4004, data)
        data = self.spi_r_byte(0x0020)
        data |= 0x4
        self.spi_w_byte(0x0020, data)

    def TRx_DCDC_Config(self, DCDCEn: bool):
        if DCDCEn:
            data = 0x8e1205a0
            self.spi_w_byte(0x1200, data)
            data = 0x00003821
            self.spi_w_byte(0x1100, data)
        else:
            data = 0x00003c21
            self.spi_w_byte(0x1100, data)
            data = 0x8e1205a4
            self.spi_w_byte(0x1200, data)

    def TRx_DCDC_HCurrent(self):
        data = 0x0000001b
        self.spi_w_byte(0x1204, data)
        data = 0x00000102
        self.spi_w_byte(0x1E5c, data)

    def TRx_DCDC_LCurrent(self):
        data = 0x00000018
        self.spi_w_byte(0x1204, data)
        data = 0x00000112
        self.spi_w_byte(0x1E5c, data)

    def TRx_FIFOMSB_Config(self, MSBEn: int):
        data = MSBEn
        self.spi_w_byte(0xA0A8, data)

    def TRx_TX_FIFORepeat(self):
        value = 0x4
        data = [0 for _ in range(4)]
        data[3] = value & 0xff
        data[2] = (value >> 8) & 0xff
        data[1] = (value >> 16) & 0xff
        data[0] = (value >> 24) & 0xff
        write_buf = [self.COMM_RSTFIFO, data[3], data[2], data[1], data[0]]
        spi.exchange(write_buf)

    def TRx_TX_FIFOReset(self):
        value = 0x1
        data = [0 for _ in range(4)]
        data[3] = value & 0xff
        data[2] = (value >> 8) & 0xff
        data[1] = (value >> 16) & 0xff
        data[0] = (value >> 24) & 0xff
        write_buf = [self.COMM_RSTFIFO, data[3], data[2], data[1], data[0]]
        spi.exchange(write_buf)

    def TRx_RX_FIFOReset(self):
        value = 0x100
        data = [0 for _ in range(4)]
        data[3] = value & 0xff
        data[2] = (value >> 8) & 0xff
        data[1] = (value >> 16) & 0xff
        data[0] = (value >> 24) & 0xff
        write_buf = [self.COMM_RSTFIFO, data[3], data[2], data[1], data[0]]
        spi.exchange(write_buf)

    def TRx_FIFOTHD(self, TxThreshold: int, RxThreshold: int):
        if TxThreshold > 128 or RxThreshold > 128:
            return False
        data = (((128-RxThreshold) << 16) | TxThreshold)
        self.spi_w_byte(0xA0A4, data)

    def TRx_TX_FIFO(self, WRITESIZE: int, txbuff):
        if WRITESIZE > 128:
            return False
        dl = [0 for _ in range(2)]
        dl[1] = WRITESIZE & 0xff
        dl[0] = (WRITESIZE >> 8) & 0xff
        write_buf = [self.COMM_TXFIFO]

        for i in range(0, int(WRITESIZE / 4)):
            data = [0 for _ in range(4)]
            data[3] = txbuff[i] & 0xff
            data[2] = (txbuff[i] >> 8) & 0xff
            data[1] = (txbuff[i] >> 16) & 0xff
            data[0] = (txbuff[i] >> 24) & 0xff
            write_buf.append(data[3])
            write_buf.append(data[2])
            write_buf.append(data[1])
            write_buf.append(data[0])
        spi.exchange(write_buf)

    def TRx_RX_FIFO(self):
        data = self.spi_r_byte(0xA004)
        value = [0 for _ in range(2)]
        value[1] = ((data >> 16) & 0xff)
        value[0] = (data >> 24) & 0xff

        write_buf = [self.COMM_RXFIFO, 0x00]
        rx_fifo = list(spi.exchange(write_buf, data >> 16))
        read = [format(i, '02x') for i in rx_fifo]
        read_1 = '0x' + ''.join(reversed(read[0:3])) + ','
        # read_1 = '0x' + ''.join(reversed(read[0:6]))  # For DoorBell Test
        # print(read_1)
        # return read_1
        # for i in range(0, int((len(read) - 3) / 4)):
        #     i = 4 * i
        #     read_1 = '0x' + ''.join(reversed(read[(3 + i):(3 + i + 4)])) + ','

    def TRx_RX_FIFOTHD(self, READSIZE: int):
        write_buf = [self.COMM_RXFIFO, 0x00]
        rx_fifo = list(spi.exchange(write_buf, READSIZE))
        read = [format(i, '02x') for i in rx_fifo]
        read_1 = '0x' + ''.join(reversed(read[0:3])) + ','

        for i in range(0, int((len(read) - 3) / 4)):
            i = 4 * i
            read_1 = '0x' + ''.join(reversed(read[(3 + i):(3 + i + 4)])) + ','
            # print(read_1)

    def TRx_TX_PacketOffLoad(self, Preamble: int, PreambleLen: int, SyncWord: int, TxType: int):
        if PreambleLen > 16:
            return False
        data = ((PreambleLen - 1) | (Preamble << 8))
        self.spi_w_byte(0xA02C, data)

        data = 3
        self.spi_w_byte(0xA034, data)
        data = SyncWord
        self.spi_w_byte(0xA038, data)

        data = self.spi_r_byte(0xA028)
        data = (data & 0xFFFFFFF0) | TxType
        self.spi_w_byte(0xA028, data)

    def TRx_TX_PN9_Enable(self):
        data = self.spi_r_byte(0xA028)
        data |= 0x07000000
        self.spi_w_byte(0xA028, data)

    def TRx_TX_PN9_Disable(self):
        data = self.spi_r_byte(0xA028)
        data &= 0xF8FFFFFF
        self.spi_w_byte(0xA028, data)

    def TRx_RX_PN9_Enable(self):
        data = 0x3ff
        self.spi_w_byte(0xb20c, data)

    def TRx_RX_PN9_Disable(self):
        data = 0
        self.spi_w_byte(0xb20c, data)

    def TRx_TX_Trigger(self, DataLen_byte: int, CCAEn: bool):
        if DataLen_byte > 2047:
            return False

        data = self.spi_r_byte(0x10A8)
        self.spi_w_byte(0x1E1C, data)

        if CCAEn:
            data = 0x00015f03
            self.spi_w_byte(0xb19c, data)

            data = (DataLen_byte << 16) | 0x02  # enable CCA
        else:
            data = (DataLen_byte << 16) | 0x01  # disable CCA

        value = [0 for _ in range(4)]
        value[3] = data & 0xff
        value[2] = (data >> 8) & 0xff
        value[1] = (data >> 16) & 0xff
        value[0] = (data >> 24) & 0xff

        write_buf = [self.COMM_TXTrigger, value[3], value[2], value[1], value[0]]
        spi.exchange(write_buf)

    def TRx_RX_Trigger(self, DataLen_byte: int, Mode802154En: bool):

        Cnt = 0
        if DataLen_byte > 2047:
            return False

        if Mode802154En:
            data = (DataLen_byte << 17) | 0x11  # enable checksum
        else:
            data = (DataLen_byte << 17) | 0x21  # ignor checksum

        value = [0 for _ in range(4)]
        value[3] = data & 0xff
        value[2] = (data >> 8) & 0xff
        value[1] = (data >> 16) & 0xff
        value[0] = (data >> 24) & 0xff

        write_buf = [self.COMM_RXTrigger, value[3], value[2], value[1], value[0]]
        spi.exchange(write_buf)

        condition = True
        while condition:
            data = self.spi_r_byte(0x1394)
            data &= 0x1
            Cnt += 1
            if data == 0 and Cnt < self.TimerOutCnt:
                condition = True
            else:
                condition = False
        if Cnt >= self.TimerOutCnt:
            print('TRx_RxOff Timeout')

    def TRx_RxOff(self):
        Cnt = 0
        data = 0
        write_buf = [self.COMM_RXTrigger, data, 0x00, 0x00, 0x00]
        spi.exchange(write_buf)
        condition = True
        while condition:
            data = self.spi_r_byte(0xA018)
            Cnt += 1
            if data and Cnt < self.TimerOutCnt:
                condition = True
            else:
                condition = False
        if Cnt >= self.TimerOutCnt:
            print('TRx_RxOff Timeout')

    def TRx_RX_SyncWord(self, SyncWord: int):
        self.spi_w_byte(0xB100, SyncWord)

    def TRx_GetRSSI_Data(self):
        data = self.spi_r_byte(0xB1FC)
        RSSI_raw = data & 0x7FFFFFFF
        RSSI_dBm = (0xFFF - ((data & 0x7FFFFFFF) - 1)) * 0.125
        return RSSI_raw, RSSI_dBm

    def TRx_CCA_Config(self, CCA_RSSITHD: int):
        data = (CCA_RSSITHD << 16) | 0x500
        self.spi_w_byte(0xA040, data)

    def TRx_GetRSSI_CCA(self):
        data = 0x00015f03
        self.spi_w_byte(0xb19c, data)

        data = 0x201
        Cnt = 0
        dl = [0 for _ in range(2)]
        dl[1] = data & 0xff
        dl[0] = (data >> 8) & 0xff
        write_buf = [self.COMM_RXTrigger, dl[1], dl[0], 0x00, 0x00]
        spi.exchange(write_buf)
        condition = True
        while condition:
            time.sleep(0.0001)
            data = self.spi_r_byte(0xB1FC)
            # print(hex(data))
            Cnt += 1
            if (data & 0x80000000) and Cnt < self.TimerOutCnt:
                condition = True
            else:
                condition = False
        if Cnt >= self.TimerOutCnt:
            print('TRx_RxOff Timeout')
        else:
            RSSI_raw = data & 0x7FFFFFFF
            RSSI_dBm = (0xFFF - ((data & 0x7FFFFFFF) - 1)) * 0.125

            data = 0x00015f00
            self.spi_w_byte(0xb19c, data)
            self.TRx_RxOff()

            data = 1
            while data:
                data = self.spi_r_byte(0xa018)

            # Reset Mac
            data = self.spi_r_byte(0x0010)
            data |= 0xF
            self.spi_w_byte(0x0010, data)

            data = self.spi_r_byte(0x0010)
            data &= 0xFFFFFFF0
            self.spi_w_byte(0x0010, data)

            return RSSI_raw, RSSI_dBm

    def TRx_RX_WUTMR_Enable(self, TOTcnt_20ms: bool):
        if TOTcnt_20ms > 0x3F:
            return False
        data = 1
        self.spi_w_byte(0x002C, data)
        if TOTcnt_20ms:
            data = 0x471 | ((TOTcnt_20ms - 1) << 16)
        else:
            data = 0x471
        self.spi_w_byte(0xA014, data)

    def TRx_RX_WUTMR_Disable(self):
        data = 0
        self.spi_w_byte(0x002C, data)
        self.spi_w_byte(0xA014, data)

    def TRx_GetWUTMR_Timer(self):
        data = self.spi_r_byte(0x8010)
        WUTRMcnt = data

        data = self.spi_r_byte(0x8014)
        WUTRMcnt |= (data << 32)
        return WUTRMcnt

    def TRx_SetWUTMR_Timer(self, WUTIMER_ms: int, PeriodicEn: bool):
        Cnt = 0
        data = 0
        self.spi_w_byte(0x8000, data)
        condition = True
        while condition:
            data = self.spi_r_byte(0x8004)
            Cnt += 1
            if data and Cnt < self.TimerOutCnt:
                condition = True
            else:
                condition = False
        if Cnt >= self.TimerOutCnt:
            print('TRx_RxOff Timeout')
        else:
            data = int((WUTIMER_ms << 15)/1000)
            print(hex(data))
            self.spi_w_byte(0x8008, data)
            if PeriodicEn:
                data = 0x11
            else:
                data = 0x01
            self.spi_w_byte(0x8000, data)

    def TRx_PAH915MHz23dBm_FSK(self, Power_0_127: int):
        if Power_0_127 > 127:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x00619FC5
        self.spi_w_byte(0x1480, data)
        data = 0
        self.spi_w_byte(0x1488, data)
        self.spi_w_byte(0x1e60, data)
        data = 0x0013d200
        self.spi_w_byte(0x1484, data)
        data = 0x1000000 | (Power_0_127 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAH490MHz23dBm_FSK(self, Power_0_127: int):
        if Power_0_127 > 127:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x00619FC5
        self.spi_w_byte(0x1480, data)
        data = 0
        self.spi_w_byte(0x1488, data)
        self.spi_w_byte(0x1e60, data)

        data = 0x0013d200
        self.spi_w_byte(0x1484, data)
        data = (0x1000000 | (Power_0_127 << 25))
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAH433MHz23dBm_FSK(self, Power_0_127: int):
        if Power_0_127 > 127:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x00619FC5
        self.spi_w_byte(0x1480, data)
        data = 0
        self.spi_w_byte(0x1488, data)
        self.spi_w_byte(0x1e60, data)

        data = 0x0013d200
        self.spi_w_byte(0x1484, data)
        data = (0x1000000 | (Power_0_127 << 25))
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAH915MHz20dBm_FSK(self, Power_0_127: int):
        if Power_0_127 > 127:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x0060ffc5
        self.spi_w_byte(0x1480, data)
        data = 0
        self.spi_w_byte(0x1488, data)
        self.spi_w_byte(0x1e60, data)
        data = 0x00129200
        self.spi_w_byte(0x1484, data)
        data = 0x1000000 | (Power_0_127 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAH868MHz20dBm_FSK(self, Power_0_127: int):
        if Power_0_127 > 127:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x0060ffc5
        self.spi_w_byte(0x1480, data)
        data = 0
        self.spi_w_byte(0x1488, data)
        self.spi_w_byte(0x1e60, data)
        data = 0x00129200
        self.spi_w_byte(0x1484, data)
        data = 0x1000000 | (Power_0_127 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAH490MHz20dBm_FSK(self, Power_0_127: int):
        if Power_0_127 > 127:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x00601fc5
        self.spi_w_byte(0x1480, data)
        data = 0x6
        self.spi_w_byte(0x1488, data)
        data = 0x00000008
        self.spi_w_byte(0x1e60, data)

        data = 0x00139200
        self.spi_w_byte(0x1484, data)
        data = 0x1000000 | (Power_0_127 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAH433MHz20dBm_FSK(self, Power_0_127: int):
        if Power_0_127 > 127:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x00601fc5
        self.spi_w_byte(0x1480, data)
        data = 0x3
        self.spi_w_byte(0x1488, data)
        data = 0x0000000c
        self.spi_w_byte(0x1e60, data)

        data = 0x00131200
        self.spi_w_byte(0x1484, data)
        data = 0x1000000 | (Power_0_127 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAH868MHz16dBm_FSK(self, Power_0_127: int):
        if Power_0_127 > 127:
            return False
        self.TRx_DCDC_HCurrent()
        data = 0x00601fc5
        self.spi_w_byte(0x1480, data)
        data =0x4
        self.spi_w_byte(0x1488, data)
        data = 0
        self.spi_w_byte(0x1e60, data)

        data = 0x00139200
        self.spi_w_byte(0x1484, data)
        data = 0x1000000 | (Power_0_127 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAL868MHz16dBm_FSK(self, Power_0_27: int):
        if Power_0_27 > 27:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x006006c5
        self.spi_w_byte(0x1480, data)
        data = 0x0002d200
        self.spi_w_byte(0x1484, data)
        data = 0x00000002
        self.spi_w_byte(0x1488, data)
        data = 0x00000008
        self.spi_w_byte(0x1e60, data)
        data = 0x1000000 | (Power_0_27 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAH868MHz14dBm_FSK(self, Power_0_127: int):
        if Power_0_127 > 127:
            return False
        self.TRx_DCDC_HCurrent()
        data = 0x0060ffc5
        self.spi_w_byte(0x1480, data)
        data = 0x6
        self.spi_w_byte(0x1488, data)
        data = 0x00000008
        self.spi_w_byte(0x1e60, data)

        data = 0x0012d200
        self.spi_w_byte(0x1484, data)
        data = 0x1000000 | (Power_0_127 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAL868MHz14dBm_FSK(self, Power_0_27: int):
        if Power_0_27 > 27:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x00600505
        self.spi_w_byte(0x1480, data)
        data = 0x00039200
        self.spi_w_byte(0x1484, data)
        data = 0x00000007
        self.spi_w_byte(0x1488, data)
        data = 0x0000000c
        self.spi_w_byte(0x1E60, data)
        data = 0x1000000 | (Power_0_27 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAL915MHz10dBm_FSK(self, Power_0_27: int):
        if Power_0_27 > 27:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x006006c5
        self.spi_w_byte(0x1480, data)
        data = 0x00039200
        self.spi_w_byte(0x1484, data)
        data = 0x00000006
        self.spi_w_byte(0x1488, data)
        data = 0x00000008
        self.spi_w_byte(0x1e60, data)
        data = 0x1000000 | (Power_0_27 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAL868MHz10dBm_FSK(self, Power_0_27: int):
        if Power_0_27 > 27:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x006006c5
        self.spi_w_byte(0x1480, data)
        data = 0x00029200
        self.spi_w_byte(0x1484, data)
        data = 0x00000006
        self.spi_w_byte(0x1488, data)
        data = 0x00000008
        self.spi_w_byte(0x1e60, data)
        data = 0x1000000 | (Power_0_27 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAL490MHz10dBm_FSK(self, Power_0_27: int):
        if Power_0_27 > 27:
            return False
        self.TRx_DCDC_LCurrent()
        data = 0x006006c5
        self.spi_w_byte(0x1480, data)
        data = 0x00029200
        self.spi_w_byte(0x1484, data)
        data = 0x00000006
        self.spi_w_byte(0x1488, data)
        data = 0x00000008
        self.spi_w_byte(0x1e60, data)
        data = 0x1000000 | (Power_0_27 << 25)
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_PAL433MHz10dBm_FSK(self, Power_0_27: int):
        if Power_0_27 > 27:
            return False

        self.TRx_DCDC_LCurrent()
        data = 0x008006c5
        self.spi_w_byte(0x1480, data)
        data = 0x00029200
        self.spi_w_byte(0x1484, data)
        data = 0x00000006
        self.spi_w_byte(0x1488, data)
        data = 0x00000008
        self.spi_w_byte(0x1e60, data)
        data = (0x1000000 | (Power_0_27 << 25))
        self.spi_w_byte(0x1E1C, data)
        self.spi_w_byte(0x10A8, data)
        data = 0x13000027
        self.spi_w_byte(0x1E20, data)

    def TRx_IQCal_Enable(self):
        Cnt = 0
        u32min_rssi = 4096
        u32new_rssi = 4096
        u32RXLO_phase = 0xe

        self._mac_clock_gatting_switch(0)

        data = self.spi_r_byte(0xb200)
        data = ((data & 0x1f) << 16) | 0x3
        self.spi_w_byte(0xb248, data)

        data = 0x0000000c
        self.spi_w_byte(0x1E3C, data)
        u32BPS = self.spi_r_byte(0x1988)
        u32Freq = self.spi_r_byte(0x1980)
        if u32BPS & 0xffffff >= 1000000:
            u32Freq -= 4000000
        else:
            u32Freq -= 2000000
        self.spi_w_byte(0x12ac, u32Freq)
        data = 1
        self.spi_w_byte(0x12b0, data)

        data = 0x201
        dl = [0 for _ in range(2)]
        dl[1] = data & 0xff
        dl[0] = (data >> 8) & 0xff
        write_buf = [self.COMM_RXTrigger, dl[1], dl[0], 0x00, 0x00]
        spi.exchange(write_buf)
        time.sleep(0.00001)
        self.TRx_RxOff()

        data = 1
        while data:
            data = self.spi_r_byte(0xa018)

        data = self.spi_r_byte(0x19a0)
        self.spi_w_byte(0x139c, data)
        data = 0
        self.spi_w_byte(0x1e00, data)
        self.spi_w_byte(0x1e30, data)
        self.spi_w_byte(0x1404, data)
        data = 0x0070e530
        self.spi_w_byte(0x1180, data)
        data = self.spi_r_byte(0x1380)
        data = (data & 0xfffffffe) | 0x2000000
        self.spi_w_byte(0x1380, data)
        data = self.spi_r_byte(0x12a4)
        data &= 0xfffbffff
        self.spi_w_byte(0x12a4, data)

        data = 0x005201a3
        self.spi_w_byte(0x1600, data)
        data = 1
        self.spi_w_byte(0x1e70, data)

        for i in range(0, 16):
            data = self.spi_r_byte(0x1620)
            data = (data & 0xfffe1fff) | (i << 13)
            self.spi_w_byte(0x1620, data)
            u32new_rssi, ff_rssi = self.TRx_GetRSSI_CCA()
            if u32new_rssi <= u32min_rssi:
                u32min_rssi = u32new_rssi
                u32RXLO_phase = i

        data = self.spi_r_byte(0x1620)
        data = (data & 0xfffe1fff) | (u32RXLO_phase << 13)
        self.spi_w_byte(0x1620, data)

        data = self.spi_r_byte(0x1180)
        data |= 0x200
        self.spi_w_byte(0x1180, data)

        data = self.spi_r_byte(0x1380)
        data = (data & 0xfdffffff) | 1
        self.spi_w_byte(0x1380, data)

        data = self.spi_r_byte(0x12a4)
        data |= 0x00040000
        self.spi_w_byte(0x12a4, data)

        data = 1
        self.spi_w_byte(0x1404, data)
        self.spi_w_byte(0xb248, data)

        # data = 0x00120000
        data = 0x000A0200
        self.spi_w_byte(0x1600, data)
        data = 0
        self.spi_w_byte(0x1e70, data)
        data = 0x00010101
        self.spi_w_byte(0x1e00, data)
        data = 0x0000000d
        self.spi_w_byte(0x1e3c, data)
        data = 0xfdffff3f
        self.spi_w_byte(0x1e30, data)

        # Reset Mac
        data = self.spi_r_byte(0x0010)
        data |= 0xF
        self.spi_w_byte(0x0010, data)

        data = self.spi_r_byte(0x0010)
        data &= 0xFFFFFFF0
        self.spi_w_byte(0x0010, data)

        self._mac_clock_gatting_switch(1)

        return u32RXLO_phase

    def TRx_IQCal_fixed(self, IQ_data: int):
        Cnt = 0
        u32min_rssi = 4096
        u32new_rssi = 4096
        u32RXLO_phase = 0xe

        self._mac_clock_gatting_switch(0)

        data = self.spi_r_byte(0xb200)
        data = ((data & 0x1f) << 16) | 0x3
        self.spi_w_byte(0xb248, data)

        data = 0x0000000c
        self.spi_w_byte(0x1E3C, data)
        u32BPS = self.spi_r_byte(0x1988)
        u32Freq = self.spi_r_byte(0x1980)
        if u32BPS & 0xffffff >= 1000000:
            u32Freq -= 4000000
        else:
            u32Freq -= 2000000
        self.spi_w_byte(0x12ac, u32Freq)
        data = 1
        self.spi_w_byte(0x12b0, data)

        data = 0x201
        dl = [0 for _ in range(2)]
        dl[1] = data & 0xff
        dl[0] = (data >> 8) & 0xff
        write_buf = [self.COMM_RXTrigger, dl[1], dl[0], 0x00, 0x00]
        spi.exchange(write_buf)
        time.sleep(0.00001)
        self.TRx_RxOff()

        data = 1
        while data:
            data = self.spi_r_byte(0xa018)

        data = self.spi_r_byte(0x19a0)
        self.spi_w_byte(0x139c, data)
        data = 0
        self.spi_w_byte(0x1e00, data)
        self.spi_w_byte(0x1e30, data)
        self.spi_w_byte(0x1404, data)
        data = 0x0070e530
        self.spi_w_byte(0x1180, data)
        data = self.spi_r_byte(0x1380)
        data = (data & 0xfffffffe) | 0x2000000
        self.spi_w_byte(0x1380, data)
        data = self.spi_r_byte(0x12a4)
        data &= 0xfffbffff
        self.spi_w_byte(0x12a4, data)

        data = 0x005201a3
        self.spi_w_byte(0x1600, data)
        data = 1
        self.spi_w_byte(0x1e70, data)

        u32RXLO_phase = IQ_data

        data = self.spi_r_byte(0x1620)
        data = (data & 0xfffe1fff) | (u32RXLO_phase << 13)
        self.spi_w_byte(0x1620, data)

        data = self.spi_r_byte(0x1180)
        data |= 0x200
        self.spi_w_byte(0x1180, data)

        data = self.spi_r_byte(0x1380)
        data = (data & 0xfdffffff) | 1
        self.spi_w_byte(0x1380, data)

        data = self.spi_r_byte(0x12a4)
        data |= 0x00040000
        self.spi_w_byte(0x12a4, data)

        data = 1
        self.spi_w_byte(0x1404, data)
        self.spi_w_byte(0xb248, data)

        # data = 0x00100000
        data = 0x000A0200
        self.spi_w_byte(0x1600, data)
        data = 0
        self.spi_w_byte(0x1e70, data)
        data = 0x00010101
        self.spi_w_byte(0x1e00, data)
        data = 0x0000000d
        self.spi_w_byte(0x1e3c, data)
        data = 0xfdffff3f
        self.spi_w_byte(0x1e30, data)

        # Reset Mac
        data = self.spi_r_byte(0x0010)
        data |= 0xF
        self.spi_w_byte(0x0010, data)

        data = self.spi_r_byte(0x0010)
        data &= 0xFFFFFFF0
        self.spi_w_byte(0x0010, data)

        self._mac_clock_gatting_switch(1)

        return u32RXLO_phase

    def TRx_PA_SETUP(self):
        a = ER4100_config_V405.ER4100_config()

        for reg_a, reg_v in a.FSK_Power_Array.items():
            self.spi_w_byte(reg_a, reg_v)

    def TRx_TX_SETUP(self):
        self.TRx_PA_SETUP()
        data = self.spi_r_byte(0x13AC)
        if self.modulation == self.TX_GFSK_MODE:
            data |= 0x300000
            self.spi_w_byte(0x13AC, data)
            data = self.spi_r_byte(0x1E04)
            data = (data & 0xFF3FFF0F) | (self.TX_FSK_MODE << 5)
        else:
            data = (data & 0xFFEFFFFF) | 0x200000
            self.spi_w_byte(0x13AC, data)
            data = self.spi_r_byte(0x1E04)
            data = (data & 0xFF3FFF0F) | (self.modulation << 5)

        if self.dr >= 100000:
            data |= (1 << 23)
        self.spi_w_byte(0x1E04, data)

        # ----------- set TX data rate --------------------------------
        data = self.spi_r_byte(0x13AC)
        data = (data & 0xFFF00000) | int(Crystal_Hz / (self.dr * 10))

        self.spi_w_byte(0x13AC, data)
        data = int(Crystal_Hz / 20 / self.dr) << 16
        self.spi_w_byte(0xA030, data)

    def TRx_RX_SETUP(self):
        global TRx_DataRate, TRx_FreqDev
        self.TRx_Filter_SETUP()
        # ---------- set RX data rate --------------------------------
        a = ER4100_config_V405.ER4100_config()

        if self.dr == 2000000:
            for reg_a, reg_v in a.RX_2MBPSh05_Array.items():
                self.spi_w_byte(reg_a, reg_v)
            i = 0x0010001a
            self.spi_w_byte(0xb200, i)
            i = 0x0000ffd8
            self.spi_w_byte(0xb1b4, i)
            i = 0x00000010
            self.spi_w_byte(0xb304, i)

            i = 0x0000083f
            self.spi_w_byte(0xb0d0, i)
            i = 0x00015f04
            self.spi_w_byte(0xb19c, i)
            i = 0x00000050
            self.spi_w_byte(0xb1a8, i)

            TRx_DataRate = self.dr

        elif self.dr == 1000000:
            if 1 > self.mod_index >= 0.5:
                for reg_a, reg_v in a.RXLPF_h0p5_OSR8.items():
                    self.spi_w_byte(reg_a, reg_v)
                for reg_a, reg_v in a.RXCHFILT_h0p5_OSR8.items():
                    self.spi_w_byte(reg_a, reg_v)
                i = 0x00100018
            elif 2 > self.mod_index >= 1:
                for reg_a, reg_v in a.RXLPF_h1_OSR8.items():
                    self.spi_w_byte(reg_a, reg_v)
                for reg_a, reg_v in a.RXCHFILT_h1_OSR8.items():
                    self.spi_w_byte(reg_a, reg_v)
                i = 0x00280018

            self.spi_w_byte(0xb200, i)
            i = 0x0000ffb0
            self.spi_w_byte(0xb1b4, i)
            i = 0x00000010
            self.spi_w_byte(0xb304, i)

            i = 0x0000083f
            self.spi_w_byte(0xb0d0, i)
            i = 0x00015f04
            self.spi_w_byte(0xb19c, i)
            i = 0x00000200
            self.spi_w_byte(0xb30c, i)
            i = 0x00000050
            self.spi_w_byte(0xb1a8, i)

            TRx_DataRate = self.dr | (1 << 24)

        else:
            if 1 > self.mod_index >= 0.5:
                for reg_a, reg_v in a.RXLPF_h0p5_OSR16.items():
                    self.spi_w_byte(reg_a, reg_v)
                for reg_a, reg_v in a.RXCHFILT_h0p5_OSR8.items():
                    self.spi_w_byte(reg_a, reg_v)
            elif 2 > self.mod_index >= 1:
                for reg_a, reg_v in a.RXLPF_h1_OSR16.items():
                    self.spi_w_byte(reg_a, reg_v)
                for reg_a, reg_v in a.RXCHFILT_h1_OSR8.items():
                    self.spi_w_byte(reg_a, reg_v)
            elif 4 > self.mod_index >= 2:
                for reg_a, reg_v in a.RXLPF_h2_OSR16.items():
                    self.spi_w_byte(reg_a, reg_v)
                for reg_a, reg_v in a.RXCHFILT_h2_OSR8.items():
                    self.spi_w_byte(reg_a, reg_v)
            elif self.mod_index >= 4:
                for reg_a, reg_v in a.RXLPF_h4_OSR16.items():
                    self.spi_w_byte(reg_a, reg_v)
                for reg_a, reg_v in a.RXCHFILT_h4_OSR16.items():
                    self.spi_w_byte(reg_a, reg_v)

            # other setting
            if self.dr == 500000:
                data = 0x0000ff10
                self.spi_w_byte(0xb1b4, data)
                if 1 > self.mod_index >= 0.5:
                    i = 0x00100016
                elif 2 > self.mod_index >= 1:
                    i = 0x00280017
                elif 4 > self.mod_index >= 2:
                    i = 0x00280016
                elif self.mod_index >= 4:
                    i = 0x00280016
                self.spi_w_byte(0xb200, i)

            elif self.dr == 250000:
                data = 0x0000ff10
                self.spi_w_byte(0xb1b4, data)
                if 1 > self.mod_index >= 0.5:
                    i = 0x00100017
                elif 2 > self.mod_index >= 1:
                    i = 0x00280018
                elif 4 > self.mod_index >= 2:
                    i = 0x00280017
                elif self.mod_index >= 4:
                    i = 0x00280017
                self.spi_w_byte(0xb200, i)

            elif self.dr == 200000:
                data = 0x0000ff10
                self.spi_w_byte(0xb1b4, data)
                if 1 > self.mod_index >= 0.5:
                    i = 0x00100017
                elif 2 > self.mod_index >= 1:
                    i = 0x00280018
                elif 4 > self.mod_index >= 2:
                    i = 0x00280017
                elif self.mod_index >= 4:
                    i = 0x00280017
                self.spi_w_byte(0xb200, i)

            elif self.dr == 125000:
                data = 0x0000ff10
                self.spi_w_byte(0xb1b4, data)
                if 1 > self.mod_index >= 0.5:
                    i = 0x00100018
                elif 2 > self.mod_index >= 1:
                    i = 0x00280019
                elif 4 > self.mod_index >= 2:
                    i = 0x00280018
                elif self.mod_index >= 4:
                    i = 0x00280018
                self.spi_w_byte(0xb200, i)

            elif self.dr == 100000:
                data = 0x0000fee8
                self.spi_w_byte(0xb1b4, data)
                if 1 > self.mod_index >= 0.5:
                    i = 0x000c0017
                elif 2 > self.mod_index >= 1:
                    i = 0x00240018
                elif 4 > self.mod_index >= 2:
                    i = 0x00240017
                elif self.mod_index >= 4:
                    i = 0x00240017
                self.spi_w_byte(0xb200, i)

            elif self.dr == 50000:
                data = 0x0000fee8
                self.spi_w_byte(0xb1b4, data)
                if 1 > self.mod_index >= 0.5:
                    i = 0x000c0018
                elif 2 > self.mod_index >= 1:
                    i = 0x00240019
                elif 4 > self.mod_index >= 2:
                    i = 0x00240018
                elif self.mod_index >= 4:
                    i = 0x00240018
                self.spi_w_byte(0xb200, i)

            elif self.dr == 12500:
                data = 0x0000fee8
                self.spi_w_byte(0xb1b4, data)
                if 1 > self.mod_index >= 0.5:
                    i = 0x000c001a
                elif 2 > self.mod_index >= 1:
                    i = 0x0024001b
                elif 4 > self.mod_index >= 2:
                    i = 0x0024001a
                elif self.mod_index >= 4:
                    i = 0x0024001a
                self.spi_w_byte(0xb200, i)

            else:
                data = 0x0000fee8
                self.spi_w_byte(0xb1b4, data)
                if 1 > self.mod_index >= 0.5:
                    i = 0x000c001c
                elif 2 > self.mod_index >= 1:
                    i = 0x0024001d
                elif 4 > self.mod_index >= 2:
                    i = 0x0024001c
                elif self.mod_index >= 4:
                    i = 0x0024001c
                self.spi_w_byte(0xb200, i)

            i = 0x00000000
            self.spi_w_byte(0xb304, i)
            i = 0x00015f03
            self.spi_w_byte(0xb19c, i)
            i = 0x00000070
            self.spi_w_byte(0xb1a8, i)

            TRx_DataRate = self.dr

        TRx_FreqDev = int(self.dr * self.mod_index)
        # print(TRx_FreqDev)

        if self.dr >= 1000000:
            i = 0x00000986
            self.spi_w_byte(0xb000, i)
            i = 0x4
        else:
            i = 0x00000906
            self.spi_w_byte(0xb000, i)
            i = 0x2
        self.spi_w_byte(0x1188, i)

    def TRx_SetChannel(self):
        global TRx_DataRate, TRx_FreqDev
        self.spi_w_byte(0x1980, self.freq)
        self.spi_w_byte(0x1984, TRx_FreqDev)
        self.spi_w_byte(0x1988, TRx_DataRate)
        data = 1
        self.spi_w_byte(0x1990, data)

        if Crystal_Hz == 38400000:
            TRx_DataRate &= (~(1 << 24))
            u32Frequency_MHz = self.freq / 1000000
            fDataRate = TRx_DataRate / 1000000
            hf32Channel = (u32Frequency_MHz + ((fDataRate * self.mod_index) / 2))
            RXmf32Channel = u32Frequency_MHz
            TXmf32Channel = u32Frequency_MHz
            lf32Channel = (u32Frequency_MHz - ((fDataRate * self.mod_index) / 2))

            if TRx_DataRate >= 1000000:
                RXmf32Channel -= ((Crystal_MHz / HW_Clk_MHz) * 2)
            else:
                RXmf32Channel -= (Crystal_MHz / HW_Clk_MHz)

            if 1556 <= u32Frequency_MHz <= 2040:
                data = 0x0018a785
                self.spi_w_byte(0x1380, data)
                c_data = 2
            elif 778 <= u32Frequency_MHz <= 1020:
                data = 0x0016a785
                self.spi_w_byte(0x1380, data)
                c_data = 4
            elif 389 <= u32Frequency_MHz <= 510:
                data = 0x0014a785
                self.spi_w_byte(0x1380, data)
                c_data = 8
            elif 194 <= u32Frequency_MHz <= 255:
                data = 0x0012a785
                self.spi_w_byte(0x1380, data)
                c_data = 16
            elif 97 <= u32Frequency_MHz <= 127:
                data = 0x0010a785
                self.spi_w_byte(0x1380, data)
                c_data = 32

            DIV_INT = ((hf32Channel * c_data) / (Crystal_MHz * 2)) - 32
            DIV_FRAC = (((hf32Channel * c_data) / (Crystal_MHz * 2)) - (
                int(hf32Channel * c_data / (Crystal_MHz * 2)))) * 262144
            data = (int(DIV_INT) << 18) | int(DIV_FRAC)
            self.spi_w_byte(0x1998, data)

            DIV_INT = ((lf32Channel * c_data) / (Crystal_MHz * 2)) - 32
            DIV_FRAC = (((lf32Channel * c_data) / (Crystal_MHz * 2)) - (
                int(lf32Channel * c_data / (Crystal_MHz * 2)))) * 262144
            data = (int(DIV_INT) << 18) | int(DIV_FRAC)
            self.spi_w_byte(0x199c, data)

            DIV_INT = ((RXmf32Channel * c_data) / (Crystal_MHz * 2)) - 32
            DIV_FRAC = (((RXmf32Channel * c_data) / (Crystal_MHz * 2)) - (
                int(RXmf32Channel * c_data / (Crystal_MHz * 2)))) * 262144
            data = (int(DIV_INT) << 18) | int(DIV_FRAC)
            self.spi_w_byte(0x19a0, data)

            DIV_INT = ((TXmf32Channel * c_data) / (Crystal_MHz * 2)) - 32
            DIV_FRAC = (((TXmf32Channel * c_data) / (Crystal_MHz * 2)) - (
                int(TXmf32Channel * c_data / (Crystal_MHz * 2)))) * 262144
            data = (int(DIV_INT) << 18) | int(DIV_FRAC)
            self.spi_w_byte(0x1994, data)


    def TRx_SingleTone_Trigger(self):
        data = 0
        self.spi_w_byte(0x0000, data)
        data = self.spi_r_byte(0x1E04)
        data |= 0x01400001
        # print('ttt:' + hex(data))
        self.spi_w_byte(0x1E04, data)

    def TRx_SingleTone_Enable(self, Power_0_127: int, Config_23dBm: bool):

        rev_id = self.spi_r_byte(0xf104)
        # print(rev_id)
        Is_HighPwr_PA = True if rev_id is (2 or 3) else False

        if Is_HighPwr_PA:
            if Power_0_127 > 127 or Power_0_127 < 0:
                return False
        else:
            if Power_0_127 > 27 or Power_0_127 < 0:
                return False

        if Is_HighPwr_PA:
            if Config_23dBm:
                if 902*1E6 <= self.freq <= 928*1E6:
                    self.TRx_PAH915MHz23dBm_FSK(Power_0_127)
                elif 470*1E6 <= self.freq <= 510*1E6:
                    self.TRx_PAH490MHz23dBm_FSK(Power_0_127)
                elif 433*1E6 <= self.freq <= 434*1E6:
                    self.TRx_PAH433MHz23dBm_FSK(Power_0_127)
                else:
                    # self.TRx_SingleToneH915MHz23dBm_Enable(Power_0_127)
                    sys.exit("Set Single Enable Fail, Please check the Frequency & Config_23dBm setup")
            else:
                if 902 * 1E6 <= self.freq <= 928 * 1E6:
                    self.TRx_PAH915MHz20dBm_FSK(Power_0_127)
                elif 868 * 1E6 <= self.freq <= 868.6 * 1E6:
                    self.TRx_PAH868MHz20dBm_FSK(Power_0_127)
                elif 470 * 1E6 <= self.freq <= 510 * 1E6:
                    self.TRx_PAH490MHz20dBm_FSK(Power_0_127)
                elif 433*1E6 <= self.freq <= 434*1E6:
                    self.TRx_PAH433MHz20dBm_FSK(Power_0_127)
                else:
                    # self.TRx_SingleToneH915MHz20dBm_Enable(Power_0_127)
                    sys.exit("Set Single Enable Fail, Please check the Frequency & Config_23dBm setup")
        else:
            if 902 * 1E6 <= self.freq <= 928 * 1E6:
                self.TRx_PAL915MHz10dBm_FSK(Power_0_127)
            elif 868 * 1E6 <= self.freq <= 868.6 * 1E6:
                self.TRx_PAL868MHz14dBm_FSK(Power_0_127)
            elif 470 * 1E6 <= self.freq <= 510 * 1E6:
                self.TRx_PAL490MHz10dBm_FSK(Power_0_127)
            elif 433*1E6 <= self.freq <= 434*1E6:
                self.TRx_PAL433MHz10dBm_FSK(Power_0_127)
            else:
                # self.TRx_SingleToneL915MHz10dBm_Enable(Power_0_127)
                sys.exit("Set Single Enable Fail, Please check the Frequency setup")

    def TRx_SingleTone_Disable(self):
        Cnt = 0
        data = self.spi_r_byte(0x1E04)
        data &= 0xFEBFFFFE
        self.spi_w_byte(0x1E04, data)

        condition = True
        while condition:
            data = self.spi_r_byte(0x1EFC)
            Cnt += 1
            if (data != 0x00020001) and Cnt < self.TimerOutCnt:
                condition = True
            else:
                condition = False
        if Cnt >= self.TimerOutCnt:
            print('TRx_RxOff Timeout')
        else:
            data = self.spi_r_byte(0x1988)
            if data & 0xFFFFFF >= 100000:
                data = self.spi_r_byte(0x1E04)
                data |= (1 << 23)
                self.spi_w_byte(0x1E04, data)
            data = 0x00000007
            self.spi_w_byte(0x0000, data)

    def TRx_PRBS9_Trigger(self):
        data = 0
        self.spi_w_byte(0x0000, data)
        data = 0x07a137bc
        self.spi_w_byte(0x1E40, data)
        data = 0xb710d793
        self.spi_w_byte(0x1E44, data)
        data = 0x8aab5add
        self.spi_w_byte(0x1E48, data)
        data = 0x39b85666
        self.spi_w_byte(0x1E4c, data)
        data = self.spi_r_byte(0x1E04)
        data |= 0x00407f01
        self.spi_w_byte(0x1E04, data)

    def TRx_Config(self):
        Cnt = 0
        a = RegAccess()
        self.TRx_TX_SETUP()
        self.TRx_RX_SETUP()
        self.TRx_SetChannel()

        self.TRx_load_Tool_CSR()

        time.sleep(0.2)  # For RCLC Calib

        # ---------- DCDC Switch ------------------------------
        # self.spi_w_field(a.DCDC_CONFIG_0_DCDC_PD12_ADDR, a.DCDC_CONFIG_0_DCDC_PD12_MASK, 1)
        # self.spi_w_field(a.DCDC_CONFIG_0_DCDC_BYPASS12_ADDR, a.DCDC_CONFIG_0_DCDC_BYPASS12_MASK, 1)
        # self.spi_w_field(a.DCDC_CONFIG_1_DCDC_PMOS_ON_BYP_ADDR, a.DCDC_CONFIG_1_DCDC_PMOS_ON_BYP_MASK, 1)

        # ---------- GPIO0 VDD ------------------------------
        # self.spi_w_field(0x4000, 0x700, 0)  # For output Tx_EN

        # ---------- Tx_EN ------------------------------
        # self.spi_w_byte(0x4004, 0x6010D)  # For output Tx_EN

        if 900 * 1E6 <= self.freq <= 930 * 1E6:
            self.spi_w_field(0x1620, 0x1E0000, 9)
        elif 865 * 1E6 <= self.freq <= 870 * 1E6:
            self.spi_w_field(0x1620, 0x1E0000, 13)
        elif 430 * 1E6 <= self.freq <= 440 * 1E6:
            self.spi_w_field(0x1620, 0x1E0000, 11)
        else:
            self.spi_w_field(0x1620, 0x1E0000, 9)

        condition = True
        while condition:
            data = self.spi_r_byte(0x1E34)
            Cnt += 1
            if (data & 0x01) and Cnt < self.TimerOutCnt:
                condition = True
            else:
                condition = False
        if Cnt >= self.TimerOutCnt:
            sys.exit("ANCTL_OSC_CAL_EN Fail")

    def TRx_Filter_SETUP(self):
        if 2000000 >= self.dr > 1000000 and 0.5 <= self.mod_index < 1:
            data = 0x1fc01b7d
        elif 1000000 >= self.dr > 500000 and 0.5 <= self.mod_index < 1:
            data = 0x1fc01b7d
        elif self.dr == 500000 and 0.5 <= self.mod_index < 1:
            data = 0x1fc22b7d
        elif self.dr < 500000 and 0.5 <= self.mod_index < 1:
            data = 0x1fc33b7d
        elif self.dr == 1000000 and 1 <= self.mod_index < 2:
            data = 0x1fc01b7d
        elif 1000000 > self.dr >= 500000 and 1 <= self.mod_index < 2:
            data = 0x1fc22b7d
        elif self.dr < 500000 and 1 <= self.mod_index < 2:
            data = 0x1fc33b7d
        elif self.dr == 500000 and 2 <= self.mod_index < 4:
            data = 0x1fc22b7d
        elif self.dr < 500000 and 2 <= self.mod_index < 4:
            data = 0x1fc22b7d
        elif self.dr < 250000 and self.mod_index >= 4:
            data = 0x1fc00b7d

        self.spi_w_byte(0x1604, data)

    def TRx_GetADC_RawData(self):
        data = 0x75ed5e4c
        AdcCalibSet = 0xf5e34e4c
        self.spi_w_byte(0x1080, AdcCalibSet)
        time.sleep(0.0005)
        ADCData = self.spi_r_byte(0x1084)
        self.spi_w_byte(0x1080, data)
        return ADCData

    def TRx_GetADC_RawData_temp(self):
        r = RegAccess()
        data = 0x75ed5e4c
        AdcCalibSet = 0xF5E34E4C
        self.spi_w_byte(0x1080, AdcCalibSet)
        self.spi_w_field(r.ASARADC_CONFIG_1_ASARADC_VIN_MUX_SEL_ADDR, r.ASARADC_CONFIG_1_ASARADC_VIN_MUX_SEL_MASK, 14)
        time.sleep(0.0005)

        ADCData = self.spi_r_byte(0x1084)
        self.spi_w_byte(0x1080, data)
        return ADCData

    def TRx_GetADC_Data(self):
        global TRx_ADCGain, TRx_ADCOffset
        data = 0x75ed5e4c
        AdcCalibSet = 0xF5E34E4C
        self.spi_w_byte(0x1080, AdcCalibSet)
        time.sleep(0.0005)

        tmp = self.spi_r_byte(0x1084)
        if TRx_ADCGain != 0 or TRx_ADCOffset != 0:
            TRx_ADCGain = 0.82226
            TRx_ADCOffset = 92.4657

        ADCVoltage_mv = tmp*TRx_ADCGain+TRx_ADCOffset
        self.spi_w_byte(0x1080, data)
        return ADCVoltage_mv

    def TRx_ADC2PCal_Enable(self, AdcCalibHP, AdcCalibLP):
        global TRx_ADCGain, TRx_ADCOffset
        if AdcCalibHP == 0 or AdcCalibLP == 0:
            TRx_ADCGain = 0
            TRx_ADCOffset = 0
            return False
        coef_a = (AdcCalibHP - AdcCalibLP) / (self.TRX_ADC_HIGH_VOLTAGE - self.TRX_ADC_LOW_VOLTAGE)
        coef_b = AdcCalibHP - (self.TRX_ADC_HIGH_VOLTAGE*coef_a)
        TRx_ADCGain = 1000 / coef_a
        TRx_ADCOffset = (-1)*(coef_b / coef_a)*1000

    def TRx_ADC1PCal_Enable(self):
        global TRx_ADCGain, TRx_ADCOffset
        AdcCalibHP = self.TRx_GetADC_RawData()
        TRx_ADCGain = 0.82226
        TRx_ADCOffset = 3300 - (TRx_ADCGain * AdcCalibHP)

    def TRx_GetModemVol_Data(self):
        global TRx_ADCGain, TRx_ADCOffset
        # print('TRx_ADCGain : %d' % TRx_ADCGain)
        # print('TRx_ADCOffset : %d' % TRx_ADCOffset)

        if TRx_ADCGain == 0 or TRx_ADCOffset == 0:  # Need to confirm
            TRx_ADCGain = 0.82226
            TRx_ADCOffset = 92.4657

        data = self.spi_r_byte(0x1E2C)
        ADCVoltage_mv = (((data >> 16) * TRx_ADCGain) + TRx_ADCOffset)
        return ADCVoltage_mv

    def TRx_ModemVol_Enable(self):
        data = 0x00000c1f
        self.spi_w_byte(0x1E2C, data)

    def TRx_ModemVol_Disable(self):
        data = 0x00000c00
        self.spi_w_byte(0x1E2C, data)

    def TRx_ModemEvt_Enable(self, EvtMask: int):
        data = EvtMask | 0x4
        self.spi_w_byte(0xA080, data)

    def TRx_GetModemEvt_Time(self):
        EVTTime = self.spi_r_byte(0xA098)
        return EVTTime

    def TRx_XtalCap40M_Config(self, Cap_I: int, Cap_O: int):
        if Cap_I > 127 or Cap_O > 127:
            return False
        data = self.spi_r_byte(0x1580)
        # Cap_I = 51
        # Cap_O = 112
        data = (data & 0xFFC000FF) | (Cap_I << 8) | (Cap_O << 15)
        # print(hex(data))
        # data = 0x00783328
        self.spi_w_byte(0x1580, data)

    def TRx_MAC_IOMode_Ready(self):
        Cnt = 0
        condition = True
        while condition:
            data = self.spi_r_byte(0xA048)
            Cnt += 1
            if not data and Cnt < self.TimerOutCnt:
                condition = True
            else:
                condition = False
        if Cnt >= self.TimerOutCnt:
            print('TRx_RxOff Timeout')

    def TRx_MAC_IOMode_Enable(self):
        data = 0x3
        self.spi_w_byte(0xA04C, data)

    def TRx_MAC_IOMode_Disable(self):
        data = 0
        self.spi_w_byte(0xA04C, data)

    def TRx_GPIO0_Config(self, GIOModeEn: bool, GIOMode: int):
        if GIOModeEn:
            data = 0x160100 | GIOMode
        else:
            data = 0x160000
        self.spi_w_byte(0x4000, data)

    def TRx_GPIO1_Config(self, GIOModeEn: bool, GIOMode: int):
        if GIOModeEn:
            data = 0x160100 | GIOMode
        else:
            data = 0x160000
        self.spi_w_byte(0x4004, data)

    def TRx_GPIOINT_Config(self, GIOModeEn: bool, GIOMode: int):
        if GIOModeEn:
            data = 0x160100 | GIOMode
        else:
            data = 0x160000
        self.spi_w_byte(0x4008, data)

    def WaitIRQn(self, wait_t: float):
        s = time.time()
        result = True
        gpio_r = gpio.read()
        data = (gpio_r >> 6) & 1  # for Tigard
        while data == 1:
            gpio_r = gpio.read()
            data = (gpio_r >> 6) & 1  # for Tigard
            if (time.time() - s) > wait_t:
                result = False
                print('Time_Out_IRQn......')
                break
        # print('IRQ: %s' % str(result))
        return result

    def read_gpio(self, par: int):
        gpio_r = gpio.read()
        # gpio_r = gpio.gpio_pins()
        data = (gpio_r >> par) & 1  # for Tigard
        # print(bin(gpio_r))
        return data

    def Packet10BytesData(self):
        global spi_tx_buff
        a = ER4100_config_V405.ER4100_config()
        data = a.Packet_10bytes
        for i in range(0, len(data)):
            spi_tx_buff[i] = data[i]
        return len(data) * 4

    def Packet10BytesDataNoPreSWTRAN(self):
        global spi_tx_buff
        a = ER4100_config_V405.ER4100_config()
        data = a.Packet_10bytesNoPreSWTRAN
        for i in range(0, len(data)):
            spi_tx_buff[i] = data[i]
        return len(data) * 4

    def Packet10BytesDataNoPreSW802(self):
        global spi_tx_buff
        a = ER4100_config_V405.ER4100_config()
        data = a.Packet_10bytesNoPreSW802
        for i in range(0, len(data)):
            spi_tx_buff[i] = data[i]
        return len(data) * 4

    def trx_repeat_mode(self):
        a = ER4100_config_V405.ER4100_config
        for reg_v, reg_a in a.tx_repeat_mode_v1.items():
            self.spi_w_byte(reg_v, reg_a)

    def _pn_singletone_setting_enable(self):
        data = 0
        temp = 0

        self.TRx_DCDC_LCurrent()
        self.spi_w_byte(0x1488, data)
        data = 0x00109200
        self.spi_w_byte(0x1484, data)
        data = 0x0060E004 | (0x7F << 6)
        self.spi_w_byte(0x1480, data)

        data = self.spi_r_byte(0x13AC)
        data &= (~(1 << 21))
        self.spi_w_byte(0x13AC, data)
        data = 0x00000100
        self.spi_w_byte(0x1E00, data)
        data = 0x3
        self.spi_w_byte(0x0000, data)

        data = 0
        self.spi_w_byte(0x1E3C, data)
        self.spi_w_byte(0x1E30, data)
        self.spi_w_byte(0x1e60, data)

        data = self.spi_r_byte(0x1994)
        temp = ((data & 0x3FFFF) << 5) | ((data & 0x7C0000) >> 18)
        self.spi_w_byte(0x138C, temp)

        data = self.spi_r_byte(0x1380)  # -----------!!!!!!!!!!!!!!!!!!!-------------------
        data = (data & 0xFFFFFFFE) | 0x1000000
        self.spi_w_byte(0x1380, data)

    def _pn_singletone_setting_disable(self):
        data = self.spi_r_byte(0x1380)
        data = (data & 0xFEFFFFFF) | (1)
        self.spi_w_byte(0x1380, data)
        data = self.spi_r_byte(0x13AC)
        data |= (1<<21)
        self.spi_w_byte(0x13AC, data)
        data = 0x00010101
        self.spi_w_byte(0x1E00, data)
        data = 0x0000000d
        self.spi_w_byte(0x1E3C, data)
        data = 0xFDFFFF3F
        self.spi_w_byte(0x1E30, data)
        data = 0x00000007
        self.spi_w_byte(0x0000, data)
        data = 0x0012d200
        self.spi_w_byte(0x1484, data)

    def _pn_cal_opt_val(self):
        global vtune_adc_hb, vtune_adc_lb, vtune_adc_target, vcoldo_lv_opt, kvc_opt, vtune_adc_opt, vtune_adc, vcoldo_lv

        ori_val = 0
        abs_target_m_adc = 0
        abs_target_m_adc_opt = 0

        ori_val = self.spi_r_byte(0x1084)
        vtune_adc = ori_val & 0x00000FFF

        abs_target_m_adc = (vtune_adc_target - (vtune_adc))
        if abs_target_m_adc < 0:
            abs_target_m_adc = abs_target_m_adc*(-1)
        abs_target_m_adc_opt = (vtune_adc_target - (vtune_adc_opt))
        if abs_target_m_adc_opt < 0:
            abs_target_m_adc_opt = abs_target_m_adc_opt*(-1)

        if (abs_target_m_adc < abs_target_m_adc_opt) and (vtune_adc >= vtune_adc_lb)\
                and (vtune_adc <= vtune_adc_hb):
            vtune_adc_opt = vtune_adc
            vcoldo_lv_opt = vcoldo_lv

            ori_val = self.spi_r_byte(0x1394)
            kvc_opt = (ori_val & 0x0000007E) >> 1

            print('vcoldo_lv_opt : %d' % vcoldo_lv_opt)
            print('vtune_adc_opt : %d' % vtune_adc_opt)
            print('kvc_opt : %d' % kvc_opt)

    def pn_opt_init(self):
        global vcoldo_lv, vcoldo_lv_opt, kvc_opt, vtune_adc_opt
        ori_val = 0
        mod_val = 0
        cnt = 0
        rst = self.TRx_STATUS_SUCESS

        ori_val = self.spi_r_byte(0x1380)
        mod_val = ((ori_val) & 0xFFFFE7FF)
        self.spi_w_byte(0x1380, mod_val)

        ori_val = self.spi_r_byte(0x1388)
        mod_val = ((ori_val) & 0xFFFFFFFD) | 0x02
        self.spi_w_byte(0x1388, mod_val)

        ori_val = self.spi_r_byte(0x1388)
        mod_val = ((ori_val) & 0xFFFFFBFF)
        self.spi_w_byte(0x1388, mod_val)

        ori_val = self.spi_r_byte(0x1388)
        mod_val = ((ori_val) & 0xFFFFFBFF) | (0x400)
        self.spi_w_byte(0x1388, mod_val)

        data = 0
        while data != 1:
            ori_val = self.spi_r_byte(0x1394)
            data = ori_val & 0x1
            cnt += 1
            if cnt >= self.TimerOutCnt:
                rst = self.TRx_STATUS_FAIL
                return rst
        vcoldo_lv = 0
        vcoldo_lv_opt = 0
        kvc_opt = 0
        vtune_adc_opt = 0

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFF7FFFFF)
        self.spi_w_byte(0x1080, mod_val)

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFFFFFFF0) | 0x0F
        self.spi_w_byte(0x1080, mod_val)

        ori_val = self.spi_r_byte(0x1384)
        mod_val = ((ori_val) & 0xFFFFF7FF) | 0x800
        self.spi_w_byte(0x1384, mod_val)

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFFFFBFFF) | 0x4000
        self.spi_w_byte(0x1080, mod_val)

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFFFFF7FF)
        self.spi_w_byte(0x1080, mod_val)

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFFFF7FFF)
        self.spi_w_byte(0x1080, mod_val)

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFFFFEFFF)
        self.spi_w_byte(0x1080, mod_val)

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFFFFF7FF) | 0x800
        self.spi_w_byte(0x1080, mod_val)

        ori_val = self.spi_r_byte(0x1384)
        mod_val = ((ori_val) & 0xEFFFFFFF)
        self.spi_w_byte(0x1384, mod_val)

        return rst

    def pn_get_kvc_init(self):
        global vcoldo_lv
        global init_kvc
        ori_val = 0
        mod_val = 0
        cnt = 0
        rst = self.TRx_STATUS_SUCESS
        # print('pn_get_kvc_init_VCOLDO : %d' % vcoldo_lv)
        ori_val = self.spi_r_byte(0x1380)
        mod_val = ((ori_val) & 0xFFFFE7FF) | (vcoldo_lv << 11)
        self.spi_w_byte(0x1380, mod_val)

        ori_val = self.spi_r_byte(0x1388)
        mod_val = ((ori_val) & 0xFFFFFBFF)
        self.spi_w_byte(0x1388, mod_val)

        ori_val = self.spi_r_byte(0x1388)
        mod_val = ((ori_val) & 0xFFFFFBFF) | 0x400
        self.spi_w_byte(0x1388, mod_val)

        data = 0
        while data != 1:
            ori_val = self.spi_r_byte(0x1394)
            data = ori_val & 0x1
            cnt += 1
            if cnt >= self.TimerOutCnt:
                rst = self.TRx_STATUS_FAIL
                return rst

        ori_val = self.spi_r_byte(0x1394)
        init_kvc = (ori_val & 0x0000007E) >> 1

        self._pn_cal_opt_val()

        return rst

    def pn_get_kvc_hi(self):
        global init_kvc
        ori_val = 0
        mod_val = 0
        cnt = 0
        rst = self.TRx_STATUS_SUCESS

        if init_kvc <= 62:
            ori_val = self.spi_r_byte(0x1384)
            mod_val = ((ori_val) & 0xFFF81FFF) | ((init_kvc+1 << 13) & 0x0007E000)
            self.spi_w_byte(0x1384, mod_val)

            ori_val = self.spi_r_byte(0x1388)
            mod_val = ((ori_val) & 0xFFFFFBFF)
            self.spi_w_byte(0x1388, mod_val)

            ori_val = self.spi_r_byte(0x1388)
            mod_val = ((ori_val) & 0xFFFFFBFF) | 0x400
            self.spi_w_byte(0x1388, mod_val)

            data = 0
            while data != 1:
                ori_val = self.spi_r_byte(0x1394)
                data = ori_val & 0x1
                cnt += 1
                if cnt >= self.TimerOutCnt:
                    rst = self.TRx_STATUS_FAIL
                    return rst

            self._pn_cal_opt_val()

        return rst

    def pn_get_kvc_lo(self):
        global init_kvc
        ori_val = 0
        mod_val = 0
        cnt = 0
        rst = self.TRx_STATUS_SUCESS

        if init_kvc >= 1:
            ori_val = self.spi_r_byte(0x1384)
            mod_val = ((ori_val) & 0xFFF81FFF) | ((init_kvc-1) << 13)
            self.spi_w_byte(0x1384, mod_val)

            ori_val = self.spi_r_byte(0x1388)
            mod_val = ((ori_val) & 0xFFFFFBFF)
            self.spi_w_byte(0x1388, mod_val)

            ori_val = self.spi_r_byte(0x1388)
            mod_val = ((ori_val) & 0xFFFFFBFF) | 0x400
            self.spi_w_byte(0x1388, mod_val)

            data = 0
            while data != 1:
                ori_val = self.spi_r_byte(0x1394)
                data = ori_val & 0x1
                cnt += 1
                if cnt >= self.TimerOutCnt:
                    rst = self.TRx_STATUS_FAIL
                    return rst

            self._pn_cal_opt_val()

        return rst

    def pn_set_best_kvc(self):
        global vcoldo_lv_opt
        global kvc_opt
        ori_val = 0
        mod_val = 0
        cnt = 0
        rst = self.TRx_STATUS_SUCESS

        ori_val = self.spi_r_byte(0x1380)
        mod_val = ((ori_val) & 0xFFFFE7FF) | (vcoldo_lv_opt << 11)
        self.spi_w_byte(0x1380, mod_val)

        ori_val = self.spi_r_byte(0x1384)
        mod_val = ((ori_val) & 0xEFF81FFF) | (kvc_opt << 13)
        self.spi_w_byte(0x1384, mod_val)

        ori_val = self.spi_r_byte(0x1388)
        mod_val = ((ori_val) & 0xFFFFFBFF)
        self.spi_w_byte(0x1388, mod_val)

        ori_val = self.spi_r_byte(0x1388)
        mod_val = ((ori_val) & 0xFFFFFBFF) | 0x400
        self.spi_w_byte(0x1388, mod_val)

        data = 0
        while data != 1:
            ori_val = self.spi_r_byte(0x1394)
            data = ori_val & 0x1
            cnt += 1
            if cnt >= self.TimerOutCnt:
                rst = self.TRx_STATUS_FAIL
                print('vcoldo_lv_opt : %d' % vcoldo_lv_opt)
                print('vtune_adc_opt : %d' % vtune_adc_opt)
                return rst

        ori_val = self.spi_r_byte(0x1384)
        mod_val = ((ori_val) & 0xFFFFF7FF)
        self.spi_w_byte(0x1384, mod_val)

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFFFFEFFF) | 0x1000
        self.spi_w_byte(0x1080, mod_val)

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFFFFFFF0) | 13
        self.spi_w_byte(0x1080, mod_val)

        ori_val = self.spi_r_byte(0x1080)
        mod_val = ((ori_val) & 0xFF7FFFFF) | 0x800000
        self.spi_w_byte(0x1080, mod_val)

        return rst

    def TRx_phase_noise_opt(self):
        global div_int, div_frac, lo_band_sel, lfvco_en, vtune_adc_hb, vtune_adc_lb, vtune_adc_target, init_kvc,\
            kvc_opt, vcoldo_lv, vcoldo_lv_opt, vtune_adc, vtune_adc_opt
        total_vcoldo_lv = 3
        rst = self.TRx_STATUS_SUCESS
        div_int = 0
        div_frac = 0
        lo_band_sel = 0
        lfvco_en = 0
        vtune_adc_hb = 0
        vtune_adc_lb = 0
        vtune_adc_target = 0
        init_kvc = 0
        kvc_opt = 0
        vcoldo_lv = 0
        vcoldo_lv_opt = 0
        vtune_adc = 0
        vtune_adc_opt = 0
        s = time.time()

        self._pn_singletone_setting_enable()

        data = self.spi_r_byte(0x1994)
        div_int = ((data) & (0x007C0000)) >> 18
        # print('div_int : %d' %div_int)
        data = self.spi_r_byte(0x1994)
        div_frac = ((data) & (0x0003FFFF))
        # print('div_frac : %d' % div_frac)
        data = self.spi_r_byte(0x1380)
        lo_band_sel = ((data) & (0x000E0000)) >> 17
        # print('lo_band_sel : %d' % lo_band_sel)
        data = self.spi_r_byte(0x1380)
        lfvco_en = ((data) & (0x00100000)) >> 20
        # print('lfvco_en : %d' % lfvco_en)
        vtune_adc_hb = 4095
        vtune_adc_lb = 0
        vtune_adc_target = 2100

        if self.pn_opt_init() == self.TRx_STATUS_SUCESS:
            # print('pn_opt_init pass')
            for i in range(total_vcoldo_lv):
                vcoldo_lv = i
                print('vcoldo_lv : %d' % vcoldo_lv)
                if self.pn_get_kvc_init():
                    rst = self.TRx_STATUS_FAIL
                    print('pn_get_kvc_init fail')

                if self.pn_get_kvc_hi():
                    rst = self.TRx_STATUS_FAIL
                    print('pn_get_kvc_hi fail')

                if self.pn_get_kvc_lo():
                    rst = self.TRx_STATUS_FAIL
                    print('pn_get_kvc_lo fail')

            if self.pn_set_best_kvc():
                rst = self.TRx_STATUS_FAIL
                print('pn_set_best_kvc fail')
            else:
                print('pn_set_best_kvc pass')
        else:
            rst = self.TRx_STATUS_FAIL

        print('PN setup took {} secs'.format(round(time.time() - s, 3)))

        print('vcoldo_lv_opt : %d' % vcoldo_lv_opt)
        print('vtune_adc_opt : %d' % vtune_adc_opt)
        print('kvc_opt : %d' % kvc_opt)

        self._pn_singletone_setting_disable()

        return vcoldo_lv_opt, vtune_adc_opt

    def _bbp_switch_agc_mode(self, agc_mode: int):
        data = self.spi_r_byte(0xb248)
        data = (data & 0xFFFFFFFC) | agc_mode
        self.spi_w_byte(0xb248, data)

    def _ana_set_gain_src(self, gain_src: int):
        data = self.spi_r_byte(0x1e3c)
        data = (data & 0xFFFFFFFE) | gain_src
        self.spi_w_byte(0x1e3c, data)

    def _ana_adc_out_switch(self, on_off: int):
        data = self.spi_r_byte(0x1180)
        data &= 0xFFFDFFFF

        if on_off:
            data |= 0x20000
        self.spi_w_byte(0x1180, data)

    def _mac_clock_gatting_switch(self, on_off: int):
        if on_off:
            data = 0x7
        else:
            data = 0x0
        self.spi_w_byte(0x0000, data)

    def TRx_if_cal(self):
        u32max_rssi = 0
        u32new_rssi = 0
        best_bpf_cf = 0xe

        # disable clock gatting
        self._mac_clock_gatting_switch(0)

        # siwtch BBP to debug mode
        data = self.spi_r_byte(0xb200)
        data = ((data & 0x1f) << 16)
        self.spi_w_byte(0xb248, data)
        self._bbp_switch_agc_mode(self.AGC_MODE_DEBUG)

        # switch gain source
        self._ana_set_gain_src(0)

        # set IRC_ADPLL (gen a tone to do cal)
        data = self.spi_r_byte(0x1980)
        self.spi_w_byte(0x12ac, data)
        data = 1
        self.spi_w_byte(0x12b0, data)

        # enable & disable rx cca mode
        data = 0x201
        dl = [0 for _ in range(2)]
        dl[1] = data & 0xff
        dl[0] = (data >> 8) & 0xff
        write_buf = [self.COMM_RXTrigger, dl[1], dl[0], 0x00, 0x00]
        spi.exchange(write_buf)
        time.sleep(0.00001)
        self.TRx_RxOff()

        # copy freq tpmcal setting
        data = self.spi_r_byte(0x19a0)
        self.spi_w_byte(0x139c, data)

        # switch to manual mode
        data = 0
        self.spi_w_byte(0x1e00, data)
        self.spi_w_byte(0x1e30, data)

        # [0] = 0 CCKGEN_PD power down cck gen
        data = 0
        self.spi_w_byte(0x1404, data)

        # config CTQBPSD
        data = 0x70E448
        self.spi_w_byte(0x1180, data)

        # [0] = 0 FS_FREQSYNTH_PD & [25] = 1 FS_RX_EN
        data = self.spi_r_byte(0x1380)
        data = (data & 0xfffffffe) | 0x2000000
        self.spi_w_byte(0x1380, data)

        # [18] = 0 IRC_ADPLL_PD
        data = self.spi_r_byte(0x12a4)
        data &= 0xfffbffff
        self.spi_w_byte(0x12a4, data)

        # config RXATOP RX POWSET for cal
        data = 0x004801ab
        self.spi_w_byte(0x1600, data)

        data = 0x028ff87c  # hard code
        self.spi_w_byte(0x1604, data)

        data = 0x440E05E0
        self.spi_w_byte(0x1620, data)

        # anactl disable rx write back
        data = 1
        self.spi_w_byte(0x1e70, data)

        for i in range(MAX_BANDPASS_FILTER_FREQ):

            # [16:13] RXATOP set RX_LOPHASE = 0 - 15
            data = self.spi_r_byte(0x1604)
            data = (data & 0x7FFFFFF) | (i << 27)
            self.spi_w_byte(0x1604, data)

            # get rssi val and check the minimize
            u32new_rssi, gg_rssi = self.TRx_GetRSSI_CCA()
            if u32max_rssi <= u32new_rssi:
                u32max_rssi = u32new_rssi
                best_bpf_cf = i

        # figure out min rssi value, and then set into RXATOP
        data = self.spi_r_byte(0x1604)
        data = (data & 0x7FFFFFF) | (best_bpf_cf << 27)
        self.spi_w_byte(0x1604, data)

        # [9] = 1 CTQBPSD_PD config ADC PD = 1
        data = 0x70E730
        self.spi_w_byte(0x1180, data)

        # [25] = 0 FS_RX_EN && [0] = 1 FS_FREQSYNTH_PD
        data = self.spi_r_byte(0x1380)
        data = (data & 0xfdffffff) | 1
        self.spi_w_byte(0x1380, data)

        # [28] = 1 IRC_ADPLL_PD
        data = self.spi_r_byte(0x12a4)
        data |= 0x00040000
        self.spi_w_byte(0x12a4, data)

        # [0] = 1 CCKGEN_PD power down cck gen
        data = 1
        self.spi_w_byte(0x1404, data)

        # [1:0] = 0x1 AGC_MODE = all dig
        data = 1
        self.spi_w_byte(0xb248, data)

        # [21:19] = 2 && [22] = 0 RXATOP reset default
        data = self.spi_r_byte(0x1600)
        data = 0x00100000
        self.spi_w_byte(0x1600, data)

        # [31:0] = 0 ANCTL_WB_REG0 : enable rx write back
        data = 0
        self.spi_w_byte(0x1e70, data)

        # switch to auto mode
        # [0][8][16] = 1 config ana ctrl : enable ana ctrl
        data = 0x00010101
        self.spi_w_byte(0x1e00, data)

        # ANCTL_CONFIG_7_5 ANACTL  reset to default
        data = 0xfdffff3f
        self.spi_w_byte(0x1e30, data)

        # switch gain source to bbp
        self._ana_set_gain_src(1)

        # enable clock gatting
        self._mac_clock_gatting_switch(1)

    def TRx_load_Tool_CSR(self):
        a = ER4100_config_V405.ER4100_config()
        for reg_a, reg_v in a.Load_Tool_CSR_array.items():
            self.spi_w_byte(reg_a, reg_v)

    def TRx_NFC_Enable(self):
        data = 1
        self.spi_w_byte(0x0030, data)
        data = 0
        self.spi_w_byte(0x0030, data)

    def TRx_NFC_Disable(self):
        data = 3
        self.spi_w_byte(0x0030, data)

    def TRx_NFC_Write(self, Addr: int, txpbuff: list, DataLen_word: int):
        if Addr+(DataLen_word << 2) > 0x400:
            sys.exit('TRx_NFC_Write Fail')
        else:
            for i in range(DataLen_word):
                if Addr+(i << 2) == 0x3FC:
                    data = self.spi_r_byte(0x93FC)
                    data = (data & 0xffffff00) | (txpbuff[i] & 0xff)
                    self.spi_w_byte(0x9000+Addr+(i << 2), data)
                else:
                    self.spi_w_byte(0x9000+Addr+(i << 2), txpbuff[i])

    def TRx_NFC_Read(self, Addr: int, rxpbuff: list, DataLen_word: int):
        if Addr+(DataLen_word << 2) > 0x400:
            sys.exit('TRx_NFC_Write Fail')
        else:
            for i in range(DataLen_word):
                rxpbuff[i] = self.spi_r_byte(0x9000+Addr+(i<<2))
                if Addr+(i<<2) == 0x3FC:
                    rxpbuff[i] &= 0xff
            return rxpbuff

    def TRx_NFC_GetConfig(self):
        data = self.spi_r_byte(0x93FC)
        TRxData = (data & 0xFF0000) >> 16

        data = self.spi_r_byte(0x93FC)
        data |= 0x08000000
        self.spi_w_byte(0x93FC, data)
        data = self.spi_r_byte(0x93FC)
        data &= 0xF700FFFF
        self.spi_w_byte(0x93FC, data)
        return TRxData

    def TRx_NFC_SetConfig(self, NFC_Config: int):
        data = 0
        for i in range(0, 8):
            if NFC_Config & (1 << i):
                data = self.spi_r_byte(0x93FC)
                data = (data & 0xF8FFFFFF) | (i << 24)
                self.spi_w_byte(0x93FC, data)

    def TRx_NFC_GetImpedance(self):
        data = self.spi_r_byte(0x1700)
        # if data :
        #     sys.exit('TRx_NFC_GetImpedance Fail')
        TRxData = (data >> 10) & 0x7
        return TRxData

    def TRx_NFC_SetImpedance(self, IMP_0_7: int):
        if IMP_0_7 > 7:
            sys.exit('TRx_NFC_SetImpedance Fail')
        data = self.spi_r_byte(0x1700)
        data = ((data & 0xFFFFE3FF) | (IMP_0_7 << 10))
        self.spi_w_byte(0x1700, data)

    def check_TRx_ID(self):
        data = self.spi_r_byte(0xF100)
        print("Read PRODUCT ID is 0x%x" % data)
        if data == 0x90000005:
            print("Read PRODUCT ID pass")
        else:
            print("Read PRODUCT ID fail")
            # sys.exit("Read PRODUCT ID fail")
        # data = self.spi_r_byte(0x1E5C)
        # print("0x1E5C is 0x%x" % data)

    def main_singletone_mode(self, gain: int, XOCAL: int, Config23dBm: bool, PN_OPT_EN: bool):
        vcoldo_result = 0
        vtune_adc_result = 0
        # self.check_TRx_ID()
        self.TRx_Init()
        self.TRx_Config()
        # app.TRx_GPIO0_Config(True, 0)
        if PN_OPT_EN:
            vcoldo_result, vtune_adc_result = self.TRx_phase_noise_opt()
        self.TRx_SingleTone_Enable(gain, Config23dBm)
        self.TRx_SingleTone_Trigger()
        self.TRx_XtalCap40M_Config(XOCAL, XOCAL)
        return vcoldo_result, vtune_adc_result

    def main_tx_init(self, gain: int, tx_type: int, matching: int, PN_OPT_EN: bool):
        self.check_TRx_ID()
        self.TRx_Init()
        self.TRx_Config()
        if PN_OPT_EN:
            self.TRx_phase_noise_opt()
        self.TRx_EnableInt(self.INT_ST_RX | self.INT_ST_RXERR | self.INT_ST_TX | self.INT_ST_TXERR |
                           self.INT_ST_TMRTHD | self.INT_ST_WAKEUP)
        self.TRx_TX_PacketOffLoad(0xAA, 4, 0x5A0FBE66, tx_type)
        self.TRx_RX_SyncWord(0x5A0FBE66)
        if matching == 23:
            if 902 * 1E6 <= self.freq <= 928 * 1E6:
                self.TRx_PAH915MHz23dBm_FSK(gain)
            elif 470 * 1E6 <= self.freq <= 510 * 1E6:
                self.TRx_PAH490MHz23dBm_FSK(gain)
            elif 433 * 1E6 <= self.freq <= 434 * 1E6:
                self.TRx_PAH433MHz23dBm_FSK(gain)
            else:
                # self.TRx_PAH915MHz23dBm_FSK(gain)
                sys.exit("Set PA Gain Fail, Please check Frequency & matching")
        elif matching == 20:
            if 902 * 1E6 <= self.freq <= 928 * 1E6:
                self.TRx_PAH915MHz20dBm_FSK(gain)
            elif 470 * 1E6 <= self.freq <= 510 * 1E6:
                self.TRx_PAH490MHz20dBm_FSK(gain)
            elif 433 * 1E6 <= self.freq <= 434 * 1E6:
                self.TRx_PAH490MHz20dBm_FSK(gain)
            else:
                sys.exit("Set PA Gain Fail, Please check Frequency & matching")
        elif matching == 16:
            if 868 * 1E6 <= self.freq <= 868.6 * 1E6:
                self.TRx_PAH868MHz16dBm_FSK(gain)
            else:
                sys.exit("Set PA Gain Fail, Please check Frequency & matching")
        elif matching == 14:
            if 868 * 1E6 <= self.freq <= 868.6 * 1E6:
                self.TRx_PAL868MHz14dBm_FSK(gain)
            else:
                sys.exit("Set PA Gain Fail, Please check Frequency & matching")
        elif matching == 10:
            if 902 * 1E6 <= self.freq <= 928 * 1E6:
                self.TRx_PAL915MHz10dBm_FSK(gain)
            elif 868 * 1E6 <= self.freq <= 868.6 * 1E6:
                self.TRx_PAL868MHz10dBm_FSK(gain)
            elif 470 * 1E6 <= self.freq <= 510 * 1E6:
                self.TRx_PAL490MHz10dBm_FSK(gain)
            elif 433 * 1E6 <= self.freq <= 434 * 1E6:
                self.TRx_PAL433MHz10dBm_FSK(gain)
            else:
                sys.exit("Set PA Gain Fail, Please check Frequency & matching")

    def main_tx_go(self, tx_type: int):
        if tx_type == self.TX_TRANSPARENT:
            datalength = self.Packet10BytesData()
        elif tx_type == self.TX_TRANSPARENTwithPRESYNC:
            datalength = self.Packet10BytesDataNoPreSWTRAN()
        else:
            datalength = self.Packet10BytesDataNoPreSW802()
        self.TRx_TX_FIFO(datalength, spi_tx_buff)
        self.TRx_TX_Trigger(datalength, False)
        self.WaitIRQn(0.5)
        result, data = self.TRx_GetIntStatus()
        self.TRx_ClearIntFlag(result)
        # print(bin(result))
        return result

    def main_IQ_calib(self):
        self.TRx_Init()
        self.TRx_Config()

        data = self.spi_r_byte(0x1604)

        # # Rx_LNA_GAIN
        # data = (data & 0xFFFFFFFC) | 0x3
        # self.spi_w_byte(0x1604, data)
        #
        # # Rx_MIX_GAIN
        # data = (data & 0xFFFFFFF3) | (0x0 << 2)
        # self.spi_w_byte(0x1604, data)

        # # Rx_FILTGAIN
        # data = (data & 0xFFFFFF8F) | (0x7 << 4)
        # self.spi_w_byte(0x1604, data)
        #
        # # Rx_PGAGAIN
        # data = (data & 0xFFFFF87F) | (0x9 << 7)
        # self.spi_w_byte(0x1604, data)

        Rx_LNA_GAIN = data & 0x3
        Rx_MIX_GAIN = (data & 0xC) >> 2
        Rx_FILTGAIN = (data & 0x70) >> 4
        Rx_PGAGAIN = (data & 0x780) >> 7

        print('0x1604 = %s' % hex(data))

        print('Rx_LNA_GAIN = %s' % hex(Rx_LNA_GAIN))
        print('Rx_MIX_GAIN = %s' % hex(Rx_MIX_GAIN))
        print('Rx_FILTGAIN = %s' % hex(Rx_FILTGAIN))
        print('Rx_PGAGAIN = %s' % hex(Rx_PGAGAIN))

        data = self.spi_r_byte(0x1620)

        # # Rx_Calibcursel
        # data = (data & 0xFFFFFFF8) | (0x0)
        # self.spi_w_byte(0x1620, data)

        Rx_Calibcursel = data & 0x7
        print('Rx_Calibcursel = %s' % hex(Rx_Calibcursel))

        print('0x1620 = %s' % hex(data))

        IQ_data = self.TRx_IQCal_Enable()
        print('IQ Calibration Value : %d' % IQ_data)

        return IQ_data

    def main_Rx_init(self, XOCAL: int, PN_OPT_EN: bool, IQ_EN: bool, IQ_data: int):
        r = RegAccess()
        self.TRx_Init()
        self.TRx_Config()

        if IQ_EN:
            self.TRx_IQCal_fixed(IQ_data)

        data = self.spi_r_byte(0x1604)
        print('0x1604 : %s' % hex(data))

        if PN_OPT_EN:
            self.TRx_phase_noise_opt()
        self.TRx_EnableInt(self.INT_ST_RX | self.INT_ST_RXERR | self.INT_ST_TX | self.INT_ST_TXERR |
                           self.INT_ST_TMRTHD | self.INT_ST_WAKEUP)
        self.TRx_RX_SyncWord(0x5A0FBE66)  # 0x5A0FBE66  0x41884B08
        self.TRx_XtalCap40M_Config(XOCAL, XOCAL)

    def main_Rx_go(self):
        r = RegAccess()
        global packetRx
        global packetRxError
        rx_result = False
        timeout_result = True

        # self.spi_w_field(r.BBP_AGC_MODE_AGC_MODE_ADDR, r.BBP_AGC_MODE_AGC_MODE_MASK, 3)
        data = self.spi_r_field(r.BBP_AGC_MODE_AGC_MODE_ADDR, r.BBP_AGC_MODE_AGC_MODE_MASK)
        print('AGC_MODE : %d' % data)

        S_AGC_IDX = self.spi_r_field(r.BBP_AGC_MODE_AGC_IDX_CUR_DEBUG_ADDR, r.BBP_AGC_MODE_AGC_IDX_CUR_DEBUG_MASK)
        print('Start AGC_IDX : %d' % S_AGC_IDX)

        self.TRx_RX_Trigger(RxLength, True)
        timeout = self.WaitIRQn(3600)
        result, data = self.TRx_GetIntStatus()
        self.TRx_ClearIntFlag(result)

        RSSI_raw, RSSI_dBm = self.TRx_GetRSSI_Data()
        rssi_result = RSSI_dBm * (-1)

        freq_offset_sw = self.spi_r_field(r.BBP_GFO_EST_INT_GFO_ESTSW_ADDR, r.BBP_GFO_EST_INT_GFO_ESTSW_MASK)
        freq_offset_pl = self.spi_r_field(r.BBP_GFO_EST_INT_GFO_ESTPL_ADDR, r.BBP_GFO_EST_INT_GFO_ESTPL_MASK)

        A_AGC_IDX = self.spi_r_field(r.BBP_AGC_MODE_AGC_IDX_CUR_DEBUG_ADDR, r.BBP_AGC_MODE_AGC_IDX_CUR_DEBUG_MASK)

        # print('freq_offset_sw : %d' % freq_offset_sw)
        # print('freq_offset_pl : %d' % freq_offset_pl)
        print('Afer Rx trigger AGC_INDEX : %d' % A_AGC_IDX)

        if freq_offset_sw & 0x800:
            cfo_sw = -(0xFFF - (freq_offset_sw - 1)) * self.dr/2 / 1024
        else:
            cfo_sw = (self.dr/2) * freq_offset_sw / 1024

        print('CFO_sw : %.2f Hz' % cfo_sw)

        if freq_offset_pl & 0x800:
            cfo_pl = -(0xFFF - (freq_offset_pl - 1)) * self.dr/2 / 1024
        else:
            cfo_pl = (self.dr/2) * freq_offset_pl / 1024

        # print('CFO_pl : %.2f Hz' % cfo_pl)

        if timeout:
            timeout_result = False

        if packetRxError == 1:
            rx_result = False
        elif packetRx == 1:
            self.TRx_RX_FIFO()
            # print('XT9000 pass')
            rx_result = True

        return rx_result, rssi_result, timeout_result, cfo_sw, cfo_pl, S_AGC_IDX, A_AGC_IDX

    def main_Rx_go_door(self):
        global packetRx
        global packetRxError
        rx_result = False
        # self.TRx_RX_Trigger(RxLength, False)
        self.WaitIRQn(0.5)
        result, data = self.TRx_GetIntStatus()
        self.TRx_ClearIntFlag(result)

        if packetRxError == 1:
            rx_result = False
        elif packetRx == 1:
            data = self.TRx_RX_FIFO()
            # print('XT9000 pass')
            if data == '0xc2856494405e':
                rx_result = True
            else:
                rx_result = False
        return rx_result

    def check_rx(self):
        global packetRx
        global packetRxError
        time_out = False
        rx_result = False
        data = self.WaitIRQn(0.2)
        if not data:
            result = 0
            time_out = True
            return result, time_out

        result, data = self.TRx_GetIntStatus()
        self.TRx_ClearIntFlag(result)

        if packetRxError == 1:
            self.TRx_RX_FIFOReset()
            rx_result = False
        elif packetRx == 1:
            # self.TRx_RX_FIFO()  # RK
            # print('XT9000 pass')
            rx_result = True

        return rx_result, time_out

    def main_PRBS9_mode(self, gain: int, Config23dBm: bool, XOCAL: int, PN_OPT_EN: bool):
        # self.check_TRx_ID()
        self.TRx_Init()
        self.TRx_Config()
        if PN_OPT_EN:
            self.TRx_phase_noise_opt()
        self.TRx_SingleTone_Enable(gain, Config23dBm)
        self.TRx_PRBS9_Trigger()
        self.TRx_XtalCap40M_Config(XOCAL, XOCAL)

    def main_idle_setup(self):
        self.TRx_Init()
        self.TRx_Config()

    def main_sleep_setup(self):
        self.TRx_Init()
        self.TRx_Config()
        self.TRx_EnableInt(self.INT_ST_RX | self.INT_ST_RXERR | self.INT_ST_TX | self.INT_ST_TXERR |
                           self.INT_ST_TMRTHD | self.INT_ST_WAKEUP)
        self.TRx_PCRMU_Enable()
        self.TRx_PowerDownMode()

    def main_shutdown_setup(self):
        self.TRx_Init()
        self.TRx_Config()
        self.TRx_EnableInt(self.INT_ST_RX | self.INT_ST_RXERR | self.INT_ST_TX | self.INT_ST_TXERR |
                           self.INT_ST_TMRTHD | self.INT_ST_WAKEUP)

        self.TRx_PCRMU_Enable()
        self.TRx_PCRMU_ShutDown_Enable()

    def main_wakeup_test(self):
        self.TRx_Init()
        self.TRx_Config()
        self.TRx_EnableInt(self.INT_ST_RX | self.INT_ST_RXERR | self.INT_ST_TX | self.INT_ST_TXERR |
                           self.INT_ST_TMRTHD | self.INT_ST_WAKEUP)
        self.TRx_PCRMU_Enable()
        wake_count = 0
        id_count = 0
        wake_result = False
        id_result = False
        tt_c = 1
        for i in range(0, tt_c):
            self.TRx_SetWUTMR_Timer(10, False)
            self.TRx_PowerDownMode()
            self.WaitIRQn(5)
            result, data = self.TRx_GetIntStatus()
            self.TRx_ClearIntFlag(result)
            if wakeupEvent == 1:
                wake_count += 1
            data = self.spi_r_byte(0xF100)
            if data == 0x90000005:
                id_count += 1
        if wake_count == tt_c:
            wake_result = True
        if id_count == tt_c:
            id_result = True
        print('wake_result = %s' % wake_result)
        print('id_result = %s' % id_result)
        return wake_result, id_result

    def Tx_modem_voltage(self):
        self.TRx_Init()
        self.TRx_Config()
        self.TRx_EnableInt(self.INT_ST_RX | self.INT_ST_RXERR | self.INT_ST_TX | self.INT_ST_TXERR |
                           self.INT_ST_TMRTHD | self.INT_ST_WAKEUP)
        self.TRx_TX_PacketOffLoad(0xAA, 4, 0x5A0FBE66, self.TX_TRANSPARENT)
        self.TRx_PAH915MHz23dBm_FSK(127)
        while 1:
            self.TRx_ModemVol_Enable()
            datalength = self.Packet10BytesData()
            self.TRx_TX_FIFO(datalength, spi_tx_buff)
            self.TRx_TX_Trigger(datalength, False)
            self.WaitIRQn(0.5)
            result, data = self.TRx_GetIntStatus()
            self.TRx_ClearIntFlag(result)
            vol_result = self.TRx_GetModemVol_Data()
            print('Modem Data : %d' % vol_result)
            self.TRx_ModemVol_Disable()
            time.sleep(0.5)

    def Tx_modem_voltage_calib(self):
        self.TRx_Init()
        self.TRx_Config()

        self.TRx_ModemVol_Enable()
        data_33V = 3908  # self.TRx_GetADC_RawData()
        data_18V = 2073  # self.TRx_GetADC_RawData()

        self.TRx_ADC2PCal_Enable(data_33V, data_18V)
        for i in range(10):
            vol_result = self.TRx_GetModemVol_Data()
            print('Modem Data : %d' % vol_result)
            time.sleep(0.5)
        self.TRx_ModemVol_Disable()

    def vdetect_test(self):
        r = RegAccess()
        data = self.spi_r_field(r.DCDC_CONFIG_1_DCDC_LOW_VDD_FLAG_ADDR, r.DCDC_CONFIG_1_DCDC_LOW_VDD_FLAG_MASK)
        print('vdetect : %d' % data)

        t_data = self.spi_r_byte(r.DCDC_CONFIG_1_DCDC_LOW_VDD_FLAG_ADDR)
        print('vdetect : %s' % bin(t_data))

    def Tx_modem_temp(self):
        for i in range(10):
            data = self.TRx_GetADC_RawData_temp()
            print('RawADC: %d' % data)
            time.sleep(0.5)

    def modem_vdd(self):
        for i in range(10):
            data = self.TRx_GetADC_RawData()
            print('RawADC: %d' % data)
            time.sleep(0.5)

    def CLK_40MHz_check(self):
        r = RegAccess()
        data = self.spi_r_byte(0xE004)
        # print(hex(data))
        self.spi_w_field(r.GPIO_0_CFG_REG_GPIO_0_MODE_ADDR, r.GPIO_0_CFG_REG_GPIO_0_MODE_MASK, 7)
        self.spi_w_field(r.GPIO_0_CFG_REG_GPIO_0_DBG_NUM_ADDR, r.GPIO_0_CFG_REG_GPIO_0_DBG_NUM_MASK, 0)
        self.spi_w_field(0xE000, 0xFF, 5)
        self.spi_w_field(0xE004, 0xFF, 1)

    def dcdc_out(self):
        self.TRx_Init()
        self.TRx_Config()
        self.TRx_EnableInt(self.INT_ST_RX | self.INT_ST_RXERR | self.INT_ST_TX | self.INT_ST_TXERR |
                           self.INT_ST_TMRTHD | self.INT_ST_WAKEUP)
        self.TRx_RX_SyncWord(0x5A0FBE66)  # 0x5A0FBE66  0x41884B08
        self.TRx_RX_Trigger(RxLength, True)

    def calib_32k(self):
        r = RegAccess()
        self.main_idle_setup()
        # time.sleep(0.01)
        data_01 = self.spi_r_field(r.DIG_LF_OSC_CONFIG5_DIG_LF_OSC_COARSECTRL_ROUT_ADDR,
                                   r.DIG_LF_OSC_CONFIG5_DIG_LF_OSC_COARSECTRL_ROUT_MASK)
        data_02 = self.spi_r_field(r.DIG_LF_OSC_CONFIG5_DIG_LF_OSC_FINECTRL_ROUT_ADDR,
                                   r.DIG_LF_OSC_CONFIG5_DIG_LF_OSC_FINECTRL_ROUT_MASK)

        print('COARSECTRL_ROUT : %d' % data_01)
        print('FINECTRL_ROUT : %d' % data_02)

    def main_NFC_Field_Test(self):
        self.TRx_Init()
        # self.TRx_NFC_SetImpedance(6)
        NFC_status = self.TRx_NFC_GetConfig()
        # data = self.TRx_NFC_GetImpedance()
        # print(data)
        if NFC_status & self.NFC_PWRGOOD:
            print('NFC Field Test Pass')
        else:
            print('NFC Field Test Fail')
        time.sleep(0.5)

    # def main_NFC_Test(self):
    #     addr = 4
    #     DataLen_word = 12
    #     read_data = [0 for _ in range(DataLen_word)]
    #     write_data = [0 for _ in range(DataLen_word)]
    #     NFCGETBUF = [0 for _ in range(DataLen_word)]
    #     NFCSETBUF = [0x11223344, 0x22222222, 0x33333333, 0x44444444,
    #                  0x55555555, 0x66666666, 0x77777777, 0x88888888,
    #                  0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC,
    #                  ]
    #     self.TRx_Init()
    #     self.TRx_EnableInt(self.INT_ST_NFC)
    #     self.TRx_NFC_SetConfig(self.NFC_USER_CFG6)
    #     self.spi_w_field(0x9008, 0xFFFF0000, 0)
    #     print('Static Lock Disable!')
    #     self.TRx_NFC_Write(addr * 4, NFCSETBUF, DataLen_word)
    #
    #     # NFCGETBUF = self.TRx_NFC_Read(addr*4, NFCGETBUF, DataLen_word)
    #     # for i in range(DataLen_word):
    #     #     print(format(NFCGETBUF[i], '#x'))
    #
    #     n = readers()
    #     connection = n[0].createConnection()
    #     connection.connect()
    #     '''
    #     print('Start NFC Reader write data to ER4100')
    #     # ------------- Write data to ER4100 -------------------------------------
    #     for i in range(0, DataLen_word):  # CmdSize>>2 = u16CmdSize/4 for Block Size 128
    #         if ((i + 4) % 4) == 0:
    #             print("[Block No.%d]" % (i+ addr))
    #
    #         data = NFCSETBUF[i]
    #         apdu = [0XA2, i+ addr, (data >> 0) & 0xFF, (data >> 8) & 0xFF, (data >> 16) & 0xFF,
    #                 (data >> 24) & 0xFF]
    #         print(" %s %s %s %s" % (hex((data >> 0) & 0xFF), hex((data >> 8) & 0xFF), hex((data >> 16) & 0xFF),
    #                                 hex((data >> 24) & 0xFF)))
    #         connection.transmit(apdu)
    #         write_data[i] = data
    #         time.sleep(0.01)
    #
    #     apdu = [0XA2, 0XFF, 0X00, 0X00, 0X20, 0X05]
    #     connection.transmit(apdu)
    #     time.sleep(0.1)
    #
    #     self.WaitIRQn(5)
    #     result, data = self.TRx_GetIntStatus()
    #     self.TRx_ClearIntFlag(result)
    #     NFC_status = self.TRx_NFC_GetConfig()
    #
    #     if NFC_status & self.NFC_USER_CFG5:
    #         print('NFC Write Data Done!')
    #
    #     self.TRx_NFC_SetConfig(self.NFC_USER_CFG6)
    #     '''
    #     print('Start NFC Reader read data from ER4100')
    #     # ------------- Read data to ER4100 -------------------------------------
    #     for s in range(0, DataLen_word):
    #
    #         if ((s + addr + 4) % 4) == 0:
    #             print("[Block No.%d]" % (s + addr))
    #
    #         apdu = [0X3A, s + addr, s + addr]
    #         data_r, sw1, sw2 = connection.transmit(apdu)
    #         print(format(data_r[0], '#x'), format(data_r[1], '#x'), format(data_r[2], '#x'), format(data_r[3], '#x'))
    #         read_data[s] = data_r[0] | (data_r[1] << 8) | (data_r[2] << 16) | (data_r[3] << 24)
    #         time.sleep(0.01)
    #
    #     apdu = [0XA2, 0XFF, 0X00, 0X00, 0X40, 0X06]
    #     connection.transmit(apdu)
    #     time.sleep(0.1)
    #
    #     self.WaitIRQn(5)
    #     result, data = self.TRx_GetIntStatus()
    #     self.TRx_ClearIntFlag(result)
    #     NFC_status = self.TRx_NFC_GetConfig()
    #
    #     if NFC_status & self.NFC_USER_CFG6:
    #         print('NFC Read Data Done!')
    #
    #     if read_data == NFCSETBUF:
    #         print('NFC Write Data = NFC Read Data')
    #
    # def NFC_read(self):
    #     n = readers()
    #     if len(n):
    #         connection = n[0].createConnection()
    #         connection.connect()
    #     apdu = [0X3A, 1, 1]
    #     data, sw1, sw2 = connection.transmit(apdu)
    #     print(data)
    #     data, sw1, sw2 = connection.transmit(apdu)
    #     print(format(data[0], '#x'), format(data[1], '#x'), format(data[2], '#x'), format(data[3], '#x'))
    #
    #     for i in range(len(data)):
    #         print(format(data[i], '#x'))

    def main_NFC_init(self):
        # NFCSETBUF = []
        # DataLen_word = 1
        # NFCGETBUF = [0 for _ in range(DataLen_word)]
        #
        # NFCGETBUF = self.TRx_NFC_Read(18*4, NFCGETBUF, DataLen_word)
        # for i in range(DataLen_word):
        #     print(format(NFCGETBUF[i], '#x'))

        self.TRx_Init()
        self.TRx_EnableInt(self.INT_ST_NFC)
        self.spi_w_byte(0x9000, 0xCC743404)
        self.spi_w_byte(0x9004, 0x801CE3E1)
        # self.spi_w_field(0x9008, 0xFFFF0000, 0)
        self.spi_w_byte(0x9008, 0x0000009E)
        self.spi_w_byte(0x900C, 0x001210E1)
        self.spi_w_byte(0x93E0, 0)

        # NFCSETBUF = []
        # DataLen_word = 1
        # NFCGETBUF = [0 for _ in range(DataLen_word)]
        #
        # NFCGETBUF = self.TRx_NFC_Read(2*4, NFCGETBUF, DataLen_word)
        # for i in range(DataLen_word):
        #     print(format(NFCGETBUF[i], '#x'))

        # NFCSETBUF = [0x55667788]
        # DataLen_word = 1
        # self.TRx_NFC_Write(18 * 4, NFCSETBUF, DataLen_word)

    def spectrum_sweep_init(self):
        self.TRx_Init()
        self.TRx_Config()
        self.TRx_EnableInt(self.INT_ST_RX | self.INT_ST_RXERR | self.INT_ST_TX | self.INT_ST_TXERR |
                           self.INT_ST_TMRTHD | self.INT_ST_WAKEUP)

    def spectrum_sweep_rx_window_init(self):
        self.TRx_Init()
        self.TRx_Config()
        self.TRx_EnableInt(self.INT_ST_RX | self.INT_ST_RXERR | self.INT_ST_TX | self.INT_ST_TXERR |
                           self.INT_ST_TMRTHD | self.INT_ST_WAKEUP)
        self.TRx_RX_SyncWord(0x5A0FBE66)
        self.TRx_RX_Trigger(0, True)
        while 1:
            rssi_raw, rssi_data = self.TRx_GetRSSI_Data()
            print('RSSI Data = %.1f' % (-rssi_data))
            time.sleep(0.1)


if __name__ == '__main__':
    TX_BPSK_MODE = 0x0
    TX_FSK_MODE = 0x3
    TX_GFSK_MODE = 0x4
    TX_TRANSPARENT = 0x0
    TX_TRANSPARENTwithPRESYNC = 0x3
    TX_802154withPRESYNC = 0xF
    #
    # ftdi_id = 'ftdi://ftdi:2232:0:2/1'
    # ftdi_id = 'ftdi://ftdi:232h:ESMT4L538G/1'
    # ftdi_id = 'ftdi://ftdi:2232:TG110713/2'
    # ftdi_id = 'ftdi://ftdi:2232:TG11071e/2'
    ftdi_id = 'ftdi://ftdi:2232:TG1112ac/2'
    # ftdi_id = 'ftdi://ftdi:2232:TG111851/2'

    gain_linear = 27  #  915M No.10 : -0.5dB Cap = 5
    gsf_switch = 1
    voltage = 3.3
    # matching_gain = 14
    matching_gain = 10
    cap = 36
    PN_OPT_EN = False
    Config23dBm = False
    r = RegAccess()

    app = trx_driver(freq=915_000_000, dr=12500, mod_index=4, modulation=TX_GFSK_MODE, id=ftdi_id)
    # app.Tx_modem_temp()
    # app.modem_vdd()
    # app.NFC_read()
    # app.main_NFC_Test()
    # app.main_NFC_Field_Test()
    # app.vdetect_test()
    # app.CLK_40MHz_check()
    # app.main_idle_setup()
    # app.main_sleep_setup()
    # app.main_shutdown_setup()
    # app.TRx_PowerDownMode()
    # app.main_wakeup_test()
    app.check_TRx_ID()
    # addr = 0xf104
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 2 or 3')
    #
    # # TRx_DCDC_LCurrent
    # addr = 0x1204
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 0x00000018')
    # addr = 0x1E5c
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 0x00000112')
    #
    # # TRx_PAH915MHz23dBm_FSK
    # addr = 0x1480
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 0x00619FC5')
    # addr = 0x1488
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 0')
    # addr = 0x1e60
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 0')
    # addr = 0x1484
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 0x0013d200')
    # addr = 0x1E1C
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 0x1000000 | (Power_0_127 << 25)')
    # addr = 0x10A8
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 0x1000000 | (Power_0_127 << 25)')
    # addr = 0x1E20
    # print(f'{hex(addr)} =', hex(app.spi_r_byte(addr)), f'expect = 0x13000027')

    # app.TRx_TX_SETUP()
    #
    # # app.spi_w_byte(0x1980, app.freq)
    # # app.spi_w_byte(0x1984, TRx_FreqDev)
    # # app.spi_w_byte(0x1988, TRx_DataRate)
    # data = 1
    # app.spi_w_byte(0x1990, data)

    # sys.exit()
    # app.main_NFC_init()
    # data = app.spi_r_field(r.REVISION_ID_REG_REVISION_ID_ADDR, r.REVISION_ID_REG_REVISION_ID_MASK)
    # print(hex(data))
    # app.spi_w_field(0x4000, 0x700, 7)  # For output Tx_EN
    # data = app.spi_r_byte(0x4000)
    # print(hex(data))

    # app.spi_w_byte_tt()
    # time.sleep(0.5)
    # data = app.spi_r_byte_tt()
    # print(hex(data))
    # time.sleep(0.5)
    # data = app.spi_r_byte_tt()
    # print(hex(data))

    # data = app.spi_r_byte(0x400C)
    # print(hex(data))

    # -------- API Tx mode example code ------------------------------------------
    if 0:
        s = 0
        app.main_tx_init(gain_linear, TX_TRANSPARENT, matching_gain, PN_OPT_EN)
        app.spi_w_field(0x1E18, 0x1C00000, s)
        app.TRx_XtalCap40M_Config(cap, cap)
        # app.TRx_GPIO0_Config(True, 0)
        #
        for i in range(1000):
            data = app.main_tx_go(TX_TRANSPARENT)
            time.sleep(0.05)
            # print('Count : %d' % i)
            print('Tx Out')
        sys.exit()

    # -------- API stay in Rx mode example code ------------------------------------------
    # app.main_Rx_init(cap, PN_OPT_EN, True, 5)
    # app.TRx_RX_Trigger(0, True)
    # time.sleep(0.01)
    # app.TRx_RxOff()

    # -------- API stay in Rx mode example code ------------------------------------------
    # IQ_arr = []
    # for i in range(50):
    #     data = app.main_IQ_calib()
    #     # data11 = app.spi_r_byte(0x1620)
    #     # print('0x1604 = %s' % hex(data11))
    #     IQ_arr.append(data)
    # print(IQ_arr)

    # -------- API Rx mode example code ------------------------------------------
    if 0:
        app.main_Rx_init(cap, PN_OPT_EN, False, 1)

        # while 1 :
        #
        #     RSSI_raw, RSSI_dBm = app.TRx_GetRSSI_CCA()
        #
        #     print('RSSI_dBm = %f' % RSSI_dBm)

        pass_count = 0
        fail_count = 0
        data_S = []
        data_A = []
        data_R = []
        for i in range(500):
            rx_result, rssi_result, timeout_result, cfo_sw, cfo_pl, S_AGC_IDX01, A_AGC_IDX01 = app.main_Rx_go()
            if rx_result:
                pass_count += 1
            else:
                fail_count += 1

            # data_S.append(S_AGC_IDX01)
            # data_A.append(A_AGC_IDX01)
            # data_R.append(rx_result)
            print('Pass count : %d' % pass_count)
            print('Fail count : %d' % fail_count)
            print('RSSI : %f' % (rssi_result))
            # print(data_S)
            # print(data_A)
            # print(data_R)
        # sys.exit()
    # -------- FSK SW mode example code ------------------------------------------
    # app.main_fsk_mode(gsf_switch, True, cap)
    # app.reg_modify(voltage, matching_gain)
    # app.er4100_pa1g_gain(gain_linear)

    # -------- FSK PRBS9 mode example code ---------------------------------------
    # app.main_PRBS9_mode(gain_linear, Config23dBm, cap, PN_OPT_EN)
    # sys.exit()
    # -------- Single Tone example code ------------------------------------------
    # app.main_singletone_mode(gain_linear, cap, Config23dBm, PN_OPT_EN)
    # sys.exit()
    app.main_Rx_init(cap, PN_OPT_EN, False, 1)

    arr_r = [0x00000010,0x00000010,0x00000000,0x00000014,0x00001100,0x00001280,0x00001284,0x00001380,0x00001384,0x00001390,0x00001580,0x00001620,0x00001624,0x00001700,0x00004004,0x0000400c,0x00001a14,0x0000b0a8,0x0000b19c,0x0000b208,0x0000b158,0x0000b15c,0x0000b160,0x0000b164,0x0000b168,0x0000b16c,0x0000b170,0x0000b174,0x0000b178,0x0000b17c,0x0000b180,0x0000b184,0x0000b188,0x0000b18c,0x0000b190,0x0000b194,0x0000b198,0x0000b1bc,0x0000b1c0,0x0000b1c4,0x0000b1c8,0x0000b1cc,0x0000b1d0,0x0000b1d4,0x0000b1d8,0x0000b1dc,0x0000b1e0,0x0000b1e4,0x0000b1e8,0x0000b1ec,0x0000b1f0,0x0000b1f4,0x0000b1f8,0x0000b250,0x0000b254,0x0000b258,0x00001200,0x00001204,0x00001e50,0x00001e54,0x00001e58,0x00001e5c,0x00001e18,0x00001e00,0x00001e3c,0x00001e30,0x00001e34,0x00001e64,0x0000b30c,0x00001488,0x00001600,0x00001e60,0x00001f00,0x00001f04,0x00001f08,0x00001f0c,0x00001f10,0x00001f14,0x00001f18,0x00001f1c,0x00001f20,0x00001f24,0x00001f28,0x00001f2c,0x00001f30,0x00001f34,0x00001f38,0x000013ac,0x00001e04,0x000013ac,0x0000a030,0x00001604,0x0000b108,0x0000b10c,0x0000b110,0x0000b400,0x0000b404,0x0000b408,0x0000b40c,0x0000b410,0x0000b414,0x0000b418,0x0000b41c,0x0000b420,0x0000b424,0x0000b428,0x0000b42c,0x0000b1b4,0x0000b200,0x0000b304,0x0000b19c,0x0000b1a8,0x0000b000,0x00001188,0x00001980,0x00001984,0x00001988,0x00001990,0x00000000,0x00001620,0x00000020,0x00000028,0x00001a00,0x00001a08,0x00004004,0x00000020,]
    # arr_r = [0x00001280]
    for aa in range(len(arr_r)):
        data = app.spi_r_byte(arr_r[aa])
        print('0x%08x,0x%08x' %(arr_r[aa], data))

    # data = app.spi_r_field(r.FREQSYNTH_CONFIG_4_FS_DIV_INT_ADDR, r.FREQSYNTH_CONFIG_4_FS_DIV_INT_MASK)
    # print('FS_DIV_INT = ' + hex(data))
    #
    # data = app.spi_r_field(r.FREQSYNTH_CONFIG_4_FS_DIV_FRAC_ADDR, r.FREQSYNTH_CONFIG_4_FS_DIV_FRAC_MASK)
    # print('FS_DIV_FRAC = ' + hex(data))
    #
    # data = app.spi_r_field(r.FREQSYNTH_TPMCAL_CONFIG_1_FS_TPMCAL_DIV_FRAC_HB_ADDR, r.FREQSYNTH_TPMCAL_CONFIG_1_FS_TPMCAL_DIV_FRAC_HB_MASK)
    # print('FS_TPMCAL_DIV_FRAC_HB = ' + hex(data))
    #
    # data = app.spi_r_field(r.FREQSYNTH_TPMCAL_CONFIG_1_FS_TPMCAL_DIV_INT_HB_ADDR, r.FREQSYNTH_TPMCAL_CONFIG_1_FS_TPMCAL_DIV_INT_HB_MASK)
    # print('FS_TPMCAL_DIV_INT_HB = ' + hex(data))
    #
    # data = app.spi_r_field(r.FREQSYNTH_TPMCAL_CONFIG_2_FS_TPMCAL_DIV_FRAC_LB_ADDR, r.FREQSYNTH_TPMCAL_CONFIG_2_FS_TPMCAL_DIV_FRAC_LB_MASK)
    # print('FS_TPMCAL_DIV_FRAC_LB = ' + hex(data))
    #
    # data = app.spi_r_field(r.FREQSYNTH_TPMCAL_CONFIG_2_FS_TPMCAL_DIV_INT_LB_ADDR, r.FREQSYNTH_TPMCAL_CONFIG_2_FS_TPMCAL_DIV_INT_LB_MASK)
    # print('FS_TPMCAL_DIV_INT_LB = ' + hex(data))