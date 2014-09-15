namespace WebHard.WinForm
{
    partial class MainForm
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
            this.barManager = new UIC.Win.Control.DVX.uBarManager();
            this.toolBar = new DevExpress.XtraBars.Bar();
            this.bsFile = new DevExpress.XtraBars.BarSubItem();
            this.bbLogout = new DevExpress.XtraBars.BarButtonItem();
            this.bbExit = new DevExpress.XtraBars.BarButtonItem();
            this.bsEdit = new DevExpress.XtraBars.BarSubItem();
            this.bbNewFolder = new DevExpress.XtraBars.BarButtonItem();
            this.bbAuthority = new DevExpress.XtraBars.BarButtonItem();
            this.bbUpload = new DevExpress.XtraBars.BarButtonItem();
            this.bbDownload = new DevExpress.XtraBars.BarButtonItem();
            this.bbDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbRenew = new DevExpress.XtraBars.BarButtonItem();
            this.bsHelp = new DevExpress.XtraBars.BarSubItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.toolBar});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsFile,
            this.bsEdit,
            this.bsHelp,
            this.bbLogout,
            this.bbExit,
            this.bbNewFolder,
            this.bbUpload,
            this.bbDownload,
            this.bbDelete,
            this.bbAuthority,
            this.bbRenew});
            this.barManager.MainMenu = this.toolBar;
            this.barManager.MaxItemId = 11;
            // 
            // toolBar
            // 
            this.toolBar.BarName = "Tool Bar";
            this.toolBar.DockCol = 0;
            this.toolBar.DockRow = 0;
            this.toolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.toolBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsHelp)});
            this.toolBar.OptionsBar.AllowQuickCustomization = false;
            this.toolBar.OptionsBar.DrawDragBorder = false;
            this.toolBar.OptionsBar.RotateWhenVertical = false;
            this.toolBar.OptionsBar.UseWholeRow = true;
            this.toolBar.Text = "Tool Bar";
            // 
            // bsFile
            // 
            this.bsFile.Caption = "파일(&F)";
            this.bsFile.Id = 0;
            this.bsFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbLogout),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbExit, true)});
            this.bsFile.Name = "bsFile";
            // 
            // bbLogout
            // 
            this.bbLogout.Caption = "로그아웃";
            this.bbLogout.Id = 3;
            this.bbLogout.Name = "bbLogout";
            this.bbLogout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnLogoutBarButtonItemClick);
            // 
            // bbExit
            // 
            this.bbExit.Caption = "끝내기(&X)";
            this.bbExit.Id = 4;
            this.bbExit.Name = "bbExit";
            this.bbExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnExitBarButtonItemClick);
            // 
            // bsEdit
            // 
            this.bsEdit.Caption = "편집(&E)";
            this.bsEdit.Id = 1;
            this.bsEdit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbNewFolder),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbAuthority),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbUpload, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbDownload),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbDelete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbRenew, true)});
            this.bsEdit.Name = "bsEdit";
            // 
            // bbNewFolder
            // 
            this.bbNewFolder.Caption = "폴더생성(&N)";
            this.bbNewFolder.Id = 5;
            this.bbNewFolder.Name = "bbNewFolder";
            this.bbNewFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnNewFolderBarButtonItemClick);
            // 
            // bbAuthority
            // 
            this.bbAuthority.Caption = "권한설정(&A)";
            this.bbAuthority.Id = 9;
            this.bbAuthority.Name = "bbAuthority";
            this.bbAuthority.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnAuthorityBarButtonItemClick);
            // 
            // bbUpload
            // 
            this.bbUpload.Caption = "업로드(&U)";
            this.bbUpload.Id = 6;
            this.bbUpload.Name = "bbUpload";
            this.bbUpload.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnUploadBarButtonItemClick);
            // 
            // bbDownload
            // 
            this.bbDownload.Caption = "다운로드(&D)";
            this.bbDownload.Id = 7;
            this.bbDownload.Name = "bbDownload";
            this.bbDownload.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnDownloadBarButtonItemClick);
            // 
            // bbDelete
            // 
            this.bbDelete.Caption = "삭제(&D)";
            this.bbDelete.Id = 8;
            this.bbDelete.Name = "bbDelete";
            this.bbDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnDeleteBarButtonItemClick);
            // 
            // bbRenew
            // 
            this.bbRenew.Caption = "새로고침(&R)";
            this.bbRenew.Id = 10;
            this.bbRenew.Name = "bbRenew";
            this.bbRenew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnRenewBarButtonItemClick);
            // 
            // bsHelp
            // 
            this.bsHelp.Caption = "도움말(&H)";
            this.bsHelp.Id = 2;
            this.bsHelp.Name = "bsHelp";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 564);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "웹하드";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIC.Win.Control.DVX.uBarManager barManager;
        private DevExpress.XtraBars.Bar toolBar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem bsFile;
        private DevExpress.XtraBars.BarButtonItem bbLogout;
        private DevExpress.XtraBars.BarSubItem bsEdit;
        private DevExpress.XtraBars.BarSubItem bsHelp;
        private DevExpress.XtraBars.BarButtonItem bbExit;
        private DevExpress.XtraBars.BarButtonItem bbNewFolder;
        private DevExpress.XtraBars.BarButtonItem bbUpload;
        private DevExpress.XtraBars.BarButtonItem bbDownload;
        private DevExpress.XtraBars.BarButtonItem bbDelete;
        private DevExpress.XtraBars.BarButtonItem bbAuthority;
        private DevExpress.XtraBars.BarButtonItem bbRenew;


    }
}