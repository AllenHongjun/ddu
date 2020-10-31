using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TigerAdmin.Common.Dto
{
    public class PagedAndFilteredInputDto : IPagedResultRequest
    {
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        [Range(1, AppConsts.DefaultMaxPageSize)]
        public int MaxResultCount { get; set; }

        public PagedAndFilteredInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }
    }
}
