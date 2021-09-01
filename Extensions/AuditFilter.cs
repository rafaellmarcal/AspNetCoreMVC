using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreMVC.Extensions
{
    public class AuditFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public AuditFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.Info("Starting audit...");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string messageAudit = $"User: {context.HttpContext.User.Identity.Name} - " +
                    $"Accessing: {context.HttpContext.Request.GetDisplayUrl()}";

                _logger.Info(messageAudit);
            }
        }
    }
}
