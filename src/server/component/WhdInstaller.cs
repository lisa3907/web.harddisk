using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.EnterpriseServices;

namespace WebHard.Component.V34
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    [RunInstaller(true)]
    public partial class WhdInstaller : System.Configuration.Install.Installer
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public WhdInstaller()
        {
            this.InitializeComponent();
        }

        //=========================================================================================
        //
        //=========================================================================================
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            try
            {
                string assembly = this.GetType().Assembly.Location;

                string applicationId = null;
                string typeLib = null;

                RegistrationHelper registrationHelper = new RegistrationHelper();
                registrationHelper.InstallAssembly(assembly, ref applicationId, ref typeLib, InstallationFlags.FindOrCreateTargetApplication);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            try
            {
                string assembly = this.GetType().Assembly.Location;
                string applicationId = "WebHard.Component.V34";

                RegistrationHelper registrationHelper = new RegistrationHelper();
                registrationHelper.UninstallAssembly(assembly, applicationId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}