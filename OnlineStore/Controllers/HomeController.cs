using Microsoft.AspNetCore.Mvc;

namespace Nullean.OnlineStore.OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
