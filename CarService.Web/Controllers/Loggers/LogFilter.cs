using Microsoft.AspNetCore.Mvc.Filters;

namespace CarService.Web.Controllers.Loggers;

public class LogFilter(ILogger<LogFilter> logger) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogWarning($"Controller Action is Executing\n" +
                                 $"Context: {context}");
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogWarning($"Execution Completed\n" +
                            $"Context: {context}");
    }
}
