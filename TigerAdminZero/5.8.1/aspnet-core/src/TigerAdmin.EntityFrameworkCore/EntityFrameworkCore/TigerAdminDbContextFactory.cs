using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TigerAdmin.Configuration;
using TigerAdmin.Web;

namespace TigerAdmin.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class TigerAdminDbContextFactory : IDesignTimeDbContextFactory<TigerAdminDbContext>
    {
        public TigerAdminDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TigerAdminDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            TigerAdminDbContextConfigurer.Configure(builder, configuration.GetConnectionString(TigerAdminConsts.ConnectionStringName));

            return new TigerAdminDbContext(builder.Options);
        }
    }
}
