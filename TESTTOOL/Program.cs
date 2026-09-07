
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    static class Program
    {
        /// <summary>
        /// Entry point of the program
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles(); 
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm view = new MainForm();
            view.StartPosition = FormStartPosition.CenterScreen;
            Controller txSerial = new Controller(view);
            Controller2 rxSerial = new Controller2(view);
            Controller3 pcbSerial = new Controller3(view);
            Application.Run(view);
        }
    }
}
