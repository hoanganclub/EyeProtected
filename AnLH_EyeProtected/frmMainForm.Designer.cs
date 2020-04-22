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
            this.chkStartUp = new System.Windows.Forms.CheckBox();
            this.MyNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTimeToLock = new System.Windows.Forms.Label();
            this.numTimeToLock = new System.Windows.Forms.NumericUpDown();
            this.lblUnit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeToLock)).BeginInit();
            this.SuspendLayout();
            // 
            // chkStartUp
            // 
            this.chkStartUp.AutoSize = true;
            this.chkStartUp.Checked = true;
            this.chkStartUp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartUp.Location = new System.Drawing.Point(12, 37);
            this.chkStartUp.Name = "chkStartUp";
            this.chkStartUp.Size = new System.Drawing.Size(146, 17);
            this.chkStartUp.TabIndex = 0;
            this.chkStartUp.Text = "Khởi động cùng windows";
            this.chkStartUp.UseVisualStyleBackColor = true;
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
            // numTimeToLock
            // 
            this.numTimeToLock.Location = new System.Drawing.Point(189, 11);
            this.numTimeToLock.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this.numTimeToLock.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numTimeToLock.Name = "numTimeToLock";
            this.numTimeToLock.Size = new System.Drawing.Size(58, 20);
            this.numTimeToLock.TabIndex = 3;
            this.numTimeToLock.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numTimeToLock.ValueChanged += new System.EventHandler(this.numTimeToLock_ValueChanged);
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
            this.Controls.Add(this.numTimeToLock);
            this.Controls.Add(this.lblTimeToLock);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkStartUp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eye Protected";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainForm_FormClosing);
            this.Load += new System.EventHandler(this.frmMainForm_Load);
            this.Resize += new System.EventHandler(this.frmMainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numTimeToLock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkStartUp;
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTimeToLock;
        private System.Windows.Forms.NumericUpDown numTimeToLock;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVersion;
    }
}

