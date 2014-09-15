using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace WebHard.Remote
{
    /// <summary></summary>
    [RunInstaller(true)]
    public class WhsInstall : System.Configuration.Install.Installer
    {
        private System.ServiceProcess.ServiceProcessInstaller WhsSPI;
        private System.ServiceProcess.ServiceInstaller WhsSVI;

        /// <summary>�ʼ� �����̳� �����Դϴ�.</summary>
        private System.ComponentModel.Container components = null;

        public WhsInstall()
        {
            this.InitializeComponent();

            string _service = WebHard.Interface.SProxy.SNG.g_whdCProxy.ServiceName;

            this.WhsSVI.DisplayName = _service;
            this.WhsSVI.ServiceName = _service;
        }

        /// <summary> 
        /// ��� ���� ��� ���ҽ��� �����մϴ�.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region ���� ��� �����̳ʿ��� ������ �ڵ�
        /// <summary>
        /// �����̳� ������ �ʿ��� �޼����Դϴ�.
        /// �� �޼����� ������ �ڵ� ������� �������� ���ʽÿ�.
        /// </summary>
        private void InitializeComponent()
        {
            this.WhsSPI = new System.ServiceProcess.ServiceProcessInstaller();
            this.WhsSVI = new System.ServiceProcess.ServiceInstaller();
            // 
            // WhsSPI
            // 
            this.WhsSPI.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.WhsSPI.Password = null;
            this.WhsSPI.Username = null;
            // 
            // WhsSVI
            // 
            this.WhsSVI.Description = "�� ���񽺴� ���ϰ����� ���� ���� ���� �����ϴ� ����� ���� �մϴ�.";
            this.WhsSVI.DisplayName = "WebHard Service V34";
            this.WhsSVI.ServiceName = "WebHard Service V34";
            this.WhsSVI.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // WhsInstall
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WhsSPI,
            this.WhsSVI});

        }
        #endregion
    }
}