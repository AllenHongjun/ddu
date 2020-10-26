using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TigerAdmin.Authorization;

namespace TigerAdmin
{
    [DependsOn(
        typeof(TigerAdminCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TigerAdminApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TigerAdminAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TigerAdminApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
