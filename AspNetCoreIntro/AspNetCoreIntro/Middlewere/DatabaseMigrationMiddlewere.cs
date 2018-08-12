namespace AspNetCoreIntro.Middlewere
{
    using AspNetCoreIntro.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public class DatabaseMigrationMiddlewere
    {
        private readonly RequestDelegate next;

        public DatabaseMigrationMiddlewere(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.RequestServices.GetRequiredService<CatsDbContext>().Database.Migrate();

            return this.next(context);
        }
    }
}
