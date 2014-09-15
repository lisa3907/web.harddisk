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

        /// <summary>필수 디자이너 변수입니다.</summary>
        private System.ComponentModel.Container components = null;

        public WhsInstall()
        {
            this.InitializeComponent();

            string _service = WebHard.Interface.SProxy.SNG.g_whdCProxy.ServiceName;

            this.WhsSVI.DisplayName = _service;
            this.WhsSVI.ServiceName = _service;
        }

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
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

        #region 구성 요소 디자이너에서 생성한 코드
        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
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
            this.WhsSVI.Description = "이 서비스는 파일공유를 위한 서버 모듈과 연결하는 기능을 제공 합니다.";
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