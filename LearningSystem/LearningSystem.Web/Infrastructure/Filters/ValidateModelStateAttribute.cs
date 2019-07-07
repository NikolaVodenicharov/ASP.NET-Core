using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Infrastructure.Filters
{
    /// <summary>
    /// The target controller must inherit the base Controller.
    /// The action must contain parameter with name model.
    /// There must be a View, with the same name like the action.
    /// If the model state is not valid, the attribute will return new view with the model data.
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        private const string ModelName = "model";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var controller = context.Controller as Controller;

                if (controller == null)
                {
                    return;
                }

                var model = context
                    .ActionArguments
                    .FirstOrDefault(a => a.Key.ToLower().Contains(ModelName))
                    .Value;

                if (model == null)
                {
                    return;
                }

                context.Result = controller.View(model);
            }
        }
    }
}
