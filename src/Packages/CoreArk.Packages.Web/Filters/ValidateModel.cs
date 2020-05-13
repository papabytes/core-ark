using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreArk.Packages.Web.Filters
{
    public class ValidateModel : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            var message = context.ModelState.Keys.ToDictionary(key => key,
                key => context.ModelState[key].Errors.Select(p => p.ErrorMessage));

            context.Result = new JsonResult(new
            {
                message = message
            }) {StatusCode = StatusCodes.Status400BadRequest};
        }
    }
}