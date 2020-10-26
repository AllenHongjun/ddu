using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace TigerAdmin.Controllers
{
    public abstract class TigerAdminControllerBase: AbpController
    {
        protected TigerAdminControllerBase()
        {
            LocalizationSourceName = TigerAdminConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
