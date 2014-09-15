namespace WebHard.WinCtrl.Dialogs
{
    partial class FolderListDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderListDialog));
            this.lbTitle = new UIC.Win.Control.DVX.uLabelControl();
            this.panelMain = new UIC.Win.Control.DVX.uPanelControl();
            this.tvFolders = new UIC.Win.Control.uTreeView();
            this.iltreeview = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new UIC.Win.Control.DVX.uSimpleButton();
            this.btnCancel = new UIC.Win.Control.DVX.uSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.Location = new System.Drawing.Point(10, 7);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(172, 14);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "선택한 폴더를 다음 폴더로 이동";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tvFolders);
            this.panelMain.Location = new System.Drawing.Point(10, 27);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(270, 210);
            this.panelMain.TabIndex = 1;
            this.panelMain.Text = "uPanelControl1";
            // 
            // tvFolders
            // 
            this.tvFolders.AllowDrop = true;
            this.tvFolders.Border3DStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.tvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFolders.ImageIndex = 1;
            this.tvFolders.ImageList = this.iltreeview;
            this.tvFolders.Location = new System.Drawing.Point(2, 2);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageIndex = 2;
            this.tvFolders.Size = new System.Drawing.Size(266, 206);
            this.tvFolders.SysImageSize = UIC.Win.Control.Library.SystemImageListSize.SmallIcons;
            this.tvFolders.TabIndex = 0;
            this.tvFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnFolderTreeViewAfterSelect);
            // 
            // iltreeview
            // 
            this.iltreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iltreeview.ImageStream")));
            this.iltreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.iltreeview.Images.SetKeyName(0, "");
            this.iltreeview.Images.SetKeyName(1, "");
            this.iltreeview.Images.SetKeyName(2, "");
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(285, 27);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 23);
            this.btnOK.TabIndex = 32;
            this.btnOK.Text = "확인";
            this.btnOK.Click += new System.EventHandler(this.OnOkButtonClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(285, 53);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 23);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "취소";
            // 
            // FolderListDialog
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(414, 249);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.lbTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FolderListDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "폴더 이동";
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UIC.Win.Control.DVX.uLabelControl lbTitle;
        private UIC.Win.Control.DVX.uPanelControl panelMain;
        private UIC.Win.Control.uTreeView tvFolders;
        private UIC.Win.Control.DVX.uSimpleButton btnOK;
        private UIC.Win.Control.DVX.uSimpleButton btnCancel;
        private System.Windows.Forms.ImageList iltreeview;
    }
}