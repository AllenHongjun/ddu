using Abp.Application.Services;
using TigerAdmin.MultiTenancy.Dto;

namespace TigerAdmin.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

