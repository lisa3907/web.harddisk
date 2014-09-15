namespace WebHard.WinCtrl.Forms
{
    partial class SingleDownDialog
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
            this.downloadWorker = new System.ComponentModel.BackgroundWorker();
            this.lbDownStatus = new UIC.Win.Control.DVX.uLabelControl();
            this.lbDownLoc = new UIC.Win.Control.DVX.uLabelControl();
            this.pbDownload = new UIC.Win.Control.uProgressBar();
            this.uLabelControl1 = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl2 = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl3 = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl4 = new UIC.Win.Control.DVX.uLabelControl();
            this.lbDownSpeed = new UIC.Win.Control.DVX.uLabelControl();
            this.lbSavePath = new UIC.Win.Control.DVX.uLabelControl();
            this.lbRemaind = new UIC.Win.Control.DVX.uLabelControl();
            this.lbDownTime = new UIC.Win.Control.DVX.uLabelControl();
            this.chAutoClose = new UIC.Win.Control.DVX.uCheckEdit();
            this.btOpen = new UIC.Win.Control.DVX.uSimpleButton();
            this.btFolder = new UIC.Win.Control.DVX.uSimpleButton();
            this.btCancel = new UIC.Win.Control.DVX.uSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chAutoClose.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // DnloaderWorker
            // 
            this.downloadWorker.WorkerReportsProgress = true;
            this.downloadWorker.WorkerSupportsCancellation = true;
            this.downloadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnDownloadWorkerDoWork);
            this.downloadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnDownloadWorkerRunWorkerCompleted);
            this.downloadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnDownloadWorkerProgressChanged);
            // 
            // lbDownStatus
            // 
            this.lbDownStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbDownStatus.Location = new System.Drawing.Point(12, 8);
            this.lbDownStatus.Name = "lbDownStatus";
            this.lbDownStatus.Size = new System.Drawing.Size(400, 14);
            this.lbDownStatus.TabIndex = 0;
            this.lbDownStatus.Text = "다운로드 중:";
            // 
            // lbDownLoc
            // 
            this.lbDownLoc.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbDownLoc.Location = new System.Drawing.Point(12, 28);
            this.lbDownLoc.Name = "lbDownLoc";
            this.lbDownLoc.Size = new System.Drawing.Size(400, 14);
            this.lbDownLoc.TabIndex = 1;
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(12, 48);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(400, 18);
            this.pbDownload.TabIndex = 2;
            // 
            // uLabelControl1
            // 
            this.uLabelControl1.Location = new System.Drawing.Point(12, 79);
            this.uLabelControl1.Name = "uLabelControl1";
            this.uLabelControl1.Size = new System.Drawing.Size(80, 14);
            this.uLabelControl1.TabIndex = 3;
            this.uLabelControl1.Text = "다운로드 시간:";
            // 
            // uLabelControl2
            // 
            this.uLabelControl2.Location = new System.Drawing.Point(12, 95);
            this.uLabelControl2.Name = "uLabelControl2";
            this.uLabelControl2.Size = new System.Drawing.Size(56, 14);
            this.uLabelControl2.TabIndex = 4;
            this.uLabelControl2.Text = "남은 시간:";
            // 
            // uLabelControl3
            // 
            this.uLabelControl3.Location = new System.Drawing.Point(12, 111);
            this.uLabelControl3.Name = "uLabelControl3";
            this.uLabelControl3.Size = new System.Drawing.Size(80, 14);
            this.uLabelControl3.TabIndex = 5;
            this.uLabelControl3.Text = "다운로드 위치:";
            // 
            // uLabelControl4
            // 
            this.uLabelControl4.Location = new System.Drawing.Point(12, 127);
            this.uLabelControl4.Name = "uLabelControl4";
            this.uLabelControl4.Size = new System.Drawing.Size(84, 14);
            this.uLabelControl4.TabIndex = 6;
            this.uLabelControl4.Text = "파일 전송 속도:";
            // 
            // lbDownSpeed
            // 
            this.lbDownSpeed.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbDownSpeed.Location = new System.Drawing.Point(98, 127);
            this.lbDownSpeed.Name = "lbDownSpeed";
            this.lbDownSpeed.Size = new System.Drawing.Size(314, 14);
            this.lbDownSpeed.TabIndex = 10;
            // 
            // lbSavePath
            // 
            this.lbSavePath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbSavePath.Location = new System.Drawing.Point(98, 111);
            this.lbSavePath.Name = "lbSavePath";
            this.lbSavePath.Size = new System.Drawing.Size(314, 14);
            this.lbSavePath.TabIndex = 9;
            // 
            // lbRemaind
            // 
            this.lbRemaind.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbRemaind.Location = new System.Drawing.Point(98, 95);
            this.lbRemaind.Name = "lbRemaind";
            this.lbRemaind.Size = new System.Drawing.Size(314, 14);
            this.lbRemaind.TabIndex = 8;
            // 
            // lbDownTime
            // 
            this.lbDownTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbDownTime.Location = new System.Drawing.Point(98, 79);
            this.lbDownTime.Name = "lbDownTime";
            this.lbDownTime.Size = new System.Drawing.Size(314, 14);
            this.lbDownTime.TabIndex = 7;
            // 
            // chAutoClose
            // 
            this.chAutoClose.Location = new System.Drawing.Point(12, 147);
            this.chAutoClose.Name = "chAutoClose";
            this.chAutoClose.Properties.Caption = "다운로드가 완료되면 대화 상자를 닫음(&C)";
            this.chAutoClose.Size = new System.Drawing.Size(400, 19);
            this.chAutoClose.TabIndex = 11;
            this.chAutoClose.CheckedChanged += new System.EventHandler(this.OnAutoCloseCheckBoxCheckedChanged);
            // 
            // btOpen
            // 
            this.btOpen.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOpen.Appearance.Options.UseFont = true;
            this.btOpen.Location = new System.Drawing.Point(146, 172);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(88, 20);
            this.btOpen.TabIndex = 12;
            this.btOpen.Text = "열기(&O)";
            this.btOpen.Click += new System.EventHandler(this.OnOpenButtonClick);
            // 
            // btFolder
            // 
            this.btFolder.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btFolder.Appearance.Options.UseFont = true;
            this.btFolder.Location = new System.Drawing.Point(235, 172);
            this.btFolder.Name = "btFolder";
            this.btFolder.Size = new System.Drawing.Size(88, 20);
            this.btFolder.TabIndex = 13;
            this.btFolder.Text = "폴더 열기(&F)";
            this.btFolder.Click += new System.EventHandler(this.OnFolderOpenButtonClick);
            // 
            // btCancel
            // 
            this.btCancel.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.Appearance.Options.UseFont = true;
            this.btCancel.Location = new System.Drawing.Point(324, 172);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(88, 20);
            this.btCancel.TabIndex = 14;
            this.btCancel.Text = "취소";
            this.btCancel.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // SingleDownDialog
            // 
            this.ClientSize = new System.Drawing.Size(424, 204);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btFolder);
            this.Controls.Add(this.btOpen);
            this.Controls.Add(this.chAutoClose);
            this.Controls.Add(this.lbDownSpeed);
            this.Controls.Add(this.lbSavePath);
            this.Controls.Add(this.lbRemaind);
            this.Controls.Add(this.lbDownTime);
            this.Controls.Add(this.uLabelControl4);
            this.Controls.Add(this.uLabelControl3);
            this.Controls.Add(this.uLabelControl2);
            this.Controls.Add(this.uLabelControl1);
            this.Controls.Add(this.pbDownload);
            this.Controls.Add(this.lbDownLoc);
            this.Controls.Add(this.lbDownStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingleDownDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.chAutoClose.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker downloadWorker;
        private UIC.Win.Control.DVX.uLabelControl lbDownStatus;
        private UIC.Win.Control.DVX.uLabelControl lbDownLoc;
        private UIC.Win.Control.uProgressBar pbDownload;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl1;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl2;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl3;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl4;
        private UIC.Win.Control.DVX.uLabelControl lbDownSpeed;
        private UIC.Win.Control.DVX.uLabelControl lbSavePath;
        private UIC.Win.Control.DVX.uLabelControl lbRemaind;
        private UIC.Win.Control.DVX.uLabelControl lbDownTime;
        private UIC.Win.Control.DVX.uCheckEdit chAutoClose;
        private UIC.Win.Control.DVX.uSimpleButton btOpen;
        private UIC.Win.Control.DVX.uSimpleButton btFolder;
        private UIC.Win.Control.DVX.uSimpleButton btCancel;
    }
}