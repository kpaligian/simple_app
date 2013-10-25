using System.Collections.Generic;
using Castle.MicroKernel.Registration;

namespace Simple.Web.Windsor.Owin.Components
{
    public class ContainerStartup : WindsorStartupBase
    {
        protected override IEnumerable<IWindsorInstaller> GetInstallers()
        {
            return new List<IWindsorInstaller>
            {
                new ComponentsInstaller() 
            };
        }
    }

}