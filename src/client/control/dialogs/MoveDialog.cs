using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using WebHard.WinCtrl.Forms;
using WebHard.WinCtrl.Library;
using WebHard.Proxy;

namespace WebHard.WinCtrl.Dialogs
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class MoveDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_mainBox"></param>
        /// <param name="p_context"></param>
        /// <param name="p_target"></param>
        /// <param name="p_source"></param>
        public MoveDialog(MainBox p_mainBox, bool p_context, TreeNode p_target, TreeNode p_source)
        {
            this.m_targetNode = p_context == true ? p_target : p_mainBox.g_mainHelper.dropNode;
            this.m_sourceNode = p_context == true ? p_source : p_mainBox.g_mainHelper.dragNode;

            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);
        }

        //=========================================================================================
        //
        //=========================================================================================
        private string m_newfileId = String.Empty;
        private string m_oldfileId = String.Empty;

        private TreeNode m_sourceNode = null;
        private TreeNode m_targetNode = null;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public string NewFileId
        {
            get
            {
                return this.m_newfileId;
            }
        }

        /// <summary></summary>
        public string OldFileId
        {
            get
            {
                return this.m_oldfileId;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_target"></param>
        /// <param name="p_source"></param>
        private void MoveTreeNode(TreeNode targetNode, TreeNode sourceNode)
        {
            DataRow targetrow = targetNode.Tag as DataRow;
            DataRow sourcerow = sourceNode.Tag as DataRow;

            string tfileid = targetrow["fileid"].ToString();
            string sfileid = sourcerow["fileid"].ToString();

            this.m_newfileId = AppMediator.SINGLETON.MoveFolder(tfileid, sfileid);
            this.m_oldfileId = sfileid;

            sourcerow["fileid"] = this.m_newfileId;
            sourceNode.Tag = sourcerow;
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnDialogLoad(object sender, EventArgs e)
        {
            //수정필요
            AsyncOperationManager.SynchronizationContext = new WindowsFormsSynchronizationContext();
            this.moveWorker.RunWorkerAsync();
        }

        private void OnMoveWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            this.MoveTreeNode(this.m_targetNode, this.m_sourceNode);
            worker.ReportProgress(100);
        }

        private void OnMoveWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void OnMoveWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}