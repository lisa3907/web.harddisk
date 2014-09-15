using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Grid;

using uBizSoft.LIB.Configuration;
using WebHard.Proxy;
using uBizSoft.UIC.Win.Control;

namespace WebHard.WinCtrl.Library
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class AppMediator
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        private AppMediator()
        {
        }

        //=========================================================================================
        //
        //=========================================================================================
        private static AppMediator m_appMediator = null;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public static AppMediator SINGLETON
        {
            get
            {
                if (m_appMediator == null)
                    m_appMediator = new AppMediator();
                return m_appMediator;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        public const string DOWNLOAD_AUTO_CLOSE = "SingDownAutoClose";
        public const string UPLOAD_AUTO_CLOSE = "UploadAutoClose";

        //=========================================================================================
        //
        //=========================================================================================
        private bool m_powerMode = false;
        private bool m_powerUser = false;

        private bool m_loadingFolder = false;
        private bool m_creatingFolder = false;

        private string m_ipaddress = String.Empty;

        private Guid m_certkey = Guid.Empty;
        private DataSet m_folderSet = null;
        private MainBox m_mainBox = null;
        private ResourceHelper m_resourceHelper = null;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public Guid CertKey
        {
            get
            {
                if (this.m_certkey == Guid.Empty)
                    this.m_certkey = new Guid(this.m_mainBox.CertKey);
                return this.m_certkey;
            }
        }

        /// <summary></summary>
        public string CompanyID
        {
            get
            {
                return this.m_mainBox.CompanyID;
            }
        }

        /// <summary></summary>
        public string Culture
        {
            get
            {
                return this.m_mainBox.Culture;
            }
        }

        /// <summary></summary>
        public DataSet FolderSet
        {
            get
            {
                return this.m_folderSet;
            }
        }

        /// <summary></summary>
        public string IPAddress
        {
            get
            {
                this.m_ipaddress = this.m_mainBox.IPAddress;

                if (String.IsNullOrEmpty(this.m_ipaddress) == true)
                    this.m_ipaddress = CfgHelper.SNG.IPAddress;
                return this.m_ipaddress;
            }
        }

        /// <summary></summary>
        public bool CreatingFolder
        {
            get
            {
                return this.m_creatingFolder;
            }
            set
            {
                this.m_creatingFolder = value;
            }
        }

        /// <summary></summary>
        public bool LoadingFolder
        {
            get
            {
                return this.m_loadingFolder;
            }
            set
            {
                this.m_loadingFolder = value;
            }
        }

        /// <summary></summary>
        public string MaxAttachFileSize
        {
            get
            {
                return this.m_mainBox.MaxAttachFileSize;
            }
        }

        /// <summary></summary>
        public bool PowerMode
        {
            get
            {
                return this.m_powerMode;
            }
            set
            {
                this.m_powerMode = value;
            }
        }

        /// <summary></summary>
        public bool PowerUser
        {
            get
            {
                return this.m_powerUser;
            }
            set
            {
                this.m_powerUser = value;
            }
        }

        /// <summary></summary>
        public ResourceHelper ResourceHelper
        {
            get
            {
                if (this.m_resourceHelper == null)
                    this.m_resourceHelper = new ResourceHelper();
                return this.m_resourceHelper;
            }
        }

        /// <summary></summary>
        public WebHard.Proxy.WhHelper WhdIProxy
        {
            get
            {
                return WebHard.Proxy.WhHelper.PRX(this.CompanyID, this.m_mainBox.ProxyWsUrl);
            }
        }

        //=========================================================================================
        // WhHelper 메서드 호출하는 부분
        //=========================================================================================
        #region WhHelper 메서드 호출하는 부분

        #region External functions

        //=========================================================================================
        // External functions
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet CreateFolder(string p_rname, string p_fileid)
        {
            return this.WhdIProxy.CreateFolder(this.CertKey, this.IPAddress, p_rname, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public DataSet CreateRootFolder(string p_rname)
        {
            return this.WhdIProxy.CreateRootFolder(this.CertKey, this.IPAddress, p_rname);
        }

        /// <summary></summary>
        /// <param name="p_guid"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_wdate"></param>
        /// <returns></returns>
        public bool DeleteFile(string p_guid, string p_fileid, DateTime p_wdate)
        {
            return this.WhdIProxy.DeleteFile(this.CertKey, this.IPAddress, p_guid, p_fileid, p_wdate);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool DeleteFolder(string p_fileid)
        {
            return this.WhdIProxy.DeleteFolder(this.CertKey, this.IPAddress, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <param name="p_seekds"></param>
        /// <returns></returns>
        public DataSet FileSearch(string p_fileid, DataSet p_seekds)
        {
            return this.WhdIProxy.FileSearch(this.CertKey, this.IPAddress, p_fileid, p_seekds, this.PowerUser);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet GetAuthFileList(string p_fileid)
        {
            return this.WhdIProxy.GetAuthFileList(this.CertKey, this.IPAddress, p_fileid, this.PowerUser);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet GetAuthList(string p_fileid)
        {
            return this.WhdIProxy.GetAuthList(this.CertKey, this.IPAddress, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet GetFileInfo(string p_fileid)
        {
            return this.WhdIProxy.GetFileInfo(this.CertKey, this.IPAddress, p_fileid, this.PowerUser);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFolderInfo(string p_fileid, bool p_powerUser)
        {
            return this.WhdIProxy.GetFolderInfo(this.CertKey, this.IPAddress, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <returns></returns>
        public DataSet GetFolderList()
        {
            return this.WhdIProxy.GetFolderList(this.CertKey, this.IPAddress, this.PowerUser);
        }

        /// <summary></summary>
        /// <returns></returns>
        public DataSet GetOrgCenterList()
        {
            return this.WhdIProxy.GetOrgCenterList(this.CertKey, this.IPAddress);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsFolderInFile(string p_fileid)
        {
            return this.WhdIProxy.IsExistsFolderInFile(this.CertKey, this.IPAddress, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public bool IsExistsRootFolder(string p_rname)
        {
            return this.WhdIProxy.IsExistsRootFolder(this.CertKey, this.IPAddress, p_rname);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsSubFolder(string p_fileid)
        {
            return this.WhdIProxy.IsExistsSubFolder(this.CertKey, this.IPAddress, p_fileid);
        }

        /// <summary></summary>
        /// <returns></returns>
        public bool IsPowerUser()
        {
            return this.WhdIProxy.IsPowerUser(this.CertKey, this.IPAddress);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <param name="p_srcds"></param>
        /// <param name="p_dstds"></param>
        /// <returns></returns>
        public bool MoveFile(string p_fileid, DataSet p_srcds, DataSet p_dstds)
        {
            return this.WhdIProxy.MoveFile(this.CertKey, this.IPAddress, p_fileid, p_srcds, p_dstds);
        }

        /// <summary></summary>
        /// <param name="p_tfileid"></param>
        /// <param name="p_sfileid"></param>
        /// <returns></returns>
        public string MoveFolder(string p_tfileid, string p_sfileid)
        {
            return this.WhdIProxy.MoveFolder(this.CertKey, this.IPAddress, p_tfileid, p_sfileid);
        }

        /// <summary></summary>
        /// <param name="p_checked"></param>
        /// <param name="p_ftype"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_authds"></param>
        /// <returns></returns>
        public bool UpdateAuthList(bool p_checked, string p_ftype, string p_fileid, DataSet p_authds)
        {
            return this.WhdIProxy.UpdateAuthList(this.CertKey, this.IPAddress, p_checked, p_ftype, p_fileid, p_authds);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <param name="p_title"></param>
        /// <param name="p_desc"></param>
        /// <returns></returns>
        public bool UpdateFileInfo(string p_fileid, string p_title, string p_desc)
        {
            return this.WhdIProxy.UpdateFileInfo(this.CertKey, this.IPAddress, p_fileid, p_title, p_desc);
        }

        /// <summary></summary>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool UpdateFolderName(string p_rname, string p_fileid)
        {
            return this.WhdIProxy.UpdateFolderName(this.CertKey, this.IPAddress, p_rname, p_fileid);
        }

        #endregion

        #region DOWNLOAD FILE 관련 메서드

        //=========================================================================================
        // DOWNLOAD FILE 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_wdate"></param>
        /// <param name="p_fileGuid"></param>
        /// <returns></returns>
        public string CheckDownloadFileHash(DateTime p_wdate, string p_fileGuid)
        {
            return this.WhdIProxy.CheckDownloadFileHash(this.CertKey, this.IPAddress, p_wdate, p_fileGuid);
        }

        /// <summary></summary>
        /// <param name="p_wdate"></param>
        /// <param name="p_fileGuid"></param>
        /// <returns></returns>
        public bool CloseDownloadFile(DateTime p_wdate, string p_fileGuid)
        {
            return this.WhdIProxy.CloseDownloadFile(this.CertKey, this.IPAddress, p_wdate, p_fileGuid);
        }

        /// <summary></summary>
        /// <param name="p_wdate"></param>
        /// <param name="p_fileGuid"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bufferSize"></param>
        /// <returns></returns>
        public byte[] DownloadFile(DateTime p_wdate, string p_fileGuid, long p_offset, int p_bufferSize)
        {
            return this.WhdIProxy.DownloadFile(this.CertKey, this.IPAddress, p_wdate, p_fileGuid, p_offset, p_bufferSize);
        }

        /// <summary></summary>
        /// <returns></returns>
        public string Ping()
        {
            return this.WhdIProxy.Ping(this.CertKey, this.IPAddress);
        }

        /// <summary></summary>
        /// <param name="p_wdate"></param>
        /// <param name="p_fileGuid"></param>
        /// <param name="p_filesize"></param>
        /// <param name="p_maxlength"></param>
        /// <returns></returns>
        public bool PrepareDownloadFile(DateTime p_wdate, string p_fileGuid, out long p_filesize, out int p_maxlength)
        {
            return this.WhdIProxy.PrepareDownloadFile(this.CertKey, this.IPAddress, p_wdate, p_fileGuid, out p_filesize, out p_maxlength);
        }

        #endregion

        #region UPLOAD FILE 관련 메서드

        //=========================================================================================
        // UPLOAD FILE 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_fileGuid"></param>
        /// <returns></returns>
        public string CheckUploadFileHash(string p_fileGuid)
        {
            return this.WhdIProxy.CheckUploadFileHash(this.CertKey, this.IPAddress, p_fileGuid);
        }

        /// <summary></summary>
        /// <param name="p_fileGuid"></param>
        public void FailureCloseUploadFile(string p_fileGuid)
        {
            this.WhdIProxy.FailureCloseUploadFile(this.CertKey, this.IPAddress, p_fileGuid);
        }

        /// <summary></summary>
        /// <param name="p_infset"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_fileGuid"></param>
        /// <param name="p_maxlength"></param>
        /// <returns></returns>
        public bool PrepareUploadFile(DataSet p_infset, out string p_fileid, out DateTime p_wdate, out string p_fileGuid, out int p_maxlength)
        {
            return this.WhdIProxy.PrepareUploadFile(this.CertKey, this.IPAddress, p_infset, out p_fileid, out p_wdate, out p_fileGuid, out p_maxlength);
        }

        /// <summary></summary>
        /// <param name="p_wdate"></param>
        /// <param name="p_fileGuid"></param>
        /// <param name="p_infset"></param>
        /// <returns></returns>
        public DataSet SuccessCloseUploadFile(DateTime p_wdate, string p_fileGuid, DataSet p_infset)
        {
            return this.WhdIProxy.SuccessCloseUploadFile(this.CertKey, this.IPAddress, p_wdate, p_fileGuid, p_infset);
        }

        /// <summary></summary>
        /// <param name="p_fileGuid"></param>
        /// <param name="p_buffer"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_writeCount"></param>
        /// <returns></returns>
        public bool UploadFile(string p_fileGuid, byte[] p_buffer, long p_offset, int p_writeCount)
        {
            return this.WhdIProxy.UploadFile(this.CertKey, this.IPAddress, p_fileGuid, p_buffer, p_offset, p_writeCount);
        }

        #endregion

        #region 항목 코드 값 가져오는 메서드

        //=========================================================================================
        // 항목 코드 값 가져오는 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_items"></param>
        /// <returns></returns>
        public DataSet GetGenInforItemList(params string[] p_items)
        {
            return this.WhdIProxy.GetGenInforItemList(this.CertKey, this.IPAddress, p_items);
        }

        #endregion

        #region 상수 값 가져오는 메서드

        //=========================================================================================
        // 상수 값 가져오는 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_appkey"></param>
        /// <param name="p_default"></param>
        /// <returns></returns>
        public string ConstantSelect(string p_appkey, string p_default)
        {
            return this.WhdIProxy.ConstantSelect(this.CertKey, this.IPAddress, p_appkey, p_default);
        }

        #endregion

        #region 다국어 지원 메서드

        //=========================================================================================
        // 다국어 지원 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_culture"></param>
        /// <param name="p_word"></param>
        /// <returns></returns>
        public string TranslateText(string p_culture, string p_word)
        {
            return this.WhdIProxy.TranslateText(this.CertKey, this.IPAddress, p_culture, p_word);
        }

        /// <summary></summary>
        /// <param name="p_dataset"></param>
        /// <param name="p_tableindex"></param>
        /// <param name="p_culture"></param>
        /// <param name="p_field"></param>
        /// <returns></returns>
        public DataSet TranslateText(DataSet p_dataset, int p_tableindex, string p_culture, string p_field)
        {
            return this.WhdIProxy.TranslateDataSet(this.CertKey, this.IPAddress, p_dataset, p_tableindex, p_culture, p_field);
        }

        #endregion

        #endregion

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_appkey"></param>
        /// <param name="p_defaultValue"></param>
        /// <returns></returns>
        public string GetConstantFromClient(string p_appkey, string p_defaultValue)
        {
            return (string)RegHelper.SNG.GetAppSettings(uBizSoft.LIB.Configuration.MKindOfLocation.Client,
                                                        uBizSoft.LIB.Configuration.MKindOfCategory.Products,
                                                        uBizSoft.LIB.Configuration.MKindOfProduct.WebHard,
                                                        p_appkey,
                                                        p_defaultValue);
        }

        /// <summary></summary>
        /// <returns></returns>
        public bool GetDownloadAutoCloseOption()
        {
            string option = this.GetConstantFromClient(DOWNLOAD_AUTO_CLOSE, "False");

            return Convert.ToBoolean(option);
        }

        /// <summary></summary>
        /// <param name="treeView"></param>
        /// <returns></returns>
        public TreeNodeCollection GetRootTreeNodeCollection(uTreeView treeView)
        {
            TreeNodeCollection result = null;

            if (AppMediator.SINGLETON.PowerUser == true)
            {
                // guid, companyid, fileid, ftype, vsize, vtype, rname,
                // title, description, attach, wcode, wname, wdate,
                // mtype, control, cmodify, cread, cdelete, cview, cfolder, cfile,
                // code, name, filecount, noauth, parentid, displayname
                DataRow newrow = this.m_mainBox.g_wctlSet.Tables["rootnode"].NewRow();

                newrow["guid"] = "";
                newrow["companyid"] = this.CompanyID;
                newrow["fileid"] = "";
                newrow["ftype"] = "T";
                newrow["vsize"] = 0;
                newrow["vtype"] = "";
                newrow["rname"] = "root";

                newrow["title"] = "";
                newrow["description"] = "";
                newrow["attach"] = "";
                newrow["wcode"] = "admin";
                newrow["wname"] = "operator";
                newrow["wdate"] = DateTime.Now;

                newrow["mtype"] = "F";
                newrow["control"] = "F";
                newrow["cmodify"] = "F";
                newrow["cread"] = "F";
                newrow["cdelete"] = "F";
                newrow["cview"] = "F";
                newrow["cfolder"] = "T";
                newrow["cfile"] = "F";

                newrow["code"] = "admin";
                newrow["name"] = "operator";
                newrow["filecount"] = 0;
                newrow["noauth"] = 1;
                newrow["parentid"] = "";
                newrow["displayname"] = "root";

                TreeNode treeNode = new TreeNode(newrow["displayname"].ToString(), 0, 0);
                treeNode.Tag = newrow;

                treeView.Nodes.Add(treeNode);

                result = treeNode.Nodes;// treeView.Nodes[0].Nodes;
            }
            else
            {
                result = treeView.Nodes;
            }

            return result;
        }

        /// <summary></summary>
        /// <returns></returns>
        public bool GetUploadAutoCloseOption()
        {
            string option = this.GetConstantFromClient(UPLOAD_AUTO_CLOSE, "False");

            return Convert.ToBoolean(option);
        }

        /// <summary></summary>
        /// <param name="p_mainBox"></param>
        public void Initialize(MainBox p_mainBox)
        {
            this.m_mainBox = p_mainBox;
        }

        /// <summary></summary>
        /// <param name="p_certString"></param>
        public void InitializeCertKey(string p_certString)
        {
            this.m_certkey = new Guid(p_certString);
        }

        /// <summary></summary>
        public void InitializeFolderSet()
        {
            this.m_folderSet = this.GetFolderList();
        }

        /// <summary></summary>
        public void InitializePowerMode()
        {
            this.m_powerMode = this.IsPowerUser();
        }

        /// <summary></summary>
        /// <param name="gridView"></param>
        public void SelectRow(GridView gridView)
        {
            int[] rows = gridView.GetSelectedRows();

            foreach (int i in rows)
                gridView.UnselectRow(i);

            if (rows.Length > 1)
                gridView.FocusedRowHandle = rows[0];

            gridView.SelectRow(gridView.FocusedRowHandle);
        }

        /// <summary></summary>
        /// <param name="p_appkey"></param>
        /// <param name="p_value"></param>
        public void SetConstant(string p_appkey, string p_value)
        {
            RegHelper.SNG.SetAppSettings(MKindOfLocation.Client, MKindOfCategory.Products, MKindOfProduct.WebHard, p_appkey, p_value);
        }

        /// <summary></summary>
        /// <param name="option"></param>
        public void SetDownloadAutoCloseOption(bool option)
        {
            this.SetConstant(DOWNLOAD_AUTO_CLOSE, option.ToString());
        }

        /// <summary></summary>
        /// <param name="option"></param>
        public void SetUploadAutoCloseOption(bool option)
        {
            this.SetConstant(UPLOAD_AUTO_CLOSE, option.ToString());
        }

        /// <summary></summary>
        /// <param name="p_caption"></param>
        /// <param name="p_action"></param>
        public void ShowNotImplementMessage(string p_caption, string p_action)
        {
            MessageBox.Show("[" + p_caption + "] " + this.ResourceHelper.TranslateWord("기능은 아직 구현되지 않았습니다."), p_action);
        }

        //=========================================================================================
        //
        //=========================================================================================
    }
}