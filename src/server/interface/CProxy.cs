using System;
using System.IO;

using LIB.Data;
using WebHard.Interface;
using LIB.Configuration;

namespace WebHard.Interface
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class CProxy
    {
        //****************************************************************************************
        //
        //****************************************************************************************
        private CProxy()
        {
        }

        //****************************************************************************************
        //
        //****************************************************************************************
        private static WebHard.Interface.CProxy m_prxMediator = null;
        private static object m_syncRoot = new Object();

        /// <summary></summary>
        public static WebHard.Interface.CProxy PRX(string p_cocd)
        {
            if (m_prxMediator == null || (m_prxMediator != null && m_prxMediator.m_cocd != p_cocd))
            {
                lock (m_syncRoot)
                {
                    if (m_prxMediator == null || (m_prxMediator != null && m_prxMediator.m_cocd != p_cocd))
                    {
                        m_prxMediator = new WebHard.Interface.CProxy();
                        m_prxMediator.m_cocd = p_cocd;
                    }
                }
            }

            return m_prxMediator;
        }

        //****************************************************************************************
        //
        //****************************************************************************************
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

        ///****************************************************************************************
        ///
        ///****************************************************************************************
        private SVC.LIC.Interface.SProxy g_licIProxy
        {
            get
            {
                return SVC.LIC.Interface.SProxy.PRX(this.CompanyID);
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        public const int BUFFER_SIZE = 4096;
        public const int MAX_LENGTH_ONE_TIME = 579123;

        //=========================================================================================
        //
        //=========================================================================================
        private string m_conn = null;
        public string GetConnString
        {
            get
            {
                if (this.m_conn == null)
                    this.m_conn = g_licIProxy.GetCategoryConnString(SProxy.KindOfCategory, SProxy.KindOfProduct);
                return this.m_conn;
            }
        }

        private KindOfDatabase m_kindofdb = KindOfDatabase.NUL;
        public KindOfDatabase GetKindOfDB
        {
            get
            {
                if (m_kindofdb == KindOfDatabase.NUL)
                    m_kindofdb = SqlConverter.SNG.ConvertStringToKindOfDB(
                        g_licIProxy.ConstantSelect(SProxy.KindOfCategory, SProxy.KindOfProduct, "KindOfDatabase", "MSSQL").ToString());

                return m_kindofdb;
            }
        }

        private string m_productId = String.Empty;
        public string GetProductId
        {
            get
            {
                if (this.m_productId == String.Empty)
                    this.m_productId = this.ConstantSelect("ProductId", String.Empty);
                return this.m_productId;
            }
        }

        private string m_rootfolder = String.Empty;
        public string GetRootFolder
        {
            get
            {
                if (this.m_rootfolder == String.Empty)
                {
                    this.m_rootfolder = this.ConstantSelect("UpDownFolder", String.Empty);
                    this.m_rootfolder = Path.Combine(this.m_rootfolder, this.CompanyID);

                    if (Directory.Exists(this.m_rootfolder) == false)
                        Directory.CreateDirectory(this.m_rootfolder);
                }
                return this.m_rootfolder;
            }
        }

        private string m_tempfolder = String.Empty;
        public string GetTempFolder
        {
            get
            {
                if (this.m_tempfolder == String.Empty)
                {
                    this.m_tempfolder = Path.Combine(this.GetRootFolder, "temp");

                    if (Directory.Exists(this.m_tempfolder) == false)
                        Directory.CreateDirectory(this.m_tempfolder);
                }
                return this.m_tempfolder;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_appkey"></param>
        /// <param name="p_default"></param>
        /// <returns></returns>
        public string ConstantSelect(string p_appkey, string p_default)
        {
            return g_licIProxy.ConstantSelect(SProxy.KindOfCategory, SProxy.KindOfProduct, p_appkey, p_default);
        }

        //=========================================================================================
        //
        //=========================================================================================
        public void WriteLog(System.Reflection.MethodBase p_prvMethod, string p_eventMsg, string p_expire)
        {
            SVC.LOG.Interface.SProxy.PRX(this.CompanyID).WriteLog(p_prvMethod, p_eventMsg, p_expire);
        }

        //=========================================================================================
        //
        //=========================================================================================
    }

    //=============================================================================================
    //
    //=============================================================================================
    public class SelectedNodeInfo
    {
        public string p_ftype;
        public string p_level;
        public string p_unode;
        public string p_node;
        public string p_rnode;
        public string RealFilename;
        public string FileName;
        public string FilePath;
        public long FileSize;
        public string FileTye;
        public string WriteDate;
    }

    //=============================================================================================
    //
    //=============================================================================================
    public class FileInformation
    {
        public string Name;
        public long Length;
    }

    //=============================================================================================
    //
    //=============================================================================================
}