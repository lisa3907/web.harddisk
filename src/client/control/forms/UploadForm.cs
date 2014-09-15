using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Windows.Forms;

using LIB.Logging;
using UIC.Win.Control;
using WebHard.WinCtrl.Dialogs;
using WebHard.WinCtrl.Library;
using WebHard.Proxy;

namespace WebHard.WinCtrl.Forms
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class UploadDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_mainBox"></param>
        public UploadDialog(MainBox p_mainBox)
        {
            g_upHelper.MainBox = p_mainBox;
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);
        }

        //=========================================================================================
        //
        //=========================================================================================
        private UploadHelper m_upHelper = null;
        private UploadHelper g_upHelper
        {
            get
            {
                if (m_upHelper == null)
                    m_upHelper = new UploadHelper();
                return m_upHelper;
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
                    this.m_listTable.Columns.Add("vname", typeof(string));
                    this.m_listTable.Columns.Add("vsize", typeof(string));
                    this.m_listTable.Columns.Add("vtype", typeof(string));
                    this.m_listTable.Columns.Add("rname", typeof(string));
                    this.m_listTable.Columns.Add("fpath", typeof(string));
                    this.m_listTable.Columns.Add("status", typeof(string));
                }

                return m_listTable;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="files"></param>
        /// <param name="infoTable"></param>
        public void UploadFile(string[] files, DataTable infoTable)
        {
            g_upHelper.infoTable = infoTable;

            this.InitializelstFileList(files);
            this.btCancel.Enabled = true;

            //수정필요
            AsyncOperationManager.SynchronizationContext = new WindowsFormsSynchronizationContext();
            this.uploadWorker.RunWorkerAsync(g_upHelper);
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="files"></param>
        private void InitializelstFileList(string[] files)
        {
            this.filelistGrid.DataSource = this.ListTable;
            this.ListTable.Rows.Clear();

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(files[i]);
                g_upHelper.totalFileLength += fileInfo.Length;

                DataRow inforow = g_upHelper.InfoRow;
                DataRow newrow = this.ListTable.NewRow();

                newrow["index"] = i.ToString();
                newrow["vname"] = fileInfo.Name;
                newrow["vsize"] = fileInfo.Length.ToString("n0");
                newrow["vtype"] = g_upHelper.GetMIMEType(fileInfo.Extension);
                newrow["rname"] = fileInfo.Name;
                newrow["fpath"] = fileInfo.DirectoryName;
                newrow["status"] = "waiting";

                this.ListTable.Rows.Add(newrow);
            }

            if (this.filelistGridView.RowCount > 0)
                this.filelistGridView.FocusedRowHandle = 0;

            this.filelistGridView.GridControl.Refresh();
        }

        /// <summary></summary>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        private bool SingleFileUpload(string fileName, ref string title, ref string desc)
        {
            bool result = true;

            using (FileInfoDialog dialog = new FileInfoDialog(fileName, title, desc))
            {
                if (dialog.ShowDialog() == DialogResult.Cancel)
                    result = false;

                title = dialog.Title;
                desc = dialog.Content;
            }

            return result;
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnFormLoad(object sender, System.EventArgs e)
        {
            this.cbAutoClose.Checked = AppMediator.SINGLETON.GetUploadAutoCloseOption();
        }

        private void OnAutoCloseCheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            AppMediator.SINGLETON.SetUploadAutoCloseOption(this.cbAutoClose.Checked);
        }

        private void OnCancelButtonClick(object sender, System.EventArgs e)
        {
            if (this.uploadWorker.IsBusy == true)
            {
                this.uploadWorker.CancelAsync();
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
        private void OnUploadWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            using (UploadHelper helper = (UploadHelper)e.Argument)
            {
                bool allskip = false;
                bool allfile = false;

                try
                {
                    helper.beginTime = DateTime.Now;
                    helper.numberOfFiles = this.filelistGridView.RowCount;

                    for (int i = 0; i < helper.numberOfFiles; i++)
                    {
                        AppMediator.SINGLETON.Ping();

                        // index, vname, vsize, vtype, rname, fpath, status
                        DataRow uprow = this.filelistGridView.GetDataRow(i);

                        helper.fileName = Path.Combine(uprow["fpath"].ToString(), uprow["rname"].ToString());

                        FileInfo fileInfo = new FileInfo(helper.fileName);

                        if (fileInfo.Length == 0)
                            continue;

                        DataSet fileSet = null;

                        bool uploadOk = true;
                        bool exists = false;

                        string fileId = String.Empty;

                        helper.fileNumber = i + 1;
                        helper.fileLength = fileInfo.Length;

                        // guid, companyid, fileid, ftype, vsize, vtype, rname, title, description, wdate
                        helper.InfoRow["rname"] = fileInfo.Name;
                        helper.InfoRow["vsize"] = helper.fileLength;
                        helper.InfoRow["vtype"] = helper.GetMIMEType(fileInfo.Extension);

                        if (AppMediator.SINGLETON.PrepareUploadFile(helper.InfoSet, out fileId, out helper.uploadDay, out helper.fileGuid, out helper.maxChunkSize) == false)
                        {
                            fileSet = AppMediator.SINGLETON.GetFileInfo(fileId);

                            if (allskip == true || fileSet == null)
                            {
                                helper.totalFileLength -= helper.fileLength;

                                helper.statusMessage = "skipped";
                                worker.ReportProgress(helper.percentProgrss, helper as object);

                                continue;
                            }

                            exists = true;

                            if (allfile == false && fileSet != null)
                            {
                                uploadOk = false;

                                DataRow filerow = fileSet.Tables[0].Rows[0];

                                if (filerow["cmodify"].ToString() == "T" &&
                                    filerow["cfile"].ToString() == "T")
                                {
                                    TimeSpan span = DateTime.Now - helper.beginTime;

                                    ConfirmDialog dialog = new ConfirmDialog(helper.MainBox, fileSet, fileInfo);

                                    switch (dialog.ShowDialog())
                                    {
                                        // '예'
                                        // 현재 파일 Upload
                                        case DialogResult.Yes:
                                            uploadOk = true;
                                            break;

                                        // 모두 '예'
                                        // 나머지 모든 파일 Upload
                                        case DialogResult.OK:
                                            uploadOk = true;
                                            allfile = true;
                                            break;

                                        // '아니오'
                                        case DialogResult.No:
                                            break;

                                        // 모두 '아니오'
                                        // 나머지 모든 파일을 Skip 한다.
                                        case DialogResult.Cancel:
                                            uploadOk = false;
                                            allskip = true;
                                            break;

                                        // '취소'
                                        case DialogResult.Ignore:
                                            e.Cancel = true;
                                            break;
                                    }

                                    helper.beginTime = DateTime.Now - span;

                                    if (e.Cancel == true)
                                        break;
                                }
                            }
                        }

                        if (uploadOk == false)
                        {
                            AppMediator.SINGLETON.FailureCloseUploadFile(helper.fileGuid);

                            helper.totalFileLength -= helper.fileLength;

                            helper.statusMessage = "canceled";
                            worker.ReportProgress(helper.percentProgrss, helper as object);

                            continue;
                        }

                        string title = fileInfo.Name;
                        string contents = String.Empty;

                        if (helper.numberOfFiles == 1)
                        {
                            if (this.SingleFileUpload(fileInfo.Name, ref title, ref contents) == false)
                            {
                                e.Cancel = true;
                                break;
                            }
                        }

                        helper.InfoRow["title"] = title;
                        helper.InfoRow["description"] = contents;

                        using (FileStream stream = new FileStream(helper.fileName, FileMode.Open, FileAccess.Read))
                        {
                            int iterations = 0;

                            helper.fileLength = stream.Length;
                            helper.fileName = stream.Name;
                            helper.fileWriteSize = 0;

                            int chunksize = 16 * 1024;

                            byte[] buffer = new byte[chunksize];

                            int bytesRead = stream.Read(buffer, 0, chunksize);

                            while (bytesRead > 0)
                            {
                                if (worker.CancellationPending == true)
                                    break;

                                try
                                {
                                    if (AppMediator.SINGLETON.UploadFile(helper.fileGuid, buffer, helper.fileWriteSize, bytesRead) == true)
                                    {
                                        if (iterations == UploadHelper.AVERAGE_COUNT + 1)
                                        {
                                            long timeForInitChunks = (long)DateTime.Now.Subtract(helper.beginTime).TotalMilliseconds;
                                            long averageChunkTime = Math.Max(1, timeForInitChunks / UploadHelper.AVERAGE_COUNT);

                                            chunksize = (int)Math.Min(helper.maxChunkSize, chunksize * UploadHelper.PREFERRED_TRANSFER_DURATION / averageChunkTime);
                                            Array.Resize<byte>(ref buffer, chunksize);
                                        }

                                        helper.fileWriteSize += bytesRead;
                                        helper.totalWriteSize += bytesRead;

                                        helper.statusMessage = "transferring";
                                        worker.ReportProgress(helper.percentProgrss, helper as object);
                                    }
                                    else
                                    {
                                        throw new Exception("server side error!");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (helper.NumRetries++ < helper.MaxRetries)
                                    {
                                        // rewind the filestream and keep trying
                                        stream.Position -= bytesRead;
                                    }
                                    else
                                    {
                                        stream.Close();
                                        throw new Exception(String.Format("Error occurred during upload, too many retries. \n{0}", ex.ToString()));
                                    }
                                }

                                bytesRead = stream.Read(buffer, 0, chunksize);
                                iterations++;
                            }

                            stream.Close();

                            if (worker.CancellationPending == true)
                                break;

                            if (helper.CheckFileHash() == true)
                            {
                                helper.InfoRow["guid"] = Path.GetFileNameWithoutExtension(helper.fileGuid);

                                if (exists == true && fileSet != null)
                                {
                                    DataRow filerow = fileSet.Tables[0].Rows[0];

                                    string guid = filerow["guid"].ToString();
                                    string fileid = filerow["fileid"].ToString();

                                    DateTime wdate = Convert.ToDateTime(filerow["wdate"].ToString());

                                    AppMediator.SINGLETON.DeleteFile(guid, fileid, wdate);
                                }

                                AppMediator.SINGLETON.SuccessCloseUploadFile(helper.uploadDay, helper.fileGuid, helper.InfoSet);

                                helper.statusMessage = "completed";
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

                    string error = AppMediator.SINGLETON.ResourceHelper.TranslateWord("작업 중 오류가 발생하였습니다.") + Environment.NewLine
                                 + ex.Message;
                    MessageBox.Show(error);
                }

                if (String.IsNullOrEmpty(helper.fileGuid) == false)
                    AppMediator.SINGLETON.FailureCloseUploadFile(helper.fileGuid);

                helper.statusMessage = "stopped";
                worker.ReportProgress(helper.percentProgrss, helper as object);

                e.Cancel = true;
                return;
            }
        }

        private void OnUploadWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            using (UploadHelper helper = (UploadHelper)e.UserState)
            {
                try
                {
                    this.lbTotalSize.Text = StringHelper.ConvertSizeToString(helper.totalFileLength);
                    this.filelistGridView.GetDataRow(helper.fileNumber - 1)["status"] = helper.statusMessage;

                    //lsFileList.vPanelScrollBar.Value = _upHelper.fileNumber - 1;
                    this.lbFileCount.Text = Convert.ToString(helper.fileNumber) + "/" + Convert.ToString(helper.numberOfFiles);
                    this.lbLocation.Text = Path.GetFileName(helper.fileName);

                    if (helper.totalFileLength > 0)
                    {
                        int tcomplete = (int)((double)helper.totalWriteSize / (double)helper.totalFileLength * 100.0);

                        this.Text = Path.GetFileName(helper.fileName) + " - " + tcomplete.ToString("0") + "% completed";
                        this.pbEntireFiles.Value = tcomplete;
                    }

                    TimeSpan span = DateTime.Now - helper.beginTime;

                    if (span.TotalSeconds > 0)
                    {
                        double speed = (double)helper.totalWriteSize / (double)span.TotalSeconds;

                        this.lbSpeed.Text = StringHelper.ConvertSizeToString(Convert.ToInt64(speed)) + "/seconds";
                        this.lbThroughTime.Text = StringHelper.ConvertTimeToString(span);

                        if (speed > 0)
                        {
                            this.lbRemainder.Text = StringHelper.ConvertTimeToString((double)(helper.totalFileLength - helper.totalWriteSize) / speed);
                        }
                    }

                    if (helper.fileLength > 0)
                    {
                        int fcomplete = (int)((double)helper.fileWriteSize / (double)helper.fileLength * 100.0);
                        this.pbCurrentFile.Value = fcomplete;
                    }

                    this.lbTransrate.Text = "(" + StringHelper.ConvertSizeToString(helper.totalWriteSize) + "/" + StringHelper.ConvertSizeToString(helper.totalFileLength) + " completed)";
                }
                catch (Exception ex)
                {
                    OFileLog.SNG.WriteLog(ex.ToString());
                }
            }
        }

        private void OnUploadWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.Close();
            }
            else
            {
                using (UploadHelper helper = (UploadHelper)e.Result)
                {
                    helper.MainBox.g_mainHelper.currNode.Text = helper.MainBox.UpdateFolderList(null);
                    helper.MainBox.RunGridLoadWorker();

                    if (this.cbAutoClose.Checked == true)
                        this.Close();

                    this.btCancel.Text = "Close";
                }
            }
        }
    
        //=========================================================================================
        //
        //=========================================================================================
    }
}