namespace AspNetCoreIntro
{
    using AspNetCoreIntro.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using Infrastructure;
    using System.Threading.Tasks;
    using AspNetCoreIntro.Data.Models;
    using System;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options =>
                options.UseSqlServer("Server=.\\SQLEXPRESS;Database=CatsServerDb;Integrated Security=true"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use((context, next) =>
            {
                context.RequestServices.GetRequiredService<CatsDbContext>().Database.Migrate();
                return next();
            });

            app.UseStaticFiles();

            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");
                return next();
            });

            app.MapWhen(
                ctx => ctx.Request.Path.Value == "/"
                    && ctx.Request.Method == HttpMethod.Get,
                home =>
                {
                    home.Run(async (context) =>
                    {
                        await context.Response.WriteAsync($"<h1>{env.ApplicationName}</h1>");

                        var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                        var catsData = db
                            .Cats
                            .Select(c => new
                            {
                                c.Id,
                                c.Name
                            })
                            .ToList();

                        await context.Response.WriteAsync("<ul>");

                        foreach (var cat in catsData)
                        {
                            await context.Response.WriteAsync($@"<li><a href=""/cats/{cat.Id}"">{cat.Name}</a></li>");
                        }

                        await context.Response.WriteAsync("</ul>");

                        await context.Response.WriteAsync(@"
                            <form action=""/cat/add"">
                                <input type=""submit"" value=""Add Cat"" />
                            </form>");
                    });
                });

            app.MapWhen(
                req => req.Request.Path.Value == "/cat/add",
                catAdd =>
                {
                    catAdd.Run(async (context) =>
                    {
                        if (context.Request.Method == HttpMethod.Get)
                        {
                            context.Response.Redirect("/cats-add-form.html");
                        }
                        else if(context.Request.Method == HttpMethod.Post)
                        {
                            var formData = context.Request.Form;

                            int catAge = 0;
                            int.TryParse(formData["Age"], out catAge);

                            var cat = new Cat
                            {
                                Name = formData["Name"],
                                Age = catAge,
                                Breed = formData["Breed"],
                                ImageUrl = formData["ImageUrl"]
                            };

                            using (var db = context.RequestServices.GetRequiredService<CatsDbContext>())
                            {     
                                db.Add(cat);

                                try
                                {
                                    if (string.IsNullOrWhiteSpace(cat.Name) ||
                                        string.IsNullOrWhiteSpace(cat.Breed) ||
                                        string.IsNullOrWhiteSpace(cat.ImageUrl))
                                    {
                                        throw new InvalidOperationException("Invalid cat data.");
                                    }

                                    await db.SaveChangesAsync();

                                    context.Response.Redirect("/");
                                }
                                catch (System.Exception)
                                {
                                    await context.Response.WriteAsync("<h2>Invalid cat data.</h2>");
                                    await context.Response.WriteAsync(@"<a href=""/cat/add"">Back to the form</a");
                                }
                            }
                        }
                    });
                });

            app.MapWhen(
                ctx => ctx.Request.Path.Value.StartsWith("/cat")
                    && ctx.Request.Method == HttpMethod.Get,
                catDetails =>
                {
                    catDetails.Run(async (context) =>
                    {
                        var urlParths = context.Request.Path.Value.Split("/", StringSplitOptions.RemoveEmptyEntries);
                        if (urlParths.Length < 2)
                        {
                            context.Response.Redirect("/");
                            return;
                        }

                        var catId = 0;
                        int.TryParse(urlParths[1], out catId);
                        if (catId == 0)
                        {
                            context.Response.Redirect("/");
                            return;
                        }

                        using (var db = context.RequestServices.GetRequiredService<CatsDbContext>())
                        {
                            var cat = await db.Cats.FindAsync(catId);
                            if (cat == null)
                            {
                                context.Response.Redirect("/");
                                return;
                            }

                            await context.Response.WriteAsync($"<h1>{cat.Name}</h1>");
                            await context.Response.WriteAsync($@"<img src=""{cat.ImageUrl}"" alt=""{cat.Name}"" width=""300"" />");
                            await context.Response.WriteAsync($"<p>Age: {cat.Age} </p>");
                            await context.Response.WriteAsync($"<p>Breed: {cat.Breed} </p>");
                        }

                    });
                });

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 Page was not found :/");
            });
        }
    }
}
