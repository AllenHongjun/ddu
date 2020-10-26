using System.Threading.Tasks;
using Abp.Application.Services;
using TigerAdmin.Authorization.Accounts.Dto;

namespace TigerAdmin.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
