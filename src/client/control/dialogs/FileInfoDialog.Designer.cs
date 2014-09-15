namespace WebHard.WinCtrl.Dialogs
{
    partial class FileInfoDialog
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
            this.uLabelControl1 = new UIC.Win.Control.DVX.uLabelControl();
            this.uLabelControl2 = new UIC.Win.Control.DVX.uLabelControl();
            this.txtTitle = new UIC.Win.Control.uTextBox();
            this.btnCancel = new UIC.Win.Control.DVX.uSimpleButton();
            this.btnConfirm = new UIC.Win.Control.DVX.uSimpleButton();
            this.txtContent = new UIC.Win.Control.DVX.uMemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // uLabelControl1
            // 
            this.uLabelControl1.Location = new System.Drawing.Point(12, 7);
            this.uLabelControl1.Name = "uLabelControl1";
            this.uLabelControl1.Size = new System.Drawing.Size(24, 14);
            this.uLabelControl1.TabIndex = 0;
            this.uLabelControl1.Text = "제목";
            // 
            // uLabelControl2
            // 
            this.uLabelControl2.Location = new System.Drawing.Point(12, 64);
            this.uLabelControl2.Name = "uLabelControl2";
            this.uLabelControl2.Size = new System.Drawing.Size(24, 14);
            this.uLabelControl2.TabIndex = 2;
            this.uLabelControl2.Text = "설명";
            // 
            // tbTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(10, 27);
            this.txtTitle.Name = "tbTitle";
            this.txtTitle.Size = new System.Drawing.Size(355, 21);
            this.txtTitle.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(290, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 20);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "취소";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Appearance.Options.UseFont = true;
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(215, 306);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 20);
            this.btnConfirm.TabIndex = 32;
            this.btnConfirm.Text = "확인";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(10, 85);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(355, 210);
            this.txtContent.TabIndex = 33;
            // 
            // FileInfoDialog
            // 
            this.ClientSize = new System.Drawing.Size(374, 334);
            this.ControlBox = false;
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.uLabelControl2);
            this.Controls.Add(this.uLabelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FileInfoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "파일 정보";
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UIC.Win.Control.DVX.uLabelControl uLabelControl1;
        private UIC.Win.Control.DVX.uLabelControl uLabelControl2;
        private UIC.Win.Control.uTextBox txtTitle;
        private UIC.Win.Control.DVX.uSimpleButton btnCancel;
        private UIC.Win.Control.DVX.uSimpleButton btnConfirm;
        private UIC.Win.Control.DVX.uMemoEdit txtContent;
    }
}