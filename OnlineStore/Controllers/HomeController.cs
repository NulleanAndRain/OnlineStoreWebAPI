using Microsoft.AspNetCore.Mvc;
using Nullean.OnlineStore.BllInterfaceProducts;
using Nullean.OnlineStore.BllInterfaceUsers;

namespace Nullean.OnlineStore.OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserBll _users;
        private readonly IOrderBll _orders;

        public HomeController(IUserBll users, IOrderBll orders)
        {
            _users = users;
            _orders = orders;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetDb()
        {


            return null;
        }
    }
}
