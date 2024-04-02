using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Transactions;

namespace ActionFilterDemo;

public class TransactionScopeFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        bool hasNotTransactionalAttribute = false;
        if (context.ActionDescriptor is ControllerActionDescriptor actionDesc)
        {
            hasNotTransactionalAttribute = actionDesc.MethodInfo.IsDefined(typeof(NotTransactionalAttribute));
        }
        if (hasNotTransactionalAttribute)
        {
            await next();
            return;
        }
        using var txScope =
                new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await next();
        if (result.Exception == null)
        {
            txScope.Complete();
        }
    }
}

// To mark an action method as not requiring transaction control, you can create a custom attribute named NotTransactionalAttribute
//  [HttpGet]
//     [NotTransactional] // Mark this action method as not requiring transaction control
//     public IActionResult NotTransactionalAction()

[AttributeUsage(AttributeTargets.Method)]
public class NotTransactionalAttribute : Attribute
{
}
