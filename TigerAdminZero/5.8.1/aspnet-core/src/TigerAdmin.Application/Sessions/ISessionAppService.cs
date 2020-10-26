using System.Threading.Tasks;
using Abp.Application.Services;
using TigerAdmin.Sessions.Dto;

namespace TigerAdmin.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
