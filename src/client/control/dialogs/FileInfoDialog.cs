using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using DevExpress.XtraEditors;

using WebHard.WinCtrl.Library;
using WebHard.Proxy;

namespace WebHard.WinCtrl.Dialogs
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class FileInfoDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="fileName"></param>
        public FileInfoDialog(string fileName)
        {
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);

            this.txtTitle.Text = fileName;
            this.txtContent.Text = fileName;

            this.txtTitle.Focus();
            this.Text = fileName + ": Enter information";
        }

        /// <summary></summary>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        public FileInfoDialog(string fileName, string title, string desc)
        {
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);

            this.txtTitle.Text = title;
            this.txtContent.Text = desc;

            this.txtContent.Focus();
            this.Text = fileName + ": Enter information";
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public string Content
        {
            get
            {
                return this.txtContent.Text;
            }
        }

        /// <summary></summary>
        public string Title
        {
            get
            {
                return this.txtTitle.Text;
            }
        }
    }
}