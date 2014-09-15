namespace WebHard.WinCtrl.Dialogs
{
    partial class AuthListDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthListDialog));
            this.label1 = new uBizSoft.UIC.Win.Control.DVX.uLabelControl();
            this.gridAuth = new uBizSoft.UIC.Win.Control.DVX.uGridControl();
            this.gridAuthView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmnGuid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnMember = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnMType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnControl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCModify = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCRead = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCView = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCFolder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAdd = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.btnDelete = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.gridList = new uBizSoft.UIC.Win.Control.DVX.uGridControl();
            this.gridListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmnRight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnChoice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkbChoice = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cbSub = new uBizSoft.UIC.Win.Control.DVX.uCheckEdit();
            this.cbAllSub = new uBizSoft.UIC.Win.Control.DVX.uCheckEdit();
            this.btnOK = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.btnCancel = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.lvHeadIcon = new System.Windows.Forms.ImageList(this.components);
            this.iltreeview = new System.Windows.Forms.ImageList(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            ((System.ComponentModel.ISupportInitialize)(this.gridAuth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAuthView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkbChoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSub.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAllSub.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "권한 설정 할 대상을 조직정보에서 추가하여 주십시오.";
            // 
            // gridAuth
            // 
            this.gridAuth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gridAuth.EmbeddedNavigator.Name = "";
            this.gridAuth.Location = new System.Drawing.Point(8, 27);
            this.gridAuth.MainView = this.gridAuthView;
            this.gridAuth.Name = "gridAuth";
            this.gridAuth.Size = new System.Drawing.Size(312, 165);
            this.gridAuth.TabIndex = 1;
            this.gridAuth.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridAuthView});
            // 
            // gridAuthView
            // 
            this.gridAuthView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmnGuid,
            this.clmnMember,
            this.clmnMType,
            this.clmnCName,
            this.clmnControl,
            this.clmnCModify,
            this.clmnCRead,
            this.clmnCDelete,
            this.clmnCView,
            this.clmnCFolder,
            this.clmnCFile});
            this.gridAuthView.GridControl = this.gridAuth;
            this.gridAuthView.Name = "gridAuthView";
            this.gridAuthView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridAuthView.OptionsBehavior.Editable = false;
            this.gridAuthView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridAuthView.OptionsView.ShowGroupPanel = false;
            this.gridAuthView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.clmnGuid, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gridAuthView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnAuthGridSelectedIndexChanged);
            this.gridAuthView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.OnAuthGridCustomDrawCell);
            // 
            // clmnGuid
            // 
            this.clmnGuid.Caption = "guid";
            this.clmnGuid.FieldName = "guid";
            this.clmnGuid.Name = "clmnGuid";
            this.clmnGuid.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            // 
            // clmnMember
            // 
            this.clmnMember.Caption = "member";
            this.clmnMember.FieldName = "member";
            this.clmnMember.Name = "clmnMember";
            // 
            // clmnMType
            // 
            this.clmnMType.Caption = "mtype";
            this.clmnMType.FieldName = "mtype";
            this.clmnMType.Name = "clmnMType";
            // 
            // clmnCName
            // 
            this.clmnCName.AppearanceHeader.Options.UseTextOptions = true;
            this.clmnCName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clmnCName.Caption = "이름";
            this.clmnCName.FieldName = "name";
            this.clmnCName.Name = "clmnCName";
            this.clmnCName.OptionsFilter.AllowFilter = false;
            this.clmnCName.Visible = true;
            this.clmnCName.VisibleIndex = 0;
            // 
            // clmnControl
            // 
            this.clmnControl.Caption = "관리권한";
            this.clmnControl.FieldName = "control";
            this.clmnControl.Name = "clmnControl";
            // 
            // clmnCModify
            // 
            this.clmnCModify.Caption = "변경";
            this.clmnCModify.FieldName = "cmodify";
            this.clmnCModify.Name = "clmnCModify";
            // 
            // clmnCRead
            // 
            this.clmnCRead.Caption = "읽기";
            this.clmnCRead.FieldName = "cread";
            this.clmnCRead.Name = "clmnCRead";
            // 
            // clmnCDelete
            // 
            this.clmnCDelete.Caption = "삭제";
            this.clmnCDelete.FieldName = "cdelete";
            this.clmnCDelete.Name = "clmnCDelete";
            // 
            // clmnCView
            // 
            this.clmnCView.Caption = "보기";
            this.clmnCView.FieldName = "cview";
            this.clmnCView.Name = "clmnCView";
            // 
            // clmnCFolder
            // 
            this.clmnCFolder.Caption = "폴더작성";
            this.clmnCFolder.FieldName = "cfolder";
            this.clmnCFolder.Name = "clmnCFolder";
            // 
            // clmnCFile
            // 
            this.clmnCFile.Caption = "파일작성";
            this.clmnCFile.FieldName = "cfile";
            this.clmnCFile.Name = "clmnCFile";
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Location = new System.Drawing.Point(326, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 20);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "추가";
            this.btnAdd.Click += new System.EventHandler(this.OnAddButtonClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Location = new System.Drawing.Point(326, 48);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(62, 20);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "제거";
            this.btnDelete.Click += new System.EventHandler(this.OnDeleteButtonClick);
            // 
            // gridList
            // 
            this.gridList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gridList.EmbeddedNavigator.Name = "";
            this.gridList.Location = new System.Drawing.Point(8, 198);
            this.gridList.MainView = this.gridListView;
            this.gridList.Name = "gridList";
            this.gridList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkbChoice});
            this.gridList.Size = new System.Drawing.Size(380, 235);
            this.gridList.TabIndex = 4;
            this.gridList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridListView});
            // 
            // gridListView
            // 
            this.gridListView.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.gridListView.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridListView.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(193)))), ((int)(((byte)(221)))));
            this.gridListView.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridListView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridListView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridListView.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(183)))), ((int)(((byte)(183)))));
            this.gridListView.Appearance.HorzLine.Options.UseBackColor = true;
            this.gridListView.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridListView.Appearance.Row.Options.UseBackColor = true;
            this.gridListView.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(65)))), ((int)(((byte)(143)))));
            this.gridListView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridListView.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(183)))), ((int)(((byte)(183)))));
            this.gridListView.Appearance.VertLine.Options.UseBackColor = true;
            this.gridListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmnRight,
            this.clmnChoice});
            this.gridListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridListView.GridControl = this.gridList;
            this.gridListView.Name = "gridListView";
            this.gridListView.OptionsCustomization.AllowFilter = false;
            this.gridListView.OptionsMenu.EnableColumnMenu = false;
            this.gridListView.OptionsMenu.EnableFooterMenu = false;
            this.gridListView.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridListView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridListView.OptionsView.EnableAppearanceEvenRow = true;
            this.gridListView.OptionsView.ShowGroupPanel = false;
            this.gridListView.RowHeight = 21;
            this.gridListView.Click += new System.EventHandler(this.OnAuthListGridClick);
            // 
            // clmnRight
            // 
            this.clmnRight.AppearanceHeader.Options.UseTextOptions = true;
            this.clmnRight.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clmnRight.Caption = "권한";
            this.clmnRight.FieldName = "right";
            this.clmnRight.Name = "clmnRight";
            this.clmnRight.OptionsColumn.AllowEdit = false;
            this.clmnRight.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.clmnRight.OptionsFilter.AllowFilter = false;
            this.clmnRight.Visible = true;
            this.clmnRight.VisibleIndex = 0;
            this.clmnRight.Width = 277;
            // 
            // clmnChoice
            // 
            this.clmnChoice.AppearanceHeader.Options.UseTextOptions = true;
            this.clmnChoice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clmnChoice.Caption = "허용";
            this.clmnChoice.ColumnEdit = this.chkbChoice;
            this.clmnChoice.FieldName = "choice";
            this.clmnChoice.Name = "clmnChoice";
            this.clmnChoice.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.clmnChoice.OptionsFilter.AllowFilter = false;
            this.clmnChoice.Visible = true;
            this.clmnChoice.VisibleIndex = 1;
            this.clmnChoice.Width = 82;
            // 
            // chkbChoice
            // 
            this.chkbChoice.AutoHeight = false;
            this.chkbChoice.Name = "chkbChoice";
            this.chkbChoice.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.OnChoiceCheckBoxEditValueChanging);
            // 
            // cbSub
            // 
            this.cbSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSub.Location = new System.Drawing.Point(8, 448);
            this.cbSub.Name = "cbSub";
            this.cbSub.Properties.Caption = "이 폴더 내의 모든 파일의 권한 설정까지 변경.";
            this.cbSub.Size = new System.Drawing.Size(380, 19);
            this.cbSub.TabIndex = 5;
            // 
            // cbAllSub
            // 
            this.cbAllSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAllSub.Location = new System.Drawing.Point(8, 468);
            this.cbAllSub.Name = "cbAllSub";
            this.cbAllSub.Properties.Caption = "하위 모든 폴더/파일의 권한 설정까지 변경.";
            this.cbAllSub.Size = new System.Drawing.Size(263, 19);
            this.cbAllSub.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(263, 487);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 20);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "확인";
            this.btnOK.Click += new System.EventHandler(this.OnOkButtonClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(326, 487);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 20);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "취소";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            // 
            // lvHeadIcon
            // 
            this.lvHeadIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lvHeadIcon.ImageStream")));
            this.lvHeadIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.lvHeadIcon.Images.SetKeyName(0, "");
            this.lvHeadIcon.Images.SetKeyName(1, "");
            // 
            // iltreeview
            // 
            this.iltreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iltreeview.ImageStream")));
            this.iltreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.iltreeview.Images.SetKeyName(0, "");
            this.iltreeview.Images.SetKeyName(1, "");
            this.iltreeview.Images.SetKeyName(2, "");
            this.iltreeview.Images.SetKeyName(3, "");
            this.iltreeview.Images.SetKeyName(4, "");
            this.iltreeview.Images.SetKeyName(5, "");
            this.iltreeview.Images.SetKeyName(6, "");
            this.iltreeview.Images.SetKeyName(7, "");
            this.iltreeview.Images.SetKeyName(8, "");
            this.iltreeview.Images.SetKeyName(9, "");
            this.iltreeview.Images.SetKeyName(10, "");
            this.iltreeview.Images.SetKeyName(11, "");
            this.iltreeview.Images.SetKeyName(12, "");
            // 
            // AuthListDialog
            // 
            this.ClientSize = new System.Drawing.Size(404, 514);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbAllSub);
            this.Controls.Add(this.cbSub);
            this.Controls.Add(this.gridList);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gridAuth);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 800);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 400);
            this.Name = "AuthListDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "공유 권한 설정";
            ((System.ComponentModel.ISupportInitialize)(this.gridAuth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAuthView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkbChoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSub.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAllSub.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private uBizSoft.UIC.Win.Control.DVX.uLabelControl label1;
        private uBizSoft.UIC.Win.Control.DVX.uGridControl gridAuth;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnAdd;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnDelete;
        private uBizSoft.UIC.Win.Control.DVX.uGridControl gridList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridListView;
        private uBizSoft.UIC.Win.Control.DVX.uCheckEdit cbSub;
        private uBizSoft.UIC.Win.Control.DVX.uCheckEdit cbAllSub;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnOK;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnCancel;
        public DevExpress.XtraGrid.Views.Grid.GridView gridAuthView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ImageList lvHeadIcon;
        private System.Windows.Forms.ImageList iltreeview;
        private System.Windows.Forms.ContextMenu contextMenu;
        private DevExpress.XtraGrid.Columns.GridColumn clmnGuid;
        private DevExpress.XtraGrid.Columns.GridColumn clmnMember;
        private DevExpress.XtraGrid.Columns.GridColumn clmnControl;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCModify;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCRead;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCDelete;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCFolder;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCFile;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCView;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkbChoice;
        private DevExpress.XtraGrid.Columns.GridColumn clmnRight;
        private DevExpress.XtraGrid.Columns.GridColumn clmnChoice;
        private DevExpress.XtraGrid.Columns.GridColumn clmnMType;
    }
}