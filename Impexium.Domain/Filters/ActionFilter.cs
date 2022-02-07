using Impexium.Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impexium.Domain.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(Response.BuildResponse(StatusCodes.Status400BadRequest, GetErrorList(context)));
            }
        }

        private List<string> GetErrorList(ActionExecutingContext context)
        {
            return context.ModelState.Values.Aggregate(
               new List<string>(),
               (a, c) =>
               {
                   a.AddRange(c.Errors.Select(r => r.ErrorMessage));
                   return a;
               },
               a => a
            );
        }
    }
}
