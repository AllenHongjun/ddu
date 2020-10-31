using Abp.Application.Services.Dto;
using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TigerAdmin.Authorization.Permissions.Dto;

namespace TigerAdmin.Authorization.Permissions
{
    public class PermissionAppService : TigerAdminAppServiceBase, IPermissionAppService
    {
        public ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions()
        {
            //throw new NotImplementedException();
            var permissions = PermissionManager.GetAllPermissions();
            var rootPermissions = permissions.Where(p => p.Parent == null);
            var result = new List<FlatPermissionWithLevelDto>();

            foreach (var rootPermission in rootPermissions)
            {
                var level = 0;
                AddPermission(rootPermission, permissions, result, level);
            }

            return new ListResultDto<FlatPermissionWithLevelDto>
            {
                Items = result
            };
        }


        private void AddPermission(Permission permission, IReadOnlyList<Permission> allPermissions, List<FlatPermissionWithLevelDto> result, int level)
        {
            var flatPermission = ObjectMapper.Map<FlatPermissionWithLevelDto>(permission);
            flatPermission.Level = level;
            // 将权限添加到List<FlatPermissionWithLevelDto>
            result.Add(flatPermission);

            if (permission.Children == null)
            {
                return;
            }

            // 如果有子权限就递归来添加。
            var children = allPermissions.Where(p => p.Parent != null && p.Parent.Name == permission.Name).ToList();
            foreach (var childPermission in children)
            {
                AddPermission(childPermission, allPermissions, result, level + 1);
            }
        }
    }
}
