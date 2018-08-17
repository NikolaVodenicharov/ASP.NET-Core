namespace AspNetCoreIntro.Handlers
{
    using System;
    using AspNetCoreIntro.Data;
    using AspNetCoreIntro.Data.Models;
    using AspNetCoreIntro.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class AddCatHandler : IHandler
    {
        public int Order => 2;

        public Func<HttpContext, bool> Condition =>
            context => context.Request.Path.Value == "/cat/add";

        public RequestDelegate RequestHandler =>
            async (context) =>
            {
                if (context.Request.Method == HttpMethod.Get)
                {
                    context.Response.Redirect("/cats-add-form.html");
                }
                else if (context.Request.Method == HttpMethod.Post)
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
            };
    }
}
