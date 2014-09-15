namespace WebHard.WinCtrl.Forms
{
    partial class MultiDownDialog
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
            this.filelistGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmnIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnGuid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCocd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnFileId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnVType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnFilePath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnRName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnSavePath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnVSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnWDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.filelistGrid = new UIC.Win.Control.DVX.uGridControl();
            this.btCancel = new UIC.Win.Control.DVX.uSimpleButton();
            this.downloadWorker = new System.ComponentModel.BackgroundWorker();
            this.chAutoClose = new UIC.Win.Control.DVX.uCheckEdit();
            this.pbDownload = new UIC.Win.Control.uProgressBar();
            this.pbDownFile = new UIC.Win.Control.uProgressBar();
            this.lbTransrate = new UIC.Win.Control.DVX.uLabelControl();
            this.lbDownTime = new UIC.Win.Control.DVX.uLabelControl();
            this.lbDownLoc = new UIC.Win.Control.DVX.uLabelControl();
            this.lbRemaind = new UIC.Win.Control.DVX.uLabelControl();
            this.lblRemaind = new UIC.Win.Control.DVX.uLabelControl();
            this.lbDownSpeed = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl3 = new UIC.Win.Control.DVX.uLabelControl();
            this.lbFileCount = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl1 = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl2 = new UIC.Win.Control.DVX.uLabelControl();
            this.lbTotalSize = new UIC.Win.Control.DVX.uLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.filelistGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filelistGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chAutoClose.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // filelistGridView
            // 
            this.filelistGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmnIndex,
            this.clmnGuid,
            this.clmnCocd,
            this.clmnFileId,
            this.clmnVType,
            this.clmnFilePath,
            this.clmnRName,
            this.clmnStatus,
            this.clmnSavePath,
            this.clmnVSize,
            this.clmnWDate});
            this.filelistGridView.GridControl = this.filelistGrid;
            this.filelistGridView.Name = "filelistGridView";
            this.filelistGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.filelistGridView.OptionsBehavior.Editable = false;
            this.filelistGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.filelistGridView.OptionsView.ShowGroupPanel = false;
            // 
            // clmnIndex
            // 
            this.clmnIndex.Caption = "index";
            this.clmnIndex.FieldName = "index";
            this.clmnIndex.Name = "clmnIndex";
            // 
            // clmnGuid
            // 
            this.clmnGuid.Caption = "guid";
            this.clmnGuid.FieldName = "guid";
            this.clmnGuid.Name = "clmnGuid";
            // 
            // clmnCocd
            // 
            this.clmnCocd.Caption = "cocd";
            this.clmnCocd.FieldName = "companyid";
            this.clmnCocd.Name = "clmnCocd";
            // 
            // clmnFileId
            // 
            this.clmnFileId.Caption = "fileid";
            this.clmnFileId.FieldName = "fileid";
            this.clmnFileId.Name = "clmnFileId";
            // 
            // clmnVType
            // 
            this.clmnVType.Caption = "vtype";
            this.clmnVType.FieldName = "vtype";
            this.clmnVType.Name = "clmnVType";
            // 
            // clmnFilePath
            // 
            this.clmnFilePath.Caption = "filepath";
            this.clmnFilePath.FieldName = "filepath";
            this.clmnFilePath.Name = "clmnFilePath";
            // 
            // clmnRName
            // 
            this.clmnRName.AppearanceHeader.Options.UseTextOptions = true;
            this.clmnRName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clmnRName.Caption = "파일명";
            this.clmnRName.FieldName = "rname";
            this.clmnRName.Name = "clmnRName";
            this.clmnRName.Visible = true;
            this.clmnRName.VisibleIndex = 0;
            // 
            // clmnStatus
            // 
            this.clmnStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.clmnStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clmnStatus.Caption = "전송상황";
            this.clmnStatus.FieldName = "status";
            this.clmnStatus.Name = "clmnStatus";
            this.clmnStatus.Visible = true;
            this.clmnStatus.VisibleIndex = 1;
            // 
            // clmnSavePath
            // 
            this.clmnSavePath.AppearanceHeader.Options.UseTextOptions = true;
            this.clmnSavePath.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clmnSavePath.Caption = "경로";
            this.clmnSavePath.FieldName = "savepath";
            this.clmnSavePath.Name = "clmnSavePath";
            this.clmnSavePath.Visible = true;
            this.clmnSavePath.VisibleIndex = 2;
            // 
            // clmnVSize
            // 
            this.clmnVSize.AppearanceCell.Options.UseTextOptions = true;
            this.clmnVSize.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.clmnVSize.AppearanceHeader.Options.UseTextOptions = true;
            this.clmnVSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clmnVSize.Caption = "크기";
            this.clmnVSize.FieldName = "vsize";
            this.clmnVSize.Name = "clmnVSize";
            this.clmnVSize.Visible = true;
            this.clmnVSize.VisibleIndex = 3;
            // 
            // clmnWDate
            // 
            this.clmnWDate.Caption = "wdate";
            this.clmnWDate.FieldName = "wdate";
            this.clmnWDate.Name = "clmnWDate";
            // 
            // filelistGrid
            // 
            this.filelistGrid.EmbeddedNavigator.Name = "";
            this.filelistGrid.Location = new System.Drawing.Point(8, 27);
            this.filelistGrid.MainView = this.filelistGridView;
            this.filelistGrid.Name = "filelistGrid";
            this.filelistGrid.Size = new System.Drawing.Size(368, 176);
            this.filelistGrid.TabIndex = 20;
            this.filelistGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.filelistGridView});
            // 
            // btCancel
            // 
            this.btCancel.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.Appearance.Options.UseFont = true;
            this.btCancel.Location = new System.Drawing.Point(301, 317);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 20);
            this.btCancel.TabIndex = 31;
            this.btCancel.Text = "취소";
            this.btCancel.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // downloadWorker
            // 
            this.downloadWorker.WorkerReportsProgress = true;
            this.downloadWorker.WorkerSupportsCancellation = true;
            this.downloadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnDownloadWorkerDoWork);
            this.downloadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnDownloadWorkerRunWorkerCompleted);
            this.downloadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnDownloadWorkerProgressChanged);
            // 
            // chAutoClose
            // 
            this.chAutoClose.Location = new System.Drawing.Point(8, 317);
            this.chAutoClose.Name = "chAutoClose";
            this.chAutoClose.Properties.Caption = "다운로드가 완료되면 대화 상자를 닫음(&C)";
            this.chAutoClose.Size = new System.Drawing.Size(250, 19);
            this.chAutoClose.TabIndex = 30;
            this.chAutoClose.CheckedChanged += new System.EventHandler(this.OnAuthCloseCheckBoxCheckedChanged);
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(8, 293);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(368, 18);
            this.pbDownload.TabIndex = 29;
            // 
            // pbDownFile
            // 
            this.pbDownFile.Location = new System.Drawing.Point(8, 269);
            this.pbDownFile.Name = "pbDownFile";
            this.pbDownFile.Size = new System.Drawing.Size(368, 18);
            this.pbDownFile.TabIndex = 28;
            // 
            // lbTransrate
            // 
            this.lbTransrate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTransrate.Location = new System.Drawing.Point(196, 249);
            this.lbTransrate.Name = "lbTransrate";
            this.lbTransrate.Size = new System.Drawing.Size(180, 14);
            this.lbTransrate.TabIndex = 27;
            // 
            // lbDownTime
            // 
            this.lbDownTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbDownTime.Location = new System.Drawing.Point(8, 249);
            this.lbDownTime.Name = "lbDownTime";
            this.lbDownTime.Size = new System.Drawing.Size(180, 14);
            this.lbDownTime.TabIndex = 26;
            // 
            // lbDownLoc
            // 
            this.lbDownLoc.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbDownLoc.Location = new System.Drawing.Point(8, 229);
            this.lbDownLoc.Name = "lbDownLoc";
            this.lbDownLoc.Size = new System.Drawing.Size(368, 14);
            this.lbDownLoc.TabIndex = 25;
            // 
            // lbRemaind
            // 
            this.lbRemaind.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbRemaind.Location = new System.Drawing.Point(264, 209);
            this.lbRemaind.Name = "lbRemaind";
            this.lbRemaind.Size = new System.Drawing.Size(112, 14);
            this.lbRemaind.TabIndex = 24;
            // 
            // lblRemaind
            // 
            this.lblRemaind.Location = new System.Drawing.Point(202, 209);
            this.lblRemaind.Name = "lblRemaind";
            this.lblRemaind.Size = new System.Drawing.Size(56, 14);
            this.lblRemaind.TabIndex = 23;
            this.lblRemaind.Text = "남은 시간:";
            // 
            // lbDownSpeed
            // 
            this.lbDownSpeed.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbDownSpeed.Location = new System.Drawing.Point(70, 209);
            this.lbDownSpeed.Name = "lbDownSpeed";
            this.lbDownSpeed.Size = new System.Drawing.Size(112, 14);
            this.lbDownSpeed.TabIndex = 22;
            // 
            // uLabelControl3
            // 
            this.uLabelControl3.Location = new System.Drawing.Point(8, 209);
            this.uLabelControl3.Name = "uLabelControl3";
            this.uLabelControl3.Size = new System.Drawing.Size(56, 14);
            this.uLabelControl3.TabIndex = 21;
            this.uLabelControl3.Text = "전송 속도:";
            // 
            // lbFileCount
            // 
            this.lbFileCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbFileCount.Location = new System.Drawing.Point(44, 7);
            this.lbFileCount.Name = "lbFileCount";
            this.lbFileCount.Size = new System.Drawing.Size(52, 14);
            this.lbFileCount.TabIndex = 17;
            // 
            // uLabelControl1
            // 
            this.uLabelControl1.Location = new System.Drawing.Point(8, 7);
            this.uLabelControl1.Name = "uLabelControl1";
            this.uLabelControl1.Size = new System.Drawing.Size(28, 14);
            this.uLabelControl1.TabIndex = 16;
            this.uLabelControl1.Text = "파일:";
            // 
            // uLabelControl2
            // 
            this.uLabelControl2.Location = new System.Drawing.Point(100, 7);
            this.uLabelControl2.Name = "uLabelControl2";
            this.uLabelControl2.Size = new System.Drawing.Size(28, 14);
            this.uLabelControl2.TabIndex = 18;
            this.uLabelControl2.Text = "파일:";
            // 
            // lbTotalSize
            // 
            this.lbTotalSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTotalSize.Location = new System.Drawing.Point(136, 7);
            this.lbTotalSize.Name = "lbTotalSize";
            this.lbTotalSize.Size = new System.Drawing.Size(116, 14);
            this.lbTotalSize.TabIndex = 19;
            // 
            // MultiDownDialog
            // 
            this.ClientSize = new System.Drawing.Size(384, 354);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.chAutoClose);
            this.Controls.Add(this.pbDownload);
            this.Controls.Add(this.pbDownFile);
            this.Controls.Add(this.lbTransrate);
            this.Controls.Add(this.lbDownTime);
            this.Controls.Add(this.lbDownLoc);
            this.Controls.Add(this.lbRemaind);
            this.Controls.Add(this.lblRemaind);
            this.Controls.Add(this.lbDownSpeed);
            this.Controls.Add(this.uLabelControl3);
            this.Controls.Add(this.filelistGrid);
            this.Controls.Add(this.lbFileCount);
            this.Controls.Add(this.uLabelControl1);
            this.Controls.Add(this.uLabelControl2);
            this.Controls.Add(this.lbTotalSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultiDownDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "파일전송 진행창";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.filelistGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filelistGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chAutoClose.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView filelistGridView;
        private UIC.Win.Control.DVX.uGridControl filelistGrid;
        private UIC.Win.Control.DVX.uSimpleButton btCancel;
        private System.ComponentModel.BackgroundWorker downloadWorker;
        private UIC.Win.Control.DVX.uCheckEdit chAutoClose;
        private UIC.Win.Control.uProgressBar pbDownload;
        private UIC.Win.Control.uProgressBar pbDownFile;
        private UIC.Win.Control.DVX.uLabelControl lbTransrate;
        private UIC.Win.Control.DVX.uLabelControl lbDownTime;
        private UIC.Win.Control.DVX.uLabelControl lbDownLoc;
        private UIC.Win.Control.DVX.uLabelControl lbRemaind;
        private UIC.Win.Control.DVX.uLabelControl lblRemaind;
        private UIC.Win.Control.DVX.uLabelControl lbDownSpeed;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl3;
        private UIC.Win.Control.DVX.uLabelControl lbFileCount;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl1;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl2;
        private UIC.Win.Control.DVX.uLabelControl lbTotalSize;
        private DevExpress.XtraGrid.Columns.GridColumn clmnIndex;
        private DevExpress.XtraGrid.Columns.GridColumn clmnGuid;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCocd;
        private DevExpress.XtraGrid.Columns.GridColumn clmnFileId;
        private DevExpress.XtraGrid.Columns.GridColumn clmnVType;
        private DevExpress.XtraGrid.Columns.GridColumn clmnFilePath;
        private DevExpress.XtraGrid.Columns.GridColumn clmnRName;
        private DevExpress.XtraGrid.Columns.GridColumn clmnStatus;
        private DevExpress.XtraGrid.Columns.GridColumn clmnSavePath;
        private DevExpress.XtraGrid.Columns.GridColumn clmnVSize;
        private DevExpress.XtraGrid.Columns.GridColumn clmnWDate;

    }
}