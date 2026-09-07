using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ER41xxTestTool
{
    struct RegInfo
    {
        public string[] regContent;
        public int[] contentMask;
        public int[] shiftBit;
        public int contentLength;

        public void setcontentLeng(int conLength)
        {
            contentLength = conLength;
        }
        public void setshiftBit(int[] bits)
        {
            shiftBit = bits;
        }
        public void setcontentMask(int[] cMask)
        {
            contentMask = cMask;
        }
        public void setregContent(string[] cReg)
        {
            regContent = cReg;
        }
    }

    public class RegContentLists2
    {
        static void SetReg()
        {
            RegInfo reg1 = new RegInfo();
            reg1.contentLength = 2;
        }
    }

    
}
