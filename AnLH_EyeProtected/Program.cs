using System;
using System.Windows.Forms;

namespace AnLH_EyeProtected
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMainForm());
        }
      
    }
}
