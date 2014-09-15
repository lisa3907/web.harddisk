namespace WebHard.WinCtrl.Dialogs
{
    partial class DeleteDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteDialog));
            this.pbSendImage = new uBizSoft.UIC.Win.Control.DVX.uPictureEdit();
            this.pbSendProgress = new uBizSoft.UIC.Win.Control.DVX.uPictureEdit();
            this.deleteWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pbSendImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSendProgress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSendImage
            // 
            this.pbSendImage.EditValue = ((object)(resources.GetObject("pbSendImage.EditValue")));
            this.pbSendImage.Location = new System.Drawing.Point(21, 12);
            this.pbSendImage.Name = "pbSendImage";
            this.pbSendImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pbSendImage.Size = new System.Drawing.Size(152, 55);
            this.pbSendImage.TabIndex = 0;
            this.pbSendImage.UseWaitCursor = true;
            // 
            // pbSendProgress
            // 
            this.pbSendProgress.EditValue = ((object)(resources.GetObject("pbSendProgress.EditValue")));
            this.pbSendProgress.Location = new System.Drawing.Point(9, 73);
            this.pbSendProgress.Name = "pbSendProgress";
            this.pbSendProgress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pbSendProgress.Size = new System.Drawing.Size(173, 10);
            this.pbSendProgress.TabIndex = 1;
            this.pbSendProgress.UseWaitCursor = true;
            // 
            // RemoveWorker
            // 
            this.deleteWorker.WorkerReportsProgress = true;
            this.deleteWorker.WorkerSupportsCancellation = true;
            this.deleteWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnDeleteWorkerDoWork);
            this.deleteWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnDeleteWorkerRunWorkerCompleted);
            this.deleteWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnDeleteWorkerProgressChanged);
            // 
            // fDelete
            // 
            this.ClientSize = new System.Drawing.Size(194, 95);
            this.ControlBox = false;
            this.Controls.Add(this.pbSendProgress);
            this.Controls.Add(this.pbSendImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fDelete";
            this.Text = "Ã³¸®Áß...";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.OnDialogLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pbSendImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSendProgress.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private uBizSoft.UIC.Win.Control.DVX.uPictureEdit pbSendImage;
        private uBizSoft.UIC.Win.Control.DVX.uPictureEdit pbSendProgress;
        private System.ComponentModel.BackgroundWorker deleteWorker;
    }
}