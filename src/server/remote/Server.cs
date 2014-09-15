using System;
using LIB.Communication.Remoting;

namespace WebHard.Remote
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class WebHardServer
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public WebHardServer()
        {
            this.m_remConst = WebHard.Interface.SProxy.SNG.g_whdCProxy;
            this.m_remConst.Server = System.Environment.MachineName;
            this.m_remConst.svObject = typeof(WebHardObject);
            this.m_remServer = new RemoteServer();
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        private RemoteConstant m_remConst = null;
        private RemoteServer m_remServer = null;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public void Start()
        {
            this.m_remServer.StartServer(this.m_remConst);
        }

        /// <summary></summary>
        public void Stop()
        {
            this.m_remServer.StopServer(this.m_remConst);
        }
    }
}