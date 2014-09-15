using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace WebHard.Interface
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public interface IProxy
    {
        //=========================================================================================
        // External functions
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        int CheckFolder(string p_cocd, Guid p_certkey, string p_ipadrs);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        DataSet CreateFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        DataSet CreateRootFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_guid"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_wdate"></param>
        /// <returns></returns>
        bool DeleteFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_guid, string p_fileid, DateTime p_wdate);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        bool DeleteFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_seekds"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        DataSet FileSearch(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_seekds, bool p_powerUser);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        DataSet GetAuthFileList(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        DataSet GetAuthList(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        DataSet GetFileInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        DataSet GetFolderInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        DataSet GetFolderList(string p_cocd, Guid p_certkey, string p_ipadrs, bool p_powerUser);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        DataSet GetOrgCenterList(string p_cocd, Guid p_certkey, string p_ipadrs);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fields"></param>
        /// <returns></returns>
        DataSet GetPersonInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fields);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        bool IsExistsFolderInFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        bool IsExistsRootFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        bool IsExistsSubFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        bool IsPowerUser(string p_cocd, Guid p_certkey, string p_ipadrs);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_srcset"></param>
        /// <param name="p_rmvset"></param>
        /// <returns></returns>
        bool MoveFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_srcset, DataSet p_rmvset);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_tfileid"></param>
        /// <param name="p_sfileid"></param>
        /// <returns></returns>
        string MoveFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_tfileid, string p_sfileid);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_checked"></param>
        /// <param name="p_ftype"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_authds"></param>
        /// <returns></returns>
        bool UpdateAuthList(string p_cocd, Guid p_certkey, string p_ipadrs, bool p_checked, string p_ftype, string p_fileid, DataSet p_authds);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_title"></param>
        /// <param name="p_desc"></param>
        /// <returns></returns>
        bool UpdateFileInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, string p_title, string p_desc);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        bool UpdateFolderName(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid);

        //=========================================================================================
        // DOWNLOAD FILE 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        string CheckDownloadFileHash(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        bool CloseDownloadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bufferSize"></param>
        /// <returns></returns>
        byte[] DownloadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        long GetFileSize(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        string Ping(string p_cocd, Guid p_certkey, string p_ipadrs);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_filesize"></param>
        /// <param name="p_maxlength"></param>
        /// <returns></returns>
        bool PrepareDownloadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, out long p_filesize, out int p_maxlength);

        //=========================================================================================
        // UPLOAD FILE 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        string CheckUploadFileHash(string p_cocd, Guid p_certkey, string p_ipadrs, string p_filename);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        void FailureCloseUploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_filename);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_infset"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_maxlength"></param>
        /// <returns></returns>
        bool PrepareUploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DataSet p_infset, out string p_fileid, out DateTime p_wdate, out string p_filename, out int p_maxlength);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_infset"></param>
        /// <returns></returns>
        DataSet SuccessCloseUploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, DataSet p_infset);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_buffer"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_writeCount"></param>
        /// <returns></returns>
        bool UploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_filename, byte[] p_buffer, long p_offset, int p_writeCount);

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
        bool AppendChunk(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, byte[] p_buffer, long p_offset, int p_bytesRead);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        string CheckFileHash(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename);

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <param name="p_offset"></param>
        /// <param name="p_bufferSize"></param>
        /// <returns></returns>
        byte[] DownloadChunk(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize);

        //=========================================================================================
        // 항목 코드 값 가져오는 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_items"></param>
        /// <returns></returns>
        DataSet GetGenInforItemList(string p_cocd, Guid p_certkey, string p_ipadrs, params string[] p_items);

        //=========================================================================================
        // 상수 값 가져오는 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_appkey"></param>
        /// <param name="p_default"></param>
        /// <returns></returns>
        string ConstantSelect(string p_cocd, Guid p_certkey, string p_ipadrs, string p_appkey, string p_default);

        //=========================================================================================
        // 다국어 지원 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_dataset"></param>
        /// <param name="p_tableindex"></param>
        /// <param name="p_culture"></param>
        /// <param name="p_columns"></param>
        /// <returns></returns>
        DataSet TranslateDataSet(string p_cocd, Guid p_certkey, string p_ipadrs, DataSet p_dataset, int p_tableindex, string p_culture, params string[] p_columns);

        /// <summary></summary>
        /// <param name="p_culture"></param>
        /// <param name="p_text"></param>
        /// <returns></returns>
        string TranslateText(string p_cocd, Guid p_certkey, string p_ipadrs, string p_culture, string p_text);

        //=========================================================================================
        //
        //=========================================================================================

        //=========================================================================================
        // license sever 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_defaultUserID"></param>
        /// <returns></returns>
        int iWHD_Initialize(string p_cocd, string p_defaultUserID);

        /// <summary></summary>
        /// <returns></returns>
        int DeleteiWHD_Initialize(string p_cocd);

        //=========================================================================================
        //
        //=========================================================================================
    }
}