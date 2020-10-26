using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TigerAdmin.EntityFrameworkCore
{
    public static class TigerAdminDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TigerAdminDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TigerAdminDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
