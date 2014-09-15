namespace WebHard.WinCtrl.Dialogs
{
    partial class ConfirmDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmDialog));
            this.uPictureBox1 = new uBizSoft.UIC.Win.Control.DVX.uPictureEdit();
            this.lbTitle = new uBizSoft.UIC.Win.Control.DVX.uLabelControl();
            this.label1 = new uBizSoft.UIC.Win.Control.DVX.uLabelControl();
            this.pbBefore = new uBizSoft.UIC.Win.Control.DVX.uPictureEdit();
            this.lbBeforeFileSize = new uBizSoft.UIC.Win.Control.DVX.uLabelControl();
            this.lbBeforeFileWriteDate = new uBizSoft.UIC.Win.Control.DVX.uLabelControl();
            this.label2 = new uBizSoft.UIC.Win.Control.DVX.uLabelControl();
            this.pbAfter = new uBizSoft.UIC.Win.Control.DVX.uPictureEdit();
            this.lbAfterFileWriteDate = new uBizSoft.UIC.Win.Control.DVX.uLabelControl();
            this.lbAfterFileSize = new uBizSoft.UIC.Win.Control.DVX.uLabelControl();
            this.btnOK = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.btnAllOk = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.btnCancel = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.btnAllCancel = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            this.btnClose = new uBizSoft.UIC.Win.Control.DVX.uSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.uPictureBox1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBefore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAfter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // uPictureBox1
            // 
            this.uPictureBox1.EditValue = ((object)(resources.GetObject("uPictureBox1.EditValue")));
            this.uPictureBox1.Location = new System.Drawing.Point(22, 17);
            this.uPictureBox1.Name = "uPictureBox1";
            this.uPictureBox1.Size = new System.Drawing.Size(30, 30);
            this.uPictureBox1.TabIndex = 0;
            // 
            // lbTitle
            // 
            this.lbTitle.Appearance.Options.UseTextOptions = true;
            this.lbTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lbTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTitle.Location = new System.Drawing.Point(76, 17);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(404, 38);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "이 폴더에 이미";
            // 
            // label1
            // 
            this.label1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label1.Location = new System.Drawing.Point(76, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "기존파일을";
            // 
            // pbBefore
            // 
            this.pbBefore.EditValue = ((object)(resources.GetObject("pbBefore.EditValue")));
            this.pbBefore.Location = new System.Drawing.Point(76, 76);
            this.pbBefore.Name = "pbBefore";
            this.pbBefore.Size = new System.Drawing.Size(30, 30);
            this.pbBefore.TabIndex = 3;
            // 
            // lbBeforeFileSize
            // 
            this.lbBeforeFileSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbBeforeFileSize.Location = new System.Drawing.Point(135, 76);
            this.lbBeforeFileSize.Name = "lbBeforeFileSize";
            this.lbBeforeFileSize.Size = new System.Drawing.Size(341, 14);
            this.lbBeforeFileSize.TabIndex = 4;
            this.lbBeforeFileSize.Text = "기존파일을";
            // 
            // lbBeforeFileWriteDate
            // 
            this.lbBeforeFileWriteDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbBeforeFileWriteDate.Location = new System.Drawing.Point(135, 92);
            this.lbBeforeFileWriteDate.Name = "lbBeforeFileWriteDate";
            this.lbBeforeFileWriteDate.Size = new System.Drawing.Size(341, 14);
            this.lbBeforeFileWriteDate.TabIndex = 5;
            this.lbBeforeFileWriteDate.Text = "기존파일을";
            // 
            // label2
            // 
            this.label2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label2.Location = new System.Drawing.Point(76, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "이 파일로 바꾸시겠습니까?";
            // 
            // pbAfter
            // 
            this.pbAfter.EditValue = ((object)(resources.GetObject("pbAfter.EditValue")));
            this.pbAfter.Location = new System.Drawing.Point(76, 132);
            this.pbAfter.Name = "pbAfter";
            this.pbAfter.Size = new System.Drawing.Size(30, 30);
            this.pbAfter.TabIndex = 3;
            // 
            // lbAfterFileWriteDate
            // 
            this.lbAfterFileWriteDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbAfterFileWriteDate.Location = new System.Drawing.Point(135, 148);
            this.lbAfterFileWriteDate.Name = "lbAfterFileWriteDate";
            this.lbAfterFileWriteDate.Size = new System.Drawing.Size(341, 14);
            this.lbAfterFileWriteDate.TabIndex = 8;
            this.lbAfterFileWriteDate.Text = "기존파일을";
            // 
            // lbAfterFileSize
            // 
            this.lbAfterFileSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbAfterFileSize.Location = new System.Drawing.Point(135, 132);
            this.lbAfterFileSize.Name = "lbAfterFileSize";
            this.lbAfterFileSize.Size = new System.Drawing.Size(341, 14);
            this.lbAfterFileSize.TabIndex = 7;
            this.lbAfterFileSize.Text = "기존파일을";
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnOK.Location = new System.Drawing.Point(7, 178);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 20);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "예(&Y)";
            // 
            // btnAllOk
            // 
            this.btnAllOk.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllOk.Appearance.Options.UseFont = true;
            this.btnAllOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAllOk.Location = new System.Drawing.Point(102, 178);
            this.btnAllOk.Name = "btnAllOk";
            this.btnAllOk.Size = new System.Drawing.Size(94, 20);
            this.btnAllOk.TabIndex = 9;
            this.btnAllOk.Text = "모두 예(&A)";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnCancel.Location = new System.Drawing.Point(197, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 20);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "아니오(&N)";
            // 
            // btnAllCancel
            // 
            this.btnAllCancel.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllCancel.Appearance.Options.UseFont = true;
            this.btnAllCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAllCancel.Location = new System.Drawing.Point(292, 178);
            this.btnAllCancel.Name = "btnAllCancel";
            this.btnAllCancel.Size = new System.Drawing.Size(94, 20);
            this.btnAllCancel.TabIndex = 9;
            this.btnAllCancel.Text = "모두 아니오(&X)";
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.btnClose.Location = new System.Drawing.Point(387, 178);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 20);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "취소(&C)";
            // 
            // ConfirmDialog
            // 
            this.ClientSize = new System.Drawing.Size(488, 210);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAllCancel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAllOk);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbAfterFileWriteDate);
            this.Controls.Add(this.lbAfterFileSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbBeforeFileWriteDate);
            this.Controls.Add(this.lbBeforeFileSize);
            this.Controls.Add(this.pbAfter);
            this.Controls.Add(this.pbBefore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.uPictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfirmDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "파일 바꾸기 확인";
            ((System.ComponentModel.ISupportInitialize)(this.uPictureBox1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBefore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAfter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private uBizSoft.UIC.Win.Control.DVX.uPictureEdit uPictureBox1;
        private uBizSoft.UIC.Win.Control.DVX.uLabelControl lbTitle;
        private uBizSoft.UIC.Win.Control.DVX.uLabelControl label1;
        private uBizSoft.UIC.Win.Control.DVX.uPictureEdit pbBefore;
        private uBizSoft.UIC.Win.Control.DVX.uLabelControl lbBeforeFileSize;
        private uBizSoft.UIC.Win.Control.DVX.uLabelControl lbBeforeFileWriteDate;
        private uBizSoft.UIC.Win.Control.DVX.uLabelControl label2;
        private uBizSoft.UIC.Win.Control.DVX.uPictureEdit pbAfter;
        private uBizSoft.UIC.Win.Control.DVX.uLabelControl lbAfterFileWriteDate;
        private uBizSoft.UIC.Win.Control.DVX.uLabelControl lbAfterFileSize;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnOK;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnAllOk;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnCancel;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnAllCancel;
        private uBizSoft.UIC.Win.Control.DVX.uSimpleButton btnClose;
    }
}