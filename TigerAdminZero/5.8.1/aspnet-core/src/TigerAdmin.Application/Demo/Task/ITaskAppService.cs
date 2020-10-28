using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TigerAdmin.Demo.Task.Dto;

namespace TigerAdmin.Demo.Task
{
    public interface ITaskAppService:IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);
    }
}
