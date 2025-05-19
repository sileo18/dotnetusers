using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace dotnetusers.Exceptions
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {

            _logger.LogError(context.Exception, "An unhandled exception occurred while processing the request.");

            var exception = context.Exception;
            var result = new ObjectResult(new { error = exception.Message })
            {
                StatusCode = 500 
            };
            context.Result = result;

            context.ExceptionHandled = true;
        }
    }    
}
