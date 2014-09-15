namespace WebHard.WinCtrl.Dialogs
{
    partial class RightsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RightsDialog));
            this.lblTitle = new uBizSoft.UIC.Win.Control.DVX.uLabelControl();
            this.authGrid = new uBizSoft.UIC.Win.Control.DVX.uGridControl();
            this.authGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmnCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnControl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCModify = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCRead = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCView = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCFolder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmnCFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAdd = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.authlistGrid = new uBizSoft.UIC.Win.Control.DVX.uGridControl();
            this.authlistGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCancel = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.btnOK = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.lvHeadIcon = new System.Windows.Forms.ImageList(this.components);
            this.iltreeview = new System.Windows.Forms.ImageList(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bblDelete = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu = new uBizSoft.UIC.Win.Control.DVX.uPopupMenu();
            this.btnDelete = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.authGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authlistGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authlistGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(12, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 14);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "그룹 혹은 멤버를 추가하여 주십시오.";
            // 
            // authGrid
            // 
            this.authGrid.EmbeddedNavigator.Name = "";
            this.authGrid.Location = new System.Drawing.Point(12, 27);
            this.authGrid.MainView = this.authGridView;
            this.authGrid.Name = "authGrid";
            this.authGrid.Size = new System.Drawing.Size(366, 172);
            this.authGrid.TabIndex = 1;
            this.authGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.authGridView});
            // 
            // authGridView
            // 
            this.authGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmnCode,
            this.clmnType,
            this.clmnName,
            this.clmnControl,
            this.clmnCModify,
            this.clmnCRead,
            this.clmnCDelete,
            this.clmnCView,
            this.clmnCFolder,
            this.clmnCFile});
            this.authGridView.GridControl = this.authGrid;
            this.authGridView.Name = "authGridView";
            this.authGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.authGridView.OptionsBehavior.Editable = false;
            this.authGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.authGridView.OptionsView.ShowGroupPanel = false;
            this.authGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.clmnType, DevExpress.Data.ColumnSortOrder.Descending)});
            this.authGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnAuthGridViewSelectedIndexChanged);
            this.authGridView.DoubleClick += new System.EventHandler(this.OnAuthGridViewDoubleClick);
            this.authGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.OnAuthGridViewCustomDrawCell);
            // 
            // clmnCode
            // 
            this.clmnCode.Caption = "member";
            this.clmnCode.FieldName = "member";
            this.clmnCode.Name = "clmnCode";
            // 
            // clmnType
            // 
            this.clmnType.Caption = "mtype";
            this.clmnType.FieldName = "mtype";
            this.clmnType.Name = "clmnType";
            this.clmnType.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            // 
            // clmnName
            // 
            this.clmnName.Caption = "이름";
            this.clmnName.FieldName = "name";
            this.clmnName.Name = "clmnName";
            this.clmnName.OptionsFilter.AllowFilter = false;
            this.clmnName.Visible = true;
            this.clmnName.VisibleIndex = 0;
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
            this.clmnCView.FieldName = "view";
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
            this.btnAdd.Location = new System.Drawing.Point(222, 205);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 20);
            this.btnAdd.TabIndex = 33;
            this.btnAdd.Text = "추가";
            this.btnAdd.Click += new System.EventHandler(this.OnAddButtonClick);
            // 
            // authlistGrid
            // 
            this.authlistGrid.EmbeddedNavigator.Name = "";
            this.authlistGrid.Location = new System.Drawing.Point(12, 231);
            this.authlistGrid.MainView = this.authlistGridView;
            this.authlistGrid.Name = "authlistGrid";
            this.authlistGrid.Size = new System.Drawing.Size(366, 177);
            this.authlistGrid.TabIndex = 34;
            this.authlistGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.authlistGridView});
            // 
            // authlistGridView
            // 
            this.authlistGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn12,
            this.gridColumn11,
            this.gridColumn20,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn19,
            this.gridColumn17,
            this.gridColumn18});
            this.authlistGridView.GridControl = this.authlistGrid;
            this.authlistGridView.Name = "authlistGridView";
            this.authlistGridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.authlistGridView.OptionsBehavior.Editable = false;
            this.authlistGridView.OptionsFilter.AllowFilterEditor = false;
            this.authlistGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.authlistGridView.OptionsView.ShowGroupPanel = false;
            this.authlistGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn11, DevExpress.Data.ColumnSortOrder.Descending)});
            this.authlistGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnAuthListGridViewSelectedIndexChanged);
            this.authlistGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnAuthListGridViewKeyUp);
            this.authlistGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnAuthListGridViewMouseUp);
            this.authlistGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.OnAuthListGridViewCustomDrawCell);
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "member";
            this.gridColumn12.FieldName = "member";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "mtype";
            this.gridColumn11.FieldName = "mtype";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "이름";
            this.gridColumn20.FieldName = "name";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsFilter.AllowFilter = false;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 0;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "관리권한";
            this.gridColumn13.FieldName = "control";
            this.gridColumn13.Name = "gridColumn13";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "변경";
            this.gridColumn14.FieldName = "cmodify";
            this.gridColumn14.Name = "gridColumn14";
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "읽기";
            this.gridColumn15.FieldName = "cread";
            this.gridColumn15.Name = "gridColumn15";
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "삭제";
            this.gridColumn16.FieldName = "cdelete";
            this.gridColumn16.Name = "gridColumn16";
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "보기";
            this.gridColumn19.FieldName = "cview";
            this.gridColumn19.Name = "gridColumn19";
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "폴더작성";
            this.gridColumn17.FieldName = "cfolder";
            this.gridColumn17.Name = "gridColumn17";
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "파일작성";
            this.gridColumn18.FieldName = "cfile";
            this.gridColumn18.Name = "gridColumn18";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(303, 414);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 20);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "취소";
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(222, 414);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 20);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "확인";
            this.btnOK.Click += new System.EventHandler(this.OnOkButtonClick);
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
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imageList;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bblDelete});
            this.barManager.MaxItemId = 2;
            // 
            // bblDelete
            // 
            this.bblDelete.Caption = "삭제";
            this.bblDelete.Id = 1;
            this.bblDelete.ImageIndex = 0;
            this.bblDelete.ImageIndexDisabled = 0;
            this.bblDelete.Name = "bblDelete";
            this.bblDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnDeleteMenuItemClick);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bblDelete)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Location = new System.Drawing.Point(303, 205);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 20);
            this.btnDelete.TabIndex = 36;
            this.btnDelete.Text = "삭제";
            this.btnDelete.Click += new System.EventHandler(this.OnDeleteButtonClick);
            // 
            // RightsDialog
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(390, 441);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.authlistGrid);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.authGrid);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "RightsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "공유 권한 설정";
            ((System.ComponentModel.ISupportInitialize)(this.authGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authlistGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authlistGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private uBizSoft.UIC.Win.Control.DVX.uLabelControl lblTitle;
        private uBizSoft.UIC.Win.Control.DVX.uGridControl authGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView authGridView;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnAdd;
        private uBizSoft.UIC.Win.Control.DVX.uGridControl authlistGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView authlistGridView;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnCancel;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnOK;
        private System.Windows.Forms.ImageList lvHeadIcon;
        private System.Windows.Forms.ImageList iltreeview;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraGrid.Columns.GridColumn clmnType;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCode;
        private DevExpress.XtraGrid.Columns.GridColumn clmnControl;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCModify;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCRead;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCDelete;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCFolder;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCFile;
        private DevExpress.XtraGrid.Columns.GridColumn clmnCView;
        private DevExpress.XtraGrid.Columns.GridColumn clmnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private uBizSoft.UIC.Win.Control.DVX.uPopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem bblDelete;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnDelete;
    }
}