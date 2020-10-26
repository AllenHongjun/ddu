using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using TigerAdmin.Configuration.Dto;

namespace TigerAdmin.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TigerAdminAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
