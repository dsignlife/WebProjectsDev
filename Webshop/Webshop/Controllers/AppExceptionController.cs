using Microsoft.AspNetCore.Mvc;

namespace Webshop.Controllers
{
    public class AppExceptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}