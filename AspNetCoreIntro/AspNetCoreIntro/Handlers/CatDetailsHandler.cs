namespace AspNetCoreIntro.Handlers
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class CatDetailsHandler : IHandler
    {
        public int Order => 3;

        public Func<HttpContext, bool> Condition =>
            context => context.Request.Path.Value.StartsWith("/cat")
                    && context.Request.Method == HttpMethod.Get

        public RequestDelegate RequestHandler => throw new NotImplementedException();
    }
}
