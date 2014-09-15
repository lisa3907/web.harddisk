using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using LIB.Data.Collection;
using LIB.Configuration;
using WebHard.Interface;

namespace WebHard.Library
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class LProxy
    {
        //=========================================================================================
        //
        //=========================================================================================
        private LProxy()
        {
        }

        //****************************************************************************************
        //
        //****************************************************************************************
        private static WebHard.Library.LProxy m_prxMediator = null;
        private static object m_syncRoot = new Object();

        /// <summary></summary>
        public static WebHard.Library.LProxy PRX(string p_cocd)
        {
            if (m_prxMediator == null || (m_prxMediator != null && m_prxMediator.m_cocd != p_cocd))
            {
                lock (m_syncRoot)
                {
                    if (m_prxMediator == null || (m_prxMediator != null && m_prxMediator.m_cocd != p_cocd))
                    {
                        m_prxMediator = new WebHard.Library.LProxy();
                        m_prxMediator.m_cocd = p_cocd;
                    }
                }
            }

            return m_prxMediator;
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
        private WebHard.Interface.CProxy g_whdCProxy
        {
            get
            {
                return WebHard.Interface.CProxy.PRX(this.CompanyID);
            }
        }

        private WebHard.Interface.SProxy g_whdIProxy
        {
            get
            {
                return WebHard.Interface.SProxy.PRX(this.CompanyID);
            }
        }

        private SVC.CTL.Interface.SProxy g_ctlIProxy
        {
            get
            {
                return SVC.CTL.Interface.SProxy.PRX(this.CompanyID);
            }
        }

        private SVC.ORG.Interface.SProxy g_orgIProxy
        {
            get
            {
                return SVC.ORG.Interface.SProxy.PRX(this.CompanyID);
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        private LIB.Data.DataHelper m_datHelper = null;
        private LIB.Data.DataHelper g_datHelper
        {
            get
            {
                if (this.m_datHelper == null)
                    this.m_datHelper = new LIB.Data.DataHelper(g_whdCProxy.GetKindOfDB);
                return this.m_datHelper;
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
        public int CheckFolder(Guid p_certapp)
        {
            int _result = 0;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _sqlstr = "-- CheckFolder -- \n"
                               + "DELETE TB_iWHD_AUTHORITY \n"
                               + " WHERE guid NOT IN (SELECT guid FROM TB_iWHD_DIRECTORY) \n";

                _result += g_datHelper.ExecuteText(g_whdCProxy.GetConnString, _sqlstr);
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet CreateFolder(Guid p_certapp, string p_psid, string p_rname, string p_fileid)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                DataSet _ds = g_orgIProxy.GetPersonInfo(p_psid, "name");

                string wcode = p_psid;
                string wname = Convert.ToString(_ds.Tables[0].Rows[0][0]);

                DatParameters _dbps = new DatParameters();
                string _authorCondition = this.GetAuthorityQuery(p_psid, true, ref _dbps);

                string _sqlstr = "-- 폴더(자식) 생성하는 스크립트(CreateFolder) \n"
                               + "DECLARE @newfileid nvarchar(256), @guid nvarchar(64) \n"
                               + " \n"
                               + "SELECT @newfileid=@fileid + RIGHT('000' + CONVERT(nvarchar, ISNULL(MAX(CONVERT(int, RIGHT(fileid, 4))), 0) + 1), 4) \n"
                               + "FROM   TB_iWHD_DIRECTORY \n"
                               + " WHERE companyid=@companyid \n"
                               + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n"
                               + " \n"
                               + "SET @guid = newid() \n"
                               + " \n"
                               + "INSERT INTO TB_iWHD_DIRECTORY \n"
                               + "( \n"
                               + "    guid, companyid, fileid, ftype, vsize, vtype, \n"
                               + "    rname, title, description, attach, wcode, wname \n"
                               + ") \n"
                               + "VALUES \n"
                               + "( \n"
                               + "    @guid, @companyid, @newfileid, @ftype, @vsize, @vtype, \n"
                               + "    @rname, @title, @description, @attach, @wcode, @wname \n"
                               + ") \n"
                               + " \n"
                               + "INSERT INTO TB_iWHD_AUTHORITY \n"
                               + "( \n"
                               + "       guid, member, mtype, name, control, cmodify, cread, cdelete, cview, cfolder, cfile \n"
                               + ") \n"
                               + "SELECT @guid, b.member, b.mtype, b.name, b.control, b.cmodify, b.cread, b.cdelete, b.cview, b.cfolder, b.cfile \n"
                               + "FROM   TB_iWHD_DIRECTORY a, TB_iWHD_AUTHORITY b \n"
                               + " WHERE a.guid = b.guid AND a.companyid=@companyid AND a.fileid=@fileid \n"
                               + " \n"
                               + "SELECT m.guid, m.companyid, m.fileid, m.ftype, m.vsize, m.vtype, m.rname, \n"
                               + "       m.title, m.description, m.attach, m.wcode, m.wname, m.wdate, \n"
                               + "       ISNULL(u.mtype, 'F') AS mtype, ISNULL(u.control, 'F') AS control, \n"
                               + "       ISNULL(u.cmodify, 'F') AS cmodify, ISNULL(u.cread, 'F') AS cread, \n"
                               + "       ISNULL(u.cdelete, 'F') AS cdelete, ISNULL(u.cview, 'F') AS cview, \n"
                               + "       ISNULL(u.cfolder, 'F') AS cfolder, ISNULL(u.cfile, 'F') AS cfile, \n"
                               + "       u.member AS code, u.name AS name, 0 AS filecount, 1 AS noauth, \n"
                               + "       SUBSTRING(m.fileid, 1, LEN(m.fileid) - 4) AS parentid, \n"
                               + "       m.rname AS displayname \n"
                               + "FROM   TB_iWHD_DIRECTORY m, TB_iWHD_AUTHORITY u \n"
                               + " WHERE m.guid = u.guid AND m.companyid=@companyid AND m.fileid=@newfileid \n" + _authorCondition;

                /*
                DataSet authorSet = g_orgIProxy.GetMembersByCode(g_whdCProxy.GetProductId, p_psid);
                DataSet personSet = authorSet.Copy();

                DataView userView = personSet.Tables[0].DefaultView;
                userView.RowFilter = "type = 'F'";

                if (userView.Count > 0)
                {
                    _sqlstr += "   AND (u.mtype = 'F' AND u.member = '" + userView[0]["code"].ToString() + "') \n";
                }
                else
                {
                    DataSet groupSet = authorSet.Copy();

                    DataView groupView = groupSet.Tables[0].DefaultView;
                    groupView.RowFilter = "type = 'T'";
                    groupView.Sort = "code";

                    if (groupView.Count > 0)
                        _sqlstr += "   AND (u.mtype = 'T' AND u.member = '" + groupView[0]["code"].ToString() + "') \n";
                }
                */

                _sqlstr += "ORDER BY m.fileid \n";

                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
                _dbps.Add("@ftype", SqlDbType.NVarChar, "T");
                _dbps.Add("@vsize", SqlDbType.Decimal, 0);
                _dbps.Add("@vtype", SqlDbType.NVarChar, String.Empty);
                _dbps.Add("@rname", SqlDbType.NVarChar, p_rname);
                _dbps.Add("@title", SqlDbType.NVarChar, String.Empty);
                _dbps.Add("@description", SqlDbType.NVarChar, String.Empty);
                _dbps.Add("@attach", SqlDbType.Xml, String.Empty);
                _dbps.Add("@wcode", SqlDbType.NVarChar, wcode);
                _dbps.Add("@wname", SqlDbType.NVarChar, wname);

                _result = g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public DataSet CreateRootFolder(Guid p_certapp, string p_psid, string p_rname)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                DataSet _ds = g_orgIProxy.GetPersonInfo(p_psid, "name");

                string wcode = p_psid;
                string wname = Convert.ToString(_ds.Tables[0].Rows[0][0]);

                // guid, companyid, fileid, ftype, vsize, vtype, rname,
                // title, description, attach, wcode, wname, wdate
                string _sqlstr = "-- 폴더(루트) 생성하는 스크립트(CreateRootFolder) \n"
                               + "DECLARE @newfileid nvarchar(256) \n"
                               + " \n"
                               + "SELECT @newfileid = RIGHT('000' + CONVERT(nvarchar, ISNULL(MAX(CONVERT(int, RIGHT(fileid, 4))), 0) + 1), 4) \n"
                               + "FROM   TB_iWHD_DIRECTORY \n"
                               + " WHERE companyid=@companyid AND LEN(fileid) <= 4 \n"
                               + " \n"
                               + "INSERT INTO TB_iWHD_DIRECTORY \n"
                               + "( \n"
                               + "    companyid, fileid, ftype, vsize, vtype, rname, \n"
                               + "    title, description, attach, wcode, wname \n"
                               + ") \n"
                               + "VALUES \n"
                               + "( \n"
                               + "    @companyid, @newfileid, @ftype, @vsize, @vtype, @rname, \n"
                               + "    @title, @description, @attach, @wcode, @wname \n"
                               + ") \n"
                               + " \n"
                               + "SELECT guid, companyid, fileid, ftype, vsize, vtype, rname, \n"
                               + "       title, description, attach, wcode, wname, wdate, \n"
                               + "       'F' AS mtype, 'T' AS control, 'F' AS cmodify, 'F' AS cread, \n"
                               + "       'F' AS cdelete, 'T' AS cview, 'F' AS cfolder, 'F' AS cfile, \n"
                               + "       @wcode AS code, @wname AS name, 0 AS filecount, 1 AS noauth, \n"
                               + "       '' AS parentid, rname AS displayname \n"
                               + "FROM   TB_iWHD_DIRECTORY \n"
                               + " WHERE companyid=@companyid AND fileid=@newfileid \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@ftype", SqlDbType.NVarChar, "T");
                _dbps.Add("@vsize", SqlDbType.Decimal, 0);
                _dbps.Add("@vtype", SqlDbType.NVarChar, String.Empty);
                _dbps.Add("@rname", SqlDbType.NVarChar, p_rname);
                _dbps.Add("@title", SqlDbType.NVarChar, String.Empty);
                _dbps.Add("@description", SqlDbType.NVarChar, String.Empty);
                _dbps.Add("@attach", SqlDbType.Xml, String.Empty);
                _dbps.Add("@wcode", SqlDbType.NVarChar, wcode);
                _dbps.Add("@wname", SqlDbType.NVarChar, wname);

                _result = g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
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
        public bool DeleteFile(Guid p_certapp, string p_guid, string p_fileid, DateTime p_wdate)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _sqlstr = "-- DeleteFile \n"
                               + "DELETE TB_iWHD_AUTHORITY WHERE guid=@guid \n"
                               + " \n"
                               + "DELETE TB_iWHD_DIRECTORY \n"
                               + " WHERE companyid=@companyid AND fileid=@fileid \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@guid", SqlDbType.NVarChar, p_guid);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);

                if (g_datHelper.ExecuteText(g_whdCProxy.GetConnString, _sqlstr, _dbps) != -1)
                {
                    string _path = this.GetFileFolder(p_wdate.Year, p_wdate.Month);

                    File.Delete(Path.Combine(_path, p_guid + ".bin"));
                    File.Delete(Path.Combine(_path, p_guid + ".xml"));

                    _result = true;
                }
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool DeleteFolder(Guid p_certapp, string p_fileid)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _sqlstr = "-- 파일을 목록을 가져오는 스크립트(DeleteFolder) \n"
                               + "SELECT * FROM TB_iWHD_DIRECTORY \n"
                               + " WHERE companyid=@companyid AND ftype=@ftypeF \n"
                               + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid \n"
                               + "ORDER BY fileid \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
                _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");

                DataSet _ds = g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);

                _sqlstr = "-- 폴더를 삭제하는 스크립트(DeleteFolder) \n"
                        + "DELETE TB_iWHD_AUTHORITY \n"
                        + " WHERE guid IN \n"
                        + "( \n"
                        + "       SELECT guid FROM TB_iWHD_DIRECTORY \n"
                        + "       WHERE  companyid=@companyid AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid \n"
                        + ") \n"
                        + " \n"
                        + "DELETE TB_iWHD_DIRECTORY \n"
                        + " WHERE companyid=@companyid AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid \n";

                _dbps.Clear();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);

                if (g_datHelper.ExecuteText(g_whdCProxy.GetConnString, _sqlstr, _dbps) != -1)
                {
                    foreach (DataRow row in _ds.Tables[0].Rows)
                    {
                        try
                        {
                            DateTime _wdate = (DateTime)row["wdate"];

                            string _path = this.GetFileFolder(_wdate.Year, _wdate.Month);

                            File.Delete(Path.Combine(_path, row["guid"].ToString() + ".bin"));
                            File.Delete(Path.Combine(_path, row["guid"].ToString() + ".xml"));
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }
                    }

                    _result = true;
                }
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
        public DataSet FileSearch(Guid p_certapp, string p_psid, string p_fileid, DataSet p_seekds, bool p_powerUser)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                DataRow row = p_seekds.Tables[0].Rows[0];

                string _seekey = row["seekey"].ToString();
                string _seeval = row["seeval"].ToString();

                _result = p_powerUser == true ? this.AdminFileSearch(p_psid, p_fileid, _seekey, _seeval) :
                                                this.UserFileSearch(p_psid, p_fileid, _seekey, _seeval);
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetAuthFileList(Guid p_certapp, string p_psid, string p_fileid, bool p_powerUser)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                _result = p_powerUser == true ? this.GetAdminAuthFileList(p_psid, p_fileid) :
                                                this.GetUserAuthFileList(p_psid, p_fileid);
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public DataSet GetAuthList(Guid p_certapp, string p_fileid)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
                _result = this.GetAuthListInfo(p_fileid);

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFileInfo(Guid p_certapp, string p_psid, string p_fileid, bool p_powerUser)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                _result = p_powerUser == true ? this.GetAdminFileInfo(p_psid, p_fileid) :
                                                this.GetUserFileInfo(p_psid, p_fileid);
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFolderInfo(Guid p_certapp, string p_psid, string p_fileid, bool p_powerUser)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                _result = p_powerUser == true ? this.GetAdminFolderInfo(p_psid, p_fileid) :
                                                this.GetUserFolderInfo(p_psid, p_fileid);
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_powerUser"></param>
        /// <returns></returns>
        public DataSet GetFolderList(Guid p_certapp, string p_psid, bool p_powerUser)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                _result = p_powerUser == true ? this.GetAdminFolderList(p_psid) :
                                                this.GetUserFolderList(p_psid);
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public DataSet GetOrgCenterList(Guid p_certapp)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
                _result = g_orgIProxy.GetMembersByName(g_whdCProxy.GetProductId, String.Empty);

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fields"></param>
        /// <returns></returns>
        public DataSet GetPersonInfo(Guid p_certapp, string p_psid, string p_fields)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
                _result = g_orgIProxy.GetPersonInfo(p_psid, p_fields);

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsFolderInFile(Guid p_certapp, string p_fileid)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _sqlstr
                    = "-- 파일을 가지고 있는지 확인하는 스크립트(IsExistsFolderInFile) \n"
                    + "SELECT COUNT(*) as norec FROM TB_iWHD_DIRECTORY \n"
                    + " WHERE companyid=@companyid AND ftype=@ftypeF \n"
                    + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
                _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");

                DataSet _ds = g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
                if (Convert.ToInt32(_ds.Tables[0].Rows[0][0]) > 0)
                    _result = true;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <returns></returns>
        public bool IsExistsRootFolder(Guid p_certapp, string p_rname)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _sqlstr
                    = "-- 지정된 이름을 가진 루트 폴더가 존재하는지 확인하는 스크립트(IsExistsRootFolder) \n"
                    + "SELECT COUNT(*) as norec FROM TB_iWHD_DIRECTORY \n"
                    + " WHERE companyid=@companyid AND ftype=@ftypeT \n"
                    + "   AND LEN(fileid) = 4 AND rname=@rname \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@ftypeT", SqlDbType.NVarChar, "T");
                _dbps.Add("@rname", SqlDbType.NVarChar, p_rname);

                DataSet _ds = g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
                if (Convert.ToInt32(_ds.Tables[0].Rows[0][0]) > 0)
                    _result = true;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool IsExistsSubFolder(Guid p_certapp, string p_fileid)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _sqlstr
                    = "-- 하위 폴더를 가지고 있는지 확인하는 스크립트(IsExistsSubFolder) \n"
                    + "SELECT COUNT(*) as norec FROM TB_iWHD_DIRECTORY \n"
                    + " WHERE companyid=@companyid AND ftype=@ftypeT \n"
                    + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
                _dbps.Add("@ftypeT", SqlDbType.NVarChar, "T");

                DataSet _ds = g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
                if (Convert.ToInt32(_ds.Tables[0].Rows[0][0]) > 0)
                    _result = true;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public bool IsPowerUser(Guid p_certapp, string p_psid)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
                _result = g_orgIProxy.IsManagerOfUser(g_whdCProxy.GetProductId, p_psid);

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_srcset"></param>
        /// <param name="p_rmvset"></param>
        /// <returns></returns>
        public bool MoveFile(Guid p_certapp, string p_fileid, DataSet p_srcset, DataSet p_rmvset)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                DataRowCollection _srcRows = p_srcset.Tables[0].Rows;
                DataRowCollection _rmvRows = p_rmvset.Tables[0].Rows;

                string _sqlstr = "-- MoveFile \n"
                               + "DECLARE @newfileid nvarchar(256) \n";

                for (int i = 0; i < _srcRows.Count; i++)
                {
                    DataRow _dr = _srcRows[i];

                    _sqlstr += "SELECT @newfileid=@fileid + RIGHT('000' + CONVERT(nvarchar, ISNULL(MAX(CONVERT(int, RIGHT(fileid, 4))), 0) + 1), 4) \n"
                            + "FROM   TB_iWHD_DIRECTORY \n"
                            + " WHERE companyid=@companyid \n"
                            + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n"
                            + " \n"
                            + "UPDATE TB_iWHD_DIRECTORY \n"
                            + "SET    fileid=@newfileid, \n"
                            + "       slmd = GETDATE() \n"
                            + " WHERE companyid=@companyid AND fileid = '" + _dr["fileid"].ToString() + "' \n";
                }

                string[] _fpaths = new string[_rmvRows.Count];
                string[] _xpaths = new string[_rmvRows.Count];

                // 옮길 폴더내에 같은 이름의 파일이 있을 경우 삭제하는 부분(사용자가 승인한 경우에만 삭제)
                for (int i = 0; i < _rmvRows.Count; i++)
                {
                    DataRow _dr = _rmvRows[i];

                    _sqlstr += "DELETE TB_iWHD_AUTHORITY \n"
                            + " WHERE guid = '" + _dr["guid"].ToString() + "' \n"
                            + " \n"
                            + "DELETE TB_iWHD_DIRECTORY \n"
                            + " WHERE companyid=@companyid AND fileid = '" + _dr["fileid"].ToString() + "' \n";

                    DateTime _wdate = (DateTime)_dr["wdate"];

                    string _path = this.GetFileFolder(_wdate.Year, _wdate.Month);

                    _fpaths[i] = Path.Combine(_path, _dr["guid"].ToString() + ".bin");
                    _xpaths[i] = Path.Combine(_path, _dr["guid"].ToString() + ".xml");
                }

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);

                if (g_datHelper.ExecuteText(g_whdCProxy.GetConnString, _sqlstr, _dbps) != -1)
                {
                    for (int _row = 0; _row < _fpaths.Length; _row++)
                    {
                        File.Delete(_fpaths[_row]);
                        File.Delete(_xpaths[_row]);
                    }
                    _result = true;
                }
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_tfileid"></param>
        /// <param name="p_sfileid"></param>
        /// <returns></returns>
        public string MoveFolder(Guid p_certapp, string p_tfileid, string p_sfileid)
        {
            string _result = String.Empty;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _sqlstr = "-- 폴더 이동하는 스크립트(MoveFolder) \n"
                               + "DECLARE @newfileid nvarchar(256) \n"
                               + " \n"
                               + "SELECT @newfileid=@tfileid + RIGHT('000' + CONVERT(nvarchar, ISNULL(MAX(CONVERT(int, RIGHT(fileid, 4))), 0) + 1), 4) \n"
                               + "FROM   TB_iWHD_DIRECTORY \n"
                               + " WHERE companyid=@companyid\n"
                               + "   AND SUBSTRING(fileid, 1, LEN(@tfileid))=@tfileid AND LEN(@tfileid) + 4 = LEN(fileid) \n"
                               + " \n"
                               + "UPDATE TB_iWHD_DIRECTORY \n"
                               + "SET    fileid=@newfileid + SUBSTRING(fileid, LEN(@sfileid) + 1, LEN(fileid) - LEN(@sfileid)) \n"
                               + " WHERE companyid=@companyid AND SUBSTRING(fileid, 1, LEN(@sfileid))=@sfileid \n"
                               + " \n"
                               + "SELECT @newfileid \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@tfileid", SqlDbType.NVarChar, p_tfileid);
                _dbps.Add("@sfileid", SqlDbType.NVarChar, p_sfileid);

                DataSet _ds = g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);

                _result = _ds.Tables[0].Rows[0][0].ToString();
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
        public bool UpdateAuthList(Guid p_certapp, bool p_checked, string p_ftype, string p_fileid, DataSet p_authds)
        {
            bool _result = true;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                DataRowCollection authorRows = p_authds.Tables[0].Rows;

                string _sqlstr = "-- UpdateAuthList \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);

                if (p_checked == true && p_ftype == "T")
                {
                    _sqlstr += "DELETE TB_iWHD_AUTHORITY \n"
                            + "FROM   TB_iWHD_AUTHORITY a, \n"
                            + "( \n"
                            + "       SELECT guid FROM TB_iWHD_DIRECTORY \n"
                            + "       WHERE  companyid=@companyid \n"
                            + "       AND    SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND fileid <> @fileid \n"
                            + ") b \n"
                            + " WHERE a.guid = b.guid \n";

                    foreach (DataRow row in authorRows)
                    {
                        _sqlstr += "INSERT INTO TB_iWHD_AUTHORITY \n"
                                + "( \n"
                                + "       guid, member, mtype, name, control, cmodify, cread, cdelete, cview, cfolder, cfile \n"
                                + ") \n"
                                + "SELECT guid, \n"
                                + "       '" + row["member"].ToString() + "', '" + row["mtype"].ToString() + "', \n"
                                + "       '" + row["name"].ToString() + "', '" + row["control"].ToString() + "', \n"
                                + "       '" + row["cmodify"].ToString() + "', '" + row["cread"].ToString() + "', \n"
                                + "       '" + row["cdelete"].ToString() + "', '" + row["cview"].ToString() + "', \n"
                                + "       '" + row["cfolder"].ToString() + "', '" + row["cfile"].ToString() + "' \n"
                                + "FROM   TB_iWHD_DIRECTORY \n"
                                + " WHERE companyid=@companyid \n"
                                + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND fileid <> @fileid \n";
                    }
                }

                _sqlstr += "DELETE TB_iWHD_AUTHORITY \n"
                        + " WHERE guid IN (SELECT guid FROM TB_iWHD_DIRECTORY WHERE companyid=@companyid AND fileid=@fileid) \n";

                foreach (DataRow row in authorRows)
                {
                    _sqlstr += "INSERT INTO TB_iWHD_AUTHORITY \n"
                            + "( \n"
                            + "       guid, member, mtype, name, control, cmodify, cread, cdelete, cview, cfolder, cfile \n"
                            + ") \n"
                            + "SELECT guid, \n"
                            + "       '" + row["member"].ToString() + "', '" + row["mtype"].ToString() + "', \n"
                            + "       '" + row["name"].ToString() + "', '" + row["control"].ToString() + "', \n"
                            + "       '" + row["cmodify"].ToString() + "', '" + row["cread"].ToString() + "', \n"
                            + "       '" + row["cdelete"].ToString() + "', '" + row["cview"].ToString() + "', \n"
                            + "       '" + row["cfolder"].ToString() + "', '" + row["cfile"].ToString() + "' \n"
                            + "FROM   TB_iWHD_DIRECTORY \n"
                            + " WHERE companyid=@companyid AND fileid=@fileid \n";
                }

                if (g_datHelper.ExecuteText(g_whdCProxy.GetConnString, _sqlstr, _dbps) == -1)
                    _result = false;
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
        public bool UpdateFileInfo(Guid p_certapp, string p_fileid, string p_title, string p_desc)
        {
            bool _result = true;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _sqlstr = "-- UpdateFileInfo \n"
                               + "UPDATE TB_iWHD_DIRECTORY \n"
                               + "SET    title=@title, \n"
                               + "       description=@description, \n"
                               + "       slmd = GETDATE() \n"
                               + " WHERE companyid=@companyid AND fileid=@fileid \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
                _dbps.Add("@title", SqlDbType.NVarChar, p_title);
                _dbps.Add("@description", SqlDbType.NVarChar, p_desc);

                if (g_datHelper.ExecuteText(g_whdCProxy.GetConnString, _sqlstr, _dbps) == -1)
                    _result = false;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_rname"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        public bool UpdateFolderName(Guid p_certapp, string p_rname, string p_fileid)
        {
            bool _result = true;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _sqlstr = "-- 폴더 이름 변경하는 스크립트(UpdateFolderName) \n"
                               + "UPDATE TB_iWHD_DIRECTORY \n"
                               + "SET    rname=@rname, \n"
                               + "       slmd = GETDATE() \n"
                               + " WHERE companyid=@companyid AND fileid=@fileid \n";

                DatParameters _dbps = new DatParameters();
                _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
                _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
                _dbps.Add("@rname", SqlDbType.NVarChar, p_rname);

                if (g_datHelper.ExecuteText(g_whdCProxy.GetConnString, _sqlstr, _dbps) == -1)
                    _result = false;
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
        public string CheckDownloadFileHash(Guid p_certapp, DateTime p_wdate, string p_filename)
        {
            string _result = String.Empty;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _realpath = Path.Combine(this.GetFileFolder(p_wdate.Year, p_wdate.Month), p_filename);

                SHA1CryptoServiceProvider _sha1Crypto = new SHA1CryptoServiceProvider();

                byte[] _hashBytes;

                using (FileStream _fs = new FileStream(_realpath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
                {
                    _hashBytes = _sha1Crypto.ComputeHash(_fs);
                }

                _result = BitConverter.ToString(_hashBytes);
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public bool CloseDownloadFile(Guid p_certapp, DateTime p_wdate, string p_filename)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _realpath = Path.Combine(this.GetFileFolder(p_wdate.Year, p_wdate.Month), p_filename);

                if (File.Exists(_realpath) == true)
                    _result = true;
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
        public byte[] DownloadFile(Guid p_certapp, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
        {
            byte[] _result = new byte[0];

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _realpath = Path.Combine(this.GetFileFolder(p_wdate.Year, p_wdate.Month), p_filename);

                if (File.Exists(_realpath) == true)
                {
                    long _filesize = new FileInfo(_realpath).Length;

                    if (p_offset <= _filesize)
                    {
                        int _readbytes;

                        using (FileStream _fs = new FileStream(_realpath, FileMode.Open, FileAccess.Read))
                        {
                            _fs.Seek(p_offset, SeekOrigin.Begin);

                            _result = new byte[p_bufferSize];
                            _readbytes = _fs.Read(_result, 0, p_bufferSize);
                        }

                        if (_readbytes != p_bufferSize)
                            Array.Resize<byte>(ref _result, _readbytes);
                    }
                }
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public long GetFileSize(Guid p_certapp, DateTime p_wdate, string p_filename)
        {
            long _result = 0;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _filepath = Path.Combine(this.GetFileFolder(p_wdate.Year, p_wdate.Month), p_filename);

                if (File.Exists(_filepath) == true)
                    _result = new FileInfo(_filepath).Length;
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <returns></returns>
        public string Ping(Guid p_certapp)
        {
            string _result = String.Empty;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
                _result = "PNG 50";

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
        public bool PrepareDownloadFile(Guid p_certapp, DateTime p_wdate, string p_filename, out long p_filesize, out int p_maxlength)
        {
            bool _result = false;

            p_filesize = p_maxlength = 0;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _realpath = Path.Combine(this.GetFileFolder(p_wdate.Year, p_wdate.Month), p_filename);

                if (File.Exists(_realpath) == true)
                {
                    FileInfo _fileinfo = new FileInfo(_realpath);

                    p_filesize = _fileinfo.Length;
                    p_maxlength = WebHard.Interface.CProxy.MAX_LENGTH_ONE_TIME;

                    _result = true;
                }
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
        public string CheckUploadFileHash(Guid p_certapp, string p_filename)
        {
            string _result = String.Empty;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _tempfile = Path.Combine(g_whdCProxy.GetTempFolder, p_filename);

                SHA1CryptoServiceProvider _sha1Crypto = new SHA1CryptoServiceProvider();

                byte[] _hashBytes;

                using (FileStream _fs = new FileStream(_tempfile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
                {
                    _hashBytes = _sha1Crypto.ComputeHash(_fs);
                }
                _result = BitConverter.ToString(_hashBytes);
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_filename"></param>
        public bool FailureCloseUploadFile(Guid p_certapp, string p_filename)
        {
            bool result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _tempfile = Path.Combine(g_whdCProxy.GetTempFolder, p_filename);

                if (File.Exists(_tempfile) == true)
                    File.Delete(_tempfile);

                result = true;
            }

            return result;
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
        public bool PrepareUploadFile(Guid p_certapp, DataSet p_infset, out string p_fileid, out DateTime p_wdate, out string p_filename, out int p_maxlength)
        {
            bool _result = false;

            p_fileid = String.Empty;
            p_wdate = DateTime.Now;
            p_filename = Guid.NewGuid().ToString() + ".bin";
            p_maxlength = CProxy.MAX_LENGTH_ONE_TIME;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                DataSet _ds = this.IsFileExists(p_infset);

                if (_ds.Tables[0].Rows.Count > 0)
                {
                    p_fileid = _ds.Tables[0].Rows[0]["fileid"].ToString();
                }
                else
                {
                    _result = true;
                }
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
        public DataSet SuccessCloseUploadFile(Guid p_certapp, string p_psid, DateTime p_wdate, string p_filename, DataSet p_infset)
        {
            DataSet _result = null;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _tempfile = Path.Combine(g_whdCProxy.GetTempFolder, p_filename);

                string _realfolder = this.GetFileFolder(p_wdate.Year, p_wdate.Month);
                string _realfile = Path.Combine(_realfolder, p_filename);

                if (File.Exists(_realfile) == true)
                    File.Delete(_realfile);

                File.Move(_tempfile, _realfile);

                _result = this.CreateFile(p_wdate, p_infset.Copy(), p_psid);
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
        public bool UploadFile(Guid p_certapp, string p_filename, byte[] p_buffer, long p_offset, int p_writeCount)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _tempfile = Path.Combine(g_whdCProxy.GetTempFolder, p_filename);

                bool _isexist = File.Exists(_tempfile);

                if (_isexist == false || p_offset == 0)
                    File.Create(_tempfile).Close();

                long _filesize = new FileInfo(_tempfile).Length;

                if (_filesize == p_offset)
                {
                    using (FileStream _fs = new FileStream(_tempfile, FileMode.Append))
                    {
                        _fs.Write(p_buffer, 0, p_writeCount);
                    }
                    _result = true;
                }
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
        public bool AppendChunk(Guid p_certapp, DateTime p_wdate, string p_filename, byte[] p_buffer, long p_offset, int p_bytesRead)
        {
            bool _result = false;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _filepath = Path.Combine(this.GetFileFolder(p_wdate.Year, p_wdate.Month), p_filename);

                // make sure that the file exists, except in the case where the file already exists and offset=0,
                // i.e. a new upload, in this case create a new file to overwrite the old one.
                bool _fileexists = File.Exists(_filepath);

                if (_fileexists == false || p_offset == 0)
                    File.Create(_filepath).Close();

                long _filesize = new FileInfo(_filepath).Length;

                // if the file size is not the same as the offset then something went wrong....
                if (_filesize == p_offset)
                {
                    _result = true;

                    // offset matches the filesize, so the chunk is to be inserted at the end of the file.
                    using (FileStream fs = new FileStream(_filepath, FileMode.Append))
                    {
                        fs.Write(p_buffer, 0, p_bytesRead);
                    }
                }
            }

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_wdate"></param>
        /// <param name="p_filename"></param>
        /// <returns></returns>
        public string CheckFileHash(Guid p_certapp, DateTime p_wdate, string p_filename)
        {
            string _result = String.Empty;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _filepath = Path.Combine(this.GetFileFolder(p_wdate.Year, p_wdate.Month), p_filename);

                SHA1CryptoServiceProvider _sha1Crypto = new SHA1CryptoServiceProvider();

                byte[] _hashBytes;

                using (FileStream _fs = new FileStream(_filepath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
                {
                    _hashBytes = _sha1Crypto.ComputeHash(_fs);
                }
                _result = BitConverter.ToString(_hashBytes);
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
        public byte[] DownloadChunk(Guid p_certapp, DateTime p_wdate, string p_filename, long p_offset, int p_bufferSize)
        {
            byte[] _result = new byte[0];

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
            {
                string _filepath = Path.Combine(this.GetFileFolder(p_wdate.Year, p_wdate.Month), p_filename);

                if (File.Exists(_filepath) == true)
                {
                    long _filesize = new FileInfo(_filepath).Length;

                    if (p_offset <= _filesize)
                    {
                        int _bytesRead;

                        using (FileStream _fs = new FileStream(_filepath, FileMode.Open, FileAccess.Read))
                        {
                            _fs.Seek(p_offset, SeekOrigin.Begin);

                            _result = new byte[p_bufferSize];
                            _bytesRead = _fs.Read(_result, 0, p_bufferSize);
                        }

                        if (_bytesRead != p_bufferSize)
                            Array.Resize<byte>(ref _result, _bytesRead);
                    }
                }
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
        public DataSet GetGenInforItemList(Guid p_certapp, params string[] p_items)
        {
            DataSet _result = new DataSet();

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
                _result = g_ctlIProxy.GetGenInforItemList(p_items);

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
        public string ConstantSelect(Guid p_certapp, string p_appkey, string p_default)
        {
            string _result = String.Empty;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
                _result = g_whdCProxy.ConstantSelect(p_appkey, p_default);

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
        public DataSet TranslateDataSet(Guid p_certapp, DataSet p_dataset, int p_tableindex, string p_culture, params string[] p_columns)
        {
            DataSet _result = new DataSet();

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
                _result = g_ctlIProxy.TranslateDataSet(p_dataset, p_tableindex, p_culture, p_columns);

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certkey"></param>
        /// <param name="p_ipadrs"></param>
        /// <param name="p_culture"></param>
        /// <param name="p_word"></param>
        /// <returns></returns>
        public string TranslateText(Guid p_certapp, string p_culture, string p_word)
        {
            string _result = String.Empty;

            if (g_whdIProxy.CheckValidApplication(p_certapp) == true)
                _result = g_ctlIProxy.TranslateText(p_culture, p_word);

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
        public int iWHD_Initialize(Guid p_certapp, string p_defaultUserID)
        {
            int _result = -1;

            string _sqlstr = "EXEC SP_iWHD_Initialize @companyid, @defaultUserID";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@defaultUserID", SqlDbType.NVarChar, p_defaultUserID);

            _result = g_datHelper.ExecuteText(g_whdCProxy.GetConnString, _sqlstr, _dbps);

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_certapp"></param>
        /// <returns></returns>
        public int DeleteiWHD_Initialize(Guid p_certapp)
        {
            int _result = -1;

            string _sqlstr = "EXEC SP_iWHD_DELETEInitialize @companyid";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);

            _result = g_datHelper.ExecuteText(g_whdCProxy.GetConnString, _sqlstr, _dbps);

            return _result;
        }

        #endregion

        //=========================================================================================
        //
        //=========================================================================================

        #region Internal functions

        //=========================================================================================
        // Internal functions
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_searchkey"></param>
        /// <param name="p_searchtext"></param>
        /// <returns></returns>
        private DataSet AdminFileSearch(string p_psid, string p_fileid, string p_searchkey, string p_searchtext)
        {
            string _sqlstr = "-- 특정 폴더에 포함된 파일 목록(검색) 가져오는 스크립트(AdminFileSearch) \n"
                           + "SELECT guid, companyid, fileid, ftype, vsize, vtype, rname, \n"
                           + "       title, description, attach, wcode, wname, wdate, \n"
                           + "       'F' AS mtype, 'T' AS control, 'F' AS cmodify, 'F' AS cread, \n"
                           + "       'F' AS cdelete, 'T' AS cview, 'F' AS cfolder, 'F' AS cfile, \n"
                           + "       'guest' AS code, 'guest' AS name, 1 AS filecount, 1 AS noauth, \n"
                           + "       SUBSTRING(fileid, 1, LEN(fileid) - 4) AS parentid, \n"
                           + "       rname AS displayname \n"
                           + "FROM   TB_iWHD_DIRECTORY \n"
                           + " WHERE companyid=@companyid AND ftype=@ftypeF \n"
                           + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n"
                           + "   AND (" + this.GetSearchCondition(p_searchkey, p_searchtext) + ") \n"
                           + "ORDER BY fileid \n";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
            _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");
            _dbps.Add("@seeval", SqlDbType.NVarChar, "%" + p_searchtext + "%");

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_wdate"></param>
        /// <param name="p_infset"></param>
        /// <returns></returns>
        private DataSet CreateFile(DateTime p_wdate, DataSet p_infset, string p_psid)
        {
            DataRow row0 = p_infset.Tables[0].Rows[0];
            DataRow row1 = p_infset.Tables[0].Rows[1];

            DataSet authorSet = g_orgIProxy.GetMembersByCode(g_whdCProxy.GetProductId, p_psid);

            DataSet authSet = this.GetAuthListInfo(row0["fileid"].ToString());

            DataSet userSet = authorSet.Copy();
            DataView userView = userSet.Tables[0].DefaultView;
            userView.RowFilter = "type = 'F'";

            string _sqlstr = "-- CreateFile \n"
                           + "DECLARE @newfileid nvarchar(256) \n"
                           + " \n"
                           + "SELECT @newfileid=@fileid + RIGHT('000' + CONVERT(nvarchar, ISNULL(MAX(CONVERT(int, RIGHT(fileid, 4))), 0) + 1), 4) \n"
                           + "FROM   TB_iWHD_DIRECTORY \n"
                           + " WHERE companyid=@companyid \n"
                           + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n"
                           + " \n"
                           + "INSERT INTO TB_iWHD_DIRECTORY \n"
                           + "( \n"
                           + "    guid, companyid, fileid, ftype, vsize, vtype, \n"
                           + "    rname, title, description, attach, wcode, wname \n"
                           + ") \n"
                           + "VALUES \n"
                           + "( \n"
                           + "    @guid, @companyid, @newfileid, @ftype, @vsize, @vtype, \n"
                           + "    @rname, @title, @description, @attach, @wcode, @wname \n"
                           + ") \n"
                           + " \n"
                           + "INSERT INTO TB_iWHD_AUTHORITY \n"
                           + "( \n"
                           + "    guid, member, mtype, name, control, cmodify, cread, cdelete, cview, cfolder, cfile \n"
                           + ") \n"
                           + "VALUES \n"
                           + "( \n"
                           + "    @guid, @member, @mtype, @name, @control, @cmodify, @cread, @cdelete, @cview, @cfolder, @cfile \n"
                           + ") \n";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, row1["fileid"].ToString());

            _dbps.Add("@guid", SqlDbType.NVarChar, row1["guid"].ToString());
            _dbps.Add("@ftype", SqlDbType.NVarChar, "F");
            _dbps.Add("@vsize", SqlDbType.Decimal, Convert.ToDecimal(row1["vsize"].ToString()));
            _dbps.Add("@vtype", SqlDbType.NVarChar, row1["vtype"].ToString());
            _dbps.Add("@rname", SqlDbType.NVarChar, row1["rname"].ToString());
            _dbps.Add("@title", SqlDbType.NVarChar, row1["title"].ToString());
            _dbps.Add("@description", SqlDbType.NVarChar, row1["description"].ToString());
            _dbps.Add("@attach", SqlDbType.Xml, String.Empty);
            _dbps.Add("@wcode", SqlDbType.NVarChar, userView[0]["code"].ToString());
            _dbps.Add("@wname", SqlDbType.NVarChar, userView[0]["audcld"].ToString());

            _dbps.Add("@member", SqlDbType.NVarChar, userView[0]["code"].ToString());
            _dbps.Add("@mtype", SqlDbType.NVarChar, userView[0]["type"].ToString());
            _dbps.Add("@name", SqlDbType.NVarChar, userView[0]["audcld"].ToString());
            _dbps.Add("@control", SqlDbType.NVarChar, "T");
            _dbps.Add("@cmodify", SqlDbType.NVarChar, "T");
            _dbps.Add("@cread", SqlDbType.NVarChar, "T");
            _dbps.Add("@cdelete", SqlDbType.NVarChar, "T");
            _dbps.Add("@cview", SqlDbType.NVarChar, "T");
            _dbps.Add("@cfolder", SqlDbType.NVarChar, "T");
            _dbps.Add("@cfile", SqlDbType.NVarChar, "T");

            DataView authView = authSet.Tables[0].DefaultView;
            authView.RowFilter = "(member <> '" + userView[0]["code"].ToString() + "')";

            foreach (DataRowView rowview in authView)
            {
                _sqlstr += "INSERT INTO TB_iWHD_AUTHORITY \n"
                        + "( \n"
                        + "    guid, member, mtype, name, control, cmodify, cread, cdelete, cview, cfolder, cfile \n"
                        + ") \n"
                        + "VALUES \n"
                        + "( \n"
                        + "    @guid, \n"
                        + "    '" + rowview["member"].ToString() + "', '" + rowview["mtype"].ToString() + "', \n"
                        + "    '" + rowview["name"].ToString() + "', '" + rowview["control"].ToString() + "', \n"
                        + "    '" + rowview["cmodify"].ToString() + "', '" + rowview["cread"].ToString() + "', \n"
                        + "    '" + rowview["cdelete"].ToString() + "', '" + rowview["cview"].ToString() + "', \n"
                        + "    '" + rowview["cfolder"].ToString() + "', '" + rowview["cfile"].ToString() + "' \n"
                        + ") \n";
            }

            _sqlstr += "SELECT a.guid, a.companyid, a.fileid, a.ftype, a.vsize, a.vtype, a.rname, \n"
                    + "       a.title, a.description, a.attach, a.wcode, a.wname, a.wdate, \n"
                    + "       ISNULL(b.mtype, 'F') AS mtype, ISNULL(b.control, 'F') AS control, \n"
                    + "       ISNULL(b.cmodify, 'F') AS cmodify, ISNULL(b.cread, 'F') AS cread, \n"
                    + "       ISNULL(b.cdelete, 'F') AS cdelete, ISNULL(b.cview, 'F') AS cview, \n"
                    + "       ISNULL(b.cfolder, 'F') AS cfolder, ISNULL(b.cfile, 'F') AS cfile, \n"
                    + "       b.member AS code, b.name AS name, 0 AS filecount, 1 AS noauth, \n"
                    + "       SUBSTRING(a.fileid, 1, LEN(a.fileid) - 4) AS parentid, \n"
                    + "       a.rname AS displayname \n"
                    + "FROM   TB_iWHD_DIRECTORY a, TB_iWHD_AUTHORITY b \n"
                    + " WHERE a.guid = b.guid AND a.companyid=@companyid AND a.fileid=@newfileid \n";

            if (userView.Count > 0)
            {
                _sqlstr += "   AND a.ftype = 'F' AND (b.mtype = 'F' AND b.member = '" + userView[0]["code"].ToString() + "') \n";
            }
            else
            {
                DataSet groupSet = authorSet.Copy();

                DataView groupView = groupSet.Tables[0].DefaultView;
                groupView.RowFilter = "type = 'T'";
                groupView.Sort = "code";

                if (groupView.Count > 0)
                    _sqlstr += "   AND a.ftype = 'F' AND (b.mtype = 'T' AND b.member = '" + groupView[0]["code"].ToString() + "') \n";
            }

            _sqlstr += "ORDER BY a.fileid \n";

            DataSet _result = g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);

            _sqlstr = "-- CreateFile \n"
                    + "SELECT * FROM TB_iWHD_DIRECTORY \n"
                    + " WHERE guid=@guid AND companyid=@companyid \n"
                    + " \n"
                    + "SELECT * FROM TB_iWHD_AUTHORITY \n"
                    + " WHERE guid=@guid \n";

            _dbps.Clear();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@guid", SqlDbType.NVarChar, row1["guid"].ToString());

            DataSet _master = g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
            _master.WriteXml(Path.Combine(this.GetFileFolder(p_wdate.Year, p_wdate.Month), row1["guid"].ToString() + ".xml"));

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        private DataSet GetAdminAuthFileList(string p_psid, string p_fileid)
        {
            string _sqlstr = "-- 특정 폴더에 포함된 파일 목록 가져오는 스크립트(GetAdminAuthFileList) \n"
                           + "SELECT guid, companyid, fileid, ftype, vsize, vtype, rname, \n"
                           + "       title, description, attach, wcode, wname, wdate, \n"
                           + "       'F' AS mtype, 'T' AS control, 'F' AS cmodify, 'F' AS cread, \n"
                           + "       'F' AS cdelete, 'T' AS cview, 'F' AS cfolder, 'F' AS cfile, \n"
                           + "       'guest' AS code, 'guest' AS name, 1 AS filecount, 1 AS noauth, \n"
                           + "       SUBSTRING(fileid, 1, LEN(fileid) - 4) AS parentid, \n"
                           + "       rname AS displayname \n"
                           + "FROM   TB_iWHD_DIRECTORY \n"
                           + " WHERE companyid=@companyid AND ftype=@ftypeF \n"
                           + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n"
                           + "ORDER BY fileid \n";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
            _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        private DataSet GetAdminFileInfo(string p_psid, string p_fileid)
        {
            string _sqlstr = "-- 지정된 파일 정보 가져오는 스크립트(GetAdminFileInfo) \n"
                           + "SELECT guid, companyid, fileid, ftype, vsize, vtype, rname, \n"
                           + "       title, description, attach, wcode, wname, wdate, \n"
                           + "       'F' AS mtype, 'T' AS control, 'F' AS cmodify, 'F' AS cread, \n"
                           + "       'F' AS cdelete, 'T' AS cview, 'F' AS cfolder, 'F' AS cfile, \n"
                           + "       'guest' AS code, 'guest' AS name, 1 AS filecount, 1 AS noauth, \n"
                           + "       SUBSTRING(fileid, 1, LEN(fileid) - 4) AS parentid, \n"
                           + "       rname AS displayname \n"
                           + "FROM   TB_iWHD_DIRECTORY \n"
                           + " WHERE companyid=@companyid AND fileid=@fileid \n";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        private DataSet GetAdminFolderInfo(string p_psid, string p_fileid)
        {
            DataSet authorSet = g_orgIProxy.GetMembersPerson(g_whdCProxy.GetProductId, p_psid);

            string name = authorSet.Tables[0].DefaultView[0]["audcld"].ToString();

            string _sqlstr = "-- 특정 폴더 및 폴더에 포함된 파일 갯수 가져오는 스크립트(GetAdminFolderInfo) \n"
                           + "SELECT a.guid, a.companyid, a.fileid, a.ftype, a.vsize, a.vtype, a.rname, \n"
                           + "       a.title, a.description, a.attach, a.wcode, a.wname, a.wdate, \n"
                           + "       'F' AS mtype, 'T' AS control, 'F' AS cmodify, 'F' AS cread, \n"
                           + "       'F' AS cdelete, 'T' AS cview, 'F' AS cfolder, 'F' AS cfile, \n"
                           + "       @member AS code, @uname AS name, 0 AS filecount, b.noauth, \n"
                           + "       SUBSTRING(fileid, 1, LEN(fileid) - 4) AS parentid, \n"
                           + "       CONVERT(nvarchar(256), '') AS displayname \n"
                           + "INTO   #TB_iTMP_FOLDER \n"
                           + "FROM   TB_iWHD_DIRECTORY a, \n"
                           + "( \n"
                           + "       SELECT u.guid, m.ftype, COUNT(m.fileid) AS noauth  \n"
                           + "       FROM   TB_iWHD_AUTHORITY u, TB_iWHD_DIRECTORY m \n"
                           + "       WHERE  m.guid = u.guid AND m.companyid=@companyid AND m.ftype=@ftypeT AND m.fileid=@fileid \n"
                           + "       GROUP BY u.guid, m.ftype \n"
                           + ") b \n"
                           + " WHERE a.guid = b.guid \n"
                           + "ORDER BY a.fileid \n"
                           + " \n"
                           + "UPDATE #TB_iTMP_FOLDER \n"
                           + "SET    filecount = b.filecount \n"
                           + "FROM   #TB_iTMP_FOLDER a, \n"
                           + "( \n"
                           + "       SELECT COUNT(*) AS filecount \n"
                           + "       FROM   TB_iWHD_DIRECTORY \n"
                           + "       WHERE  companyid=@companyid AND ftype=@ftypeF \n"
                           + "       AND    SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n"
                           + ") b \n"
                           + " WHERE a.fileid=@fileid \n"
                           + " \n"
                           + "UPDATE #TB_iTMP_FOLDER \n"
                           + "SET    displayname = CASE WHEN filecount > 0 \n"
                           + "                     THEN rname + '(' + CONVERT(nvarchar(4), filecount) + ')' \n"
                           + "                     ELSE rname END \n"
                           + " \n"
                           + "SELECT * FROM #TB_iTMP_FOLDER \n"
                           + "ORDER BY fileid \n";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
            _dbps.Add("@ftypeT", SqlDbType.NVarChar, "T");
            _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");
            _dbps.Add("@member", SqlDbType.NVarChar, p_psid);
            _dbps.Add("@uname", SqlDbType.NVarChar, name);

            /*
            using (System.IO.StreamWriter w = new StreamWriter(@"c:\temp\GetUserAuthFileList.txt", true))
            {
                w.WriteLine("-- 특정 폴더 및 폴더에 포함된 파일 갯수 가져오는 스크립트(GetAdminFolderInfo)");
                w.WriteLine(_sqlstr);

                foreach (DatParameter _dp in _dbps.AllValues)
                {
                    w.WriteLine(_dp.Name + " : '" + _dp.Value.ToString() + "'");
                }
            }
            */

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <returns></returns>
        private DataSet GetAdminFolderList(string p_psid)
        {
            DataSet _ds = g_orgIProxy.GetMembersPerson(g_whdCProxy.GetProductId, p_psid);

            string name = _ds.Tables[0].DefaultView[0]["audcld"].ToString();

            string _sqlstr = "-- 전체 폴더 및 폴더에 포함된 파일 갯수 가져오는 스크립트(GetAdminFolderList) \n"
                           + "SELECT a.guid, a.companyid, a.fileid, a.ftype, a.vsize, a.vtype, a.rname, \n"
                           + "       a.title, a.description, a.attach, a.wcode, a.wname, a.wdate, \n"
                           + "       'F' AS mtype, 'T' AS control, 'F' AS cmodify, 'F' AS cread, \n"
                           + "       'F' AS cdelete, 'T' AS cview, 'F' AS cfolder, 'F' AS cfile, \n"
                           + "       @member AS code, @uname AS name, 0 AS filecount, b.noauth, \n"
                           + "       SUBSTRING(a.fileid, 1, LEN(a.fileid) - 4) AS parentid, \n"
                           + "       CONVERT(nvarchar(256), '') AS displayname \n"
                           + "INTO   #TB_iTMP_FOLDER \n"
                           + "FROM   TB_iWHD_DIRECTORY a, \n"
                           + "( \n"
                           + "       SELECT m.guid, m.ftype, m.fileid, COUNT(m.fileid) AS noauth \n"
                           + "       FROM   TB_iWHD_DIRECTORY m, TB_iWHD_AUTHORITY u \n"
                           + "       WHERE  m.guid = u.guid AND m.companyid=@companyid AND m.ftype=@ftypeT \n"
                           + "       GROUP BY m.guid, m.ftype, m.fileid \n"
                           + ") b \n"
                           + " WHERE a.guid = b.guid \n"
                           + "ORDER BY a.fileid \n"
                           + " \n"
                           + "UPDATE #TB_iTMP_FOLDER \n"
                           + "SET    filecount = b.filecount \n"
                           + "FROM   #TB_iTMP_FOLDER a, \n"
                           + "( \n"
                           + "       SELECT x.companyid, x.fileid, COUNT(*) AS filecount \n"
                           + "       FROM   #TB_iTMP_FOLDER x, TB_iWHD_DIRECTORY y \n"
                           + "       WHERE  x.companyid=@companyid AND y.ftype=@ftypeF AND x.companyid = y.companyid \n"
                           + "       AND    x.fileid = SUBSTRING(y.fileid, 1, LEN(x.fileid)) AND LEN(x.fileid) + 4 = LEN(y.fileid) \n"
                           + "       GROUP BY x.companyid, x.fileid \n"
                           + ") b \n"
                           + " WHERE a.companyid = b.companyid AND a.fileid = b.fileid \n"
                           + " \n"
                           + "UPDATE #TB_iTMP_FOLDER \n"
                           + "SET    displayname = CASE WHEN filecount > 0 \n"
                           + "                     THEN rname + '(' + CONVERT(nvarchar(4), filecount) + ')' \n"
                           + "                     ELSE rname END \n"
                           + " \n"
                           + "SELECT * FROM #TB_iTMP_FOLDER \n"
                           + "ORDER BY fileid \n";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@ftypeT", SqlDbType.NVarChar, "T");
            _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");
            _dbps.Add("@member", SqlDbType.NVarChar, p_psid);
            _dbps.Add("@uname", SqlDbType.NVarChar, name);

            /*
            using (System.IO.StreamWriter w = new StreamWriter(@"c:\temp\GetUserAuthFileList.txt", true))
            {
                w.WriteLine("-- 전체 폴더 및 폴더에 포함된 파일 갯수 가져오는 스크립트(GetAdminFolderList)");
                w.WriteLine(_sqlstr);

                foreach (DatParameter _dp in _dbps.AllValues)
                {
                    w.WriteLine(_dp.Name + " : '" + _dp.Value.ToString() + "'");
                }
            }
            */

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        private DataSet GetAuthListInfo(string p_fileid)
        {
            //guid, member, mtype, name, control, cmodify, cread, cdelete, cview, cfolder, cfile
            string _sqlstr = "-- 권한정보를 가져오는 스크립트(GetAuthListInfo) \n"
                           + "SELECT u.guid, u.member, u.mtype, u.name, u.control, \n"
                           + "       u.cmodify, u.cread, u.cdelete, u.cview, u.cfolder, u.cfile \n"
                           + "FROM   TB_iWHD_AUTHORITY u, TB_iWHD_DIRECTORY m \n"
                           + " WHERE u.guid = m.guid AND m.companyid=@companyid AND m.fileid=@fileid \n"
                           + "ORDER BY u.mtype, u.name \n";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <param name="p_beginAnd"></param>
        /// <param name="p_parms"></param>
        /// <returns></returns>
        private string GetAuthorityQuery(string p_psid, bool p_beginAnd, ref DatParameters p_parms)
        {
            string _result = String.Empty;

            DataSet authorSet = g_orgIProxy.GetMembersByCode(g_whdCProxy.GetProductId, p_psid);

            DataSet groupSet = authorSet.Copy();

            DataView groupView = groupSet.Tables[0].DefaultView;
            groupView.RowFilter = "type = 'T'";
            groupView.Sort = "code";

            string groupQuery = String.Empty;

            for (int i = 0; i < groupView.Count; i++)
            {
                if (i == 0)
                {
                    //groupQuery += " OR (u.cview=@cviewT AND u.mtype=@mtypeT AND (";
                    groupQuery += " OR (u.mtype=@mtypeT AND (";

                    p_parms.Add("@cviewT", SqlDbType.NVarChar, "T");
                    p_parms.Add("@mtypeT", SqlDbType.NVarChar, "T");
                }

                groupQuery += "u.member=@group" + i;

                p_parms.Add("@group" + i, SqlDbType.NVarChar, groupView[i]["code"].ToString());

                groupQuery += i < groupView.Count - 1 ? " OR " : ")) ";
            }

            DataSet userSet = authorSet.Copy();

            DataView userView = userSet.Tables[0].DefaultView;
            userView.RowFilter = "type = 'F'";
            userView.Sort = "code";

            string userQuery = String.Empty;

            for (int i = 0; i < userView.Count; i++)
            {
                if (i == 0)
                {
                    userQuery += " (u.mtype=@mtypeF AND (";

                    p_parms.Add("@mtypeF", SqlDbType.NVarChar, "F");
                }

                userQuery += "u.member=@user" + i;

                p_parms.Add("@user" + i, SqlDbType.NVarChar, userView[i]["code"].ToString());

                userQuery += i < userView.Count - 1 ? " OR " : ")) ";
            }

            if (String.IsNullOrEmpty(userQuery) == true)
                userQuery = " (u.guid = '') ";

            if (p_beginAnd == true)
                _result = " AND ";

            _result += " ( " + userQuery + groupQuery + ") \n";

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_year"></param>
        /// <param name="p_month"></param>
        /// <returns></returns>
        private string GetFileFolder(int p_year, int p_month)
        {
            string _result = Path.Combine(g_whdCProxy.GetRootFolder,
                                          "files"
                                          + @"\" + p_year.ToString("0000")
                                          + @"\" + p_year.ToString("0000") + p_month.ToString("00"));

            if (Directory.Exists(_result) == false)
                Directory.CreateDirectory(_result);

            return _result;
        }

        /// <summary></summary>
        /// <param name="p_seekey"></param>
        /// <param name="p_seeval"></param>
        /// <returns></returns>
        private string GetSearchCondition(string p_seekey, string p_seeval)
        {
            if (p_seekey == "1")
                return "(title LIKE @seeval)";

            if (p_seekey == "2")
                return "(title LIKE @seeval OR description LIKE @seeval)";

            if (p_seekey == "3")
                return "(title LIKE @seeval OR description LIKE @seeval OR rname LIKE @seeval)";

            return "(rname LIKE @seeval)";
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        private DataSet GetUserAuthFileList(string p_psid, string p_fileid)
        {
            DatParameters _dbps = new DatParameters();

            string _authorCondition = this.GetAuthorityQuery(p_psid, false, ref _dbps);

            string _sqlstr = "-- 특정 폴더에 포함된 파일 목록 가져오는 스크립트(GetUserAuthFileList) \n"
                           + "SELECT m.guid, m.companyid, m.fileid, m.ftype, m.vsize, m.vtype, m.rname, \n"
                           + "       m.title, m.description, m.attach, m.wcode, m.wname, m.wdate, \n"
                           + "       u.mtype, u.control, u.cmodify, u.cread, \n"
                           + "       u.cdelete, u.cview, u.cfolder, u.cfile, \n"
                //+ "       u.member AS code, u.name AS name, \n"
                           + "       1 AS filecount, 1 AS noauth, \n"
                           + "       SUBSTRING(m.fileid, 1, LEN(m.fileid) - 4) AS parentid, \n"
                           + "       m.rname AS displayname \n"
                           + "FROM   TB_iWHD_DIRECTORY m, \n"
                           + "( \n"
                           + "       SELECT u.guid, "
                           + "              MAX(ISNULL(u.mtype,'F')) AS mtype, MAX(ISNULL(u.control,'F')) AS control, \n"
                           + "              MAX(ISNULL(u.cmodify,'F')) AS cmodify, MAX(ISNULL(u.cread,'F')) AS cread, \n"
                           + "              MAX(ISNULL(u.cdelete,'F')) AS cdelete, MAX(ISNULL(u.cview,'F')) AS cview, \n"
                           + "              MAX(ISNULL(u.cfolder,'F')) AS cfolder, MAX(ISNULL(u.cfile,'F')) AS cfile \n"
                           + "       FROM	TB_iWHD_AUTHORITY u \n"
                           + "       WHERE  " + _authorCondition + " \n"
                           + "       GROUP BY u.guid \n"
                           + ") u \n"
                           + " WHERE m.guid = u.guid AND m.companyid=@companyid AND m.ftype=@ftypeF \n"
                           + "   AND SUBSTRING(m.fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(m.fileid) \n"
                           + "ORDER BY m.fileid \n";

            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
            _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");

            /*
            using (System.IO.StreamWriter w = new StreamWriter(@"c:\temp\GetUserAuthFileList.txt", true))
            {
                w.WriteLine("-------------특정 폴더에 포함된 파일 목록 가져오는 스크립트(GetUserAuthFileList)-------------");
                w.WriteLine(_sqlstr);
                w.WriteLine(_authorCondition);

                foreach (DatParameter _dp in _dbps.AllValues)
                {
                    w.WriteLine(_dp.Name + " : [" + _dp.Value.ToString() + "]");                    
                }
            }      
            */

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        private DataSet GetUserFileInfo(string p_psid, string p_fileid)
        {
            DatParameters _dbps = new DatParameters();

            string _authorCondition = this.GetAuthorityQuery(p_psid, true, ref _dbps);

            string _sqlstr = "-- 지정된 파일 정보 가져오는 스크립트(GetUserFileInfo) \n"
                           + "SELECT m.guid, m.companyid, m.fileid, m.ftype, m.vsize, m.vtype, m.rname, \n"
                           + "       m.title, m.description, m.attach, m.wcode, m.wname, m.wdate, \n"
                           + "       u.mtype, u.control, u.cmodify, u.cread, \n"
                           + "       u.cdelete, u.cview, u.cfolder, u.cfile, \n"
                           + "       u.member AS code, u.name AS name, 1 AS filecount, 1 AS noauth, \n"
                           + "       SUBSTRING(m.fileid, 1, LEN(m.fileid) - 4) AS parentid, \n"
                           + "       m.rname AS displayname \n"
                           + "FROM   TB_iWHD_DIRECTORY m, TB_iWHD_AUTHORITY u \n"
                           + " WHERE m.guid = u.guid AND m.companyid=@companyid AND m.fileid=@fileid \n" + _authorCondition;

            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <param name="p_fileid"></param>
        /// <returns></returns>
        private DataSet GetUserFolderInfo(string p_psid, string p_fileid)
        {
            DatParameters _dbps = new DatParameters();

            string _authorCondition = this.GetAuthorityQuery(p_psid, true, ref _dbps);

            string _sqlstr = "-- 특정 폴더 및 폴더에 포함된 파일 갯수 가져오는 스크립트(GetUserFolderInfo) \n"
                           + "SELECT m.guid, m.companyid, m.fileid, m.ftype, m.vsize, m.vtype, m.rname, \n"
                           + "       m.title, m.description, m.attach, m.wcode, m.wname, m.wdate, \n"
                           + "       ISNULL(u.mtype,'F') AS mtype, ISNULL(u.control,'F') AS control, \n"
                           + "       ISNULL(u.cmodify,'F') AS cmodify, ISNULL(u.cread,'F') AS cread, \n"
                           + "       ISNULL(u.cdelete,'F') AS cdelete, ISNULL(u.cview,'F') AS cview, \n"
                           + "       ISNULL(u.cfolder,'F') AS cfolder, ISNULL(u.cfile,'F') AS cfile, \n"
                           + "       u.member AS code, u.name, 0 AS filecount, 1 AS noauth, \n"
                           + "       SUBSTRING(fileid, 1, LEN(fileid) - 4) AS parentid, \n"
                           + "       CONVERT(nvarchar(256), '') AS displayname \n"
                           + "INTO   #TB_iTMP_FOLDER \n"
                           + "FROM   TB_iWHD_DIRECTORY m, \n"
                           + "( \n"
                           + "       SELECT u.guid, "
                           + "              MAX(ISNULL(u.mtype,'F')) AS mtype, MAX(ISNULL(u.control,'F')) AS control, \n"
                           + "              MAX(ISNULL(u.cmodify,'F')) AS cmodify, MAX(ISNULL(u.cread,'F')) AS cread, \n"
                           + "              MAX(ISNULL(u.cdelete,'F')) AS cdelete, MAX(ISNULL(u.cview,'F')) AS cview, \n"
                           + "              MAX(ISNULL(u.cfolder,'F')) AS cfolder, MAX(ISNULL(u.cfile,'F')) AS cfile \n"
                           + "              ,MAX(ISNULL(u.member,'')) AS member, MAX(ISNULL(u.name,'')) AS name \n"
                           + "       FROM	TB_iWHD_AUTHORITY u \n"
                           + "       WHERE  1=1 " + _authorCondition + " \n"
                           + "       GROUP BY u.guid \n"
                           + ") u \n"
                           + " WHERE m.guid = u.guid AND m.companyid=@companyid AND m.ftype=@ftypeT AND m.fileid=@fileid \n"// + _authorCondition
                           + "ORDER BY m.fileid \n"
                           + " \n"
                           + "UPDATE #TB_iTMP_FOLDER \n"
                           + "SET    filecount = b.filecount \n"
                           + "FROM   #TB_iTMP_FOLDER a, \n"
                           + "( \n"
                           + "       SELECT COUNT(*) AS filecount \n"
                           + "       FROM   TB_iWHD_DIRECTORY \n"
                           + "       WHERE  companyid=@companyid AND ftype=@ftypeF \n"
                           + "       AND    SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n"
                           + "       AND    guid IN \n"
                           + "       ( \n"
                           + "              SELECT DISTINCT m.guid \n"
                           + "              FROM   TB_iWHD_DIRECTORY m, TB_iWHD_AUTHORITY u \n"
                           + "              WHERE  m.guid = u.guid AND m.companyid=@companyid AND m.ftype=@ftypeF \n" + _authorCondition
                           + "              AND    SUBSTRING(m.fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(m.fileid) \n"
                           + "       ) \n"
                           + ") b \n"
                           + " WHERE a.fileid=@fileid \n"
                           + " \n"
                           + "UPDATE #TB_iTMP_FOLDER \n"
                           + "SET    displayname = CASE WHEN filecount > 0 \n"
                           + "                     THEN rname + '(' + CONVERT(nvarchar(4), filecount) + ')' \n"
                           + "                     ELSE rname END \n"
                           + " \n"
                           + "SELECT * FROM #TB_iTMP_FOLDER \n"
                           + "ORDER BY fileid \n";

            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
            _dbps.Add("@ftypeT", SqlDbType.NVarChar, "T");
            _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");

            /*
            using (System.IO.StreamWriter w = new StreamWriter(@"c:\temp\GetUserAuthFileList.txt", true))
            {
                w.WriteLine("-- 특정 폴더 및 폴더에 포함된 파일 갯수 가져오는 스크립트(GetUserFolderInfo)");
                w.WriteLine(_sqlstr);                

                foreach (DatParameter _dp in _dbps.AllValues)
                {
                    w.WriteLine(_dp.Name + " : '" + _dp.Value.ToString() + "'");
                }
            }
            */

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <returns></returns>
        private DataSet GetUserFolderList(string p_psid)
        {
            DatParameters _dbps = new DatParameters();

            string _authorCondition = this.GetAuthorityQuery(p_psid, false, ref _dbps);

            string _sqlstr = "-- 전체 폴더 및 폴더에 포함된 파일 갯수 가져오는 스크립트(GetUserFolderList) \n"
                           + "SELECT m.guid, m.companyid, m.fileid, m.ftype, m.vsize, m.vtype, m.rname, \n"
                           + "       m.title, m.description, m.attach, m.wcode, m.wname, m.wdate, \n"
                           + "       ISNULL(u.mtype,'F') AS mtype, ISNULL(u.control,'F') AS control, \n"
                           + "       ISNULL(u.cmodify,'F') AS cmodify, ISNULL(u.cread,'F') AS cread, \n"
                           + "       ISNULL(u.cdelete,'F') AS cdelete, ISNULL(u.cview,'F') AS cview, \n"
                           + "       ISNULL(u.cfolder,'F') AS cfolder, ISNULL(u.cfile,'F') AS cfile, \n"
                //+ "       u.member AS code, u.name, "
                           + "       0 AS filecount, 1 AS noauth, \n"
                           + "       SUBSTRING(fileid, 1, LEN(fileid) - 4) AS parentid, \n"
                           + "       CONVERT(nvarchar(256), '') AS displayname \n"
                           + "INTO   #TB_iTMP_FOLDER \n"
                           + "FROM   TB_iWHD_DIRECTORY m, \n"
                           + "( \n"
                           + "       SELECT u.guid, "
                           + "              MAX(ISNULL(u.mtype,'F')) AS mtype, MAX(ISNULL(u.control,'F')) AS control, \n"
                           + "              MAX(ISNULL(u.cmodify,'F')) AS cmodify, MAX(ISNULL(u.cread,'F')) AS cread, \n"
                           + "              MAX(ISNULL(u.cdelete,'F')) AS cdelete, MAX(ISNULL(u.cview,'F')) AS cview, \n"
                           + "              MAX(ISNULL(u.cfolder,'F')) AS cfolder, MAX(ISNULL(u.cfile,'F')) AS cfile \n"
                           + "       FROM	TB_iWHD_AUTHORITY u \n"
                           + "       WHERE  " + _authorCondition + " \n"
                           + "       GROUP BY u.guid \n"
                           + ") u \n"
                           + "WHERE  m.guid = u.guid AND m.companyid=@companyid AND m.ftype=@ftypeT \n"
                           + " \n"
                           + "UPDATE #TB_iTMP_FOLDER \n"
                           + "SET    filecount = b.filecount \n"
                           + "FROM   #TB_iTMP_FOLDER a, \n"
                           + "( \n"
                           + "       SELECT x.companyid, x.fileid, COUNT(*) AS filecount \n"
                           + "       FROM   #TB_iTMP_FOLDER x, \n"
                           + "       ( \n"
                           + "              SELECT DISTINCT m.fileid, m.ftype \n"
                           + "              FROM   TB_iWHD_DIRECTORY m, TB_iWHD_AUTHORITY u \n"
                           + "              WHERE  m.guid = u.guid AND m.companyid=@companyid AND m.ftype=@ftypeF AND \n" + _authorCondition
                           + "       ) y \n"
                           + "       WHERE  x.companyid=@companyid AND y.ftype=@ftypeF \n"
                           + "       AND    x.fileid = SUBSTRING(y.fileid, 1, LEN(x.fileid)) AND LEN(x.fileid) + 4 = LEN(y.fileid) \n"
                           + "       GROUP BY x.companyid, x.fileid \n"
                           + ") b \n"
                           + " WHERE a.companyid = b.companyid AND a.fileid = b.fileid \n"
                           + " \n"
                           + "UPDATE #TB_iTMP_FOLDER \n"
                           + "SET    displayname = CASE WHEN filecount > 0 \n"
                           + "                     THEN rname + '(' + CONVERT(nvarchar(4), filecount) + ')' \n"
                           + "                     ELSE rname END \n"
                           + " \n"
                           + "SELECT * FROM #TB_iTMP_FOLDER \n"
                           + "ORDER BY fileid \n";

            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@ftypeT", SqlDbType.NVarChar, "T");
            _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");

            /*
            using (System.IO.StreamWriter w = new StreamWriter(@"c:\temp\GetUserAuthFileList.txt", true))
            {
                w.WriteLine("-------------전체 폴더 및 폴더에 포함된 파일 갯수 가져오는 스크립트(GetUserFolderList)-------------");
                w.WriteLine(_sqlstr);
                w.WriteLine(_authorCondition);

                foreach (DatParameter _dp in _dbps.AllValues)
                {
                    w.WriteLine(_dp.Name + " : [" + _dp.Value.ToString() + "]");
                }
            }
            */

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_dataSet"></param>
        /// <returns></returns>
        private DataSet IsFileExists(DataSet p_dataSet)
        {
            DataRowCollection rows = p_dataSet.Tables[0].Rows;

            string _sqlstr = "-- 지정된 폴더에 같은 이름을 가진 파일이 있는지 확인하는 스크립트(IsFileExists) \n"
                           + "SELECT * FROM TB_iWHD_DIRECTORY \n"
                           + " WHERE companyid=@companyid AND ftype=@ftypeF AND rname=@rname \n"
                           + "   AND SUBSTRING(fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(fileid) \n";

            DatParameters _dbps = new DatParameters();
            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");
            _dbps.Add("@rname", SqlDbType.NVarChar, rows[1]["rname"].ToString());
            _dbps.Add("@fileid", SqlDbType.NVarChar, rows[1]["fileid"].ToString());

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        /// <summary></summary>
        /// <param name="p_psid"></param>
        /// <param name="p_fileid"></param>
        /// <param name="p_searchkey"></param>
        /// <param name="p_searchtext"></param>
        /// <returns></returns>
        private DataSet UserFileSearch(string p_psid, string p_fileid, string p_searchkey, string p_searchtext)
        {
            DatParameters _dbps = new DatParameters();

            string _authorCondition = this.GetAuthorityQuery(p_psid, true, ref _dbps);

            string _sqlstr = "-- 특정 폴더에 포함된 파일 목록(검색) 가져오는 스크립트(UserFileSearch) \n"
                           + "SELECT m.guid, m.companyid, m.fileid, m.ftype, m.vsize, m.vtype, m.rname, \n"
                           + "       m.title, m.description, m.attach, m.wcode, m.wname, m.wdate, \n"
                           + "       u.mtype, u.control, u.cmodify, u.cread, \n"
                           + "       u.cdelete, u.cview, u.cfolder, u.cfile, \n"
                           + "       u.member AS code, u.name AS name, 1 AS filecount, 1 AS noauth, \n"
                           + "       SUBSTRING(m.fileid, 1, LEN(m.fileid) - 4) AS parentid, \n"
                           + "       m.rname AS displayname \n"
                           + "FROM   TB_iWHD_DIRECTORY m, TB_iWHD_AUTHORITY u \n"
                           + " WHERE m.guid = u.guid AND m.companyid=@companyid AND m.ftype=@ftypeF \n"
                           + "   AND SUBSTRING(m.fileid, 1, LEN(@fileid))=@fileid AND LEN(@fileid) + 4 = LEN(m.fileid) \n"
                           + "   AND (" + this.GetSearchCondition(p_searchkey, p_searchtext) + ") \n" + _authorCondition
                           + "ORDER BY m.fileid \n";

            _dbps.Add("@companyid", SqlDbType.NVarChar, this.CompanyID);
            _dbps.Add("@fileid", SqlDbType.NVarChar, p_fileid);
            _dbps.Add("@ftypeF", SqlDbType.NVarChar, "F");
            _dbps.Add("@seeval", SqlDbType.NVarChar, "%" + p_searchtext + "%");

            return g_datHelper.ExecuteDataSet(g_whdCProxy.GetConnString, _sqlstr, _dbps);
        }

        #endregion

        //=========================================================================================
        //
        //=========================================================================================
    }
}