using Microsoft.AspNetCore.Mvc;

namespace ZIP.FileSender.Controllers {
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}