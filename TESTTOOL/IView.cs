using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ER41xxTestTool
{
    public interface IView
    {
        void SetController(Controller txSerial);
        //Open serial port event
        void OpenComEvent(Object sender, SerialPortEventArgs e);
        //Close serial port event
        void CloseComEvent(Object sender, SerialPortEventArgs e);
        //Serial port receive data event
        void ComReceiveDataEvent(Object sender, SerialPortEventArgs e);

        //==============================================================
         // Owen will closed
        void SetController2(Controller2 rxSerial);
        //Open serial port eventa
        void OpenComEvent2(Object sender, SerialPortEventArgs2 e);
        //Close serial port event
        void CloseComEvent2(Object sender, SerialPortEventArgs2 e);
        //Serial port receive data event
        void ComReceiveDataEvent2(Object sender, SerialPortEventArgs2 e);

        //==============================================================
        void SetController3(Controller3 pcbSerial);
        //Open serial port event
        void OpenComEvent3(Object sender, SerialPortEventArgs3 e);
        //Close serial port event
        void CloseComEvent3(Object sender, SerialPortEventArgs3 e);
        //Serial port receive data event
        void ComReceiveDataEvent3(Object sender, SerialPortEventArgs3 e);

           // Owen will closed END
    }
}
