using System;
using System.Data;
using System.Text;

using LIB.Configuration;

namespace WebHard.Proxy
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class WhHelper
    {
        //=========================================================================================
        // 
        //=========================================================================================
        private WhHelper()
        {
        }

        //****************************************************************************************
        //
        //****************************************************************************************
        private static WebHard.Proxy.WhHelper m_prxMediator = null;
        private static object m_syncRoot = new Object();

        /// <summary></summary>
        public static WebHard.Proxy.WhHelper PRX(string p_cocd, string p_wsUrl)
        {
            if (m_prxMediator == null || (m_prxMediator != null && m_prxMediator.m_cocd != p_cocd))
            {
                lock (m_syncRoot)
                {
                    if (m_prxMediator == null || (m_prxMediator != null && m_prxMediator.m_cocd != p_cocd))
                    {
                        m_prxMediator = new WebHard.Proxy.WhHelper();

                        m_prxMediator.m_cocd = p_cocd;
                        m_prxMediator.m_wsUrl = p_wsUrl;
                    }
                }
            }

            return m_prxMediator;
        }

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

        private string m_wsUrl = String.Empty;
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
        private WebHard.Proxy.WebHardWS.WhdHelper m_whdWProxy = null;
        private WebHard.Proxy.WebHardWS.WhdHelper g_whdWProxy
        {
            get
            {
                if (this.m_whdWProxy == null)
                {
                    this.m_whdWProxy = new WebHard.Proxy.WebHardWS.WhdHelper();
                    this.m_whdWProxy.Url = this.WSUrl;
                }

                return this.m_whdWProxy;
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
        public int CheckFolder(Guid p_certkey, string p_ipadrs)
        {
            return g_whdWProxy.CheckFolder(this.CompanyID, p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet CreateFolder(Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
        {
            return g_whdWProxy.CreateFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public DataSet CreateRootFolder(Guid p_certkey, string p_ipadrs, string p_rname)
        {
            return g_whdWProxy.CreateRootFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_guid"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_wdate"></param>
        /// <returns></returns>
        public bool DeleteFile(Guid p_certkey, string p_ipadrs, string p_guid, string p_fileid, DateTime p_wdate)
        {
            return g_whdWProxy.DeleteFile(this.CompanyID, p_certkey, p_ipadrs, p_guid, p_fileid, p_wdate);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool DeleteFolder(Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            return g_whdWProxy.DeleteFolder(this.CompanyID, p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_seekds"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet FileSearch(Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_seekds, bool p_powerUser)
        {
            return g_whdWProxy.FileSearch(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_seekds, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetAuthFileList(Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {
            return g_whdWProxy.GetAuthFileList(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet GetAuthList(Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            return g_whdWProxy.GetAuthList(this.CompanyID, p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFileInfo(Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {
            return g_whdWProxy.GetFileInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFolderInfo(Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {
            return g_whdWProxy.GetFolderInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFolderList(Guid p_certkey, string p_ipadrs, bool p_powerUser)
        {
            return g_whdWProxy.GetFolderList(this.CompanyID, p_certkey, p_ipadrs, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public DataSet GetOrgCenterList(Guid p_certkey, string p_ipadrs)
        {
            return g_whdWProxy.GetOrgCenterList(this.CompanyID, p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fields"></param>
        /// <returns></returns>
        public DataSet GetPersonInfo(Guid p_certkey, string p_ipadrs, string p_fields)
        {

            return g_whdWProxy.GetPersonInfo(this.CompanyID, p_certkey, p_ipadrs, p_fields);

            //return g_whdSProxy.GetPersonInfo(p_certkey, p_ipadrs, p_fields);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsFolderInFile(Guid p_certkey, string p_ipadrs, string p_fileid)
        {

            return g_whdWProxy.IsExistsFolderInFile(this.CompanyID, p_certkey, p_ipadrs, p_fileid);

            //return g_whdSProxy.IsExistsFolderInFile(p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public bool IsExistsRootFolder(Guid p_certkey, string p_ipadrs, string p_rname)
        {

            return g_whdWProxy.IsExistsRootFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname);

            //return g_whdSProxy.IsExistsRootFolder(p_certkey, p_ipadrs, p_rname);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsSubFolder(Guid p_certkey, string p_ipadrs, string p_fileid)
        {

            return g_whdWProxy.IsExistsSubFolder(this.CompanyID, p_certkey, p_ipadrs, p_fileid);

            //return g_whdSProxy.IsExistsSubFolder(p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public bool IsPowerUser(Guid p_certkey, string p_ipadrs)
        {

            return g_whdWProxy.IsPowerUser(this.CompanyID, p_certkey, p_ipadrs);

            //return g_whdSProxy.IsPowerUser(p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_srcset"></param>
        /// <param name="p_rmvset"></param>
        /// <returns></returns>
        public bool MoveFile(Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_srcset, DataSet p_rmvset)
        {

            return g_whdWProxy.MoveFile(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_srcset, p_rmvset);

            //return g_whdSProxy.MoveFile(p_certkey, p_ipadrs, p_fileid, p_srcset, p_rmvset);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_tfileid"></param>
        /// <param name="p_sfileid"></param>
        /// <returns></returns>
        public string MoveFolder(Guid p_certkey, string p_ipadrs, string p_tfileid, string p_sfileid)
        {

            return g_whdWProxy.MoveFolder(this.CompanyID, p_certkey, p_ipadrs, p_tfileid, p_sfileid);

            //return g_whdSProxy.MoveFolder(p_certkey, p_ipadrs, p_tfileid, p_sfileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_checked"></param>
        /// <param name="p_ftype"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_authds"></param>
        /// <returns></returns>
        public bool UpdateAuthList(Guid p_certkey, string p_ipadrs, bool p_checked, string p_ftype, string p_fileid, DataSet p_authds)
        {

            return g_whdWProxy.UpdateAuthList(this.CompanyID, p_certkey, p_ipadrs, p_checked, p_ftype, p_fileid, p_authds);

            //return g_whdSProxy.UpdateAuthList(p_certkey, p_ipadrs, p_checked, p_ftype, p_fileid, p_authds);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_title"></param>
        /// <param name="p_desc"></param>
        /// <returns></returns>
        public bool UpdateFileInfo(Guid p_certkey, string p_ipadrs, string p_fileid, string p_title, string p_desc)
        {

            return g_whdWProxy.UpdateFileInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_title, p_desc);

            //return g_whdSProxy.UpdateFileInfo(p_certkey, p_ipadrs, p_fileid, p_title, p_desc);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool UpdateFolderName(Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
        {

            return g_whdWProxy.UpdateFolderName(this.CompanyID, p_certkey, p_ipadrs, p_rname, p_fileid);

            //return g_whdSProxy.UpdateFolderName(p_certkey, p_ipadrs, p_rname, p_fileid);
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
        public string CheckDownloadFileHash(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {

            return g_whdWProxy.CheckDownloadFileHash(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);

            //return g_whdSProxy.CheckDownloadFileHash(p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public bool CloseDownloadFile(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {

            return g_whdWProxy.CloseDownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);

            //return g_whdSProxy.CloseDownloadFile(p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bufferSize"></param>
        /// <returns></returns>
        public byte[] DownloadFile(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
        {

            return g_whdWProxy.DownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);

            //return g_whdSProxy.DownloadFile(p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public long GetFileSize(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {

            return g_whdWProxy.GetFileSize(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);

            //return g_whdSProxy.GetFileSize(p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public string Ping(Guid p_certkey, string p_ipadrs)
        {

            return g_whdWProxy.Ping(this.CompanyID, p_certkey, p_ipadrs);

            //return g_whdSProxy.Ping(p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_filesize"></param>
        /// <param name="p_maxlength"></param>
        /// <returns></returns>
        public bool PrepareDownloadFile(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, out long p_filesize, out int p_maxlength)
        {

            return g_whdWProxy.PrepareDownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, out p_filesize, out p_maxlength);

            //return g_whdSProxy.PrepareDownloadFile(p_certkey, p_ipadrs, p_wdate, p_filename, out p_filesize, out p_maxlength);
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
        public string CheckUploadFileHash(Guid p_certkey, string p_ipadrs, string p_filename)
        {

            return g_whdWProxy.CheckUploadFileHash(this.CompanyID, p_certkey, p_ipadrs, p_filename);

            //return g_whdSProxy.CheckUploadFileHash(p_certkey, p_ipadrs, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        public void FailureCloseUploadFile(Guid p_certkey, string p_ipadrs, string p_filename)
        {
            
            {
                g_whdWProxy.FailureCloseUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_filename);
            }
            //else
            {
                //g_whdSProxy.FailureCloseUploadFile(p_certkey, p_ipadrs, p_filename);
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
        public bool PrepareUploadFile(Guid p_certkey, string p_ipadrs, DataSet p_infset, out string p_fileid, out DateTime p_wdate, out string p_filename, out int p_maxlength)
        {

            return g_whdWProxy.PrepareUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_infset, out p_fileid, out p_wdate, out p_filename, out p_maxlength);

            //return g_whdSProxy.PrepareUploadFile(p_certkey, p_ipadrs, p_infset, out p_fileid, out p_wdate, out p_filename, out p_maxlength);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_infset"></param>
        /// <returns></returns>
        public DataSet SuccessCloseUploadFile(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, DataSet p_infset)
        {

            return g_whdWProxy.SuccessCloseUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_infset);

            //return g_whdSProxy.SuccessCloseUploadFile(p_certkey, p_ipadrs, p_wdate, p_filename, p_infset);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_buffer"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_writeCount"></param>
        /// <returns></returns>
        public bool UploadFile(Guid p_certkey, string p_ipadrs, string p_filename, byte[] p_buffer, long p_offset, int p_writeCount)
        {

            return g_whdWProxy.UploadFile(this.CompanyID, p_certkey, p_ipadrs, p_filename, p_buffer, p_offset, p_writeCount);

            //return g_whdSProxy.UploadFile(p_certkey, p_ipadrs, p_filename, p_buffer, p_offset, p_writeCount);
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
        public bool AppendChunk(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, byte[] p_buffer, long p_offset, int p_bytesRead)
        {

            return g_whdWProxy.AppendChunk(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_buffer, p_offset, p_bytesRead);

            //return g_whdSProxy.AppendChunk(p_certkey, p_ipadrs, p_wdate, p_filename, p_buffer, p_offset, p_bytesRead);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public string CheckFileHash(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {

            return g_whdWProxy.CheckFileHash(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);

            //return g_whdSProxy.CheckFileHash(p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bufferSize"></param>
        /// <returns></returns>
        public byte[] DownloadChunk(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
        {

            return g_whdWProxy.DownloadChunk(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);

            //return g_whdSProxy.DownloadChunk(p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);
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
        public DataSet GetGenInforItemList(Guid p_certkey, string p_ipadrs, params string[] p_items)
        {

            return g_whdWProxy.GetGenInforItemList(this.CompanyID, p_certkey, p_ipadrs, p_items);

            //return g_whdSProxy.GetGenInforItemList(p_certkey, p_ipadrs, p_items);
        }

        #endregion

        #region 상수 값 가져오는 메서드

        //=============================================================================================
        // 상수 값 가져오는 메서드
        //=============================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_appkey"></param>
        /// <param name="p_default"></param>
        /// <returns></returns>
        public string ConstantSelect(Guid p_certkey, string p_ipadrs, string p_appkey, string p_default)
        {

            return g_whdWProxy.ConstantSelect(this.CompanyID, p_certkey, p_ipadrs, p_appkey, p_default);

            //return g_whdSProxy.ConstantSelect(p_certkey, p_ipadrs, p_appkey, p_default);
        }

        #endregion

        #region 다국어 지원 메서드

        //=============================================================================================
        // 다국어 지원 메서드
        //=============================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_dataset"></param>
        /// <param name="p_tableindex"></param>
        /// <param name="p_culture"></param>
        /// <param name="p_columns"></param>
        /// <returns></returns>
        public DataSet TranslateDataSet(Guid p_certkey, string p_ipadrs, DataSet p_dataset, int p_tableindex, string p_culture, params string[] p_columns)
        {

            return g_whdWProxy.TranslateDataSet(this.CompanyID, p_certkey, p_ipadrs, p_dataset, p_tableindex, p_culture, p_columns);

            //return g_whdSProxy.TranslateDataSet(p_certkey, p_ipadrs, p_dataset, p_tableindex, p_culture, p_columns);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_culture"></param>
        /// <param name="p_word"></param>
        /// <returns></returns>
        public string TranslateText(Guid p_certkey, string p_ipadrs, string p_culture, string p_word)
        {

            return g_whdWProxy.TranslateText(this.CompanyID, p_certkey, p_ipadrs, p_culture, p_word);

            //return g_whdSProxy.TranslateText(p_certkey, p_ipadrs, p_culture, p_word);
        }

        #endregion

        //=========================================================================================
        //
        //=========================================================================================
    }
}