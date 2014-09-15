using System;
using System.Data;
using System.Runtime.Serialization.Formatters;

using LIB.Configuration;

namespace WebHard.Interface
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class SProxy : IDisposable//, IProxy
    {
        //=========================================================================================
        //
        //=========================================================================================
        private SProxy()
        {
        }

        //****************************************************************************************
        //
        //****************************************************************************************
        private static WebHard.Interface.SProxy m_prxMediator = null;
        private static object m_syncRoot = new Object();

        /// <summary></summary>
        public static WebHard.Interface.SProxy PRX(string p_cocd)
        {
            if (m_prxMediator == null || (m_prxMediator != null && m_prxMediator.m_cocd != p_cocd))
            {
                lock (m_syncRoot)
                {
                    if (m_prxMediator == null || (m_prxMediator != null && m_prxMediator.m_cocd != p_cocd))
                    {
                        if (String.IsNullOrEmpty(p_cocd) == true)
                            throw new Exception("undefined company identification");
                        else
                        {
                            m_prxMediator = new WebHard.Interface.SProxy();
                            m_prxMediator.m_cocd = p_cocd;
                        }
                    }
                }
            }

            return m_prxMediator;
        }

        public static WebHard.Interface.SProxy SNG
        {
            get
            {
                return new WebHard.Interface.SProxy();
            }
        }

        //*********************************************************************************************************//
        //
        //*********************************************************************************************************//
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
        public const MKindOfCategory KindOfCategory = MKindOfCategory.Products;
        public const MKindOfProduct KindOfProduct = MKindOfProduct.WebHard;

        //=========================================================================================
        //
        //=========================================================================================
        private SVC.LIC.Interface.SProxy g_licIProxy
        {
            get
            {
                return SVC.LIC.Interface.SProxy.PRX(this.CompanyID);
            }
        }

        private static string m_serverUri = String.Empty;
        public string GetServerUri
        {
            get
            {
                if (m_serverUri == String.Empty)
                    m_serverUri = g_licIProxy.GetCategoryServerUri(SProxy.KindOfCategory, SProxy.KindOfProduct);

                return m_serverUri;
            }
        }

        private LIB.Communication.Remoting.RemoteConstant m_whdCProxy = null;
        public LIB.Communication.Remoting.RemoteConstant g_whdCProxy
        {
            get
            {
                if (m_whdCProxy == null || (m_whdCProxy != null && m_whdCProxy.CompanyID != this.CompanyID))
                {
                    m_whdCProxy = new LIB.Communication.Remoting.RemoteConstant(
                        this.CompanyID,
                         "WebHard Service V34",
                        "tcp",
                        "binary",
                        8093,
                        "WebHard.soap",
                        "Full",
                        "WebHard Proxy Server Event",

                        "fd3de1d3-f7c7-4e07-9d4b-3e77a3ee4041",
                        "WEBHARD_DBC",                              // AppConnString
                        "WEBHARD_CFG",                              // AppConfig
                        "WEBHARD_SVC",                              // AppSvcID (LogFileName)
                        "WEBHARD",                                  // AppSrvName

                        "3.4.2007.10",
                        "SingleCall"
                        );
                }

                return m_whdCProxy;
            }
        }

        private LIB.Communication.Remoting.RemoteProxy m_whdRProxy = null;
        public WebHard.Interface.IProxy g_whdRProxy
        {
            get
            {
                m_whdRProxy = new LIB.Communication.Remoting.RemoteProxy(
                    g_whdCProxy,
                    this.GetServerUri,
                    typeof(WebHard.Interface.IProxy)
                    );

                return (IProxy)m_whdRProxy.remoteObject;
            }
        }

        //**********************************************************************************************************//
        //
        //**********************************************************************************************************//
        private Guid m_certapp = Guid.Empty;
        public Guid g_certapp
        {
            get
            {
                if (m_certapp == Guid.Empty)
                    m_certapp = new Guid(g_whdCProxy.AppGuid);
                return m_certapp;
            }
        }

        public bool CheckValidApplication(Guid p_certapp)
        {
            return p_certapp.Equals(this.g_certapp);
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
            //    return g_whdSProxy.CheckFolder(this.CompanyID, p_certkey, p_ipadrs);


            return g_whdRProxy.CheckFolder(this.CompanyID, p_certkey, p_ipadrs);

            //return g_whdLProxy.CheckFolder(this.CompanyID, p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet CreateFolder(Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
        {



            //    return g_whdSProxy.CreateFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname, p_fileid);


            return g_whdRProxy.CreateFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname, p_fileid);

            //return g_whdLProxy.CreateFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public DataSet CreateRootFolder(Guid p_certkey, string p_ipadrs, string p_rname)
        {
            return g_whdRProxy.CreateRootFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname);
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



            //    return g_whdSProxy.DeleteFile(this.CompanyID, p_certkey, p_ipadrs, p_guid, p_fileid, p_wdate);


            return g_whdRProxy.DeleteFile(this.CompanyID, p_certkey, p_ipadrs, p_guid, p_fileid, p_wdate);

            //return g_whdLProxy.DeleteFile(this.CompanyID, p_certkey, p_ipadrs, p_guid, p_fileid, p_wdate);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool DeleteFolder(Guid p_certkey, string p_ipadrs, string p_fileid)
        {



            //    return g_whdSProxy.DeleteFolder(this.CompanyID, p_certkey, p_ipadrs, p_fileid);


            return g_whdRProxy.DeleteFolder(this.CompanyID, p_certkey, p_ipadrs, p_fileid);

            //return g_whdLProxy.DeleteFolder(this.CompanyID, p_certkey, p_ipadrs, p_fileid);
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



            //    return g_whdSProxy.FileSearch(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_seekds, p_powerUser);


            return g_whdRProxy.FileSearch(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_seekds, p_powerUser);

            //return g_whdLProxy.FileSearch(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_seekds, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetAuthFileList(Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {



            //    return g_whdSProxy.GetAuthFileList(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);


            return g_whdRProxy.GetAuthFileList(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);

            //return g_whdLProxy.GetAuthFileList(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet GetAuthList(Guid p_certkey, string p_ipadrs, string p_fileid)
        {



            //    return g_whdSProxy.GetAuthList(this.CompanyID, p_certkey, p_ipadrs, p_fileid);


            return g_whdRProxy.GetAuthList(this.CompanyID, p_certkey, p_ipadrs, p_fileid);

            //return g_whdLProxy.GetAuthList(this.CompanyID, p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFileInfo(Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {



            //    return g_whdSProxy.GetFileInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);


            return g_whdRProxy.GetFileInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);

            //return g_whdLProxy.GetFileInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFolderInfo(Guid p_certkey, string p_ipadrs, string p_fileid, bool p_powerUser)
        {



            //    return g_whdSProxy.GetFolderInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);


            return g_whdRProxy.GetFolderInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);

            //return g_whdLProxy.GetFolderInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFolderList(Guid p_certkey, string p_ipadrs, bool p_powerUser)
        {



            //    return g_whdSProxy.GetFolderList(this.CompanyID, p_certkey, p_ipadrs, p_powerUser);


            return g_whdRProxy.GetFolderList(this.CompanyID, p_certkey, p_ipadrs, p_powerUser);

            //return g_whdLProxy.GetFolderList(this.CompanyID, p_certkey, p_ipadrs, p_powerUser);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public DataSet GetOrgCenterList(Guid p_certkey, string p_ipadrs)
        {



            //    return g_whdSProxy.GetOrgCenterList(this.CompanyID, p_certkey, p_ipadrs);


            return g_whdRProxy.GetOrgCenterList(this.CompanyID, p_certkey, p_ipadrs);

            //return g_whdLProxy.GetOrgCenterList(this.CompanyID, p_certkey, p_ipadrs);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fields"></param>
        /// <returns></returns>
        public DataSet GetPersonInfo(Guid p_certkey, string p_ipadrs, string p_fields)
        {



            //    return g_whdSProxy.GetPersonInfo(this.CompanyID, p_certkey, p_ipadrs, p_fields);


            return g_whdRProxy.GetPersonInfo(this.CompanyID, p_certkey, p_ipadrs, p_fields);

            //return g_whdLProxy.GetPersonInfo(this.CompanyID, p_certkey, p_ipadrs, p_fields);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsFolderInFile(Guid p_certkey, string p_ipadrs, string p_fileid)
        {



            //    return g_whdSProxy.IsExistsFolderInFile(this.CompanyID, p_certkey, p_ipadrs, p_fileid);


            return g_whdRProxy.IsExistsFolderInFile(this.CompanyID, p_certkey, p_ipadrs, p_fileid);

            //return g_whdLProxy.IsExistsFolderInFile(this.CompanyID, p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public bool IsExistsRootFolder(Guid p_certkey, string p_ipadrs, string p_rname)
        {



            //    return g_whdSProxy.IsExistsRootFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname);


            return g_whdRProxy.IsExistsRootFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname);

            //return g_whdLProxy.IsExistsRootFolder(this.CompanyID, p_certkey, p_ipadrs, p_rname);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsSubFolder(Guid p_certkey, string p_ipadrs, string p_fileid)
        {



            //    return g_whdSProxy.IsExistsSubFolder(this.CompanyID, p_certkey, p_ipadrs, p_fileid);


            return g_whdRProxy.IsExistsSubFolder(this.CompanyID, p_certkey, p_ipadrs, p_fileid);

            //return g_whdLProxy.IsExistsSubFolder(this.CompanyID, p_certkey, p_ipadrs, p_fileid);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public bool IsPowerUser(Guid p_certkey, string p_ipadrs)
        {



            //    return g_whdSProxy.IsPowerUser(this.CompanyID, p_certkey, p_ipadrs);


            return g_whdRProxy.IsPowerUser(this.CompanyID, p_certkey, p_ipadrs);

            //return g_whdLProxy.IsPowerUser(this.CompanyID, p_certkey, p_ipadrs);
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



            //    return g_whdSProxy.MoveFile(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_srcset, p_rmvset);


            return g_whdRProxy.MoveFile(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_srcset, p_rmvset);

            //return g_whdLProxy.MoveFile(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_srcset, p_rmvset);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_tfileid"></param>
        /// <param name="p_sfileid"></param>
        /// <returns></returns>
        public string MoveFolder(Guid p_certkey, string p_ipadrs, string p_tfileid, string p_sfileid)
        {



            //    return g_whdSProxy.MoveFolder(this.CompanyID, p_certkey, p_ipadrs, p_tfileid, p_sfileid);


            return g_whdRProxy.MoveFolder(this.CompanyID, p_certkey, p_ipadrs, p_tfileid, p_sfileid);

            //return g_whdLProxy.MoveFolder(this.CompanyID, p_certkey, p_ipadrs, p_tfileid, p_sfileid);
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



            //    return g_whdSProxy.UpdateAuthList(this.CompanyID, p_certkey, p_ipadrs, p_checked, p_ftype, p_fileid, p_authds);


            return g_whdRProxy.UpdateAuthList(this.CompanyID, p_certkey, p_ipadrs, p_checked, p_ftype, p_fileid, p_authds);

            //return g_whdLProxy.UpdateAuthList(this.CompanyID, p_certkey, p_ipadrs, p_checked, p_ftype, p_fileid, p_authds);
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



            //    return g_whdSProxy.UpdateFileInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_title, p_desc);


            return g_whdRProxy.UpdateFileInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_title, p_desc);

            //return g_whdLProxy.UpdateFileInfo(this.CompanyID, p_certkey, p_ipadrs, p_fileid, p_title, p_desc);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool UpdateFolderName(Guid p_certkey, string p_ipadrs, string p_rname, string p_fileid)
        {



            //    return g_whdSProxy.UpdateFolderName(this.CompanyID, p_certkey, p_ipadrs, p_rname, p_fileid);


            return g_whdRProxy.UpdateFolderName(this.CompanyID, p_certkey, p_ipadrs, p_rname, p_fileid);

            //return g_whdLProxy.UpdateFolderName(this.CompanyID, p_certkey, p_ipadrs, p_rname, p_fileid);
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



            //    return g_whdSProxy.CheckDownloadFileHash(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);


            return g_whdRProxy.CheckDownloadFileHash(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);

            //return g_whdLProxy.CheckDownloadFileHash(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public bool CloseDownloadFile(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {



            //    return g_whdSProxy.CloseDownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);


            return g_whdRProxy.CloseDownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);

            //return g_whdLProxy.CloseDownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);
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



            //    return g_whdSProxy.DownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);


            return g_whdRProxy.DownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);

            //return g_whdLProxy.DownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public long GetFileSize(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {



            //    return g_whdSProxy.GetFileSize(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);


            return g_whdRProxy.GetFileSize(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);

            //return g_whdLProxy.GetFileSize(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public string Ping(Guid p_certkey, string p_ipadrs)
        {



            //    return g_whdSProxy.Ping(this.CompanyID, p_certkey, p_ipadrs);


            return g_whdRProxy.Ping(this.CompanyID, p_certkey, p_ipadrs);

            //return g_whdLProxy.Ping(this.CompanyID, p_certkey, p_ipadrs);
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



            //    return g_whdSProxy.PrepareDownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, out p_filesize, out p_maxlength);


            return g_whdRProxy.PrepareDownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, out p_filesize, out p_maxlength);

            //return g_whdLProxy.PrepareDownloadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, out p_filesize, out p_maxlength);
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



            //    return g_whdSProxy.CheckUploadFileHash(this.CompanyID, p_certkey, p_ipadrs, p_filename);


            return g_whdRProxy.CheckUploadFileHash(this.CompanyID, p_certkey, p_ipadrs, p_filename);

            //return g_whdLProxy.CheckUploadFileHash(this.CompanyID, p_certkey, p_ipadrs, p_filename);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        public void FailureCloseUploadFile(Guid p_certkey, string p_ipadrs, string p_filename)
        {

            
            //    g_whdSProxy.FailureCloseUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_filename);
            
            
            
            g_whdRProxy.FailureCloseUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_filename);
            
            
            
            //    g_whdLProxy.FailureCloseUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_filename);
            
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
            return g_whdRProxy.PrepareUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_infset, out p_fileid, out p_wdate, out p_filename, out p_maxlength);
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



            //    return g_whdSProxy.SuccessCloseUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_infset);


            return g_whdRProxy.SuccessCloseUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_infset);

            //return g_whdLProxy.SuccessCloseUploadFile(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_infset);
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



            //    return g_whdSProxy.UploadFile(this.CompanyID, p_certkey, p_ipadrs, p_filename, p_buffer, p_offset, p_writeCount);


            return g_whdRProxy.UploadFile(this.CompanyID, p_certkey, p_ipadrs, p_filename, p_buffer, p_offset, p_writeCount);

            //return g_whdLProxy.UploadFile(this.CompanyID, p_certkey, p_ipadrs, p_filename, p_buffer, p_offset, p_writeCount);
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



            //    return g_whdSProxy.AppendChunk(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_buffer, p_offset, p_bytesRead);


            return g_whdRProxy.AppendChunk(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_buffer, p_offset, p_bytesRead);

            //return g_whdLProxy.AppendChunk(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_buffer, p_offset, p_bytesRead);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public string CheckFileHash(Guid p_certkey, string p_ipadrs, DateTime p_wdate, string p_filename)
        {
            return g_whdRProxy.CheckFileHash(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename);
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



            //    return g_whdSProxy.DownloadChunk(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);


            return g_whdRProxy.DownloadChunk(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);

            //return g_whdLProxy.DownloadChunk(this.CompanyID, p_certkey, p_ipadrs, p_wdate, p_filename, p_offset, p_bufferSize);
        }

        #endregion

        #region 항목 코드 값 가져오는 메서드

        //=========================================================================================
        // 항목 코드 값 가져오는 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public DataSet GetGenInforItemList(Guid p_certkey, string p_ipadrs, params string[] p_items)
        {

            //    return g_whdSProxy.GetGenInforItemList(this.CompanyID, p_certkey, p_ipadrs, p_items);


            return g_whdRProxy.GetGenInforItemList(this.CompanyID, p_certkey, p_ipadrs, p_items);

            //return g_whdLProxy.GetGenInforItemList(this.CompanyID, p_certkey, p_ipadrs, p_items);
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
        public string ConstantSelect(Guid p_certkey, string p_ipadrs, string p_appkey, string p_default)
        {

            //    return g_whdSProxy.ConstantSelect(this.CompanyID, p_certkey, p_ipadrs, p_appkey, p_default);


            return g_whdRProxy.ConstantSelect(this.CompanyID, p_certkey, p_ipadrs, p_appkey, p_default);

            //return g_whdLProxy.ConstantSelect(this.CompanyID, p_certkey, p_ipadrs, p_appkey, p_default);
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
        public DataSet TranslateDataSet(Guid p_certkey, string p_ipadrs, DataSet p_dataset, int p_tableindex, string p_culture, params string[] p_columns)
        {
            return g_whdRProxy.TranslateDataSet(this.CompanyID, p_certkey, p_ipadrs, p_dataset, p_tableindex, p_culture, p_columns);
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_culture"></param>
        /// <param name="p_text"></param>
        /// <returns></returns>
        public string TranslateText(Guid p_certkey, string p_ipadrs, string p_culture, string p_text)
        {
            return g_whdRProxy.TranslateText(this.CompanyID, p_certkey, p_ipadrs, p_culture, p_text);
        }

        #endregion

        #region license sever 관련 메서드

        //=========================================================================================
        // license sever 관련 메서드
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_defaultUserID"></param>
        /// <returns></returns>
        public int iWHD_Initialize(string p_defaultUserID)
        {
            return g_whdRProxy.iWHD_Initialize(this.CompanyID, p_defaultUserID);
        }

        /// <summary></summary>
        /// <returns></returns>
        public int DeleteiWHD_Initialize()
        {
            return g_whdRProxy.DeleteiWHD_Initialize(this.CompanyID);
        }

        #endregion
        //=========================================================================================
        //
        //=========================================================================================

        #region IDisposable 멤버

        /// <summary></summary>
        public void Dispose()
        {
            if (this.m_whdRProxy != null)
                this.m_whdRProxy.Dispose();
        }

        #endregion

        //=========================================================================================
        //
        //=========================================================================================
    }
}