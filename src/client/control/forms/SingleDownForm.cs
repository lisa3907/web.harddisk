using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using uBizSoft.LIB.Logging;
using WebHard.WinCtrl.Library;
using WebHard.Proxy;

namespace WebHard.WinCtrl.Forms
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class SingleDownDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public SingleDownDialog()
        {
            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);
        }

        /// <summary></summary>
        /// <param name="p_mainBox"></param>
        /// <param name="p_execute"></param>
        public SingleDownDialog(MainBox p_mainBox, bool p_execute)
            : this()
        {
            g_dnHelper.MainBox = p_mainBox;
            g_dnHelper.executefile = p_execute;
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

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="fileRow"></param>
        /// <param name="fileName"></param>
        public void DownloadFile(DataRow fileRow, string fileName)
        {
            this.Show();

            g_dnHelper.dnloadDay = Convert.ToDateTime(fileRow["wdate"]);
            g_dnHelper.virtualName = fileRow["guid"].ToString() + ".bin";
            g_dnHelper.saveFileName = fileName;

            g_dnHelper.virtualSize = Convert.ToInt64(fileRow["vsize"].ToString());
            g_dnHelper.realName = fileRow["rname"].ToString();

            Uri uri = new Uri(AppMediator.SINGLETON.WhdIProxy.WSUrl);

            this.lbDownLoc.Text = fileRow["rname"].ToString() + "(" + uri.Host + ")";
            this.lbSavePath.Text = StringHelper.GetShortDisplayName(g_dnHelper.saveFileName, 40);

            this.btCancel.Enabled = true;

            //수정필요
            AsyncOperationManager.SynchronizationContext = new WindowsFormsSynchronizationContext();
            this.downloadWorker.RunWorkerAsync(g_dnHelper);
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="path"></param>
        private void Execute(string path)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(path);
            process.Start();
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnFormLoad(object sender, System.EventArgs e)
        {
            this.chAutoClose.Checked = AppMediator.SINGLETON.GetDownloadAutoCloseOption();
        }

        private void OnAutoCloseCheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            AppMediator.SINGLETON.SetDownloadAutoCloseOption(this.chAutoClose.Checked);
        }

        private void OnOpenButtonClick(object sender, System.EventArgs e)
        {
            this.Execute(g_dnHelper.saveFileName);
        }

        private void OnFolderOpenButtonClick(object sender, System.EventArgs e)
        {
            this.Execute(Path.GetDirectoryName(g_dnHelper.saveFileName));
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

        private void OnDownloadWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;

            using (DownloadHelper _helper = (DownloadHelper)e.Argument)
            {
                try
                {
                    int iterations = 0;
                    int maxsize = 0;
                    int chunksize = 16 * 1024;

                    e.Cancel = true;

                    _helper.beginTime = DateTime.Now;

                    AppMediator.SINGLETON.Ping();

                    if (AppMediator.SINGLETON.PrepareDownloadFile(_helper.dnloadDay, _helper.virtualName, out _helper.fileSize, out maxsize) == true)
                    {
                        if (File.Exists(_helper.saveFileName) == true)
                        {
                            if (_helper.CheckFileHash() == true)
                            {
                                _helper.totalReadSize = _helper.fileSize;
                                _helper.statusMessage = "completed";
                                _worker.ReportProgress(_helper.percentProgrss, _helper as object);

                                e.Result = _helper as object;
                                e.Cancel = false;
                                return;
                            }

                            File.Create(_helper.saveFileName).Close();
                        }

                        using (FileStream stream = new FileStream(_helper.saveFileName, FileMode.Append, FileAccess.Write))
                        {
                            while (_helper.totalReadSize < _helper.fileSize)
                            {
                                if (_worker.CancellationPending == true)
                                    break;

                                if (iterations == DownloadHelper.AVERAGE_COUNT)
                                {
                                    long timeForInitChunks = (long)DateTime.Now.Subtract(_helper.beginTime).TotalMilliseconds;
                                    long averageChunkTime = Math.Max(1, timeForInitChunks / DownloadHelper.AVERAGE_COUNT);

                                    chunksize = (int)Math.Min(maxsize, chunksize * DownloadHelper.PREFERRED_TRANSFER_DURATION / averageChunkTime);
                                }

                                try
                                {
                                    byte[] buffer = AppMediator.SINGLETON.DownloadFile(_helper.dnloadDay, _helper.virtualName, _helper.totalReadSize, chunksize);

                                    if (buffer.Length < 1)
                                        break;

                                    stream.Write(buffer, 0, buffer.Length);
                                    _helper.totalReadSize += buffer.Length;
                                }
                                catch (Exception ex)
                                {
                                    if (_helper.NumRetries++ < _helper.MaxRetries)
                                    {
                                        // swallow the exception and try again
                                    }
                                    else
                                    {
                                        stream.Close();
                                        throw new Exception(String.Format("Error occurred during download {0}, too many retries.", ex.Message));
                                    }
                                }

                                _helper.statusMessage = "downloading";
                                _worker.ReportProgress(_helper.percentProgrss, _helper as object);

                                iterations++;
                            }

                            stream.Close();

                            if (_worker.CancellationPending == false)
                            {
                                if (_helper.CheckFileHash() == true)
                                {
                                    AppMediator.SINGLETON.CloseDownloadFile(_helper.dnloadDay, _helper.virtualName);

                                    _helper.statusMessage = "completed";
                                    _worker.ReportProgress(_helper.percentProgrss, _helper as object);

                                    e.Cancel = false;
                                }
                            }
                        }
                    }

                    if (_worker.CancellationPending == false && e.Cancel == false)
                    {
                        e.Result = _helper as object;
                        e.Cancel = false;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    OFileLog.SNG.WriteLog(ex.ToString());
                }

                _helper.statusMessage = "canceled";
                _worker.ReportProgress(_helper.percentProgrss, _helper as object);

                if (File.Exists(_helper.saveFileName) == true)
                    File.Delete(_helper.saveFileName);

                e.Cancel = true;
                return;
            }
        }

        private void OnDownloadWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            using (DownloadHelper _helper = (DownloadHelper)e.UserState)
            {
                try
                {
                    double tcomplete = ((double)_helper.totalReadSize / (double)_helper.virtualSize) * 100.0;

                    this.Text = String.Format("{0} - {1}% completed", _helper.realName, tcomplete.ToString("0"));

                    TimeSpan span = DateTime.Now - _helper.beginTime;

                    double speed = (double)_helper.totalReadSize / (double)span.TotalSeconds;

                    this.lbDownTime.Text = StringHelper.ConvertTimeToString(span);
                    this.lbDownSpeed.Text = StringHelper.ConvertSizeToString(Convert.ToInt64(speed)) + "/sec";

                    if (speed > 0)
                    {
                        this.lbRemaind.Text = String.Format("{0} ({1} / {2} copied)",
                                                            StringHelper.ConvertTimeToString((double)(_helper.virtualSize - _helper.totalReadSize) / speed),
                                                            StringHelper.ConvertSizeToString(_helper.totalReadSize),
                                                            StringHelper.ConvertSizeToString(_helper.virtualSize));
                    }

                    this.pbDownload.Value = (int)((double)_helper.totalReadSize / (double)_helper.fileSize * 100.0);
                    this.lbDownStatus.Text = _helper.statusMessage;
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
                using (DownloadHelper _helper = (DownloadHelper)e.Result)
                {
                    if (File.Exists(_helper.saveFileName) == true)
                    {
                        if (_helper.executefile == true)
                            this.Execute(_helper.saveFileName);
                    }
                    else
                    {
                        MessageBox.Show("File download failure!");
                        this.Close();
                    }

                    if (this.chAutoClose.Checked == true)
                        this.Close();

                    this.btOpen.Enabled = true;
                    this.btFolder.Enabled = true;
                    this.btCancel.Text = "Close";
                }
            }
        }
    }
}