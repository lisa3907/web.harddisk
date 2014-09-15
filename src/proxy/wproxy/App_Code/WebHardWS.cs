using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Services;

//=================================================================================================
//
//=================================================================================================
/// <summary></summary>
[WebService(Namespace = "http://www.webhard.com/WebHard.WProxy/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WhdHelper : System.Web.Services.WebService
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public WhdHelper()
    {
    }

    //=============================================================================================
    //
    //=============================================================================================
    private WebHard.Interface.SProxy g_whdIProxy(string p_cocd)
    {
        return WebHard.Interface.SProxy.PRX(p_cocd);
    }

    //=============================================================================================
    //
    //=============================================================================================

    #region External functions

    //=============================================================================================
    // External functions
    //=============================================================================================
    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <returns></returns>
    [WebMethod]
    public int CheckFolder(string p_cocd, Guid p_certkey, string p_ipadrs)
    {
        return g_whdIProxy(p_cocd).CheckFolder(p_certkey, p_ipadrs);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_rname"></param>
    /// <param name="p_fileid"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet CreateFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
    {
        return g_whdIProxy(p_cocd).CreateFolder(p_certkey, p_ipadrs, p_rname, p_fileid);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_rname"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet CreateRootFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname)
    {
        return g_whdIProxy(p_cocd).CreateRootFolder(p_certkey, p_ipadrs, p_rname);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_guid"></param>
    /// <param name="p_fileid"></param>
    /// <param name="p_wdate"></param>
    /// <returns></returns>
    [WebMethod]
    public bool DeleteFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_guid, string p_fileid, DateTime p_wdate)
    {
        return g_whdIProxy(p_cocd).DeleteFile(p_certkey, p_ipadrs, p_guid, p_fileid, p_wdate);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <returns></returns>
    [WebMethod]
    public bool DeleteFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid)
    {
        return g_whdIProxy(p_cocd).DeleteFolder(p_certkey, p_ipadrs, p_fileid);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <param name="p_seekds"></param>
    /// <param name="p_powerUser"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet FileSearch(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_seekds, bool p_powerUser)
    {
        return g_whdIProxy(p_cocd).FileSearch(p_certkey, p_ipadrs, p_fileid, p_seekds, p_powerUser);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <param name="p_powerUser"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetAuthFileList(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
    {
        return g_whdIProxy(p_cocd).GetAuthFileList(p_certkey, p_ipadrs, p_fileid, p_powerUser);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetAuthList(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid)
    {
        return g_whdIProxy(p_cocd).GetAuthList(p_certkey, p_ipadrs, p_fileid);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <param name="p_powerUser"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetFileInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
    {
        return g_whdIProxy(p_cocd).GetFileInfo(p_certkey, p_ipadrs, p_fileid, p_powerUser);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <param name="p_powerUser"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetFolderInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
    {
        return g_whdIProxy(p_cocd).GetFolderInfo(p_certkey, p_ipadrs, p_fileid, p_powerUser);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_powerUser"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetFolderList(string p_cocd, Guid p_certkey, string p_ipadrs, bool p_powerUser)
    {
        return g_whdIProxy(p_cocd).GetFolderList(p_certkey, p_ipadrs, p_powerUser);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetOrgCenterList(string p_cocd, Guid p_certkey, string p_ipadrs)
    {
        return g_whdIProxy(p_cocd).GetOrgCenterList(p_certkey, p_ipadrs);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fields"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetPersonInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fields)
    {
        return g_whdIProxy(p_cocd).GetPersonInfo(p_certkey, p_ipadrs, p_fields);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <returns></returns>
    [WebMethod]
    public bool IsExistsFolderInFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid)
    {
        return g_whdIProxy(p_cocd).IsExistsFolderInFile(p_certkey, p_ipadrs, p_fileid);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_rname"></param>
    /// <returns></returns>
    [WebMethod]
    public bool IsExistsRootFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname)
    {
        return g_whdIProxy(p_cocd).IsExistsRootFolder(p_certkey, p_ipadrs, p_rname);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <returns></returns>
    [WebMethod]
    public bool IsExistsSubFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid)
    {
        return g_whdIProxy(p_cocd).IsExistsSubFolder(p_certkey, p_ipadrs, p_fileid);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <returns></returns>
    [WebMethod]
    public bool IsPowerUser(string p_cocd, Guid p_certkey, string p_ipadrs)
    {
        return g_whdIProxy(p_cocd).IsPowerUser(p_certkey, p_ipadrs);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <param name="p_srcset"></param>
    /// <param name="p_rmvset"></param>
    /// <returns></returns>
    [WebMethod]
    public bool MoveFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, DataSet p_srcset, DataSet p_rmvset)
    {
        return g_whdIProxy(p_cocd).MoveFile(p_certkey, p_ipadrs, p_fileid, p_srcset, p_rmvset);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_tfileid"></param>
    /// <param name="p_sfileid"></param>
    /// <returns></returns>
    [WebMethod]
    public string MoveFolder(string p_cocd, Guid p_certkey, string p_ipadrs, string p_tfileid, string p_sfileid)
    {
        return g_whdIProxy(p_cocd).MoveFolder(p_certkey, p_ipadrs, p_tfileid, p_sfileid);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_checked"></param>
    /// <param name="p_ftype"></param>
    /// <param name="p_fileid"></param>
    /// <param name="p_authds"></param>
    /// <returns></returns>
    [WebMethod]
    public bool UpdateAuthList(string p_cocd, Guid p_certkey, string p_ipadrs, bool p_checked, string p_ftype, string p_fileid, DataSet p_authds)
    {
        return g_whdIProxy(p_cocd).UpdateAuthList(p_certkey, p_ipadrs, p_checked, p_ftype, p_fileid, p_authds);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_fileid"></param>
    /// <param name="p_title"></param>
    /// <param name="p_desc"></param>
    /// <returns></returns>
    [WebMethod]
    public bool UpdateFileInfo(string p_cocd, Guid p_certkey, string p_ipadrs, string p_fileid, string p_title, string p_desc)
    {
        return g_whdIProxy(p_cocd).UpdateFileInfo(p_certkey, p_ipadrs, p_fileid, p_title, p_desc);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_rname"></param>
    /// <param name="p_fileid"></param>
    /// <returns></returns>
    [WebMethod]
    public bool UpdateFolderName(string p_cocd, Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
    {
        return g_whdIProxy(p_cocd).UpdateFolderName(p_certkey, p_ipadrs, p_rname, p_fileid);
    }

    #endregion

    #region DOWNLOAD FILE 관련 메서드

    //=============================================================================================
    // DOWNLOAD FILE 관련 메서드
    //=============================================================================================
    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <returns></returns>
    [WebMethod]
    public string CheckDownloadFileHash(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
    {
        return g_whdIProxy(p_cocd).CheckDownloadFileHash(p_certkey, p_ipadrs, p_wdate, p_filename);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <returns></returns>
    [WebMethod]
    public bool CloseDownloadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
    {
        return g_whdIProxy(p_cocd).CloseDownloadFile(p_certkey, p_ipadrs, p_wdate, p_filename);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <param name="p_offset"></param>
    /// <param name="p_bufferSize"></param>
    /// <returns></returns>
    [WebMethod]
    public byte[] DownloadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
    {
        return g_whdIProxy(p_cocd).DownloadFile(p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <returns></returns>
    [WebMethod]
    public long GetFileSize(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
    {
        return g_whdIProxy(p_cocd).GetFileSize(p_certkey, p_ipadrs, p_wdate, p_filename);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <returns></returns>
    [WebMethod]
    public string Ping(string p_cocd, Guid p_certkey, string p_ipadrs)
    {
        return g_whdIProxy(p_cocd).Ping(p_certkey, p_ipadrs);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <param name="p_filesize"></param>
    /// <param name="p_maxlength"></param>
    /// <returns></returns>
    [WebMethod]
    public bool PrepareDownloadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, out long p_filesize, out int p_maxlength)
    {
        return g_whdIProxy(p_cocd).PrepareDownloadFile(p_certkey, p_ipadrs, p_wdate, p_filename, out p_filesize, out p_maxlength);
    }

    #endregion

    #region UPLOAD FILE 관련 메서드

    //=============================================================================================
    // UPLOAD FILE 관련 메서드
    //=============================================================================================
    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_filename"></param>
    /// <returns></returns>
    [WebMethod]
    public string CheckUploadFileHash(string p_cocd, Guid p_certkey, string p_ipadrs, string p_filename)
    {
        return g_whdIProxy(p_cocd).CheckUploadFileHash(p_certkey, p_ipadrs, p_filename);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_filename"></param>
    [WebMethod]
    public void FailureCloseUploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_filename)
    {
        g_whdIProxy(p_cocd).FailureCloseUploadFile(p_certkey, p_ipadrs, p_filename);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_infset"></param>
    /// <param name="p_fileid"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <param name="p_maxlength"></param>
    /// <returns></returns>
    [WebMethod]
    public bool PrepareUploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, DataSet p_infset, out string p_fileid, out DateTime p_wdate, out string p_filename, out int p_maxlength)
    {
        return g_whdIProxy(p_cocd).PrepareUploadFile(p_certkey, p_ipadrs, p_infset, out p_fileid, out p_wdate, out p_filename, out p_maxlength);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <param name="p_infset"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet SuccessCloseUploadFile(string p_cocd,Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, DataSet p_infset)
    {
        return g_whdIProxy(p_cocd).SuccessCloseUploadFile(p_certkey, p_ipadrs, p_wdate, p_filename, p_infset);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_filename"></param>
    /// <param name="p_buffer"></param>
    /// <param name="p_offset"></param>
    /// <param name="p_writeCount"></param>
    /// <returns></returns>
    [WebMethod]
    public bool UploadFile(string p_cocd, Guid p_certkey, string p_ipadrs, string p_filename, byte[] p_buffer, long p_offset, int p_writeCount)
    {
        return g_whdIProxy(p_cocd).UploadFile(p_certkey, p_ipadrs, p_filename, p_buffer, p_offset, p_writeCount);
    }

    #endregion

    #region General File Transfer

    //=============================================================================================
    // General File Transfer 
    //=============================================================================================
    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <param name="p_buffer"></param>
    /// <param name="p_offset"></param>
    /// <param name="p_bytesRead"></param>
    /// <returns></returns>
    [WebMethod]
    public bool AppendChunk(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, byte[] p_buffer, long p_offset, int p_bytesRead)
    {
        return g_whdIProxy(p_cocd).AppendChunk(p_certkey, p_ipadrs, p_wdate, p_filename, p_buffer, p_offset, p_bytesRead);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <returns></returns>
    [WebMethod]
    public string CheckFileHash(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
    {
        return g_whdIProxy(p_cocd).CheckFileHash(p_certkey, p_ipadrs, p_wdate, p_filename);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_wdate"></param>
    /// <param name="p_filename"></param>
    /// <param name="p_offset"></param>
    /// <param name="p_bufferSize"></param>
    /// <returns></returns>
    [WebMethod]
    public byte[] DownloadChunk(string p_cocd, Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
    {
        return g_whdIProxy(p_cocd).DownloadChunk(p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);
    }

    #endregion

    #region 항목 코드 값 가져오는 메서드

    //=============================================================================================
    // 항목 코드 값 가져오는 메서드
    //=============================================================================================
    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_items"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetGenInforItemList(string p_cocd, Guid p_certkey, string p_ipadrs, params string[] p_items)
    {
        return g_whdIProxy(p_cocd).GetGenInforItemList(p_certkey, p_ipadrs, p_items);
    }

    #endregion

    #region 상수 값 가져오는 메서드

    //=============================================================================================
    // 상수 값 가져오는 메서드
    //=============================================================================================
    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_ipadrs"></param>
    /// <param name="p_appkey"></param>
    /// <param name="p_default"></param>
    /// <returns></returns>
    [WebMethod]
    public string ConstantSelect(string p_cocd, Guid p_certkey, string p_ipadrs, string p_appkey, string p_default)
    {
        return g_whdIProxy(p_cocd).ConstantSelect(p_certkey, p_ipadrs, p_appkey, p_default);
    }

    #endregion

    #region 다국어 지원 메서드

    //=============================================================================================
    // 다국어 지원 메서드
    //=============================================================================================
    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_dataset"></param>
    /// <param name="p_tableindex"></param>
    /// <param name="p_culture"></param>
    /// <param name="p_columns"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet TranslateDataSet(string p_cocd, Guid p_certkey, string p_ipadrs, DataSet p_dataset, int p_tableindex, string p_culture, params string[] p_columns)
    {
        return g_whdIProxy(p_cocd).TranslateDataSet(p_certkey, p_ipadrs, p_dataset, p_tableindex, p_culture, p_columns);
    }

    /// <summary></summary>
    /// <param name="p_cocd"></param>
    /// <param name="p_certkey"></param>
    /// <param name="p_culture"></param>
    /// <param name="p_text"></param>
    /// <returns></returns>
    [WebMethod]
    public string TranslateText(string p_cocd, Guid p_certkey, string p_ipadrs, string p_culture, string p_text)
    {
        return g_whdIProxy(p_cocd).TranslateText(p_certkey, p_ipadrs, p_culture, p_text);
    }

    #endregion

    //=============================================================================================
    //
    //=============================================================================================
}