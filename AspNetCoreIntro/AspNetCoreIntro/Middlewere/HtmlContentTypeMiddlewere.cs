namespace AspNetCoreIntro.Middlewere
{
    using AspNetCoreIntro.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class HtmlContentTypeMiddlewere
    {
        private readonly RequestDelegate next;

        public HtmlContentTypeMiddlewere(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add(HttpHeader.ContentType, HttpHeader.TextHtml);
            return this.next(context);
        }
    }
}
