using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Infrastructure.Attributes
{
    /// <summary>
    /// This attribute remove the "magic" string when writing a route for controller.
    /// Now we can do [RouteController(nameof(HomeController))]. The "Controller" word
    /// will be removed and the "Home" will be passed to the RouteAttribute.
    /// </summary>
    public class RouteControllerAttribute : RouteAttribute
    {
        private const string Controller = "Controller";

        public RouteControllerAttribute(string template)
            :base(template.Replace(Controller, string.Empty))
        {

        }
    }
}
