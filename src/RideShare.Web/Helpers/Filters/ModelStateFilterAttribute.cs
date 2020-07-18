using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RideShare.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Helpers.Filters
{
    /// <summary>
    /// This Filter checks ModelState.IsValid and returns BadRequest(ModelState) if it's not valid and controller action won't we invoked.
    /// </summary>
    public class ModelStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var baseController = context.Controller as BaseController;

                context.Result = baseController?.BadRequest(context.ModelState) ?? new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
