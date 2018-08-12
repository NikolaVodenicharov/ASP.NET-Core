namespace AspNetCoreIntro.Handlers
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class AddCatHandler : IHandler
    {
        public int Order => 2;

        public Func<HttpContext, bool> Condition =>
            context => context.Request.Path.Value == "/cat/add";

        public RequestDelegate RequestHandler => throw new NotImplementedException();
    }
}
