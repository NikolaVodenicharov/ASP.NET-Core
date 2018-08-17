namespace AspNetCoreIntro
{
    using AspNetCoreIntro.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Infrastructure.Extensions;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options =>
                options.UseSqlServer(AppSettings.DatabaseConnectionString));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDatabaseMigration();
            app.UseStaticFiles();
            app.UseHtmlContentType();
            app.UseRequestHandlers();
            app.UseNotFoundHandler();
        }
    }
}
