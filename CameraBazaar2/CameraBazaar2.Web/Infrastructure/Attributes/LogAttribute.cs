using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;

namespace CameraBazaar2.Web.Infrastructure.Attributes
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            var username = "Anonymous";
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                username = context.HttpContext.User.Identity.Name;
            }

            var controllerName = context.Controller.GetType().Name;

            var longActionName = context.ActionDescriptor.DisplayName;
            var startIndex = longActionName.IndexOf(controllerName) + controllerName.Length + 1;
            var endIndex = longActionName.IndexOf("(");
            var length = endIndex - startIndex;
            var actionName = longActionName.Substring(startIndex, length);

            var some = context.HttpContext.Connection.RemoteIpAddress;
            var logMessage = $"{DateTime.Now} – {context.HttpContext.Connection.RemoteIpAddress} – {username} – {controllerName}.{actionName}";

            if (context.Exception != null)
            {
                var exceptionType = context.Exception.GetType().Name;
                var exceptionMessage = context.Exception.Message;

                logMessage = $"[!] {logMessage} - {exceptionType} - {exceptionMessage}";
            }
        }
    }
}
