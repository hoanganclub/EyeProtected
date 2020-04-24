using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace AnLH_EyeProtected
{
    public class WindowsHelper
    {
        #region Turnoff screen
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        static extern void mouse_event(Int32 dwFlags, Int32 dx, Int32 dy, Int32 dwData, UIntPtr dwExtraInfo);

        private const int WmSyscommand = 0x0112;
        private const int ScMonitorpower = 0xF170;
        private const int MonitorShutoff = 2;
        private const int MouseeventfMove = 0x0001;

        public static void MonitorOff(IntPtr handle)
        {
            SendMessage(handle, WmSyscommand, (IntPtr)ScMonitorpower, (IntPtr)MonitorShutoff);
        }

        private static void MonitorOn()
        {
            mouse_event(MouseeventfMove, 0, 1, 0, UIntPtr.Zero);
            Thread.Sleep(40);
            mouse_event(MouseeventfMove, 0, -1, 0, UIntPtr.Zero);
        }
        #endregion

        #region BlockInput
        private partial class NativeMethods
        {

            /// Return Type: BOOL->int
            ///fBlockIt: BOOL->int
            [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool BlockInput([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);

        }

        public static void BlockInput(TimeSpan span)
        {
            try
            {
                NativeMethods.BlockInput(true);
                Thread.Sleep(span);
            }
            finally
            {
                NativeMethods.BlockInput(false);
            }

        }

        #endregion
        #region Lock Station
        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();
        #endregion

    }
}
