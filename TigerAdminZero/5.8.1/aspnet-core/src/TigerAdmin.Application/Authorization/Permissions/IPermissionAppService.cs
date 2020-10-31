using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Authorization.Permissions.Dto;

namespace TigerAdmin.Authorization.Permissions
{
    public  interface IPermissionAppService:IApplicationService
    {
        /// <summary>
        /// 获取所有的权限树
        /// </summary>
        /// <returns></returns>
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
