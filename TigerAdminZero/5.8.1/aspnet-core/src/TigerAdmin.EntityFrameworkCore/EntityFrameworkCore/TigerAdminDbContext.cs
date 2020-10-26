using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TigerAdmin.Authorization.Roles;
using TigerAdmin.Authorization.Users;
using TigerAdmin.MultiTenancy;

namespace TigerAdmin.EntityFrameworkCore
{
    public class TigerAdminDbContext : AbpZeroDbContext<Tenant, Role, User, TigerAdminDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public TigerAdminDbContext(DbContextOptions<TigerAdminDbContext> options)
            : base(options)
        {
        }
    }
}
