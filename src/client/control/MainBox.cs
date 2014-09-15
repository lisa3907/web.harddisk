using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList.Nodes;

using uBizSoft.LIB.Logging;
using uBizSoft.LIB.Configuration;
using WebHard.WinCtrl.Dialogs;
using WebHard.WinCtrl.Forms;
using WebHard.WinCtrl.Library;

using uBizSoft.UIC.Win.Control;
using uBizSoft.UIC.Win.Control.Library;

namespace WebHard.WinCtrl
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class MainBox : DevExpress.XtraEditors.XtraUserControl
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public MainBox()
        {
            AppMediator.SINGLETON.Initialize(this);

            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            g_infoTable = g_wctlTable.Copy();

            this.InitializeComponent();

            this.fileGrid.DataSource = this.ListTable;
            this.InitializeSearchItem();
        }

        //=========================================================================================
        //
        //=========================================================================================
        private static DataSet m_wctlSet = null;

        //=========================================================================================
        //
        //=========================================================================================
        private int m_selectIndex;

        private bool m_searchResult = false;
        private bool m_visibleChanged = true;
        private bool m_filelistFocused = false;

        private string m_cocd = String.Empty;
        private string m_certkey = String.Empty;
        private string m_culture = String.Empty;
        private string m_ipaddress = String.Empty;
        private string m_loginId = String.Empty;
        private string m_loginName = String.Empty;
        private string m_proxyWsUrl = String.Empty;
        private string m_wsUrl = String.Empty;

        private string m_maxAttachFileSize = String.Empty;

        private string m_webHardWSDeleteOption = String.Empty;
        private string m_webHardWSDeletePeriod = String.Empty;

        private string m_title = String.Empty;
        private string m_contents = String.Empty;
        private bool m_isPowerPage = false;

        private DataSet m_authListSet = null;

        private DataTable m_listTable = null;
        private DataTable m_infoTable;

        private DataRow[] m_fileInfoList;

        private ImageList m_iconImages;

        private MainHelper m_mainHelper = null;

        private SystemImageList m_sImgList = null;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public string CertKey
        {
            get
            {
                return this.m_certkey;
            }
            set
            {
                this.m_certkey = value;
            }
        }

        /// <summary></summary>
        public string CompanyID
        {
            get
            {
                return this.m_cocd;
            }
            set
            {
                this.m_cocd = value;
            }
        }

        /// <summary></summary>
        public string Culture
        {
            get
            {
                return this.m_culture;
            }
            set
            {
                this.m_culture = value;
            }
        }

        /// <summary></summary>
        public string IPAddress
        {
            get
            {
                return this.m_ipaddress;
            }
            set
            {
                this.m_ipaddress = value;
            }
        }

        /// <summary></summary>
        public string LoginID
        {
            get
            {
                return this.m_loginId;
            }
            set
            {
                this.m_loginId = value;
            }
        }

        /// <summary></summary>
        public string LoginName
        {
            get
            {
                return this.m_loginName;
            }
            set
            {
                this.m_loginName = value;
            }
        }

        /// <summary></summary>
        public string ProxyWsUrl
        {
            get
            {
                return this.m_proxyWsUrl;
            }
            set
            {
                this.m_proxyWsUrl = value;
            }
        }

        /// <summary></summary>
        public string WSUrl
        {
            get
            {
                return this.m_wsUrl;
            }
            set
            {
                this.m_wsUrl = value;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <returns></returns>
        public bool HasPermission()
        {
            SecurityPermissionFlag flag = SecurityPermissionFlag.AllFlags;
            SecurityPermission permission = new SecurityPermission(flag);
            try
            {
                permission.Demand();
            }
            catch (SecurityException)
            {
                return false;
            }
            return true;
        }

        /// <summary></summary>
        public void OnUnload()
        {
            if (this.treeWorker.IsBusy == true)
                this.treeWorker.CancelAsync();

            if (this.gridWorker.IsBusy == true)
                this.gridWorker.CancelAsync();
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public DataSet AuthListSet
        {
            get
            {
                if (m_authListSet == null)
                    m_authListSet = new DataSet();
                return m_authListSet;
            }
        }

        /// <summary></summary>
        public MainHelper g_mainHelper
        {
            get
            {
                if (m_mainHelper == null)
                    m_mainHelper = new MainHelper();
                return m_mainHelper;
            }
        }

        /// <summary></summary>
        public DataSet g_wctlSet
        {
            get
            {
                if (m_wctlSet == null)
                {
                    string resource = String.Format("{0}.schema.WinControl.xsd", this.GetType().Namespace);

                    Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

                    using (Stream stream = assembly.GetManifestResourceStream(resource))
                    {
                        m_wctlSet = new DataSet();
                        m_wctlSet.ReadXmlSchema(stream);
                    }
                }
                return m_wctlSet;
            }
        }

        /// <summary></summary>
        public string MaxAttachFileSize
        {
            get
            {
                if (String.IsNullOrEmpty(this.m_maxAttachFileSize) == true)
                    this.m_maxAttachFileSize = "-1";
                return this.m_maxAttachFileSize;
            }
            set
            {
                this.m_maxAttachFileSize = value;
            }
        }

        /// <summary></summary>
        public string WebHardWSDeleteOption
        {
            get
            {
                return this.m_webHardWSDeleteOption;
            }
            set
            {
                this.m_webHardWSDeleteOption = value;
            }
        }

        /// <summary></summary>
        public string WebHardWSDeletePeriod
        {
            get
            {
                return this.m_webHardWSDeletePeriod;
            }
            set
            {
                this.m_webHardWSDeletePeriod = value;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary>
        /// guid, companyid, fileid, ftype, vsize, vtype, rname, title, description, wdate
        /// </summary>
        private DataRowCollection g_infoRows
        {
            get
            {
                return g_infoTable.Rows;
            }
        }

        /// <summary></summary>
        private DataTable g_infoTable
        {
            get
            {
                return this.m_infoTable;
            }
            set
            {
                this.m_infoTable = value;
            }
        }

        /// <summary>
        /// guid, companyid, fileid, ftype, vsize, vtype, rname, title, description, wdate
        /// </summary>
        private DataTable g_wctlTable
        {
            get
            {
                return g_wctlSet.Tables["authinfo"];
            }
        }

        /// <summary></summary>
        private DataTable ListTable
        {
            get
            {
                if (this.m_listTable == null)
                {
                    this.m_listTable = new DataTable();

                    this.m_listTable.Columns.Add("title", typeof(string));
                    this.m_listTable.Columns.Add("rname", typeof(string));
                    this.m_listTable.Columns.Add("vsize", typeof(string));
                    this.m_listTable.Columns.Add("vtype", typeof(string));
                    this.m_listTable.Columns.Add("wdate", typeof(string));
                    this.m_listTable.Columns.Add("wname", typeof(string));
                    this.m_listTable.Columns.Add("fileauth", typeof(string));
                    this.m_listTable.Columns.Add("location", typeof(string));
                    this.m_listTable.Columns.Add("tag", typeof(DataRow));
                }

                return this.m_listTable;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public void CreateNewFolder()
        {
            if (this.tvFolders.SelectedNode != null && this.tvFolders.LabelEdit == false)
            {
                DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;

                if (tagrow["cfolder"].ToString() == "T")
                    this.CreateFolder();
            }
        }

        /// <summary></summary>
        public void Delete()
        {
            bool deleteOk = true;

            string message = String.Empty;

            if (this.m_filelistFocused == true)
            {
                int[] selectedRowIndices = this.fileGridView.GetSelectedRows();

                DataRow[] selectedRows = new DataRow[selectedRowIndices.Length];

                for (int i = 0; i < selectedRowIndices.Length; i++)
                {
                    selectedRows[i] = this.fileGridView.GetDataRow(selectedRowIndices[i]);
                }

                for (int i = 0; i < selectedRows.Length; i++)
                {
                    DataRow tagrow = selectedRows[i]["tag"] as DataRow;

                    if (tagrow["cdelete"].ToString() == "F")
                    {
                        deleteOk = false;
                        message = String.Format("'{0}' {1}",
                                                tagrow["rname"].ToString(),
                                                AppMediator.SINGLETON.ResourceHelper.TranslateWord("파일을 삭제할 권한이 없습니다."));

                        break;
                    }

                    if (this.WebHardWSDeleteOption == "T")
                    {
                        DateTime wdate = Convert.ToDateTime(tagrow["wdate"].ToString());
                        DateTime edate = DateTime.Now.AddDays(Convert.ToInt32(this.WebHardWSDeletePeriod));

                        if (edate < wdate)
                        {
                            deleteOk = false;
                            message = String.Format("'{0}' {1}",
                                                    tagrow["rname"].ToString(),
                                                    AppMediator.SINGLETON.ResourceHelper.TranslateWord("파일은 삭제할 수 없습니다."));

                            break;
                        }
                    }
                }

                if (deleteOk == true)
                    this.DeleteFile();
            }
            else
            {
                DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;

                if (tagrow["cdelete"].ToString() != "T")
                {
                    deleteOk = false;
                    message = AppMediator.SINGLETON.ResourceHelper.TranslateWord("선택한 폴더를 삭제할 권한이 없습니다.");
                }
                else
                {
                    this.DeleteFolder();
                }
            }

            if (deleteOk == false)
                MessageBox.Show(message);
        }

        /// <summary></summary>
        public void Download()
        {
            if (this.fileGridView.SelectedRowsCount > 0)
                this.FileDownload();
        }

        /// <summary></summary>
        public void SetAuthority()
        {
            if (this.m_filelistFocused == true)
            {
                int[] selectedRowIndices = this.fileGridView.GetSelectedRows();

                DataRow[] rows = new DataRow[selectedRowIndices.Length];

                for (int i = 0; i < selectedRowIndices.Length; i++)
                {
                    rows[i] = this.fileGridView.GetDataRow(selectedRowIndices[i]);
                }

                bool modified = true;

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow row = rows[i];
                    DataRow tagrow = row["tag"] as DataRow;

                    if (tagrow["control"].ToString() == "F")
                        modified = false;
                }

                if (modified == true)
                    this.SetFileAuth();
            }
            else
            {
                DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;

                if (tagrow["control"].ToString() == "T")
                    this.SetAuth("T");
            }
        }

        /// <summary></summary>
        public void Upload()
        {
            if (fdOpen.ShowDialog() == DialogResult.OK)
            {
                long maxsize = Convert.ToInt64(this.MaxAttachFileSize);

                if (maxsize > 0)
                {
                    for (int i = 0; i < fdOpen.FileNames.Length; i++)
                    {
                        string filepath = fdOpen.FileNames[i];

                        FileInfo fileinfo = new FileInfo(filepath);

                        if (fileinfo.Length > maxsize)
                        {
                            string message = String.Format(AppMediator.SINGLETON.ResourceHelper.TranslateWord("첨부파일 크기는 최대 {0} MB 이하 입니다."), maxsize / 1024000);
                            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }
                    }
                }

                DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;

                g_infoRows[1]["guid"] = tagrow["guid"].ToString();
                g_infoRows[1]["companyid"] = this.CompanyID;
                g_infoRows[1]["fileid"] = tagrow["fileid"].ToString();
                g_infoRows[1]["ftype"] = String.Empty;
                g_infoRows[1]["vsize"] = 0;
                g_infoRows[1]["vtype"] = String.Empty;
                g_infoRows[1]["rname"] = String.Empty;
                g_infoRows[1]["title"] = String.Empty;
                g_infoRows[1]["description"] = String.Empty;
                g_infoRows[1]["wdate"] = DateTime.Now;

                UploadDialog dialog = new UploadDialog(this);

                dialog.Show();
                dialog.UploadFile(fdOpen.FileNames, g_infoTable.Copy());
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="fileSet"></param>
        /// <param name="treeNode"></param>
        private void BindAuthFileList(DataSet fileSet, TreeNode treeNode)
        {
            if (treeNode == null)
                return;

            // guid, companyid, fileid, ftype, vsize, vtype, rname,
            // title, description, attach, wcode, wname, wdate,
            // mtype, control, cmodify, cread, cdelete, cview, cfolder, cfile,
            // code, name, filecount, noauth, parentid, displayname
            DataRow tagrow = treeNode.Tag as DataRow;

            g_infoRows.Clear();

            for (int i = 0; i < 2; i++)
            {
                // guid, companyid, fileid, vtype, vsize, vtype, rname, title, description, wdate
                DataRow row = g_infoTable.NewRow();

                row["guid"] = tagrow["guid"].ToString();
                row["companyid"] = tagrow["companyid"].ToString();
                row["fileid"] = tagrow["fileid"].ToString();
                row["ftype"] = tagrow["ftype"].ToString();
                row["vsize"] = 0;
                row["vtype"] = String.Empty;
                row["rname"] = tagrow["rname"].ToString();
                row["title"] = tagrow["title"].ToString();
                row["description"] = tagrow["description"].ToString();
                row["wdate"] = tagrow["wdate"];

                g_infoRows.Add(row);
            }

            if (this.m_searchResult == true)
            {
                this.OnSearchButtonClick(null, null);
            }
            else
            {
                this.BindingFileList(fileSet.Copy(), true, treeNode.FullPath);
            }
        }

        /// <summary></summary>
        /// <param name="fileSet"></param>
        /// <param name="clear"></param>
        /// <param name="fullPath"></param>
        private void BindingFileList(DataSet fileSet, bool clear, string fullPath)
        {
            if (clear == true)
                this.ListTable.Rows.Clear();

            if (this.m_searchResult == true)
            {
                if (this.m_iconImages == null)
                    this.m_iconImages = new ImageList();
            }
            else
            {
                this.m_iconImages = new ImageList();
            }

            if (fileSet.Tables[0].Rows.Count == 0)
            {
                this.txtTitle.Text = String.Empty;
                this.txtDesc.Text = String.Empty;
            }

            for (int i = 0; i < fileSet.Tables[0].Rows.Count; i++)
            {
                // guid, companyid, fileid, ftype, vsize, vtype, rname,
                // title, description, attach, wcode, wname, wdate,
                // mtype, control, cmodify, cread, cdelete, cview, cfolder, cfile,
                // code, name, filecount, noauth, parentid, displayname
                DataRow filerow = fileSet.Tables[0].Rows[i];

                // title, rname, vsize, vtype, wdate, wname, fileauth, location, tag
                DataRow newrow = this.ListTable.NewRow();

                newrow["title"] = filerow["title"].ToString();
                newrow["rname"] = filerow["rname"].ToString();

                decimal fileSize = Convert.ToDecimal(filerow["vsize"].ToString());
                newrow["vsize"] = fileSize / 1024 < 1 ? "1KB" : Convert.ToDecimal((fileSize / 1024)).ToString("N0") + "KB";

                newrow["vtype"] = filerow["vtype"].ToString();
                newrow["wdate"] = Convert.ToDateTime(filerow["wdate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                newrow["wname"] = filerow["wname"].ToString();

                string fileAuth = String.Empty;

                fileAuth += filerow["cmodify"].ToString() == "T" ? "m-" : "_-";
                fileAuth += filerow["cread"].ToString() == "T"   ? "r-" : "_-";
                fileAuth += filerow["cdelete"].ToString() == "T" ? "d-" : "_-";
                fileAuth += filerow["cview"].ToString() == "T"   ? "v-" : "_-";

                newrow["fileauth"] = fileAuth;
                newrow["location"] = fullPath;
                newrow["tag"] = filerow;

                if (this.m_searchResult == true)
                {
                    this.fileGridView.Columns["location"].Visible = true;
                    this.fileGridView.Columns["location"].Width = 100;
                }
                else
                {
                    this.fileGridView.Columns["location"].Visible = false;
                    this.fileGridView.Columns["location"].Width = 0;
                }

                this.ListTable.Rows.Add(newrow);
            }

            if (this.m_searchResult == false)
                this.fileGridView.GridControl.Refresh();

            if (this.fileGridView.RowCount > 0 && this.m_searchResult == false)
            {
                this.fileGridView.FocusedRowHandle = 0;
                this.fileGridView.SelectRow(0);
            }
        }

        /// <summary></summary>
        /// <param name="parentNode"></param>
        /// <param name="parentId"></param>
        private void BuildFolder(TreeNode parentNode, string parentId)
        {
            string filter = String.Format("parentid = '{0}'", parentId);

            DataRow[] rows = AppMediator.SINGLETON.FolderSet.Copy().Tables[0].Select(filter, "fileid");

            foreach (DataRow row in rows)
            {
                string name = row["displayname"].ToString();
                string fileid = row["fileid"].ToString();

                TreeNode treeNode = new TreeNode(name);
                treeNode.Tag = row;

                if (row["noauth"].ToString() == "0")
                    treeNode.ForeColor = Color.Red;

                parentNode.Nodes.Add(treeNode);

                this.BuildFolder(treeNode, fileid);
            }
        }

        /// <summary></summary>
        private void CreateFolder()
        {
            // guid, member, mtype, name, control, cmodify, cread, cdelete, cview, cfolder, cfile
            this.AuthListSet.Clear();
            this.AuthListSet.Tables.Clear();
            this.AuthListSet.Tables.Add(g_wctlSet.Tables["authlist"].Copy());

            string tempName = this.GetNewFolderName("folder");

            TreeNode newNode = new TreeNode(tempName);

            if (this.tvFolders.Nodes.Count == 0)
            {
                this.tvFolders.Nodes.Add(newNode);
            }
            else
            {
                this.tvFolders.SelectedNode.Nodes.Add(newNode);
            }

            this.tvFolders.LabelEdit = true;
            this.tvFolders.SelectedNode = newNode;
            this.tvFolders.SelectedNode.BeginEdit();

            AppMediator.SINGLETON.CreatingFolder = true;
        }

        /// <summary></summary>
        private void DeleteFile()
        {
            DialogResult result = DialogResult.No;

            if (this.fileGridView.GetSelectedRows().Length > 1)
            {
                result = MessageBox.Show("delete ok?", "delete file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else if (this.fileGridView.GetSelectedRows().Length > 0)
            {
                DataRow tagrow = this.fileGridView.GetDataRow(this.fileGridView.GetSelectedRows()[0])["tag"] as DataRow;

                result = MessageBox.Show("[" + tagrow["rname"].ToString() + "] delete ok?", "delete file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (result == DialogResult.Yes)
            {
                List<int> indexlist = new List<int>();

                int[] selectedRowIndices = this.fileGridView.GetSelectedRows();

                DataRow[] selectedRows = new DataRow[selectedRowIndices.Length];

                for (int i = 0; i < selectedRowIndices.Length; i++)
                {
                    indexlist.Add(selectedRowIndices[i]);
                    selectedRows[i] = this.fileGridView.GetDataRow(selectedRowIndices[i]);
                }

                DeleteDialog dialog = new DeleteDialog(selectedRows);
                dialog.ShowDialog();

                indexlist.Sort();
                indexlist.Reverse();

                for (int i = 0; i < indexlist.Count; i++)
                {
                    this.fileGridView.DeleteRow(indexlist[i]);
                }

                g_mainHelper.currNode = this.tvFolders.SelectedNode;
                g_mainHelper.currNode.Text = this.UpdateFolderList(g_mainHelper.currNode.Tag);

                this.RefreshFileList();
            }
        }

        /// <summary></summary>
        private void DeleteFolder()
        {
            DataRow tagrow = tvFolders.SelectedNode.Tag as DataRow;

            if (AppMediator.SINGLETON.IsExistsSubFolder(tagrow["fileid"].ToString()) == true)
            {
                MessageBox.Show("delete behind-folder(s) in the folder to delete folder.", "can't delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (AppMediator.SINGLETON.IsExistsFolderInFile(tagrow["fileid"].ToString()) == true)
            {
                MessageBox.Show("delete file(s) in the folder to delete folder.", "can't delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (MessageBox.Show("'" + tagrow["rname"].ToString() + "' delete folder?", "confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (AppMediator.SINGLETON.DeleteFolder(tagrow["fileid"].ToString()) == true)
                    this.tvFolders.SelectedNode.Remove();
            }
        }

        /// <summary></summary>
        private void ExecuteFile()
        {
            DataRow tagrow = this.fileGridView.GetDataRow(this.fileGridView.GetSelectedRows()[0])["tag"] as DataRow;

            string tempDir = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);

            if (Directory.Exists(tempDir) == false)
                Directory.CreateDirectory(tempDir);

            SingleDownDialog dialog = new SingleDownDialog(this, true);
            dialog.DownloadFile(tagrow, Path.Combine(tempDir, tagrow["rname"].ToString()));
        }

        /// <summary></summary>
        private void FileDownload()
        {
            if (this.fileGridView.SelectedRowsCount == 1)
            {
                DataRow tagrow = this.fileGridView.GetDataRow(this.fileGridView.GetSelectedRows()[0])["tag"] as DataRow;

                fdSave.FileName = tagrow["rname"].ToString();

                if (fdSave.ShowDialog() == DialogResult.OK)
                {
                    SingleDownDialog dialog = new SingleDownDialog(this, false);
                    dialog.DownloadFile(tagrow, fdSave.FileName);
                }
            }
            else if (this.fileGridView.SelectedRowsCount > 1)
            {
                if (bdSave.ShowDialog() == DialogResult.OK)
                {
                    MultiDownDialog dialog = new MultiDownDialog(this);

                    DataTable downTable = new DataTable();

                    // index, guid, companyid, fileid, vtype, vsize, rname, wdate, status, filepath, savepath
                    downTable = g_wctlSet.Tables["multidownload"];
                    downTable.Clear();
                    downTable.Rows.Clear();

                    int[] selectedRowIndices = this.fileGridView.GetSelectedRows();

                    DataRow[] rows = new DataRow[selectedRowIndices.Length];

                    for (int i = 0; i < selectedRowIndices.Length; i++)
                    {
                        rows[i] = fileGridView.GetDataRow(selectedRowIndices[i]);
                    }

                    long totalSize = 0;

                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow tagrow = rows[i]["tag"] as DataRow;
                        DataRow newrow = downTable.NewRow();

                        newrow["index"] = i;
                        newrow["guid"] = tagrow["guid"].ToString();
                        newrow["companyid"] = this.CompanyID;
                        newrow["fileid"] = tagrow["fileid"].ToString();
                        newrow["vtype"] = tagrow["vtype"].ToString();
                        newrow["vsize"] = Convert.ToInt64(tagrow["vsize"].ToString());
                        newrow["rname"] = tagrow["rname"].ToString();
                        newrow["wdate"] = tagrow["wdate"];
                        newrow["status"] = "waiting";
                        newrow["filepath"] = String.Empty;
                        newrow["savepath"] = bdSave.SelectedPath;

                        downTable.Rows.Add(newrow);

                        totalSize += Convert.ToInt64(tagrow["vsize"].ToString());
                    }

                    if (rows.Length > 0)
                        dialog.DownloadFile(downTable, bdSave.SelectedPath, totalSize);
                }
            }
        }

        /// <summary></summary>
        /// <param name="nodeCols"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        private TreeNode FindTreeNodeByFileId(TreeNodeCollection nodeCols, string fileId)
        {
            TreeNode result = null;

            foreach (TreeNode node in nodeCols)
            {
                DataRow tagrow = node.Tag as DataRow;

                if (tagrow["fileid"].ToString() == fileId)
                {
                    result = node;
                    break;
                }

                if (node.Nodes.Count > 0)
                    result = this.FindTreeNodeByFileId(node.Nodes, fileId);

                if (result != null)
                    break;
            }

            return result;
        }

        /// <summary></summary>
        /// <param name="targetNode"></param>
        /// <param name="sourceNode"></param>
        private void FindTreeNodeForMove(TreeNode targetNode, TreeNode sourceNode)
        {
            DataRow ttagrow = targetNode.Tag as DataRow;
            DataRow stagrow = sourceNode.Tag as DataRow;

            TreeNode parentNode = this.FindTreeNodeByFileId(this.tvFolders.Nodes, ttagrow["fileid"].ToString());

            if (parentNode == null)
                return;

            for (int i = 0; i < parentNode.Nodes.Count; i++)
            {
                DataRow tagrow = parentNode.Nodes[i].Tag as DataRow;

                if (tagrow != null)
                {
                    if (tagrow["rname"].ToString() == stagrow["rname"].ToString())
                    {
                        MessageBox.Show("Folder name is already exists.");
                        return;
                    }
                }
            }

            if (sourceNode.Parent == null)
            {
                this.tvFolders.Nodes.Remove(sourceNode);
            }
            else
            {
                sourceNode.Parent.Nodes.Remove(sourceNode);
            }

            this.Cursor = Cursors.WaitCursor;

            MoveDialog dialog = new MoveDialog(this, true, parentNode, sourceNode);
            dialog.ShowDialog();

            parentNode.Nodes.Add(sourceNode);
            parentNode.ExpandAll();

            this.RenewMovedFoldInfo(sourceNode, dialog.NewFileId, dialog.OldFileId);
        }

        /// <summary></summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private DataSet GetAuthFileList(object tag)
        {
            DataSet result = null;

            if (tag != null)
            {
                DataRow tagrow = tag as DataRow;
                result = AppMediator.SINGLETON.GetAuthFileList(tagrow["fileid"].ToString());
            }

            return result;
        }

        /// <summary></summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Icon GetFileExtICon(string fileName)
        {
            if (this.m_sImgList == null)
                this.m_sImgList = new SystemImageList(SystemImageListSize.SmallIcons);

            int index = this.m_sImgList.GetIconIndex(fileName);

            return this.m_sImgList.GetIcon(index);
        }

        /// <summary></summary>
        /// <param name="defaultName"></param>
        /// <returns></returns>
        private string GetNewFolderName(string defaultName)
        {
            string result = defaultName;

            if (this.tvFolders.Nodes.Count == 0)
                return result;

            if (this.tvFolders.SelectedNode == null)
                return result;

            if (this.IsExistFolderName(this.tvFolders.SelectedNode.Nodes, defaultName) == false)
                return result;

            int count = 1;

            while (true)
            {
                result = defaultName + count.ToString();

                if (this.IsExistFolderName(this.tvFolders.SelectedNode.Nodes, result) == false)
                    break;

                count++;
            }

            return result;
        }

        /// <summary></summary>
        private void InitializeSearchItem()
        {
            this.cbSearch.Properties.Items.Add(new SearchItem("제목", "1", -1));
            this.cbSearch.Properties.Items.Add(new SearchItem("제목+설명", "2", -1));
            this.cbSearch.Properties.Items.Add(new SearchItem("제목+설명+파일명", "3", -1));
            this.cbSearch.Properties.Items.Add(new SearchItem("파일명", "4", -1));

            this.cbSearch.SelectedIndex = 0;
        }

        /// <summary></summary>
        /// <param name="nodeCols"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsExistFolderName(TreeNodeCollection nodeCols, string name)
        {
            bool result = false;

            foreach (TreeNode node in nodeCols)
            {
                DataRow tagrow = node.Tag as DataRow;

                if (tagrow == null)
                    continue;

                if (name.ToLower() == tagrow["rname"].ToString().ToLower())
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary></summary>
        private void MoveFile()
        {
            FolderListDialog dialog = new FolderListDialog(this, (TreeNode)this.tvFolders.SelectedNode.Clone(), false);

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            if (g_mainHelper.dropTreeNode == null)
                return;

            this.Cursor = Cursors.WaitCursor;

            int[] selectedRowIndices = this.fileGridView.GetSelectedRows();

            DataRow[] rows = new DataRow[selectedRowIndices.Length];

            for (int i = 0; i < selectedRowIndices.Length; i++)
            {
                rows[i] = this.fileGridView.GetDataRow(selectedRowIndices[i]);
            }

            DataSet moveSet = new DataSet();
            moveSet.Tables.Add(g_wctlTable.Copy());

            DataSet deleteSet = new DataSet();
            deleteSet.Tables.Add(g_wctlTable.Copy());

            DataRow ttagrow = g_mainHelper.dropTreeNode.Tag as DataRow;

            DataSet tfileSet = AppMediator.SINGLETON.GetAuthFileList(ttagrow["fileid"].ToString());

            bool allOK = false;
            bool allNo = false;
            bool oneOK = false;

            for (int i = 0; i < rows.Length; i++)
            {
                DataRow row = rows[i];
                DataRow tagrow = row["tag"] as DataRow;

                DataSet tempSet = tfileSet.Copy();
                DataView dataView = tempSet.Tables[0].DefaultView;

                dataView.RowFilter = "rname = '" + tagrow["rname"].ToString() + "'";

                if (dataView.Count > 0)
                {
                    if (dataView[0]["cmodify"].ToString() == "T" &&
                        dataView[0]["cfile"].ToString() == "T")
                    {
                        if (allOK == false && allNo == false)
                        {
                            ConfirmDialog confirm = new ConfirmDialog(this,
                                                                      dataView[0]["vsize"].ToString(),
                                                                      dataView[0]["wdate"].ToString(),
                                                                      tagrow["rname"].ToString(),
                                                                      tagrow["vsize"].ToString(),
                                                                      tagrow["wdate"].ToString());

                            switch (confirm.ShowDialog())
                            {
                                case DialogResult.Yes:
                                    oneOK = true;
                                    break;

                                case DialogResult.OK:
                                    oneOK = true;
                                    allOK = true;
                                    break;

                                case DialogResult.No:
                                    oneOK = false;
                                    break;

                                case DialogResult.Cancel:
                                    oneOK = false;
                                    allNo = true;
                                    break;

                                case DialogResult.Ignore:
                                    return;
                            }
                        }

                        if (oneOK == true)
                        {
                            // guid, companyid, fileid, ftype, vsize, vtype, rname, title, description
                            DataRow mnewrow = moveSet.Tables[0].NewRow();

                            mnewrow["guid"] = tagrow["guid"];
                            mnewrow["companyid"] = this.CompanyID;
                            mnewrow["fileid"] = tagrow["fileid"];
                            mnewrow["ftype"] = tagrow["ftype"];
                            mnewrow["vsize"] = 1;
                            mnewrow["vtype"] = tagrow["vtype"];
                            mnewrow["rname"] = tagrow["rname"];
                            mnewrow["title"] = tagrow["title"];
                            mnewrow["description"] = tagrow["description"];
                            mnewrow["wdate"] = tagrow["wdate"];

                            moveSet.Tables[0].Rows.Add(mnewrow);

                            DataRow dnewrow = deleteSet.Tables[0].NewRow();

                            dnewrow["guid"] = dataView[0]["guid"];
                            dnewrow["companyid"] = this.CompanyID;
                            dnewrow["fileid"] = dataView[0]["fileid"];
                            dnewrow["ftype"] = dataView[0]["ftype"];
                            dnewrow["vsize"] = dataView[0]["vsize"];
                            dnewrow["vtype"] = dataView[0]["vtype"];
                            dnewrow["rname"] = dataView[0]["rname"];
                            dnewrow["title"] = dataView[0]["title"];
                            dnewrow["description"] = dataView[0]["description"];
                            dnewrow["wdate"] = dataView[0]["wdate"];

                            deleteSet.Tables[0].Rows.Add(dnewrow);
                        }
                    }
                    else
                    {
                        MessageBox.Show(AppMediator.SINGLETON.ResourceHelper.TranslateWord("권한이 없어서 파일을 바꿀 수 없습니다."));
                    }
                }
                else
                {
                    DataRow newrow = moveSet.Tables[0].NewRow();

                    newrow["guid"] = tagrow["guid"];
                    newrow["companyid"] = this.CompanyID;
                    newrow["fileid"] = tagrow["fileid"];
                    newrow["ftype"] = tagrow["ftype"];
                    newrow["vsize"] = tagrow["vsize"];
                    newrow["vtype"] = tagrow["vtype"];
                    newrow["rname"] = tagrow["rname"];
                    newrow["title"] = tagrow["title"];
                    newrow["description"] = tagrow["description"];
                    newrow["wdate"] = tagrow["wdate"];

                    moveSet.Tables[0].Rows.Add(newrow);
                }
            }

            if (moveSet.Tables[0].Rows.Count > 0)
            {
                AppMediator.SINGLETON.MoveFile(ttagrow["fileid"].ToString(), moveSet, deleteSet);

                this.RunGridLoadWorker();

                TreeNode targetNode = this.FindTreeNodeByFileId(this.tvFolders.Nodes, ttagrow["fileid"].ToString());

                if (targetNode != null)
                    targetNode.Text = this.UpdateFolderList(targetNode.Tag);
            }

            g_mainHelper.dropTreeNode = null;
            this.Cursor = Cursors.Default;
        }

        /// <summary></summary>
        private void MoveFolder()
        {
            g_mainHelper.dropTreeNode = null;
            TreeNode sourceNode = this.tvFolders.SelectedNode;

            FolderListDialog dialog = new FolderListDialog(this, (TreeNode)this.tvFolders.SelectedNode.Clone(), true);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                this.FindTreeNodeForMove(g_mainHelper.dropTreeNode, sourceNode);
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary></summary>
        private void RefreshFileList()
        {
            if (this.tvFolders.SelectedNode != null)
            {
                DataSet fileSet = this.GetAuthFileList(this.tvFolders.SelectedNode.Tag);

                this.BindAuthFileList(fileSet, this.tvFolders.SelectedNode);
            }
        }

        /// <summary></summary>
        private void RefreshFolderTreeView()
        {
            TreeNodeCollection nodeCols = AppMediator.SINGLETON.GetRootTreeNodeCollection(this.tvFolders);

            DataView dataView = AppMediator.SINGLETON.FolderSet.Copy().Tables[0].DefaultView;
            dataView.RowFilter = "parentid = ''";
            dataView.Sort = "fileid";

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

                this.BuildFolder(treeNode, fileid);
            }
        }

        /// <summary></summary>
        /// <param name="movedNode"></param>
        /// <param name="newId"></param>
        /// <param name="oldId"></param>
        private void RenewMovedFoldInfo(TreeNode movedNode, string newId, string oldId)
        {
            foreach (TreeNode node in movedNode.Nodes)
            {
                DataRow tagrow = node.Tag as DataRow;

                string tempid = tagrow["fileid"].ToString();
                string fileid = newId + tempid.Substring(oldId.Length);

                tagrow["fileid"] = fileid;
                node.Tag = tagrow;

                if (node.Nodes.Count > 0)
                    this.RenewMovedFoldInfo(node, newId, oldId);
            }
        }

        /// <summary></summary>
        /// <param name="treeNode"></param>
        /// <param name="searchKey"></param>
        /// <param name="searchValue"></param>
        private void Search(TreeNode treeNode, string searchKey, string searchValue)
        {
            DataRow tagrow = treeNode.Tag as DataRow;

            DataSet searchSet = new DataSet();

            searchSet.Tables.Clear();
            searchSet.Tables.Add(g_wctlSet.Tables["search"].Copy());
            searchSet.Tables[0].Rows.Clear();

            DataRow searchrow = searchSet.Tables[0].NewRow();

            searchrow["seekey"] = searchKey;
            searchrow["seeval"] = searchValue;

            searchSet.Tables[0].Rows.Add(searchrow);

            DataSet resultSet = AppMediator.SINGLETON.FileSearch(tagrow["fileid"].ToString(), searchSet.Copy());

            this.BindingFileList(resultSet, false, treeNode.FullPath);

            //수정필요(하위 폴더까지 모두 검색해야 하는가?)
            foreach (TreeNode node in treeNode.Nodes)
            {
                this.Search(node, searchKey, searchValue);
            }
        }

        /// <summary></summary>
        /// <param name="ftype"></param>
        private void SetAuth(string ftype)
        {
            if (this.tvFolders.SelectedNode.Tag == null)
                return;

            DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;
            DataSet authSet = AppMediator.SINGLETON.GetAuthList(tagrow["fileid"].ToString());

            string title = "[" + tagrow["rname"].ToString() + "] assign rights";

            AuthListDialog dialog = new AuthListDialog(this, authSet, false, title);
            dialog.Owner = this.ParentForm;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.AllSubFoldersChecked == true)
                {
                    this.UpdateAllChildAuthority(this.tvFolders.SelectedNode, ftype);
                }
                else
                {
                    AppMediator.SINGLETON.UpdateAuthList(dialog.SubFolderChecked, ftype, tagrow["fileid"].ToString(), this.AuthListSet.Copy());
                }

                if (AppMediator.SINGLETON.PowerUser == true)
                {
                    this.RefreshFileList();
                }
                else
                {
                    this.RunGridLoadWorker();
                }
            }
        }

        /// <summary></summary>
        /// <param name="enabled"></param>
        private void SetBarItemEnabled(bool enabled)
        {
            foreach (DevExpress.XtraBars.BarItem item in this.barManager.Items)
            {
                if (item.GetType().Name == "BarLargeButtonItem")
                    item.Enabled = enabled;
            }
        }

        /// <summary></summary>
        private void SetFileAuth()
        {
            if (this.fileGridView.GetSelectedRows().Length > 0)
            {
                DataRow tagrow = this.fileGridView.GetDataRow(this.m_selectIndex)["tag"] as DataRow;
                DataSet authSet = AppMediator.SINGLETON.GetAuthList(tagrow["fileid"].ToString());

                string title = "<" + tagrow["rname"].ToString() + "> assign rights";

                AuthListDialog dialog = new AuthListDialog(this, authSet, true, title);
                dialog.Owner = this.ParentForm;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    int[] selectedRowIndices = this.fileGridView.GetSelectedRows();

                    DataRow[] rows = new DataRow[selectedRowIndices.Length];

                    for (int i = 0; i < selectedRowIndices.Length; i++)
                    {
                        rows[i] = this.fileGridView.GetDataRow(selectedRowIndices[i]);
                    }

                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow row = rows[i]["tag"] as DataRow;

                        AppMediator.SINGLETON.UpdateAuthList(dialog.SubFolderChecked, "F", row["fileid"].ToString(), this.AuthListSet.Copy());
                    }

                    if (AppMediator.SINGLETON.PowerUser == true)
                    {
                        this.RefreshFileList();
                    }
                    else
                    {
                        this.RunGridLoadWorker();
                    }
                }
            }
        }

        /// <summary></summary>
        /// <param name="treeNode"></param>
        /// <param name="ftype"></param>
        private void UpdateAllChildAuthority(TreeNode treeNode, string ftype)
        {
            DataRow tagrow = treeNode.Tag as DataRow;

            AppMediator.SINGLETON.UpdateAuthList(true, ftype, tagrow["fileid"].ToString(), this.AuthListSet.Copy());

            foreach (TreeNode node in treeNode.Nodes)
            {
                this.UpdateAllChildAuthority(node, ftype);
            }
        }

        /// <summary></summary>
        private void UpdateFileInfo()
        {
            int[] selectedRowIndices = this.fileGridView.GetSelectedRows();

            this.m_fileInfoList = new DataRow[selectedRowIndices.Length];

            for (int i = 0; i < selectedRowIndices.Length; i++)
            {
                this.m_fileInfoList[i] = this.fileGridView.GetDataRow(selectedRowIndices[i]);
            }

            if (this.fileGridView.SelectedRowsCount == 1)
            {
                DataRow row = this.fileGridView.GetDataRow(this.m_selectIndex);
                DataRow tagrow = row["tag"] as DataRow;

                FileInfoDialog dialog = new FileInfoDialog(tagrow["rname"].ToString(),
                                                           tagrow["title"].ToString(),
                                                           tagrow["description"].ToString());

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.m_title = dialog.Title;
                    this.m_contents = dialog.Content;

                    AppMediator.SINGLETON.UpdateFileInfo(tagrow["fileid"].ToString(), this.m_title, this.m_contents);
                    this.RefreshFileList();
                }
            }
            else if (fileGridView.SelectedRowsCount > 1)
            {
                FileInfoDialog dialog = new FileInfoDialog("batch", "batch modify inform", "Entire seleted files will modify information.");

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.m_title = dialog.Title;
                    this.m_contents = dialog.Content;

                    for (int i = 0; i < this.m_fileInfoList.Length; i++)
                    {
                        DataRow row = this.m_fileInfoList[i];
                        DataRow tagrow = row["tag"] as DataRow;

                        AppMediator.SINGLETON.UpdateFileInfo(tagrow["fileid"].ToString(), this.m_title, this.m_contents);
                    }

                    this.RefreshFileList();
                }
            }
        }

        /// <summary></summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public string UpdateFolderList(object tag)
        {
            string result = String.Empty;

            if (tag == null)
                tag = g_mainHelper.currNode.Tag;

            if (tag != null)
            {
                DataRow tagrow = tag as DataRow;

                DataSet dataSet = AppMediator.SINGLETON.GetFolderInfo(tagrow["fileid"].ToString(), AppMediator.SINGLETON.PowerUser);

                DataRow row = null;

                if (dataSet == null ||
                    dataSet.Tables.Count == 0 ||
                    dataSet.Tables[0].Rows.Count == 0)
                {
                    row = tagrow;
                }
                else
                {
                    row = dataSet.Tables[0].Rows[0];
                }

                result = row["displayname"].ToString();
            }

            return result;
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnMainBoxVisibleChanged(object sender, EventArgs e)
        {
            if (this.m_visibleChanged == true)
            {
                this.m_visibleChanged = false;

                if (String.IsNullOrEmpty(this.m_certkey) == false)
                {
                    AppMediator.SINGLETON.InitializeCertKey(this.CertKey);

                    this.RunTreeLoadWorker();

                    AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);
                }
                else
                {
                    this.m_visibleChanged = true;
                }
            }
        }

        //=========================================================================================
        // ToolBar EventHandler
        //=========================================================================================
        private void OnNewFolderBarLargeButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CreateNewFolder();
        }

        private void OnUploadBarLargeButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Upload();
        }

        private void OnDownloadBarLargeButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Download();
        }

        private void OnRenewBarLargeButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RunGridLoadWorker();
        }

        private void OnDeleteBarLargeButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete();
        }

        private void OnAuthorityBarLargeButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetAuthority();
        }

        private void OnPowerUserBarLargeButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (AppMediator.SINGLETON.PowerUser == false)
            {
                this.bbPowerUser.Caption = AppMediator.SINGLETON.ResourceHelper.TranslateWord("사용자");
                //this.bbPowerUser.LargeImageIndex = 21;
                //this.bbPowerUser.LargeImageIndexDisabled = 22;
                //this.bbPowerUser.LargeImageIndexHot = 23;

                this.bbiTreeAddFolder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiTreeDeleteFolder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bbiTreeRight.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiTreeUpdateName.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bbiTreeMoveFolder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                this.bbiListUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bbiListExecuteFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bbiListDeleteFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bbiListMoveFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bbiListDownload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bbiListAssignRights.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiListModifyInformation.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                AppMediator.SINGLETON.PowerUser = true;
                m_isPowerPage = true;
            }
            else
            {
                this.bbPowerUser.Caption = AppMediator.SINGLETON.ResourceHelper.TranslateWord("관리자");
                //this.bbPowerUser.LargeImageIndex = 18;
                //this.bbPowerUser.LargeImageIndexDisabled = 19;
                //this.bbPowerUser.LargeImageIndexHot = 20;

                this.bbiTreeAddFolder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiTreeDeleteFolder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiTreeRight.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiTreeUpdateName.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiTreeMoveFolder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                this.bbiListUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiListExecuteFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiListDeleteFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiListMoveFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiListDownload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiListAssignRights.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                this.bbiListModifyInformation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                AppMediator.SINGLETON.PowerUser = false;
                m_isPowerPage = false;
            }

            this.RunTreeLoadWorker();
        }

        private void OnSearchButtonClick(object sender, EventArgs e)
        {            
            this.Cursor = Cursors.WaitCursor;
            this.fileGridView.GridControl.Cursor = Cursors.WaitCursor;

            this.ListTable.Rows.Clear();
            this.m_searchResult = true;

            SearchItem item = (SearchItem)this.cbSearch.SelectedItem;
            this.Search(this.tvFolders.SelectedNode, item.Value, this.txtSearch.Text);

            this.Cursor = Cursors.Default;
            this.fileGridView.GridControl.Cursor = Cursors.Default;
        }

        private void OnSearchTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.OnSearchButtonClick(null, null);
        }

        //=========================================================================================
        // Folder TreeView EventHandler
        //=========================================================================================
        private void OnFolderTreeViewAfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (AppMediator.SINGLETON.PowerUser == true && e.Node == this.tvFolders.TopNode)
                    return;

                e.Node.ImageIndex = 1;
                e.Node.SelectedImageIndex = 1;
            }
        }

        private void OnFolderTreeViewAfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (AppMediator.SINGLETON.PowerUser == true && e.Node == this.tvFolders.TopNode)
                    return;

                e.Node.ImageIndex = 2;
                e.Node.SelectedImageIndex = 2;
            }
        }

        private void OnFolderTreeViewAfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            try
            {
                if (AppMediator.SINGLETON.CreatingFolder == true)
                {
                    string nodeText = this.tvFolders.SelectedNode.Text;

                    if (e.Label != null)
                        nodeText = e.Label;

                    if (nodeText.Trim().Length == 0)
                    {
                        MessageBox.Show("Wrong folder name");
                        e.Node.Remove();

                        return;
                    }

                    if (this.IsExistFolderName(this.tvFolders.SelectedNode.Parent.Nodes, nodeText) == true)
                    {
                        MessageBox.Show("Folder name is already exists.");
                        e.Node.Remove();

                        return;
                    }

                    if (AppMediator.SINGLETON.PowerUser == true)
                    {
                        DataSet dataSet = AppMediator.SINGLETON.CreateRootFolder(nodeText);
                        this.tvFolders.SelectedNode.Tag = dataSet.Tables[0].Rows[0];
                    }
                    else
                    {
                        DataRow tagrow = this.tvFolders.SelectedNode.Parent.Tag as DataRow;
                        DataSet dataSet = AppMediator.SINGLETON.CreateFolder(nodeText, tagrow["fileid"].ToString());

                        this.tvFolders.SelectedNode.Tag = dataSet.Tables[0].Rows[0];
                    }

                    AppMediator.SINGLETON.CreatingFolder = false;
                }
                else
                {
                    if (AppMediator.SINGLETON.PowerUser == true)
                    {
                        e.CancelEdit = true;
                        return;
                    }

                    if (e.Label != null)
                    {
                        if (e.Label.Trim().Length == 0)
                        {
                            MessageBox.Show("Wrong folder name.");

                            e.CancelEdit = true;
                            return;
                        }

                        if (this.tvFolders.SelectedNode.Parent != null)
                        {
                            for (int i = 0; i < this.tvFolders.SelectedNode.Parent.Nodes.Count - 1; i++)
                            {
                                if (this.tvFolders.SelectedNode.Parent.Nodes[i].Text == e.Label)
                                {
                                    MessageBox.Show("Folder name is already exists that defined by you.");

                                    e.CancelEdit = true;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (AppMediator.SINGLETON.IsExistsRootFolder(e.Label) == true)
                            {
                                MessageBox.Show("Folder name is already exists that defined by you.");

                                e.CancelEdit = true;
                                return;
                            }
                        }

                        DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;
                        AppMediator.SINGLETON.UpdateFolderName(e.Label, tagrow["fileid"].ToString());
                    }
                }

                this.RunGridLoadWorker();
            }
            catch (Exception ex)
            {
                OFileLog.SNG.WriteLog(ex.ToString());
            }
            finally
            {
                this.tvFolders.LabelEdit = false;
            }
        }

        private void OnFolderTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (g_mainHelper.dragging == true)
                return;

            this.m_searchResult = false;

            g_mainHelper.currNode = e.Node;

            // guid, companyid, fileid, ftype, vsize, vtype, rname,
            // title, description, attach, wcode, wname, wdate,
            // mtype, control, cmodify, cread, cdelete, cview, cfolder, cfile,
            // code, name, filecount, noauth, parentid, displayname
            DataRow tagrow = e.Node.Tag as DataRow;

            if (tagrow == null)
                return;

            if (AppMediator.SINGLETON.LoadingFolder == false)
            {
                g_infoRows.Clear();

                for (int i = 0; i < 2; i++)
                {
                    DataRow row = g_infoTable.NewRow();

                    row["guid"] = tagrow["guid"].ToString();
                    row["companyid"] = tagrow["companyid"].ToString();
                    row["fileid"] = tagrow["fileid"].ToString();
                    row["ftype"] = tagrow["ftype"].ToString();
                    row["vsize"] = 0;
                    row["vtype"] = String.Empty;
                    row["rname"] = tagrow["rname"].ToString();
                    row["title"] = tagrow["title"].ToString();
                    row["description"] = tagrow["description"].ToString();
                    row["wdate"] = tagrow["wdate"];

                    g_infoRows.Add(row);
                }
            }

            this.bbUpload.Enabled = tagrow["cfile"].ToString() == "T";
            this.bbDownload.Enabled = tagrow["cread"].ToString() == "T";
            this.bbAuthority.Enabled = tagrow["control"].ToString() == "T";

            if (AppMediator.SINGLETON.LoadingFolder == false)
                this.RunGridLoadWorker();
        }

        private void OnFolderTreeViewBeforeLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                DataRow tagrow = e.Node.Tag as DataRow;

                this.tvFolders.SelectedNode.Text = tagrow["rname"].ToString();
                e.Node.Text = tagrow["rname"].ToString();                

                if (tagrow["cmodify"].ToString() == "F")
                    e.CancelEdit = true;
            }
        }

        private void OnFolderTreeViewDragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            DragHelper.ImageList_DragLeave(this.tvFolders.Handle);

            g_mainHelper.dropNode = this.tvFolders.GetNodeAt(this.tvFolders.PointToClient(new Point(e.X, e.Y)));

            DataRow dragrow = g_mainHelper.dragNode.Tag as DataRow;
            DataRow droprow = g_mainHelper.dropNode.Tag as DataRow;

            if (g_mainHelper.dragNode != g_mainHelper.dropNode)
            {
                if (droprow["cfolder"].ToString() == "T")
                {
                    DialogResult result = MessageBox.Show("'Do you want to move the folder from " + g_mainHelper.dragNode.Text + "' to '" + g_mainHelper.dropNode.Text + "'?",
                                                          "moving folder",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);

                    if (DialogResult.Yes == result)
                    {
                        for (int i = 0; i < g_mainHelper.dropNode.Nodes.Count; i++)
                        {
                            DataRow tagrow = g_mainHelper.dropNode.Nodes[i].Tag as DataRow;

                            if (tagrow != null)
                            {
                                if (tagrow["rname"].ToString() == dragrow["rname"].ToString())
                                {
                                    MessageBox.Show("Already exists folder name.");
                                    g_mainHelper.dragging = false;

                                    return;
                                }
                            }
                        }

                        if (g_mainHelper.dragNode.Parent == null)
                        {
                            this.tvFolders.Nodes.Remove(g_mainHelper.dragNode);
                        }
                        else
                        {
                            g_mainHelper.dragNode.Parent.Nodes.Remove(g_mainHelper.dragNode);
                        }

                        this.Cursor = Cursors.WaitCursor;

                        g_mainHelper.dropNode.Nodes.Add(g_mainHelper.dragNode);

                        MoveDialog dialog = new MoveDialog(this, false, null, null);
                        dialog.ShowDialog();

                        this.RenewMovedFoldInfo(g_mainHelper.dragNode, dialog.NewFileId, dialog.OldFileId);

                        g_mainHelper.dropNode.ExpandAll();
                        g_mainHelper.dragNode = null;
                        g_mainHelper.dropNode = null;

                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("You have not authority to create folder(s).");
                }
            }

            g_mainHelper.dragging = false;
            this.RunGridLoadWorker();
        }

        private void OnFolderTreeViewDragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            DragHelper.ImageList_DragEnter(this.tvFolders.Handle, e.X - this.tvFolders.Left, e.Y - this.tvFolders.Top);
            g_mainHelper.dragging = true;
        }

        private void OnFolderTreeViewDragLeave(object sender, System.EventArgs e)
        {
            DragHelper.ImageList_DragLeave(this.tvFolders.Handle);
            g_mainHelper.dragging = false;
        }

        private void OnFolderTreeViewDragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point point = this.PointToClient(new Point(e.X, e.Y));
            DragHelper.ImageList_DragMove(point.X - this.tvFolders.Left, point.Y - this.tvFolders.Top);

            g_mainHelper.dropNode = this.tvFolders.GetNodeAt(this.tvFolders.PointToClient(new Point(e.X, e.Y)));

            if (g_mainHelper.dropNode == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move;

            if (g_mainHelper.overNode != g_mainHelper.dropNode)
            {
                DragHelper.ImageList_DragShowNolock(false);
                this.tvFolders.SelectedNode = g_mainHelper.dropNode;

                DragHelper.ImageList_DragShowNolock(true);
                g_mainHelper.overNode = g_mainHelper.dropNode;
            }

            TreeNode dropNode = g_mainHelper.dropNode;

            while (dropNode.Parent != null)
            {
                if (dropNode.Parent == g_mainHelper.dragNode)
                    e.Effect = DragDropEffects.None;

                dropNode = dropNode.Parent;
            }
        }

        private void OnFolderTreeViewEnter(object sender, System.EventArgs e)
        {
            this.m_filelistFocused = false;

            if (this.tvFolders.SelectedNode != null && tvFolders.LabelEdit == false)
            {
                DataRow tagrow = tvFolders.SelectedNode.Tag as DataRow;

                bbAuthority.Enabled = tagrow["control"].ToString() == "T";
            }
        }

        private void OnFolderTreeViewGiveFeedback(object sender, System.Windows.Forms.GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                e.UseDefaultCursors = false;
                this.tvFolders.Cursor = Cursors.Default;
            }
            else
            {
                e.UseDefaultCursors = true;
            }
        }

        private void OnFolderTreeViewItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            if (AppMediator.SINGLETON.CreatingFolder == false)
            {
                g_mainHelper.dragNode = (TreeNode)e.Item;

                DataRow dragrow = g_mainHelper.dragNode.Tag as DataRow;

                if (dragrow["cmodify"].ToString() == "T")
                {
                    g_mainHelper.dragging = true;
                    this.tvFolders.SelectedNode = g_mainHelper.dragNode;

                    this.ilDrags.Images.Clear();
                    this.ilDrags.ImageSize = new Size(g_mainHelper.dragNode.Bounds.Size.Width + this.tvFolders.Indent, g_mainHelper.dragNode.Bounds.Height);

                    Bitmap bitmap = new Bitmap(g_mainHelper.dragNode.Bounds.Width + this.tvFolders.Indent, g_mainHelper.dragNode.Bounds.Height);
                    Graphics graphics = Graphics.FromImage(bitmap);

                    graphics.DrawImage(this.ilTrees.Images[1], 0, 0);
                    graphics.DrawString(g_mainHelper.dragNode.Text, this.tvFolders.Font, new SolidBrush(this.tvFolders.ForeColor), (float)this.tvFolders.Indent, 1.0f);

                    this.ilDrags.Images.Add(bitmap);

                    Point point = this.tvFolders.PointToClient(Control.MousePosition);

                    int x = point.X + this.tvFolders.Indent - g_mainHelper.dragNode.Bounds.Left;
                    int y = point.Y - g_mainHelper.dragNode.Bounds.Top + 50;

                    if (DragHelper.ImageList_BeginDrag(this.ilDrags.Handle, 0, x, y))
                    {
                        this.tvFolders.DoDragDrop(bitmap, DragDropEffects.Move);
                        DragHelper.ImageList_EndDrag();
                    }
                }
            }
        }

        private void OnFolderTreeViewKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;

                this.tvFolders.LabelEdit = true;
                this.tvFolders.SelectedNode.Text = tagrow["rname"].ToString();
                this.tvFolders.SelectedNode.BeginEdit();
            }
        }

        private void OnFolderTreeViewMouseMove(object sender, MouseEventArgs e)
        {
            Point point = PointToClient(Control.MousePosition);
            TreeNode treeNode = this.tvFolders.GetNodeAt(point);

            if (treeNode == null)
                return;

            if (point.Y < 30)
            {
                if (treeNode.PrevVisibleNode != null)
                {
                    treeNode = treeNode.PrevVisibleNode;
                    DragHelper.ImageList_DragShowNolock(false);

                    treeNode.EnsureVisible();
                    this.tvFolders.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
            else if (point.Y > this.tvFolders.Size.Height - 30)
            {
                if (treeNode.NextVisibleNode != null)
                {
                    treeNode = treeNode.NextVisibleNode;
                    DragHelper.ImageList_DragShowNolock(false);

                    treeNode.EnsureVisible();
                    this.tvFolders.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
        }

        private void OnFolderTreeViewMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            Point point = new Point(e.X, e.Y);
            this.tvFolders.PointToClient(point);

            TreeNode treeNode = this.tvFolders.GetNodeAt(point);

            if (treeNode == null)
                return;

            Rectangle rect = new Rectangle(treeNode.Bounds.X - this.tvFolders.ImageList.ImageSize.Width,
                                           treeNode.Bounds.Y,
                                           treeNode.Bounds.Width + this.tvFolders.ImageList.ImageSize.Width,
                                           treeNode.Bounds.Height);

            if (rect.Contains(point) == true)
            {
                DataRow tagrow = treeNode.Tag as DataRow;

                this.tvFolders.SelectedNode = treeNode;

                if (this.tvFolders.SelectedNode != null)
                {
                    this.bbiTreeAddFolder.Enabled = tagrow["cfolder"].ToString() == "T";
                    this.bbiTreeDeleteFolder.Enabled = tagrow["cdelete"].ToString() == "T";
                    this.bbiTreeRight.Enabled = tagrow["control"].ToString() == "T";
                    this.bbiTreeUpdateName.Enabled = tagrow["cmodify"].ToString() == "T";
                    this.bbiTreeMoveFolder.Enabled = tagrow["cmodify"].ToString() == "T";
                }
                else
                {
                    this.bbiTreeAddFolder.Enabled = false;
                    this.bbiTreeDeleteFolder.Enabled = false;
                    this.bbiTreeRight.Enabled = false;
                    this.bbiTreeUpdateName.Enabled = false;
                    this.bbiTreeMoveFolder.Enabled = false;
                }

                // 관리자 페이지라면
                if (m_isPowerPage == true)
                {
                    // 비활성된 메뉴는 안보이게 처리한다.                    
                    if (bbiTreeAddFolder.Enabled == false)
                    {
                        bbiTreeAddFolder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {
                        bbiTreeAddFolder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }

                    if (bbiTreeRight.Enabled == false)
                    {
                        bbiTreeRight.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {
                        bbiTreeRight.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                }

                pmTreeMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void OnFolderTreeViewQueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e)
        {
            if (e.EscapePressed == true)
            {
                e.Action = DragAction.Cancel;
                g_mainHelper.dragging = false;
            }
        }

        //=========================================================================================
        // FileList Grid EventHandler
        //=========================================================================================
        private void OnFileListGridCustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "rname")
            {
                e.DisplayText = "     " + e.CellValue.ToString();

                System.Drawing.Icon icon = this.GetFileExtICon(fileGridView.GetDataRow(e.RowHandle)["rname"].ToString());

                if (icon != null)
                    e.Graphics.DrawIcon(icon, e.Bounds.Left + 2, e.Bounds.Top + 1);

                e.Handled = false;
            }
        }

        private void OnFileListGridDoubleClick(object sender, EventArgs e)
        {
            if (this.tvFolders.SelectedNode == null)
                return;

            Point point = this.fileGrid.PointToClient(new Point(MousePosition.X, MousePosition.Y));
            GridHitInfo hitinfo = this.fileGridView.CalcHitInfo(point);

            if (hitinfo.InRow == true && hitinfo.InRowCell == true)
            {
                DataRow tagrow = this.fileGridView.GetDataRow(this.fileGridView.GetSelectedRows()[0])["tag"] as DataRow;

                if (tagrow["cread"].ToString() == "T")
                    this.ExecuteFile();
            }
        }

        private void OnFileListGridDragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) == true)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;
                DataRow row = g_infoRows[1];

                // 폴더 guid, fileid 를 우선 설정한다.
                row["guid"] = tagrow["guid"].ToString();
                row["companyid"] = this.CompanyID;
                row["fileid"] = tagrow["fileid"].ToString();
                row["ftype"] = String.Empty;
                row["vsize"] = 0;
                row["vtype"] = String.Empty;
                row["rname"] = String.Empty;
                row["title"] = String.Empty;
                row["description"] = String.Empty;
                row["wdate"] = DateTime.Now;

                UploadDialog dialog = new UploadDialog(this);

                dialog.Show();
                dialog.UploadFile(files, g_infoTable.Copy());
            }

            this.Invalidate();
        }

        private void OnFileListGridDragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;

            if (tagrow["cfile"].ToString() == "T")
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop) == true)
                {
                    bool exists = true;

                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                    for (int i = 0; i < files.Length; i++)
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(files[i]);

                        if (fileInfo.Exists == false)
                        {
                            exists = false;
                            break;
                        }
                    }

                    if (exists == true)
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }

            e.Effect = DragDropEffects.None;
        }

        private void OnFileListGridEnter(object sender, System.EventArgs e)
        {            
            this.m_filelistFocused = true;

            if (this.fileGridView.SelectedRowsCount > 0)
            {                
                DataRow ftagrow = this.fileGridView.GetDataRow(this.m_selectIndex)["tag"] as DataRow;                

                bbAuthority.Enabled = ftagrow["control"].ToString() == "T";                
            }
        }

        private void OnFileListGridSelectedIndexChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                this.m_selectIndex = e.FocusedRowHandle;

                DataRow tagrow = this.fileGridView.GetDataRow(e.FocusedRowHandle)["tag"] as DataRow;

                this.txtTitle.Text = tagrow["title"].ToString();
                this.txtDesc.Text = tagrow["description"].ToString();
            }
            catch (Exception ex)
            {
                OFileLog.SNG.WriteLog(ex.ToString());
            }
        }

        private void OnFileListGridMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (this.tvFolders.SelectedNode != null)
            {
                DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;
                this.bbiListUpload.Enabled = tagrow["cfile"].ToString() == "T";
            }
            else
            {
                this.bbiListUpload.Enabled = false;
                this.bbiListDownload.Enabled = false;
            }

            if (this.fileGridView.SelectedRowsCount > 0)
            {
                int[] selectedRowIndices = this.fileGridView.GetSelectedRows();

                DataRow[] rows = new DataRow[selectedRowIndices.Length];

                for (int i = 0; i < selectedRowIndices.Length; i++)
                {
                    rows[i] = fileGridView.GetDataRow(selectedRowIndices[i]);
                }

                bool isDelete = true;
                bool isModify = true;

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow row = rows[i];
                    DataRow tagrow = row["tag"] as DataRow;

                    if (tagrow["cdelete"].ToString() == "F")
                        isDelete = false;

                    if (tagrow["cmodify"].ToString() == "F")
                        isModify = false;

                    if (this.WebHardWSDeleteOption == "T")
                    {
                        DateTime wdate = Convert.ToDateTime(tagrow["wdate"].ToString());
                        DateTime edate = DateTime.Now.AddDays(Convert.ToInt32(this.WebHardWSDeletePeriod));

                        if (edate < wdate)
                            isDelete = false;
                    }
                }

                DataRow ftagrow = this.fileGridView.GetDataRow(this.m_selectIndex)["tag"] as DataRow;

                this.bbiListExecuteFile.Enabled = ftagrow["cread"].ToString() == "T";
                this.bbiListDeleteFile.Enabled = isDelete;
                this.bbiListMoveFile.Enabled = isModify;
                this.bbiListDownload.Enabled = ftagrow["cread"].ToString() == "T";
                this.bbiListAssignRights.Enabled = ftagrow["control"].ToString() == "T";
                this.bbiListModifyInformation.Enabled = isModify;
            }
            else
            {
                this.bbiListExecuteFile.Enabled = false;
                this.bbiListDeleteFile.Enabled = false;
                this.bbiListMoveFile.Enabled = false;
                this.bbiListDownload.Enabled = false;
                this.bbiListAssignRights.Enabled = false;
                this.bbiListModifyInformation.Enabled = false;
            }

            pmListMenu.ShowPopup(Control.MousePosition);
        }

        private void OnFileListGridSelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (this.fileGridView.SelectedRowsCount > 0)
            {
                int[] selectedRowIndices = this.fileGridView.GetSelectedRows();

                double totalsize = 0;

                for (int i = 0; i < selectedRowIndices.Length; i++)
                {
                    if (selectedRowIndices[i] > 0)
                    {
                        DataRow tagrow = this.fileGridView.GetDataRow(selectedRowIndices[i])["tag"] as DataRow;

                        if (tagrow == null)
                            continue;

                        totalsize += Convert.ToInt64(tagrow["vsize"].ToString());
                    }
                }

                this.sbText.Caption = StringHelper.ConvertSizeToString(totalsize, selectedRowIndices.Length);

                if (this.m_filelistFocused == true)
                {
                    DataRow ftagrow = this.fileGridView.GetDataRow(this.m_selectIndex)["tag"] as DataRow;

                    bbAuthority.Enabled = ftagrow["control"].ToString() == "T";
                }
                
            }
            else
            {
                this.sbText.Caption = StringHelper.ConvertSizeToString(0, 0);
            }
        }

        //=========================================================================================
        // Folder TreeView ContextMenu EventHandler
        //=========================================================================================
        private void OnAddFolderMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CreateFolder();
        }

        private void OnDeleteFolderMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteFolder();
        }

        private void OnMoveFolderMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.MoveFolder();
        }

        private void OnUpdateNameMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.tvFolders.LabelEdit = true;

            DataRow tagrow = this.tvFolders.SelectedNode.Tag as DataRow;

            this.tvFolders.SelectedNode.Text = tagrow["rname"].ToString();
            this.tvFolders.SelectedNode.BeginEdit();
        }

        private void OnAssignFolderAuthMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetAuth("T");
        }

        //=========================================================================================
        // File Grid ContextMenu EventHandler
        //=========================================================================================
        private void OnExecuteFileMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ExecuteFile();
        }

        private void OnDeleteFileMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteFile();
        }

        private void OnMoveFileMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.MoveFile();
        }

        private void OnUploadFileMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.OnUploadBarLargeButtonItemClick(null, null);
        }

        private void OnDownloadFileMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FileDownload();
        }

        private void OnAssignFileAuthMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetFileAuth();
        }

        private void OnModifyFileMenuItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.UpdateFileInfo();
        }

        //=========================================================================================
        // Folder Tree Handling Functions
        //=========================================================================================
        /// <summary></summary>
        private void RunTreeLoadWorker()
        {
            if (this.treeWorker.IsBusy == false)
            {
                this.lbFolder.Text = "connecting...";
                this.SetBarItemEnabled(false);

                AsyncOperationManager.SynchronizationContext = new WindowsFormsSynchronizationContext();
                this.treeWorker.RunWorkerAsync();
            }
        }

        private void OnTreeWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Cancel = false;
            e.Result = null;

            try
            {
                while (true)
                {
                    //--------------------------------------------------------------------------------------------------------------
                    //
                    //--------------------------------------------------------------------------------------------------------------
                    double percent = 20.0;
                    worker.ReportProgress((int)percent);
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    // 상수 값을 초기화합니다.
                    this.m_maxAttachFileSize = AppMediator.SINGLETON.ConstantSelect("MaxAttachFileSize", "-1");

                    percent = 40.0;
                    worker.ReportProgress((int)percent);
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }

                    AppMediator.SINGLETON.LoadingFolder = true;

                    percent += 60.0;
                    worker.ReportProgress((int)percent);
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    AppMediator.SINGLETON.InitializePowerMode();

                    percent += 80.0;
                    worker.ReportProgress((int)percent);
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    AppMediator.SINGLETON.InitializeFolderSet();

                    percent = 100.0;
                    worker.ReportProgress((int)percent);
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    break;
                }
            }
            catch (Exception ex)
            {
                OFileLog.SNG.WriteLog(ex.ToString());

                e.Result = "Raise error while read data from database.\r\n" + ex.Message;
                e.Cancel = true;
            }
        }

        private void OnTreeWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.sbProgress.EditValue = e.ProgressPercentage;
            this.sbText.Caption = e.ProgressPercentage < 100 ? e.ProgressPercentage + "% 완료됨" : "폴더 구조 가져오는 중";
        }

        private void OnTreeWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled == true)
                {
                    this.sbText.Caption = "상태: 취소됨";
                }
                else
                {
                    this.sbText.Caption = "상태: 완료됨";
                    this.SetBarItemEnabled(true);

                    //------------------------------------------------------------------------------------------------------//
                    // Bindings
                    //------------------------------------------------------------------------------------------------------//
                    this.tvFolders.Nodes.Clear();
                    this.RefreshFolderTreeView();

                    this.bbPowerUser.Visibility = AppMediator.SINGLETON.PowerMode == true ? DevExpress.XtraBars.BarItemVisibility.Always :
                                                                                            DevExpress.XtraBars.BarItemVisibility.Never;
                    
                    this.tvFolders.Sorted = true;
                    this.tvFolders.Focus();

                    if (this.tvFolders.TopNode != null)
                    {
                        this.tvFolders.SelectedNode = this.tvFolders.TopNode;
                        this.tvFolders.SelectedNode.Expand();
                    }

                    this.RunGridLoadWorker();
                }
            }
            catch (Exception ex)
            {
                OFileLog.SNG.WriteLog(ex.ToString());
            }
            finally
            {
                AppMediator.SINGLETON.LoadingFolder = false;

                this.lbFolder.Text = "folder";
                this.sbProgress.EditValue = 0;
            }
        }

        //=========================================================================================
        // Grid List Handling Functions
        //=========================================================================================
        /// <summary></summary>
        public void RunGridLoadWorker()
        {
            this.txtSearch.Text = String.Empty;
            this.cbSearch.SelectedIndex = 0;

            this.m_searchResult = false;
            g_mainHelper.dragging = false;

            if (this.tvFolders.SelectedNode != null)
            {
                if (this.gridWorker.IsBusy == false)
                {
                    object[] param = new object[16];
                    param[0] = this.tvFolders.SelectedNode.Tag;

                    this.tvFolders.Enabled = false;

                    AsyncOperationManager.SynchronizationContext = new WindowsFormsSynchronizationContext();
                    this.gridWorker.RunWorkerAsync(param);
                }
            }
        }

        private void OnGridWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Cancel = false;
            e.Result = null;

            try
            {
                object[] param = (object[])e.Argument;

                while (true)
                {
                    //--------------------------------------------------------------------------------------------------------------
                    //
                    //--------------------------------------------------------------------------------------------------------------
                    double percent = 20.0;
                    worker.ReportProgress((int)percent);
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    DataRow row = param[0] as DataRow;
                    DataSet folderSet = AppMediator.SINGLETON.GetFolderInfo(row["fileid"].ToString(), AppMediator.SINGLETON.PowerUser);

                    param[1] = folderSet;

                    percent += 20.0;
                    worker.ReportProgress((int)percent);
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    DataRowCollection rows = folderSet.Tables[0].Rows;

                    if (rows.Count > 0)
                        param[2] = this.UpdateFolderList(rows[0]);

                    param[3] = this.GetAuthFileList(param[0]);

                    percent = 100.0;
                    worker.ReportProgress((int)percent);
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    e.Result = param;
                    break;
                }
            }
            catch (Exception ex)
            {
                OFileLog.SNG.WriteLog(ex.ToString());
                
                e.Result = "Raise error while read data from database.\r\n" + ex.Message;
                e.Cancel = true;
            }
        }

        private void OnGridWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.sbProgress.EditValue = e.ProgressPercentage;
            this.sbText.Caption = e.ProgressPercentage < 100 ? e.ProgressPercentage + "% 완료됨" : this.sbText.Caption = "파일 목록 가져오는 중";
        }

        private void OnGridWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    this.sbText.Caption = "상태: 취소됨";
                }
                else
                {
                    this.sbText.Caption = "상태: 완료됨";

                    //------------------------------------------------------------------------------------------------------//
                    // Bindings
                    //------------------------------------------------------------------------------------------------------//
                    object[] param = (object[])e.Result;

                    //수정필요
                    //아래 부분때문에 폴더 이동할 때 이동된 폴더 이름이 변경된다.
                    //this.g_mainHelper.currNode.Text = (string)param[2];
                    DataRow temprow = param[0] as DataRow;
                    DataRow selectedrow = tvFolders.SelectedNode.Tag as DataRow;

                    if (String.IsNullOrEmpty((string)param[2]) == false)
                    {                                                
                        if (temprow["guid"] == selectedrow["guid"])
                        {
                            this.tvFolders.SelectedNode.Text = (string)param[2];
                        }
                    }

                    DataSet folderSet = (DataSet)param[1];
                    DataRowCollection rows = folderSet.Tables[0].Rows;

                    if (rows.Count > 0)
                    {
                        if (AppMediator.SINGLETON.PowerUser == true)
                            this.tvFolders.SelectedNode.ForeColor = rows[0]["noauth"].ToString() == "0" ? Color.Red : Color.Black;

                        if (selectedrow["guid"] == temprow["guid"])
                        {
                            this.tvFolders.SelectedNode.Tag = rows[0];
                        }

                        g_mainHelper.currNode = this.tvFolders.SelectedNode;
                    }
                    else
                    {
                        this.ListTable.Rows.Clear();
                    }

                    this.BindAuthFileList((DataSet)param[3], this.tvFolders.SelectedNode);
                }
            }
            catch (Exception ex)
            {
                OFileLog.SNG.WriteLog(ex.ToString());
            }
            finally
            {
                this.sbProgress.EditValue = 0;
                this.tvFolders.Enabled = true;
            }
        }

        private void OnFoldersMouseClick(object sender, MouseEventArgs e)
        {
            /*
            if (this.tvFolders.SelectedNode != null)
            {
                DataRow tagrow = tvFolders.SelectedNode.Tag as DataRow;

                bbAuthority.Enabled = tagrow["control"].ToString() == "T";
            }
            */
        }

        private void OnfileGridMouseClick(object sender, MouseEventArgs e)
        {
            /*
            if (this.fileGridView.SelectedRowsCount > 0)
            {                
                DataRow ftagrow = this.fileGridView.GetDataRow(this.m_selectIndex)["tag"] as DataRow;                

                bbAuthority.Enabled = ftagrow["control"].ToString() == "T";                
            }
            */
        }
        
        //=========================================================================================
        //
        //=========================================================================================
    }
}