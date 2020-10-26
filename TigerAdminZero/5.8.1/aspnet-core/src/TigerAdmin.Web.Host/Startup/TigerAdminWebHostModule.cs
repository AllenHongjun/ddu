using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TigerAdmin.Configuration;

namespace TigerAdmin.Web.Host.Startup
{
    [DependsOn(
       typeof(TigerAdminWebCoreModule))]
    public class TigerAdminWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TigerAdminWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TigerAdminWebHostModule).GetAssembly());
        }
    }
}
