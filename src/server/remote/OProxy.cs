using System;
//using System.Collections;
using System.Data;
//using System.Diagnostics;
//using System.IO;

using LIB.Communication.Remoting;
//using WebHard.Interface;

namespace WebHard.Remote
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class WebHardObject : RemoteObject, WebHard.Interface.IProxy
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public WebHardObject()
        {
        }

        //=========================================================================================
        //
        //=========================================================================================
        private WebHard.Interface.SProxy g_whdIProxy(string p_cocd)
        {
            return WebHard.Interface.SProxy.PRX(p_cocd);
        }

        private WebHard.Component.V34.WebHardCOM m_whdSProxy = null;
        private WebHard.Component.V34.WebHardCOM g_whdSProxy(string p_cocd)
        {
            this.m_whdSProxy = new WebHard.Component.V34.WebHardCOM();
            this.m_whdSProxy.CompanyID = p_cocd;

            return this.m_whdSProxy;
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
        public int CheckFolder(string p_cocd, Guid p_certkey, string p_ipadrs)
        {
            return g_whdSProxy(p_cocd).CheckFolder(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet CreateFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
        {
            return g_whdSProxy(p_cocd).CreateFolder(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_rname, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public DataSet CreateRootFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname)
        {
            return g_whdSProxy(p_cocd).CreateRootFolder(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_rname);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_guid"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_wdate"></param>
        /// <returns></returns>
        public bool DeleteFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_guid, string p_fileid, DateTime p_wdate)
        {
            return g_whdSProxy(p_cocd).DeleteFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_guid, p_fileid, p_wdate);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool DeleteFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            return g_whdSProxy(p_cocd).DeleteFolder(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_seekds"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet FileSearch(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_seekds, bool p_powerUser)
        {
            return g_whdSProxy(p_cocd).FileSearch(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid, p_seekds, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetAuthFileList(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {
            return g_whdSProxy(p_cocd).GetAuthFileList(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet GetAuthList(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            return g_whdSProxy(p_cocd).GetAuthList(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFileInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {
            return g_whdSProxy(p_cocd).GetFileInfo(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFolderInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {
            return g_whdSProxy(p_cocd).GetFolderInfo(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFolderList(string p_cocd, Guid p_certkey, string p_ipadrs, bool p_powerUser)
        {
            return g_whdSProxy(p_cocd).GetFolderList(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public DataSet GetOrgCenterList(string p_cocd, Guid p_certkey, string p_ipadrs)
        {
            return g_whdSProxy(p_cocd).GetOrgCenterList(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fields"></param>
        /// <returns></returns>
        public DataSet GetPersonInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fields)
        {
            return g_whdSProxy(p_cocd).GetPersonInfo(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fields);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsFolderInFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            return g_whdSProxy(p_cocd).IsExistsFolderInFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public bool IsExistsRootFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname)
        {
            return g_whdSProxy(p_cocd).IsExistsRootFolder(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_rname);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsSubFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid)
        {
            return g_whdSProxy(p_cocd).IsExistsSubFolder(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public bool IsPowerUser(string p_cocd, Guid p_certkey, string p_ipadrs)
        {
            return g_whdSProxy(p_cocd).IsPowerUser(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_srcset"></param>
        /// <param name="p_rmvset"></param>
        /// <returns></returns>
        public bool MoveFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_srcset, DataSet p_rmvset)
        {
            return g_whdSProxy(p_cocd).MoveFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid, p_srcset, p_rmvset);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_tfileid"></param>
        /// <param name="p_sfileid"></param>
        /// <returns></returns>
        public string MoveFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_tfileid, string p_sfileid)
        {
            return g_whdSProxy(p_cocd).MoveFolder(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_tfileid, p_sfileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_checked"></param>
        /// <param name="p_ftype"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_authds"></param>
        /// <returns></returns>
        public bool UpdateAuthList(string p_cocd, Guid p_certkey, string p_ipadrs, bool p_checked, string p_ftype, string p_fileid, DataSet p_authds)
        {
            return g_whdSProxy(p_cocd).UpdateAuthList(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_checked, p_ftype, p_fileid, p_authds);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_title"></param>
        /// <param name="p_desc"></param>
        /// <returns></returns>
        public bool UpdateFileInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, string p_title, string p_desc)
        {
            return g_whdSProxy(p_cocd).UpdateFileInfo(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_fileid, p_title, p_desc);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool UpdateFolderName(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
        {
            return g_whdSProxy(p_cocd).UpdateFolderName(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_rname, p_fileid);
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
        public string CheckDownloadFileHash(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {
            return g_whdSProxy(p_cocd).CheckDownloadFileHash(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public bool CloseDownloadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {
            return g_whdSProxy(p_cocd).CloseDownloadFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bufferSize"></param>
        /// <returns></returns>
        public byte[] DownloadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
        {
            return g_whdSProxy(p_cocd).DownloadFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public long GetFileSize(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {
            return g_whdSProxy(p_cocd).GetFileSize(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public string Ping(string p_cocd, Guid p_certkey, string p_ipadrs)
        {
            return g_whdSProxy(p_cocd).Ping(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_filesize"></param>
        /// <param name="p_maxlength"></param>
        /// <returns></returns>
        public bool PrepareDownloadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, out long p_filesize, out int p_maxlength)
        {
            return g_whdSProxy(p_cocd).PrepareDownloadFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_wdate, p_filename, out p_filesize, out p_maxlength);
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
        public string CheckUploadFileHash(string p_cocd, Guid p_certkey, string p_ipadrs, string p_filename)
        {
            return g_whdSProxy(p_cocd).CheckUploadFileHash(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        public void FailureCloseUploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_filename)
        {
            g_whdSProxy(p_cocd).FailureCloseUploadFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_filename);
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
        public bool PrepareUploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DataSet p_infset, out string p_fileid, out DateTime p_wdate, out string p_filename, out int p_maxlength)
        {
            return g_whdSProxy(p_cocd).PrepareUploadFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_infset, out p_fileid, out p_wdate, out p_filename, out p_maxlength);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_infset"></param>
        /// <returns></returns>
        public DataSet SuccessCloseUploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, DataSet p_infset)
        {
            return g_whdSProxy(p_cocd).SuccessCloseUploadFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_wdate, p_filename, p_infset);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_buffer"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_writeCount"></param>
        /// <returns></returns>
        public bool UploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_filename, byte[] p_buffer, long p_offset, int p_writeCount)
        {
            return g_whdSProxy(p_cocd).UploadFile(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_filename, p_buffer, p_offset, p_writeCount);
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
        public bool AppendChunk(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, byte[] p_buffer, long p_offset, int p_bytesRead)
        {
            return g_whdSProxy(p_cocd).AppendChunk(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_wdate, p_filename, p_buffer, p_offset, p_bytesRead);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public string CheckFileHash(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {
            return g_whdSProxy(p_cocd).CheckFileHash(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bufferSize"></param>
        /// <returns></returns>
        public byte[] DownloadChunk(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
        {
            return g_whdSProxy(p_cocd).DownloadChunk(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);
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
        public DataSet GetGenInforItemList(string p_cocd, Guid p_certkey, string p_ipadrs, params string[] p_items)
        {
            return g_whdSProxy(p_cocd).GetGenInforItemList(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_items);
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
        public string ConstantSelect(string p_cocd, Guid p_certkey, string p_ipadrs, string p_appkey, string p_default)
        {
            return g_whdSProxy(p_cocd).ConstantSelect(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_appkey, p_default);
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
        public DataSet TranslateDataSet(string p_cocd, Guid p_certkey, string p_ipadrs, DataSet p_dataset, int p_tableindex, string p_culture, params string[] p_columns)
        {
            return g_whdSProxy(p_cocd).TranslateDataSet(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_dataset, p_tableindex, p_culture, p_columns);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_culture"></param>
        /// <param name="p_text"></param>
        /// <returns></returns>
        public string TranslateText(string p_cocd, Guid p_certkey, string p_ipadrs, string p_culture, string p_text)
        {
            return g_whdSProxy(p_cocd).TranslateText(g_whdIProxy(p_cocd).g_certapp, p_certkey, p_ipadrs, p_culture, p_text);
        }

        #endregion

        //=========================================================================================
        //
        //=========================================================================================
    
        #region license sever 관련 메서드

        //=========================================================================================
        // license sever 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_defaultUserID"></param>
        /// <returns></returns>
        public int iWHD_Initialize(string p_cocd, string p_defaultUserID)
        {
            return g_whdSProxy(p_cocd).iWHD_Initialize(g_whdIProxy(p_cocd).g_certapp, p_defaultUserID);
        }

        /// <summary></summary>
        /// <returns></returns>
        public int DeleteiWHD_Initialize(string p_cocd)
        {
            return g_whdSProxy(p_cocd).DeleteiWHD_Initialize(g_whdIProxy(p_cocd).g_certapp);
        }

        #endregion
        //=========================================================================================
        //
        //=========================================================================================
    }
}