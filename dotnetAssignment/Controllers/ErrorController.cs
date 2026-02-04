using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace dotnetAssignment.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var statusCodeResult =
                HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "The resource you requested could not be found";
                    //ViewBag.Path = statusCodeResult.OriginalPath;
                    //ViewBag.QS = statusCodeResult.OriginalQueryString;
                    _logger.LogWarning($"404 not found error occurred in {statusCodeResult.OriginalPath}" +
                        $"with query string {statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("ResourceNotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
        HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            //ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            //ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
            _logger.LogError($"An exception occurred in {exceptionHandlerPathFeature.Path} with message " +
                $"{exceptionHandlerPathFeature.Error.Message} and statck trace is {exceptionHandlerPathFeature.Error.StackTrace}");
            return View("Error");
        }
    }
}
