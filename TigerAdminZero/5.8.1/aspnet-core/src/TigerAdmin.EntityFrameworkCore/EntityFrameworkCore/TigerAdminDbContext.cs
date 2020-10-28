using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TigerAdmin.Authorization.Roles;
using TigerAdmin.Authorization.Users;
using TigerAdmin.MultiTenancy;
using TigerAdmin.Demo.Task;

namespace TigerAdmin.EntityFrameworkCore
{
    public class TigerAdminDbContext : AbpZeroDbContext<Tenant, Role, User, TigerAdminDbContext>
    {
        /* Define a DbSet for each entity of the application */

        #region demo
        public DbSet<Task> Tasks { get; set; }
        #endregion


        public TigerAdminDbContext(DbContextOptions<TigerAdminDbContext> options)
            : base(options)
        {
        }
    }
}
