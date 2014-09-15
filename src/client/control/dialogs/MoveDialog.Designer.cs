namespace WebHard.WinCtrl.Dialogs
{
    partial class MoveDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveDialog));
            this.pbSendProgress = new UIC.Win.Control.DVX.uPictureEdit();
            this.pbSendImage = new UIC.Win.Control.DVX.uPictureEdit();
            this.moveWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pbSendProgress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSendImage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSendProgress
            // 
            this.pbSendProgress.EditValue = ((object)(resources.GetObject("pbSendProgress.EditValue")));
            this.pbSendProgress.Location = new System.Drawing.Point(10, 72);
            this.pbSendProgress.Name = "pbSendProgress";
            this.pbSendProgress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pbSendProgress.Size = new System.Drawing.Size(173, 10);
            this.pbSendProgress.TabIndex = 3;
            this.pbSendProgress.UseWaitCursor = true;
            // 
            // pbSendImage
            // 
            this.pbSendImage.EditValue = ((object)(resources.GetObject("pbSendImage.EditValue")));
            this.pbSendImage.Location = new System.Drawing.Point(20, 11);
            this.pbSendImage.Name = "pbSendImage";
            this.pbSendImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pbSendImage.Size = new System.Drawing.Size(152, 55);
            this.pbSendImage.TabIndex = 2;
            this.pbSendImage.UseWaitCursor = true;
            // 
            // MoveWorker
            // 
            this.moveWorker.WorkerReportsProgress = true;
            this.moveWorker.WorkerSupportsCancellation = true;
            this.moveWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnMoveWorkerDoWork);
            this.moveWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnMoveWorkerRunWorkerCompleted);
            this.moveWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnMoveWorkerProgressChanged);
            // 
            // MoveDialog
            // 
            this.ClientSize = new System.Drawing.Size(192, 93);
            this.ControlBox = false;
            this.Controls.Add(this.pbSendProgress);
            this.Controls.Add(this.pbSendImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ã³¸®Áß...";
            this.Load += new System.EventHandler(this.OnDialogLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pbSendProgress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSendImage.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIC.Win.Control.DVX.uPictureEdit pbSendProgress;
        private UIC.Win.Control.DVX.uPictureEdit pbSendImage;
        private System.ComponentModel.BackgroundWorker moveWorker;
    }
}