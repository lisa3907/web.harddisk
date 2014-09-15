using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LIB.Logging;
using WebHard.WinCtrl.Forms;
using WebHard.WinCtrl.Library;
using WebHard.Proxy;

namespace WebHard.WinCtrl.Dialogs
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class FolderListDialog : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_mainBox"></param>
        /// <param name="p_sourceNode"></param>
        /// <param name="p_isFolder"></param>
        public FolderListDialog(MainBox p_mainBox, TreeNode p_sourceNode, bool p_isFolder)
        {
            this.m_mainBox = p_mainBox;
            this.m_sourceNode = p_sourceNode;
            this.m_isFolder = p_isFolder;

            this.InitializeComponent();

            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);

            if (p_isFolder == true)
            {
                this.Text = AppMediator.SINGLETON.ResourceHelper.TranslateWord("폴더 이동");
                this.lbTitle.Text = AppMediator.SINGLETON.ResourceHelper.TranslateWord("선택한 폴더를 다음 폴더로 이동");
            }
            else
            {
                this.Text = AppMediator.SINGLETON.ResourceHelper.TranslateWord("파일 이동");
                this.lbTitle.Text = AppMediator.SINGLETON.ResourceHelper.TranslateWord("선택한 파일을 다음 폴더로 이동");
            }

            this.InitializeFolderTreeView();
        }

        //=========================================================================================
        //
        //=========================================================================================
        private bool m_isFolder;

        private MainBox m_mainBox;

        private TreeNode m_sourceNode;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="parentNode"></param>
        /// <param name="parentId"></param>
        /// <param name="dataTable"></param>
        private void BuildFolder(TreeNode parentNode, string parentId, DataTable dataTable)
        {
            string filter = String.Format("parentid = '{0}'", parentId);

            DataRow[] rows = dataTable.Select(filter, "fileid");

            foreach (DataRow row in rows)
            {
                string name = row["displayname"].ToString();
                string fileid = row["fileid"].ToString();

                TreeNode treeNode = new TreeNode(name);
                treeNode.Tag = row;

                if (row["noauth"].ToString() == "0")
                    treeNode.ForeColor = Color.Red;

                parentNode.Nodes.Add(treeNode);

                this.BuildFolder(treeNode, fileid, dataTable);
            }
        }

        /// <summary></summary>
        private void InitializeFolderTreeView()
        {
            try
            {
                this.tvFolders.Nodes.Clear();

                DataSet folderSet = AppMediator.SINGLETON.GetFolderList();

                TreeNodeCollection nodeCols = AppMediator.SINGLETON.GetRootTreeNodeCollection(this.tvFolders);

                DataView dataView = folderSet.Copy().Tables[0].DefaultView;
                dataView.RowFilter = "parentid = ''";
                dataView.Sort = "fileid";

                DataTable dataTable = folderSet.Copy().Tables[0];

                for (int i = 0; i < dataView.Count; i++)
                {
                    DataRowView rowview = dataView[i];

                    string name = rowview["displayname"].ToString();
                    string fileid = rowview["fileid"].ToString();

                    TreeNode treeNode = new TreeNode(name);
                    treeNode.Tag = rowview.Row;

                    if (rowview["noauth"].ToString() == "0")
                        treeNode.ForeColor = Color.Red;

                    nodeCols.Add(treeNode);

                    this.BuildFolder(treeNode, fileid, dataTable);
                }

                this.tvFolders.Sorted = true;

                this.tvFolders.Nodes[0].Expand();
                this.tvFolders.Focus();
            }
            catch (Exception ex)
            {
                OFileLog.SNG.WriteLog(ex.ToString());
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnFolderTreeViewAfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            DataRow ttagrow = e.Node.Tag as DataRow;
            DataRow stagrow = this.m_sourceNode.Tag as DataRow;

            string tfileid = ttagrow["fileid"].ToString();
            string sfileid = stagrow["fileid"].ToString();

            this.btnOK.Enabled = true;

            if (ttagrow["cfolder"].ToString() != "T")
            {
                this.btnOK.Enabled = false;
            }
            else if (tfileid == sfileid)
            {
                this.btnOK.Enabled = false;
            }
            else if (this.m_isFolder == true)
            {
                this.btnOK.Enabled = !tfileid.StartsWith(sfileid);
            }
        }

        private void OnOkButtonClick(object sender, System.EventArgs e)
        {
            this.m_mainBox.g_mainHelper.dropTreeNode = this.tvFolders.SelectedNode;

            TreeNode selectedNode = this.tvFolders.SelectedNode;

            while (selectedNode.Parent != null)
            {
                selectedNode = selectedNode.Parent;
            }

            this.m_mainBox.g_mainHelper.rootIndex = selectedNode.Index;
        }
    }
}