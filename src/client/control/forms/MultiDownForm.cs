using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using uBizSoft.LIB.Logging;
using uBizSoft.UIC.Win.Control;
using WebHard.WinCtrl.Library;
using WebHard.Proxy;

namespace WebHard.WinCtrl.Forms
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class MultiDownDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public MultiDownDialog()
        {
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);
        }

        /// <summary></summary>
        /// <param name="p_mainBox"></param>
        public MultiDownDialog(MainBox p_mainBox)
            : this()
        {
            g_dnHelper.MainBox = p_mainBox;
        }

        //=========================================================================================
        //
        //=========================================================================================
        private DownloadHelper m_dnHelper = null;
        private DownloadHelper g_dnHelper
        {
            get
            {
                if (m_dnHelper == null)
                    m_dnHelper = new DownloadHelper();
                return m_dnHelper;
            }
        }

        private DataTable m_listTable = null;
        private DataTable ListTable
        {
            get
            {
                if (this.m_listTable == null)
                {
                    this.m_listTable = new DataTable();

                    this.m_listTable.Columns.Add("index", typeof(string));
                    this.m_listTable.Columns.Add("guid", typeof(string));
                    this.m_listTable.Columns.Add("companyid", typeof(string));
                    this.m_listTable.Columns.Add("fileid", typeof(string));
                    this.m_listTable.Columns.Add("vtype", typeof(string));
                    this.m_listTable.Columns.Add("vsize", typeof(string));
                    this.m_listTable.Columns.Add("rname", typeof(string));
                    this.m_listTable.Columns.Add("wdate", typeof(DateTime));
                    this.m_listTable.Columns.Add("status", typeof(string));
                    this.m_listTable.Columns.Add("filepath", typeof(string));
                    this.m_listTable.Columns.Add("savepath", typeof(string));
                }

                return this.m_listTable;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="downTable"></param>
        /// <param name="savePath"></param>
        /// <param name="totalSize"></param>
        public void DownloadFile(DataTable downTable, string savePath, long totalSize)
        {
            this.Show();

            g_dnHelper.infoTable = downTable;
            g_dnHelper.savePath = savePath;
            g_dnHelper.totalFileLength = totalSize;

            this.InitializelstFileList();

            //¼öÁ¤ÇÊ¿ä
            AsyncOperationManager.SynchronizationContext = new WindowsFormsSynchronizationContext();
            this.downloadWorker.RunWorkerAsync(g_dnHelper);
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        private void InitializelstFileList()
        {
            this.filelistGrid.DataSource = this.ListTable;
            this.ListTable.Rows.Clear();

            for (int i = 0; i < g_dnHelper.InfoRows.Count; i++)
            {
                // index, guid, companyid, fileid, vtype, vsize, rname, wdate, status, filepath, savepath
                DataRow row = g_dnHelper.InfoRows[i];
                DataRow newrow = this.ListTable.NewRow();

                newrow["index"] = i.ToString();
                newrow["guid"] = row["guid"].ToString();
                newrow["companyid"] = row["companyid"].ToString();
                newrow["fileid"] = row["fileid"].ToString();
                newrow["vtype"] = row["vtype"].ToString();
                newrow["vsize"] = Convert.ToInt64(row["vsize"].ToString()).ToString("n0");
                newrow["rname"] = row["rname"].ToString();
                newrow["wdate"] = row["wdate"];
                newrow["status"] = "waiting";
                newrow["filepath"] = row["filepath"].ToString();
                newrow["savepath"] = row["savepath"].ToString();

                this.ListTable.Rows.Add(newrow);
            }

            if (this.filelistGridView.RowCount > 0)
                this.filelistGridView.FocusedRowHandle = 0;

            this.filelistGridView.GridControl.Refresh();
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnFormLoad(object sender, EventArgs e)
        {
            this.chAutoClose.Checked = AppMediator.SINGLETON.GetDownloadAutoCloseOption();
        }

        private void OnAuthCloseCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            AppMediator.SINGLETON.SetDownloadAutoCloseOption(this.chAutoClose.Checked);
        }

        private void OnCancelButtonClick(object sender, System.EventArgs e)
        {
            if (this.downloadWorker.IsBusy == true)
            {
                this.downloadWorker.CancelAsync();
                this.btCancel.Enabled = false;
            }
            else
            {
                this.Close();
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnDownloadWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            using (DownloadHelper helper = (DownloadHelper)e.Argument)
            {
                try
                {
                    int iterations = 0;
                    int maxsize = 0;
                    int chunksize = 16 * 1024;

                    helper.beginTime = DateTime.Now;
                    helper.numberOfFiles = this.filelistGridView.RowCount;

                    for (int i = 0; i < helper.numberOfFiles; i++)
                    {
                        DataRow row = this.filelistGridView.GetDataRow(i);

                        AppMediator.SINGLETON.Ping();

                        helper.fileNumber = i + 1;
                        helper.fileName = row["rname"].ToString();
                        helper.virtualName = row["guid"].ToString() + ".bin";
                        helper.dnloadDay = Convert.ToDateTime(row["wdate"]);
                        helper.saveFileName = Path.Combine(helper.savePath, helper.fileName);

                        FileInfo fileInfo = new FileInfo(helper.saveFileName);

                        DialogResult dialogResult = DialogResult.Yes;

                        if (fileInfo.Exists == true)
                            dialogResult = MessageBox.Show("[" + helper.fileName + "] overwrite?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult != DialogResult.Yes)
                        {
                            helper.statusMessage = "skipped";
                            worker.ReportProgress(helper.percentProgrss, helper as object);

                            continue;
                        }

                        if (File.Exists(helper.saveFileName) == true)
                        {
                            if (helper.CheckFileHash() == true)
                            {
                                helper.statusMessage = "¿Ï·áµÊ";
                                worker.ReportProgress(helper.percentProgrss, helper as object);

                                continue;
                            }

                            File.Create(helper.saveFileName).Close();
                        }

                        if (AppMediator.SINGLETON.PrepareDownloadFile(helper.dnloadDay, helper.virtualName, out helper.fileSize, out maxsize) == false)
                        {
                            helper.statusMessage = "canceled";
                            worker.ReportProgress(helper.percentProgrss, helper as object);

                            continue;
                        }

                        helper.fileReadSize = 0;

                        using (FileStream stream = new FileStream(helper.saveFileName, FileMode.Append, FileAccess.Write))
                        {
                            while (helper.fileReadSize < helper.fileSize)
                            {
                                if (worker.CancellationPending == true)
                                    break;

                                if (iterations == DownloadHelper.AVERAGE_COUNT)
                                {
                                    long timeForInitChunks = (long)DateTime.Now.Subtract(helper.beginTime).TotalMilliseconds;
                                    long averageChunkTime = Math.Max(1, timeForInitChunks / DownloadHelper.AVERAGE_COUNT);

                                    chunksize = (int)Math.Min(maxsize, chunksize * DownloadHelper.PREFERRED_TRANSFER_DURATION / averageChunkTime);
                                }

                                try
                                {
                                    byte[] buffer = AppMediator.SINGLETON.DownloadFile(helper.dnloadDay, helper.virtualName, helper.fileReadSize, chunksize);

                                    if (buffer.Length < 1)
                                        break;

                                    stream.Write(buffer, 0, buffer.Length);

                                    helper.fileReadSize += buffer.Length;
                                    helper.totalReadSize += buffer.Length;
                                }
                                catch (Exception ex)
                                {
                                    if (helper.NumRetries++ < helper.MaxRetries)
                                    {
                                    }
                                    else
                                    {
                                        stream.Close();
                                        throw new Exception(String.Format("Error occurred during download {0}, too many retries.", ex.Message));
                                    }
                                }

                                helper.statusMessage = "transferring";
                                worker.ReportProgress(helper.percentProgrss, helper as object);

                                iterations++;
                            }

                            stream.Close();

                            if (worker.CancellationPending == true)
                                break;

                            if (helper.CheckFileHash() == true)
                            {
                                AppMediator.SINGLETON.CloseDownloadFile(helper.dnloadDay, helper.virtualName);

                                helper.statusMessage = "¿Ï·áµÊ";
                                worker.ReportProgress(helper.percentProgrss, helper as object);
                            }
                        }
                    }

                    if (worker.CancellationPending == false && e.Cancel == false)
                    {
                        e.Result = helper as object;
                        e.Cancel = false;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    OFileLog.SNG.WriteLog(ex.ToString());
                }

                helper.statusMessage = "stopped";
                worker.ReportProgress(helper.percentProgrss, helper as object);

                if (File.Exists(helper.saveFileName) == true)
                    File.Delete(helper.saveFileName);

                e.Cancel = true;
                return;
            }
        }

        private void OnDownloadWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            using (DownloadHelper helper = (DownloadHelper)e.UserState)
            {
                try
                {
                    this.lbDownLoc.Text = helper.fileName;
                    this.filelistGridView.GetDataRow(helper.fileNumber - 1)["status"] = helper.statusMessage;
                    
                    this.lbFileCount.Text = Convert.ToString(helper.fileNumber) + "/" + helper.numberOfFiles.ToString();
                    this.lbTotalSize.Text = StringHelper.ConvertSizeToString(helper.totalFileLength);

                    int fcompleterate = (int)((double)helper.fileReadSize / (double)helper.fileSize * 100.0);
                    
                    this.pbDownFile.Value = fcompleterate;

                    int tcompleterate = (int)((double)helper.totalReadSize / (double)helper.totalFileLength * 100.0);

                    this.Text = helper.fileName + " - " + tcompleterate.ToString("0") + "% completed";
                    this.lbDownTime.Text = tcompleterate + "% completed";
                    
                    this.pbDownload.Value = tcompleterate;

                    TimeSpan span = DateTime.Now - helper.beginTime;

                    double speed = (double)helper.totalReadSize / (double)span.TotalSeconds;

                    this.lbDownSpeed.Text = StringHelper.ConvertSizeToString(Convert.ToInt64(speed)) + "/seconds";

                    if (speed > 0)
                        this.lbRemaind.Text = StringHelper.ConvertTimeToString((helper.totalFileLength - helper.totalReadSize) / speed);

                    this.lbTransrate.Text = "(" + StringHelper.ConvertSizeToString(helper.totalReadSize) + "/" + StringHelper.ConvertSizeToString(helper.totalFileLength) + " completed)";
                }
                catch (Exception ex)
                {
                    OFileLog.SNG.WriteLog(ex.ToString());
                }
            }
        }

        private void OnDownloadWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.Close();
            }
            else
            {
                using (DownloadHelper helper = (DownloadHelper)e.Result)
                {
                    if (this.chAutoClose.Checked)
                        this.Close();

                    this.btCancel.Text = "Close";
                }
            }
        }
    }
}