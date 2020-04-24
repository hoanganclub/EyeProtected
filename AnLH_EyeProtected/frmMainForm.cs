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
        //private const int WH_KEYBOARD_LL = 13;
        //private const int WM_KEYDOWN = 0x0100;
        //private static IntPtr _hookID = IntPtr.Zero;
        public const string ApplicationName = "EyeProtected";
        public string CurrentUsername { get; private set; }


        private TimerPlus _timer;
        bool _isFirstTime = true;
        bool _isClickSaveButton = false;


        public frmMainForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmMainForm_FormClosing);
            CurrentUsername = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        void LockScreen(object sender, ElapsedEventArgs e)
        {
            if (!CheckWindowsFullscreen.IsForegroundFullScreen())
            {
                MyNotifyIcon.Visible = true;
                MyNotifyIcon.BalloonTipText = CurrentUsername.Split('\\')[1] + " ơi đứng dạy vận động nào, ngồi nhiều không tốt đâu ^^\"!";
                MyNotifyIcon.ShowBalloonTip(3000);
                Thread.Sleep(10000);
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    WindowsHelper.MonitorOff(this.Handle);
                }));
                WindowsHelper.LockWorkStation();
                TimeSpan time = new TimeSpan(0, 0, 5);
                WindowsHelper.BlockInput(time);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string applicationPath = Application.ExecutablePath.ToString();

            RegistryHelper.DeleteToAutoRunOnStartWindwosRegistry(ApplicationName);
            bool saveAutoStartWithWindows = RegistryHelper.WriteToAutoRunOnStartWindwosRegistry(ApplicationName, applicationPath);


            string periodTime = tbxPeriodTime.Value.ToString();
            string isRunOnStartWindows = chkRunOnStartWindows.Checked ? "1" : "0";
            string applicationVersion = GetCurrentApplicationVersion();

            string userData = periodTime + ";" + isRunOnStartWindows + ";" + applicationVersion;
            bool saveAppSetting = RegistryHelper.WriteToCurrentUserSetting(CurrentUsername, ApplicationName, userData);

            if (saveAutoStartWithWindows && saveAppSetting)
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (!saveAutoStartWithWindows)
                {
                    MessageBox.Show("Lưu tự động khởi động cùng windows thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (!saveAppSetting)
                {
                    MessageBox.Show("Lưu cấu hình thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            try
            {
                if (_timer != null)
                {
                    _timer.Stop();
                }

            }
            catch { }

            InitCounter(Convert.ToInt32(tbxPeriodTime.Value));
            _isClickSaveButton = true;
            this.WindowState = FormWindowState.Minimized;
            CloseAllOtherInstance();
        }

        private void CloseAllOtherInstance()
        {
            try
            {
                string currentProcess = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                System.Diagnostics.Process[] procs;
                procs = System.Diagnostics.Process.GetProcessesByName(currentProcess);
                if (procs.Length >= 2)
                {
                    var currentApplicationProcess = Process.GetCurrentProcess();
                    foreach (var app in procs)
                    {
                        if (app.Id != currentApplicationProcess.Id)
                        {
                            try
                            {
                                app.Kill();
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            string userDataRaw = RegistryHelper.ReadFromCurrentUserSetting(CurrentUsername, ApplicationName);

            if (!string.IsNullOrEmpty(userDataRaw))
            {
                string[] arrUserData = userDataRaw.Split(';');
                if (arrUserData.Length == 3)
                {
                    int periodTime = Convert.ToInt32(arrUserData[0].ToString());
                    tbxPeriodTime.Value = periodTime;
                    bool isRunOnStartWindows = arrUserData[1].ToString() == "1" ? true : false;
                    chkRunOnStartWindows.Checked = isRunOnStartWindows;
                    string applicationVersion = arrUserData[2];
                    string thisCurrentAppPath = Application.ExecutablePath.ToString();
                    string savedAppPath = RegistryHelper.ReadFromAutoRunOnStartWindwosRegistry(ApplicationName);

                    if (applicationVersion.ToUpper().Trim() == GetCurrentApplicationVersion().ToUpper().Trim())
                    {

                        if (savedAppPath == thisCurrentAppPath)
                        {
                            _isFirstTime = false;
                            this.WindowState = FormWindowState.Minimized;
                        }
                        else
                        {
                            _isFirstTime = false;
                            MessageBox.Show("File bạn mở có đường dẫn khác với đường dãn file trước kia, Nếu bạn muốn sử dụng đường dẫn hiện tại vui lòng nhấn \"Lưu lại\"", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    OpenNewInstance(applicationVersion, savedAppPath);
                    InitCounter(periodTime);
                }

            }

            lblVersion.Text = GetCurrentApplicationVersion().ToUpper().Trim();
        }

        private void OpenNewInstance(string applicationVersion, string savedAppPath)
        {

            string currentAppPath = Application.ExecutablePath.ToString().ToLower();
            if (applicationVersion.ToUpper().Trim() == GetCurrentApplicationVersion().ToUpper().Trim() && currentAppPath == savedAppPath.ToLower())
            {
                _isFirstTime = false;
                Application.Exit();
                return;
            }
        }

        private void InitCounter(int i_TimeCounter)
        {
            _timer = new TimerPlus(); // set the time
            _timer.Interval = TimeSpan.FromMinutes(i_TimeCounter).TotalMilliseconds;
            _timer.AutoReset = true;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(LockScreen);
            _timer.Start();
        }

        private void frmMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                //Stop your infinite loop
                if (_timer != null)
                {
                    _timer.Stop();
                }

            }
            else
            {
                if (_isFirstTime)
                {
                    btnSave_Click(null, null);
                }
                else
                {
                    _isFirstTime = false;
                    this.WindowState = FormWindowState.Minimized;
                }
                e.Cancel = true;
            }
        }

        private void tbxPeriodTime_ValueChanged(object sender, EventArgs e)
        {
            if (this.tbxPeriodTime.Value <= 10)
            {
                MessageBox.Show("Giá trị thời gian phải là số tự nhiên lơn hơn 10", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxPeriodTime.Value = 11;
                tbxPeriodTime.Focus();
            }
        }

        private string GetCurrentApplicationVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            return version;
        }

        #region Hide program to system icon tray
        private void frmMainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                MyNotifyIcon.Visible = true;
                if (_isFirstTime)
                {
                    if (!_isClickSaveButton)
                    {
                        btnSave_Click(null, null);
                    }
                    MyNotifyIcon.ShowBalloonTip(3000);
                    _isFirstTime = false;
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
            MyNotifyIcon.Text = ApplicationName + ". Sẽ khóa sau " + Convert.ToInt32(_timer.TimeLeft / 60000) + " phút.";
        }
        #endregion

    }

}
