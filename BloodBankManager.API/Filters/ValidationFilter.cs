using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters; 

namespace BloodBankManager.API.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (!filterContext.ModelState.IsValid)
        {
            var messages = filterContext.ModelState
                .SelectMany(v => v.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            filterContext.Result = new BadRequestObjectResult(messages);
        }
    }
}
