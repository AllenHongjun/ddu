using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Authorization.Permissions.Dto
{
    public class FlatPermissionWithLevelDto:FlatPermissionDto
    {
        public int Level { get; set; }
    }
}
