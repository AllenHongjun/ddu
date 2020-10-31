using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Authorization.Permissions.Dto
{
    public class FlatPermissionDto
    {
        public string ParentName { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 是否默认授权
        /// </summary>
        public bool IsGrantedByDefault { get; set; }
    }
}
