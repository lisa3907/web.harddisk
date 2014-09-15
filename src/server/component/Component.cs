using System;
using System.Data;
using System.EnterpriseServices;
using System.Reflection;

using SVC.SSO.Interface;

namespace WebHard.Component.V34
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    [ConstructionEnabled(Enabled = true, Default = @"none")]
    [Description("WebHard.Component.V34")]
    [EventTrackingEnabled()]
    [JustInTimeActivation(true)]
    [ObjectPooling(Enabled = true, MinPoolSize = 8, MaxPoolSize = 32, CreationTimeout = 60000)]
    [Transaction(TransactionOption.NotSupported)]
    public class WebHardCOM : System.EnterpriseServices.ServicedComponent
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public WebHardCOM()
        {
        }

        //=========================================================================================
        //
        //=========================================================================================
        private string m_const = String.Empty;

        //=========================================================================================
        //
        //=========================================================================================
        private string m_cocd = String.Empty;
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

        //=========================================================================================
        //
        //=========================================================================================
        protected override void Activate()
        {
        }

        protected override bool CanBePooled()
        {
            return true;
        }

        protected override void Construct(string p_const)
        {
            this.m_const = p_const;
        }

        protected override void Deactivate()
        {
            base.Deactivate();
            GC.Collect();
        }

        //=========================================================================================
        //
        //=========================================================================================
        private WebHard.Interface.CProxy g_whdCProxy
        {
            get
            {
                return WebHard.Interface.CProxy.PRX(this.CompanyID);
            }
        }

        private WebHard.Library.LProxy g_whdLProxy
        {
            get
            {
                return WebHard.Library.LProxy.PRX(this.CompanyID);
            }
        }

        private SVC.SSO.Interface.SProxy g_ssoIProxy
        {
            get
            {
                return SVC.SSO.Interface.SProxy.PRX(this.CompanyID);
            }
        }

        //=========================================================================================
        //
        //=========================================================================================

        #region External functions

        //=========================================================================================
        // External functions
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        [AutoComplete]
        public int CheckFolder(Guid p_certapp, Guid p_certkey, string p_ipadrs)
        {
            int _result = -1;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.CheckFolder(p_certapp);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet CreateFolder(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.CreateFolder(p_certapp, _user.psid, p_rname, p_fileid);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet CreateRootFolder(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_rname)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.CreateRootFolder(p_certapp, _user.psid, p_rname);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_guid"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_wdate"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool DeleteFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_guid, string p_fileid, DateTime p_wdate)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.DeleteFile(p_certapp, p_guid, p_fileid, p_wdate);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool DeleteFolder(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.DeleteFolder(p_certapp, p_fileid);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_seekds"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet FileSearch(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_seekds, bool p_powerUser)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.FileSearch(p_certapp, _user.psid, p_fileid, p_seekds, p_powerUser);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet GetAuthFileList(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.GetAuthFileList(p_certapp, _user.psid, p_fileid, p_powerUser);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet GetAuthList(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.GetAuthList(p_certapp, p_fileid);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet GetFileInfo(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.GetFileInfo(p_certapp, _user.psid, p_fileid, p_powerUser);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet GetFolderInfo(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.GetFolderInfo(p_certapp, _user.psid, p_fileid, p_powerUser);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet GetFolderList(Guid p_certapp, Guid p_certkey, string p_ipadrs, bool p_powerUser)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.GetFolderList(p_certapp, _user.psid, p_powerUser);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet GetOrgCenterList(Guid p_certapp, Guid p_certkey, string p_ipadrs)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.GetOrgCenterList(p_certapp);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fields"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet GetPersonInfo(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fields)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.GetPersonInfo(p_certapp, _user.psid, p_fields);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsExistsFolderInFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.IsExistsFolderInFile(p_certapp, p_fileid);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsExistsRootFolder(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_rname)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.IsExistsRootFolder(p_certapp, p_rname);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsExistsSubFolder(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.IsExistsSubFolder(p_certapp, p_fileid);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsPowerUser(Guid p_certapp, Guid p_certkey, string p_ipadrs)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.IsPowerUser(p_certapp, _user.psid);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_srcset"></param>
        /// <param name="p_rmvset"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool MoveFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_srcset, DataSet p_rmvset)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.MoveFile(p_certapp, p_fileid, p_srcset, p_rmvset);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_tfileid"></param>
        /// <param name="p_sfileid"></param>
        /// <returns></returns>
        [AutoComplete]
        public string MoveFolder(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_tfileid, string p_sfileid)
        {
            string _result = String.Empty;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.MoveFolder(p_certapp, p_tfileid, p_sfileid);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_checked"></param>
        /// <param name="p_ftype"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_authds"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool UpdateAuthList(Guid p_certapp, Guid p_certkey, string p_ipadrs, bool p_checked, string p_ftype, string p_fileid, DataSet p_authds)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.UpdateAuthList(p_certapp, p_checked, p_ftype, p_fileid, p_authds);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_title"></param>
        /// <param name="p_desc"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool UpdateFileInfo(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_fileid, string p_title, string p_desc)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.UpdateFileInfo(p_certapp, p_fileid, p_title, p_desc);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool UpdateFolderName(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.UpdateFolderName(p_certapp, p_rname, p_fileid);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        #endregion

        #region DOWNLOAD FILE 관련 메서드

        //=========================================================================================
        // DOWNLOAD FILE 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        [AutoComplete]
        public string CheckDownloadFileHash(Guid p_certapp, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {
            string _result = String.Empty;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.CheckDownloadFileHash(p_certapp, p_wdate, p_filename);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool CloseDownloadFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.CloseDownloadFile(p_certapp, p_wdate, p_filename);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bufferSize"></param>
        /// <returns></returns>
        [AutoComplete]
        public byte[] DownloadFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
        {
            byte[] _result = null;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.DownloadFile(p_certapp, p_wdate, p_filename, p_offset, p_bufferSize);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetFileSize(Guid p_certapp, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {
            long _result = -1;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.GetFileSize(p_certapp, p_wdate, p_filename);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        [AutoComplete]
        public string Ping(Guid p_certapp, Guid p_certkey, string p_ipadrs)
        {
            string _result = String.Empty;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.Ping(p_certapp);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_filesize"></param>
        /// <param name="p_maxlength"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool PrepareDownloadFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, out long p_filesize, out int p_maxlength)
        {
            bool _result = false;

            p_filesize = -1;
            p_maxlength = -1;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.PrepareDownloadFile(p_certapp, p_wdate, p_filename, out p_filesize, out p_maxlength);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        #endregion

        #region UPLOAD FILE 관련 메서드

        //=========================================================================================
        // UPLOAD FILE 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        [AutoComplete]
        public string CheckUploadFileHash(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_filename)
        {
            string _result = String.Empty;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.CheckUploadFileHash(p_certapp, p_filename);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        [AutoComplete]
        public void FailureCloseUploadFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_filename)
        {
            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    g_whdLProxy.FailureCloseUploadFile(p_certapp, p_filename);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_infset"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_maxlength"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool PrepareUploadFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, DataSet p_infset, out string p_fileid, out DateTime p_wdate, out string p_filename, out int p_maxlength)
        {
            bool _result = false;

            p_fileid = String.Empty;
            p_wdate = DateTime.MinValue;
            p_filename = String.Empty;
            p_maxlength = -1;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.PrepareUploadFile(p_certapp, p_infset, out p_fileid, out p_wdate, out p_filename, out p_maxlength);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_infset"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet SuccessCloseUploadFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, DataSet p_infset)
        {
            DataSet _result = null;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.SuccessCloseUploadFile(p_certapp, _user.psid, p_wdate, p_filename, p_infset);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }
            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_buffer"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_writeCount"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool UploadFile(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_filename, byte[] p_buffer, long p_offset, int p_writeCount)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.UploadFile(p_certapp, p_filename, p_buffer, p_offset, p_writeCount);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        #endregion

        #region General File Transfer

        //=========================================================================================
        // General File Transfer 
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_buffer"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bytesRead"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool AppendChunk(Guid p_certapp, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, byte[] p_buffer, long p_offset, int p_bytesRead)
        {
            bool _result = false;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.AppendChunk(p_certapp, p_wdate, p_filename, p_buffer, p_offset, p_bytesRead);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        [AutoComplete]
        public string CheckFileHash(Guid p_certapp, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {
            string _result = String.Empty;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.CheckFileHash(p_certapp, p_wdate, p_filename);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bufferSize"></param>
        /// <returns></returns>
        [AutoComplete]
        public byte[] DownloadChunk(Guid p_certapp, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
        {
            byte[] _result = null;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.DownloadChunk(p_certapp, p_wdate, p_filename, p_offset, p_bufferSize);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        #endregion

        #region 항목 코드 값 가져오는 메서드

        //=========================================================================================
        // 항목 코드 값 가져오는 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_items"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet GetGenInforItemList(Guid p_certapp, Guid p_certkey, string p_ipadrs, params string[] p_items)
        {
            DataSet _result = new DataSet();

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.GetGenInforItemList(p_certapp, p_items);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        #endregion

        #region 상수 값 가져오는 메서드

        //=========================================================================================
        // 상수 값 가져오는 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_appkey"></param>
        /// <param name="p_default"></param>
        /// <returns></returns>
        [AutoComplete]
        public string ConstantSelect(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_appkey, string p_default)
        {
            string _result = String.Empty;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.ConstantSelect(p_certapp, p_appkey, p_default);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        #endregion

        #region 다국어 지원 메서드

        //=========================================================================================
        // 다국어 지원 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_dataset"></param>
        /// <param name="p_tableindex"></param>
        /// <param name="p_culture"></param>
        /// <param name="p_columns"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataSet TranslateDataSet(Guid p_certapp, Guid p_certkey, string p_ipadrs, DataSet p_dataset, int p_tableindex, string p_culture, params string[] p_columns)
        {
            DataSet _result = null;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.TranslateDataSet(p_certapp, p_dataset, p_tableindex, p_culture, p_columns);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }
            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_culture"></param>
        /// <param name="p_word"></param>
        /// <returns></returns>
        [AutoComplete]
        public string TranslateText(Guid p_certapp, Guid p_certkey, string p_ipadrs, string p_culture, string p_word)
        {
            string _result = String.Empty;

            try
            {
                UserClass _user = g_ssoIProxy.IsAuthenticatedUser(p_certkey, p_ipadrs);
                if (_user.exists == true)
                    _result = g_whdLProxy.TranslateText(p_certapp, p_culture, p_word);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        #endregion

        #region license sever 관련 메서드

        //=========================================================================================
        // license sever 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certapp"></param>
        /// <param name="p_defaultUserID"></param>
        /// <returns></returns>
        [AutoComplete]
        public int iWHD_Initialize(Guid p_certapp, string p_defaultUserID)
        {
            int _result = -1;

            try
            {
                _result = g_whdLProxy.iWHD_Initialize(p_certapp, p_defaultUserID);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certapp"></param>
        /// <returns></returns>
        [AutoComplete]
        public int DeleteiWHD_Initialize(Guid p_certapp)
        {
            int _result = -1;

            try
            {
                _result = g_whdLProxy.DeleteiWHD_Initialize(p_certapp);
            }
            catch (Exception ex)
            {
                g_whdCProxy.WriteLog(MethodInfo.GetCurrentMethod(), ex.ToString(), "live");
                throw ex;
            }

            return _result;
        }

        #endregion

        //=========================================================================================
        //
        //=========================================================================================
    }
}