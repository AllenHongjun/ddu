using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Demo.Task.Dto
{   
    [AutoMapFrom(typeof(Task))]
    public class TaskListDto:EntityDto<int>,IHasCreationTime
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }
    }
}
