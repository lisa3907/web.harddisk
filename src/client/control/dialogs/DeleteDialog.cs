using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using uBizSoft.UIC.Win.Control;

using WebHard.WinCtrl.Forms;
using WebHard.WinCtrl.Library;
using WebHard.Proxy;

namespace WebHard.WinCtrl.Dialogs
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class DeleteDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_selectedObjs"></param>
        public DeleteDialog(object[] p_selectedObjs)
        {
            this.m_selectedRows = p_selectedObjs;
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);
        }

        //=========================================================================================
        //
        //=========================================================================================
        private object[] m_selectedRows = null;

        //=========================================================================================
        //
        //=========================================================================================
        private void OnDialogLoad(object sender, EventArgs e)
        {
            //수정필요
            AsyncOperationManager.SynchronizationContext = new WindowsFormsSynchronizationContext();
            this.deleteWorker.RunWorkerAsync();
        }

        private void OnDeleteWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int percent = 0;

            for (int i = 0; i < this.m_selectedRows.Length; i++)
            {
                DataRow row = this.m_selectedRows.GetValue(i) as DataRow;
                DataRow tagrow = row["tag"] as DataRow;

                string guid = tagrow["guid"].ToString();
                string fileid = tagrow["fileid"].ToString();

                DateTime wdate = Convert.ToDateTime(tagrow["wdate"].ToString());

                AppMediator.SINGLETON.DeleteFile(guid, fileid, wdate);

                percent = (int)((double)i / (double)this.m_selectedRows.Length * 100.0);
                worker.ReportProgress(percent);
            }

            percent = 100;
            worker.ReportProgress(percent);
        }

        private void OnDeleteWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void OnDeleteWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}