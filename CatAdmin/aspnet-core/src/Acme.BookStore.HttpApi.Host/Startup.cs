using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Acme.BookStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {   
            // 看一下国外使用 abp开发的开源项目
            services.AddApplication<BookStoreHttpApiHostModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}
