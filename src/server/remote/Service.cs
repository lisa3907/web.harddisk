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
        /// <summary>�ʼ� �����̳� �����Դϴ�.</summary>
        private System.ComponentModel.Container components = null;

        public WebHardSvc()
        {
            this.InitializeComponent();
            this.ServiceName = WebHard.Interface.SProxy.SNG.g_whdCProxy.ServiceName;
        }

        // ���μ����� �� �������Դϴ�.
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;

            // ���� ���μ��� ������ �� �̻��� ����� ���񽺰� ����� �� �ֽ��ϴ�.
            // �� ���μ����� �ٸ� ���񽺸� �߰��Ϸ��� �� ��° ���� ��ü�� ���鵵��
            // ���� ���� �����մϴ�. ���� ��� ������ �����ϴ�.
            //
            //   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new AppService(), new MySecondUserService()};
            //
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new WebHardSvc() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }

        /// <summary> 
        /// �����̳� ������ �ʿ��� �޼����Դϴ�. 
        /// �� �޼����� ������ �ڵ� ������� �������� ���ʽÿ�.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "WebHard Service V34";
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

        //*********************************************************************************************************//
        //
        //*********************************************************************************************************//
        /// <summary>
        /// ���񽺰� �۾��� ������ �� �ֵ��� �ʿ��� ������ �����մϴ�.
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
        /// �� ���񽺸� �����մϴ�.
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