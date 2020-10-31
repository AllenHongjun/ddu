using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Common.Dto
{
    public class PagedSortedAndFilteredInputDto:PagedAndSortedInputDto
    {
        public string Filter { get; set; }
    }
}
