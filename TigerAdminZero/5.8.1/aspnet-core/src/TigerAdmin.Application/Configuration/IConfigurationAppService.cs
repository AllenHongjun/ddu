using System.Threading.Tasks;
using TigerAdmin.Configuration.Dto;

namespace TigerAdmin.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
