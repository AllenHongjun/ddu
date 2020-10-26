using Abp.Authorization;
using TigerAdmin.Authorization.Roles;
using TigerAdmin.Authorization.Users;

namespace TigerAdmin.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
