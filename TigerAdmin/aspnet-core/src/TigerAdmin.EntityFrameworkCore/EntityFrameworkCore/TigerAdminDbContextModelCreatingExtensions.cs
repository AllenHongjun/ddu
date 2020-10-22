using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace TigerAdmin.EntityFrameworkCore
{
    public static class TigerAdminDbContextModelCreatingExtensions
    {
        public static void ConfigureTigerAdmin(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(TigerAdminConsts.DbTablePrefix + "YourEntities", TigerAdminConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}