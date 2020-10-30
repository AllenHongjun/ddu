using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TigerAdmin.Authorization.Roles;
using TigerAdmin.Authorization.Users;
using TigerAdmin.MultiTenancy;
using TigerAdmin.Demo;
using TigerAdmin.Demo.Events;

namespace TigerAdmin.EntityFrameworkCore
{
    public class TigerAdminDbContext : AbpZeroDbContext<Tenant, Role, User, TigerAdminDbContext>
    {
        /* Define a DbSet for each entity of the application */

        #region demo
        //public DbSet<Task> Tasks { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<EventRegistration> EventRegistrations { get; set; }
        #endregion


        public TigerAdminDbContext(DbContextOptions<TigerAdminDbContext> options)
            : base(options)
        {
        }
    }
}
