namespace AnLH_EyeProtected
{
    partial class frmMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainForm));
            this.chkRunOnStartWindows = new System.Windows.Forms.CheckBox();
            this.MyNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTimeToLock = new System.Windows.Forms.Label();
            this.tbxPeriodTime = new System.Windows.Forms.NumericUpDown();
            this.lblUnit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbxPeriodTime)).BeginInit();
            this.SuspendLayout();
            // 
            // chkRunOnStartWindows
            // 
            this.chkRunOnStartWindows.AutoSize = true;
            this.chkRunOnStartWindows.Checked = true;
            this.chkRunOnStartWindows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRunOnStartWindows.Location = new System.Drawing.Point(12, 37);
            this.chkRunOnStartWindows.Name = "chkRunOnStartWindows";
            this.chkRunOnStartWindows.Size = new System.Drawing.Size(146, 17);
            this.chkRunOnStartWindows.TabIndex = 0;
            this.chkRunOnStartWindows.Text = "Khởi động cùng windows";
            this.chkRunOnStartWindows.UseVisualStyleBackColor = true;
            // 
            // MyNotifyIcon
            // 
            this.MyNotifyIcon.BalloonTipText = "Phần mềm đã được ẩn ở khay hệ thống.";
            this.MyNotifyIcon.BalloonTipTitle = "Eye protected";
            this.MyNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("MyNotifyIcon.Icon")));
            this.MyNotifyIcon.Text = "Eye protected";
            this.MyNotifyIcon.Visible = true;
            this.MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MyNotifyIcon_MouseDoubleClick);
            this.MyNotifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MyNotifyIcon_MouseMove);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(206, 60);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu lại";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTimeToLock
            // 
            this.lblTimeToLock.AutoSize = true;
            this.lblTimeToLock.Location = new System.Drawing.Point(13, 13);
            this.lblTimeToLock.Name = "lblTimeToLock";
            this.lblTimeToLock.Size = new System.Drawing.Size(170, 13);
            this.lblTimeToLock.TabIndex = 2;
            this.lblTimeToLock.Text = "Tắt màn hình mỗi khoảng thời gian";
            // 
            // tbxPeriodTime
            // 
            this.tbxPeriodTime.Location = new System.Drawing.Point(189, 11);
            this.tbxPeriodTime.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this.tbxPeriodTime.Minimum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.tbxPeriodTime.Name = "tbxPeriodTime";
            this.tbxPeriodTime.Size = new System.Drawing.Size(58, 20);
            this.tbxPeriodTime.TabIndex = 3;
            this.tbxPeriodTime.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.tbxPeriodTime.ValueChanged += new System.EventHandler(this.tbxPeriodTime_ValueChanged);
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(253, 13);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(28, 13);
            this.lblUnit.TabIndex = 4;
            this.lblUnit.Text = "phút";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Version:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.ForeColor = System.Drawing.Color.Red;
            this.lblVersion.Location = new System.Drawing.Point(64, 65);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 13);
            this.lblVersion.TabIndex = 6;
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 95);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.tbxPeriodTime);
            this.Controls.Add(this.lblTimeToLock);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkRunOnStartWindows);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eye Protected";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainForm_FormClosing);
            this.Load += new System.EventHandler(this.frmMainForm_Load);
            this.Resize += new System.EventHandler(this.frmMainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tbxPeriodTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRunOnStartWindows;
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTimeToLock;
        private System.Windows.Forms.NumericUpDown tbxPeriodTime;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVersion;
    }
}

