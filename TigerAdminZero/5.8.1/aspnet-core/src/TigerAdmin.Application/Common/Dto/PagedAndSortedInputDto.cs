using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Common.Dto
{
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public PagedAndSortedInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }

        public string Sorting { get; set; }


    }
}
