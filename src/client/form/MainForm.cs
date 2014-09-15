using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web;
using System.Windows.Forms;

using DevExpress.XtraEditors;

using uBizSoft.LIB.Logging;
using uBizSoft.UIC.Win.Control;
using WebHard.WinCtrl;
using WebHard.WinCtrl.Library;
using uBizSoft.UIC.Win.Control.Library;

namespace WebHard.WinForm
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public MainForm()
        {
            string uid = String.Empty;
            string unm = String.Empty;
            string cocd = String.Empty;
            string corp = String.Empty;
            string certkey = String.Empty;
            string culture = String.Empty;
            string proxyurl = String.Empty;

            //수정필요(테스트를 위해 설정함)
            this.m_cocd = "1001";
            this.m_wsUrl = this.GetConstantFromClient("DefaultWSUrl", String.Empty);

            // URL을 통해 매개 변수가 전달되었는지 여부를 확인합니다.
            if (ApplicationDeployment.IsNetworkDeployed == true)
            {
                if (ApplicationDeployment.CurrentDeployment.ActivationUri != null)
                {
                    string query = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                    NameValueCollection nvcols = HttpUtility.ParseQueryString(query);

                    if (nvcols.Count > 0)
                    {
                        List<string> parms = new List<string>();
                        parms.AddRange(nvcols.AllKeys);

                        if (parms.Contains("cocd") == true)
                            cocd = nvcols["cocd"];

                        if (parms.Contains("corp") == true)
                            corp = nvcols["corp"];

                        if (parms.Contains("certkey") == true)
                            certkey = nvcols["certkey"];

                        if (parms.Contains("culture") == true)
                            culture = nvcols["culture"];

                        if (parms.Contains("uid") == true)
                            uid = nvcols["uid"];

                        if (parms.Contains("unm") == true)
                            unm = nvcols["unm"];

                        if (parms.Contains("proxyurl") == true)
                            proxyurl = nvcols["proxyurl"];
                    }
                }
            }

            if (String.IsNullOrEmpty(proxyurl) == true)
                proxyurl = this.GetConstantFromClient("ProxyWSUrl", String.Empty);

            bool isValidCertkey = false;

            if (String.IsNullOrEmpty(certkey) == false)
            {
                // 사업장아이디를 설정합니다.
                this.m_cocd = cocd;
                this.m_corp = corp;

                if (String.IsNullOrEmpty(this.m_cocd) == true)
                    this.m_cocd = this.GetConstantFromClient("DefaultCompanyID", String.Empty);

                this.m_certKey = new Guid(certkey);

                // 인증키가 유효한지 확인합니다.
                isValidCertkey = g_logDialog.IsValidCertificationKey(this.m_cocd, this.m_corp, this.m_wsUrl, this.m_certKey);

                if (isValidCertkey == true)
                {
                    // 아이디, 이름 등 기초정보를 설정합니다.
                    this.m_loginId = uid;

                    if (String.IsNullOrEmpty(this.m_loginId) == true)
                        this.m_loginId = g_logDialog.PersonalID;

                    this.m_loginName = unm;

                    if (String.IsNullOrEmpty(this.m_loginName) == true)
                        this.m_loginName = g_logDialog.UserName;
                }
            }

            if (isValidCertkey == false)
            {
                // 로그인 창을 표시해서 인증요청을 합니다.
                if (this.Login() == false)
                    Environment.Exit(0);
            }

            this.InitializeComponent();

            this.Text += String.Format(", CID: {0}, PID: {1}", this.m_cocd, this.m_loginId);

            // MainBox 개체를 생성합니다.
            this.m_mainBox = new MainBox();
            this.m_mainBox.Dock = DockStyle.Fill;

            this.m_mainBox.CertKey = this.m_certKey.ToString();
            this.m_mainBox.CompanyID = this.m_cocd;
            this.m_mainBox.Culture = culture;
            this.m_mainBox.LoginID = this.m_loginId;
            this.m_mainBox.LoginName = this.m_loginName;
            this.m_mainBox.ProxyWsUrl = proxyurl;
            this.m_mainBox.WSUrl = this.m_wsUrl;

            this.Controls.Add(this.m_mainBox);

            // 다국어 지원을 실행합니다.
            AppMediator.SINGLETON.ResourceHelper.TranslateText(this, this.components);
        }

        //=========================================================================================
        //
        //=========================================================================================
        private string m_cocd = String.Empty;
        private string m_corp = String.Empty;
        private string m_wsUrl = String.Empty;
        private string m_loginId = String.Empty;
        private string m_loginName = String.Empty;

        private Guid m_certKey = Guid.Empty;

        private MainBox m_mainBox = null;

        //=========================================================================================
        //
        //=========================================================================================
        private uBizSoft.FAC.FWS.LoginDlg.fLoginDlg m_logDialog = null;
        private uBizSoft.FAC.FWS.LoginDlg.fLoginDlg g_logDialog
        {
            get
            {
                if (this.m_logDialog == null)
                {
                    this.m_logDialog = new uBizSoft.FAC.FWS.LoginDlg.fLoginDlg();
                    this.m_logDialog.CompanyID = this.m_cocd;
                    this.m_logDialog.WebServiceUrl = this.m_wsUrl;
                }
                return this.m_logDialog;
            }
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <param name="p_appkey"></param>
        /// <param name="p_default"></param>
        /// <returns></returns>
        private string GetConstantFromClient(string p_appkey, string p_default)
        {
            string result = ConfigurationManager.AppSettings[p_appkey];

            if (String.IsNullOrEmpty(result) == true)
                result = p_default;

            return result;
        }

        /// <summary></summary>
        private bool Login()
        {
            bool _result = false;

            try
            {
                g_logDialog.ClearLoginCertkey();
                g_logDialog.SetAutoLogin(false);

                // 01) 로그인 창을 표시합니다.
                if (g_logDialog.ShowDialog() != DialogResult.Cancel)
                {
                    this.m_cocd = g_logDialog.CompanyID;
                    this.m_corp = g_logDialog.CorporateID;
                    this.m_certKey = g_logDialog.CertificationKey;
                    this.m_loginId = g_logDialog.PersonalID;
                    this.m_loginName = g_logDialog.UserName;

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                OFileLog.SNG.WriteLog(ex.ToString());
            }

            return _result;
        }

        //=========================================================================================
        //
        //=========================================================================================
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            LayoutHelper.SaveFormLayout(this);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            LayoutHelper.RestoreFormLayout(this);
        }

        private void OnLogoutBarButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Environment.Exit(0);
        }

        private void OnExitBarButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Environment.Exit(0);
        }

        private void OnNewFolderBarButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.m_mainBox.CreateNewFolder();
        }

        private void OnAuthorityBarButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.m_mainBox.SetAuthority();
        }

        private void OnUploadBarButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.m_mainBox.Upload();
        }

        private void OnDownloadBarButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.m_mainBox.Download();
        }

        private void OnDeleteBarButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.m_mainBox.Delete();
        }

        private void OnRenewBarButtonItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.m_mainBox.RunGridLoadWorker();
        }
    }
}