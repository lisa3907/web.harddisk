namespace WebHard.WinCtrl.Forms
{
    partial class UploadDialog
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
            this.clmnVName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnVType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnRName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnFPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnVSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.filelistGrid = new UIC.Win.Control.DVX.uGridControl();
            this.btCancel = new UIC.Win.Control.DVX.uSimpleButton();
            this.uploadWorker = new System.ComponentModel.BackgroundWorker();
            this.cbAutoClose = new UIC.Win.Control.DVX.uCheckEdit();
            this.pbEntireFiles = new UIC.Win.Control.uProgressBar();
            this.pbCurrentFile = new UIC.Win.Control.uProgressBar();
            this.lbTransrate = new UIC.Win.Control.DVX.uLabelControl();
            this.lbThroughTime = new UIC.Win.Control.DVX.uLabelControl();
            this.lbLocation = new UIC.Win.Control.DVX.uLabelControl();
            this.lbRemainder = new UIC.Win.Control.DVX.uLabelControl();
            this.lblRemaind = new UIC.Win.Control.DVX.uLabelControl();
            this.lbSpeed = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl3 = new UIC.Win.Control.DVX.uLabelControl();
            this.lbFileCount = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl1 = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl2 = new UIC.Win.Control.DVX.uLabelControl();
            this.lbTotalSize = new UIC.Win.Control.DVX.uLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.filelistGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filelistGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAutoClose.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // filelistGridView
            // 
            this.filelistGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmnIndex,
            this.clmnVName,
            this.clmnVType,
            this.clmnRName,
            this.clmnStatus,
            this.clmnFPath,
            this.clmnVSize});
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
            // clmnVName
            // 
            this.clmnVName.Caption = "vname";
            this.clmnVName.FieldName = "vname";
            this.clmnVName.Name = "clmnVName";
            // 
            // clmnVType
            // 
            this.clmnVType.Caption = "vtype";
            this.clmnVType.FieldName = "vtype";
            this.clmnVType.Name = "clmnVType";
            // 
            // clmnRName
            // 
            this.clmnRName.Caption = "파일명";
            this.clmnRName.FieldName = "rname";
            this.clmnRName.Name = "clmnRName";
            this.clmnRName.Visible = true;
            this.clmnRName.VisibleIndex = 0;
            this.clmnRName.Width = 128;
            // 
            // clmnStatus
            // 
            this.clmnStatus.Caption = "전송상황";
            this.clmnStatus.FieldName = "status";
            this.clmnStatus.Name = "clmnStatus";
            this.clmnStatus.Visible = true;
            this.clmnStatus.VisibleIndex = 1;
            this.clmnStatus.Width = 84;
            // 
            // clmnFPath
            // 
            this.clmnFPath.Caption = "경로";
            this.clmnFPath.FieldName = "fpath";
            this.clmnFPath.Name = "clmnFPath";
            this.clmnFPath.Visible = true;
            this.clmnFPath.VisibleIndex = 2;
            this.clmnFPath.Width = 65;
            // 
            // clmnVSize
            // 
            this.clmnVSize.Caption = "크기";
            this.clmnVSize.FieldName = "vsize";
            this.clmnVSize.Name = "clmnVSize";
            this.clmnVSize.Visible = true;
            this.clmnVSize.VisibleIndex = 3;
            this.clmnVSize.Width = 70;
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
            // uploadWorker
            // 
            this.uploadWorker.WorkerReportsProgress = true;
            this.uploadWorker.WorkerSupportsCancellation = true;
            this.uploadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnUploadWorkerDoWork);
            this.uploadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnUploadWorkerRunWorkerCompleted);
            this.uploadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnUploadWorkerProgressChanged);
            // 
            // cbAutoClose
            // 
            this.cbAutoClose.Location = new System.Drawing.Point(8, 317);
            this.cbAutoClose.Name = "cbAutoClose";
            this.cbAutoClose.Properties.Caption = "업로드가 완료되면 대화 상자를 닫음(&C)";
            this.cbAutoClose.Size = new System.Drawing.Size(250, 19);
            this.cbAutoClose.TabIndex = 30;
            this.cbAutoClose.CheckedChanged += new System.EventHandler(this.OnAutoCloseCheckBoxCheckedChanged);
            // 
            // pbEntireFiles
            // 
            this.pbEntireFiles.Location = new System.Drawing.Point(8, 293);
            this.pbEntireFiles.Name = "pbEntireFiles";
            this.pbEntireFiles.Size = new System.Drawing.Size(368, 18);
            this.pbEntireFiles.TabIndex = 29;
            // 
            // pbCurrentFile
            // 
            this.pbCurrentFile.Location = new System.Drawing.Point(8, 269);
            this.pbCurrentFile.Name = "pbCurrentFile";
            this.pbCurrentFile.Size = new System.Drawing.Size(368, 18);
            this.pbCurrentFile.TabIndex = 28;
            // 
            // lbTransrate
            // 
            this.lbTransrate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTransrate.Location = new System.Drawing.Point(196, 249);
            this.lbTransrate.Name = "lbTransrate";
            this.lbTransrate.Size = new System.Drawing.Size(180, 14);
            this.lbTransrate.TabIndex = 27;
            // 
            // lbThroughTime
            // 
            this.lbThroughTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbThroughTime.Location = new System.Drawing.Point(8, 249);
            this.lbThroughTime.Name = "lbThroughTime";
            this.lbThroughTime.Size = new System.Drawing.Size(180, 14);
            this.lbThroughTime.TabIndex = 26;
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbLocation.Location = new System.Drawing.Point(8, 229);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(368, 14);
            this.lbLocation.TabIndex = 25;
            // 
            // lbRemainder
            // 
            this.lbRemainder.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbRemainder.Location = new System.Drawing.Point(264, 209);
            this.lbRemainder.Name = "lbRemainder";
            this.lbRemainder.Size = new System.Drawing.Size(112, 14);
            this.lbRemainder.TabIndex = 24;
            // 
            // lblRemaind
            // 
            this.lblRemaind.Location = new System.Drawing.Point(202, 209);
            this.lblRemaind.Name = "lblRemaind";
            this.lblRemaind.Size = new System.Drawing.Size(56, 14);
            this.lblRemaind.TabIndex = 23;
            this.lblRemaind.Text = "남은 시간:";
            // 
            // lbSpeed
            // 
            this.lbSpeed.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbSpeed.Location = new System.Drawing.Point(70, 209);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(112, 14);
            this.lbSpeed.TabIndex = 22;
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
            // UploadDialog
            // 
            this.ClientSize = new System.Drawing.Size(384, 354);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.cbAutoClose);
            this.Controls.Add(this.pbEntireFiles);
            this.Controls.Add(this.pbCurrentFile);
            this.Controls.Add(this.lbTransrate);
            this.Controls.Add(this.lbThroughTime);
            this.Controls.Add(this.lbLocation);
            this.Controls.Add(this.lbRemainder);
            this.Controls.Add(this.lblRemaind);
            this.Controls.Add(this.lbSpeed);
            this.Controls.Add(this.uLabelControl3);
            this.Controls.Add(this.filelistGrid);
            this.Controls.Add(this.lbFileCount);
            this.Controls.Add(this.uLabelControl1);
            this.Controls.Add(this.uLabelControl2);
            this.Controls.Add(this.lbTotalSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UploadDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "파일전송 진행창";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.filelistGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filelistGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAutoClose.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView filelistGridView;
        private UIC.Win.Control.DVX.uGridControl filelistGrid;
        private UIC.Win.Control.DVX.uSimpleButton btCancel;
        private System.ComponentModel.BackgroundWorker uploadWorker;
        private UIC.Win.Control.DVX.uCheckEdit cbAutoClose;
        private UIC.Win.Control.uProgressBar pbEntireFiles;
        private UIC.Win.Control.uProgressBar pbCurrentFile;
        private UIC.Win.Control.DVX.uLabelControl lbTransrate;
        private UIC.Win.Control.DVX.uLabelControl lbThroughTime;
        private UIC.Win.Control.DVX.uLabelControl lbLocation;
        private UIC.Win.Control.DVX.uLabelControl lbRemainder;
        private UIC.Win.Control.DVX.uLabelControl lblRemaind;
        private UIC.Win.Control.DVX.uLabelControl lbSpeed;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl3;
        private UIC.Win.Control.DVX.uLabelControl lbFileCount;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl1;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl2;
        private UIC.Win.Control.DVX.uLabelControl lbTotalSize;
        private DevExpress.XtraGrid.Columns.GridColumn clmnIndex;
        private DevExpress.XtraGrid.Columns.GridColumn clmnVName;
        private DevExpress.XtraGrid.Columns.GridColumn clmnVType;
        private DevExpress.XtraGrid.Columns.GridColumn clmnRName;
        private DevExpress.XtraGrid.Columns.GridColumn clmnStatus;
        private DevExpress.XtraGrid.Columns.GridColumn clmnFPath;
        private DevExpress.XtraGrid.Columns.GridColumn clmnVSize;
    }
}