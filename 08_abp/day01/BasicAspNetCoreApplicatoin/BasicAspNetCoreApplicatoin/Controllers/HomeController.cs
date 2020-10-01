using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace BasicAspNetCoreApplicatoin.Controllers
{
    public class HomeController : AbpController
    {
        public IActionResult Index()
        {   
            // 属于内容管理系统细分的 课程管理系统
            return Content("Hello World!");
        }
    }
}
