using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;



namespace AnLH_EyeProtected
{
    public partial class frmMainForm : Form
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        //private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        //static bool b_While = true;
        public frmMainForm()
        {
            //_hookID = SetHook(_proc);
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmMainForm_FormClosing);
        }

        bool b_FirstTime = true;
        bool b_SaveClick = false;

        /// <summary>
        /// set startup with windows
        /// </summary>
        private void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chkStartUp.Checked)
                rk.SetValue("AnLH_EyeProtected", Application.ExecutablePath.ToString());
            else
                rk.DeleteValue("AnLH_EyeProtected", false);
        }

        #region Hookkey
        //private static IntPtr SetHook(LowLevelKeyboardProc proc)
        //{
        //    using (Process curProcess = Process.GetCurrentProcess())
        //    using (ProcessModule curModule = curProcess.MainModule)
        //    {
        //        return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
        //            GetModuleHandle(curModule.ModuleName), 0);
        //    }
        //}

        //private delegate IntPtr LowLevelKeyboardProc(
        //    int nCode, IntPtr wParam, IntPtr lParam);

        //private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        //{
        //    if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        //    {
        //        int vkCode = Marshal.ReadInt32(lParam);
        //        Keys key = (Keys)vkCode;
        //        if (key == Keys.H)
        //        {
        //            b_While = false;
        //        }
        //    }
        //    return CallNextHookEx(_hookID, nCode, wParam, lParam);
        //}

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr SetWindowsHookEx(int idHook,
        //    LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        //    IntPtr wParam, IntPtr lParam);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion

        #region Hide program to system icon tray

        private void frmMainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                MyNotifyIcon.Visible = true;
                if (b_FirstTime)
                {
                    if (!b_SaveClick)
                    {
                        btnSave_Click(null, null);
                    }
                    MyNotifyIcon.ShowBalloonTip(3000);
                    b_FirstTime = false;
                }
                this.ShowInTaskbar = false;
                this.Hide();

            }


        }


        private void MyNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            MyNotifyIcon.Visible = false;
        }
        private void MyNotifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            MyNotifyIcon.Text = "Eye Protected. Remaining " + Convert.ToInt32(t.TimeLeft / 60000) + " minutes.";
        }
        #endregion

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
        public partial class NativeMethods
        {

            /// Return Type: BOOL->int
            ///fBlockIt: BOOL->int
            [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool BlockInput([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);

        }

        public static void BlockInput(TimeSpan span)
        {

            int i_Second = (int)span.TotalSeconds;
            //int i_CounterSecond = 0;
            try
            {
                //while (b_While || i_CounterSecond == i_Second)
                //{
                NativeMethods.BlockInput(true);
                Thread.Sleep(span);
                //i_CounterSecond++;
                //}
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

        #region Save and Read registry
        /// <summary>
        /// Save Registry
        /// </summary>
        /// <param name="i_Time"></param>
        /// <param name="b_StartWithWindows"></param>
        /// <returns></returns>
        private bool fnSaveToRegistry(int i_Time, bool b_StartWithWindows = true)
        {
            bool b_Return = false;
            try
            {
                string s_UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                Microsoft.Win32.RegistryKey MyRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("AnLH_Eye_Protected");
                string s_Value = i_Time.ToString() + ";" + (b_StartWithWindows ? "1" : "0") + ";" + fnGetVersion();
                MyRegistryKey.SetValue(s_UserName, s_Value);
                MyRegistryKey.Close();
                b_Return = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Vấn đề lưu Registry!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return b_Return;
        }
        /// <summary>
        /// Read registry
        /// </summary>
        /// <returns></returns>
        private string fnReadRegistry()
        {
            string s_Return = "";
            Microsoft.Win32.RegistryKey MyRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AnLH_Eye_Protected");
            string s_UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            try
            {
                s_Return = MyRegistryKey.GetValue(s_UserName).ToString();
            }
            catch { }
            return s_Return;
        }


        #endregion

        #region Load Info
        private void sLoadInfo(string s_Value)
        {
            string[] arr_Value = s_Value.Split(';');
            decimal de_TimeToLock = Convert.ToDecimal(arr_Value[0].ToString());
            numTimeToLock.Value = de_TimeToLock;
            bool b_CheckStartUp = arr_Value[1].ToString() == "1" ? true : false;
            chkStartUp.Checked = b_CheckStartUp;
            //if (!b_FirstTime)
            //{
                subDoSomeThingEveryXMinutes(Convert.ToInt32(numTimeToLock.Value));
            //}
            
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            string s_Location_Path = Application.ExecutablePath.ToString();
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            try
            {
                if (chkStartUp.Checked)
                {
                    registryKey.SetValue("AnLH_Eye_Protected", Application.ExecutablePath);
                }
                else
                {
                    registryKey.DeleteValue("AnLH_Eye_Protected");
                }
            }
            catch { }

            fnSaveToRegistry(Convert.ToInt32(numTimeToLock.Value), chkStartUp.Checked);
            MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                t.Stop();
            }
            catch { }
   
            subDoSomeThingEveryXMinutes(Convert.ToInt32(numTimeToLock.Value));
            b_SaveClick = true;
            this.WindowState = FormWindowState.Minimized;

        }

        private void numTimeToLock_ValueChanged(object sender, EventArgs e)
        {
            if (this.numTimeToLock.Value <= 20)
            {
                MessageBox.Show("Giá trị thời gian phải là số tự nhiên lơn hơn 20", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                numTimeToLock.Value = 1;
                numTimeToLock.Focus();
            }
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            //fnSaveToRegistry(10,true);
            string s_Value = fnReadRegistry();
            string[] arr_Value = s_Value.Split(';');
            if (!string.IsNullOrEmpty(s_Value))
            {
                sLoadInfo(s_Value);
                try
                {
                    if (arr_Value[2].ToString().ToUpper().Trim() == fnGetVersion().ToUpper().Trim())
                    {
                        b_FirstTime = false;
                        this.WindowState = FormWindowState.Minimized;
                    }
                }
                catch 
                {
                    //System.Diagnostics.Process.Start("ShutDown", "-r  -t 30 -c\"Khởi động lại máy tính để hoàn tất quá trình cài đặt.\""); 
                    MessageBox.Show("Bạn cần khởi động lại máy để hoàn tất quá trình cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Focus();
                }
                
            }
            //int i_TimeCounter = Convert.ToInt32(s_Value.Split(';')[0].ToString());
            string current = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            System.Diagnostics.Process[] procs;
            procs = System.Diagnostics.Process.GetProcessesByName(current);
            if (procs.Length >= 2)
            {
                try
                {
                    if (arr_Value[2].ToString().ToUpper().Trim() == fnGetVersion().ToUpper().Trim())
                    {
                        b_FirstTime = false;
                        Application.Exit();
                        return;
                    }
                }
                catch { }
                
            }

            //Process thisProc = Process.GetCurrentProcess();
            //if (IsProcessOpen("AnLH_EyeProtected") == false)
            //{
            //    //System.Windows.MessageBox.Show("Application not open!");
            //    //System.Windows.Application.Current.Shutdown();
            //}
            //else
            //{
            //    // Check how many total processes have the same name as the current one
            //    if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1 )
            //    {
            //        // If ther is more than one, than it is already running.
            //        try
            //        {
            //            if (arr_Value[2].ToString().ToUpper().Trim() == fnGetVersion().ToUpper().Trim())
            //            {
            //                b_FirstTime = false;
            //                Application.Exit();
            //            }
            //        }
            //        catch { }
                    
            //    }
            //}
            lblVersion.Text = fnGetVersion().ToUpper().Trim();


        }
        //Set do something every x minute
        TimerPlus t;
        private void subDoSomeThingEveryXMinutes(int i_TimeCounter)
        {
            t = new TimerPlus(); // set the time
            t.Interval = TimeSpan.FromMinutes(i_TimeCounter).TotalMilliseconds;
            t.AutoReset = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(subLockScreen);
            t.Start();
        }


        void subLockScreen(object sender, ElapsedEventArgs e)
        {
            if (!CheckWindowsFullscreen.IsForegroundFullScreen())
            {
                string s_Value = fnReadRegistry();
                if (!string.IsNullOrEmpty(s_Value))
                {
                    string[] arr_Value = s_Value.Split(';');
                    try
                    {
                        if (arr_Value[2].ToString().ToUpper().Trim() != fnGetVersion().ToUpper().Trim())
                        {
                            t.Stop();
                            Application.Exit();
                        }

                    }
                    catch { }

                }
                string s_UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                MyNotifyIcon.Visible = true;
                MyNotifyIcon.BalloonTipText = s_UserName.Split('\\')[1] + " ơi đứng dạy vận động nào, ngồi nhiều không tốt đâu ^^\"!";
                MyNotifyIcon.ShowBalloonTip(3000);
                Thread.Sleep(10000);
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    MonitorOff(this.Handle);
                }));
                LockWorkStation();

                TimeSpan time = new TimeSpan(0, 0, 20);
                BlockInput(time);
            }
        }

        private void frmMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                //Stop your infinite loop
                t.Stop();
            }
            else
            {
                if (b_FirstTime)
                {
                    btnSave_Click(null, null);
                }
                else
                {
                    b_FirstTime = false;
                    this.WindowState = FormWindowState.Minimized;
                }
                e.Cancel = true;
            }
        }

        #region Check program running

        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;

        }


        #endregion

        private string fnGetVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            return version;
        }


    }

}
