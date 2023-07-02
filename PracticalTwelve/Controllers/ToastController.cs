using Microsoft.AspNetCore.Mvc;

namespace PracticalTwelve.Controllers
{
    public class ToastController : Controller
    {
        /// <summary>
        /// Create toast message at bottom-right corner 
        /// </summary>
        /// <param name="message">message to display(default message will be success.)</param>
        /// <returns></returns>
        public IActionResult Index(string message = "Success!!")
        {
            ViewData["Message"] = message;
            return PartialView("_Toast");
        }
    }
}
