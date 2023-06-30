using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PracticalTwelve.Models;
using System.Diagnostics;

namespace PracticalEleven.Controllers
{
    public class ErrorController : Controller
    {
        readonly IDictionary<int, string> _statusMessages = new Dictionary<int, string>();
        public ErrorController()
        {
            _statusMessages.Add(401, "User identity was not found!");
            _statusMessages.Add(403, "You don't have permission to access this resources!");
            _statusMessages.Add(404, "Sorry, the page you are looking for could not be found.");
            _statusMessages.Add(500, "Whoops, something went wrong on our servers!");
            _statusMessages.Add(503, "Service unavailable!");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error/{statusCode?}")]
        public IActionResult Error(int statusCode)
        {
            var errorFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            string message = String.Empty;
            if (errorFeature == null)
            {
                var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
                message = exceptionFeature?.Error.Message ?? _statusMessages[500];
            }
            else
            {
                message = (_statusMessages.ContainsKey(statusCode)) ? _statusMessages[statusCode] : "Something went wrong!";
            }

            return View(new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = statusCode,
                Message = message,
                OriginalPath = errorFeature?.OriginalPath ?? string.Empty,
            });
        }
    }
}
