using System;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;

namespace WebHard.Remote
{
    public class WebHardSvc : System.ServiceProcess.ServiceBase
    {
        //*********************************************************************************************************//
        //
        //*********************************************************************************************************//
        private WebHard.Remote.WebHardServer m_appServer = null;
        private WebHard.Remote.WebHardServer g_appServer
        {
            get
            {
                if (m_appServer == null)
                    m_appServer = new WebHard.Remote.WebHardServer();

                return m_appServer;
            }
        }

        //*********************************************************************************************************//
        //
        //*********************************************************************************************************//
        /// <summary>필수 디자이너 변수입니다.</summary>
        private System.ComponentModel.Container components = null;

        public WebHardSvc()
        {
            this.InitializeComponent();
            this.ServiceName = WebHard.Interface.SProxy.SNG.g_whdCProxy.ServiceName;
        }

        // 프로세스의 주 진입점입니다.
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;

            // 같은 프로세스 내에서 둘 이상의 사용자 서비스가 실행될 수 있습니다.
            // 이 프로세스에 다른 서비스를 추가하려면 두 번째 서비스 개체를 만들도록
            // 다음 줄을 변경합니다. 예를 들면 다음과 같습니다.
            //
            //   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new AppService(), new MySecondUserService()};
            //
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new WebHardSvc() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "WebHard Service V34";
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

        //*********************************************************************************************************//
        //
        //*********************************************************************************************************//
        /// <summary>
        /// 서비스가 작업을 수행할 수 있도록 필요한 동작을 설정합니다.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            try
            {
                g_appServer.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 이 서비스를 중지합니다.
        /// </summary>
        protected override void OnStop()
        {
            g_appServer.Stop();
        }
    
        //*********************************************************************************************************//
        //
        //*********************************************************************************************************//
    }
}