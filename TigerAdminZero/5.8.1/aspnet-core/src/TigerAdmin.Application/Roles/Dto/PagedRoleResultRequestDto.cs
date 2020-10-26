using Abp.Application.Services.Dto;

namespace TigerAdmin.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

