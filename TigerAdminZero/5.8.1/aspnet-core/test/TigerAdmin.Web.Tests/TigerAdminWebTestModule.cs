using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TigerAdmin.EntityFrameworkCore;
using TigerAdmin.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace TigerAdmin.Web.Tests
{
    [DependsOn(
        typeof(TigerAdminWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class TigerAdminWebTestModule : AbpModule
    {
        public TigerAdminWebTestModule(TigerAdminEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TigerAdminWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TigerAdminWebMvcModule).Assembly);
        }
    }
}