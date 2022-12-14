using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nullean.OnlineStore.BllInterfaceProducts;
using Nullean.OnlineStore.BllInterfaceUsers;
using Nullean.OnlineStore.Entities;
using Nullean.OnlineStore.OnlineStore.Models;
using System.Security.Claims;

namespace Nullean.OnlineStore.OnlineStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserBll _users;
        private readonly IOrderBll _orders;

        private const string USER_ID_FIELD = "user_id";

        public HomeController(IUserBll users, IOrderBll orders)
        {
            _users = users;
            _orders = orders;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _users.GetUserDetials(
                Guid.Parse(User.Claims.SingleOrDefault(cl => cl.Type == USER_ID_FIELD).Value)
            );

            if (res.Errors?.Any() ?? false)
            {
                await LogOut();
            }
            var user = res.ResponseBody;

            if (user == null)
            {
                await LogOut();
                return Redirect("/Home/LogIn");
            }

            var model = new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                Orders = user.Orders,
            };
            return View(model);
        }

        [Authorize(Policy = Constants.RoleNames.Admin)]
        public async Task<IActionResult> Admin()
        {
            var res = await _users.GetUserDetials(
                Guid.Parse(User.Claims.SingleOrDefault(cl => cl.Type == USER_ID_FIELD).Value)
            );

            if (res.Errors?.Any() ?? false)
            {
                await LogOut();
            }
            var user = res.ResponseBody;

            if (user == null)
            {
                await LogOut();
                return Redirect("/Home/LogIn");
            }

            var model = new UserModelDetailed
            {
                Id = user.Id,
                Username = user.Username,
                Orders = user.Orders,
                OrdersCount = user.TotalOrdersCount,
                OrdersTotalPrice = user.TotalOrdersPrice
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult LogIn(LogInModel model, string returnUrl)
        {
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await _users.LoginUser(model.Username, model.Password);

            if (res.Errors?.Any() ?? false)
            {
                foreach (var e in res.Errors)
                    ModelState.AddModelError("", e.Message);
                return View(model);
            }

            var user = res.ResponseBody;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(USER_ID_FIELD, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);

            return Redirect(model.ReturnUrl ?? "/");
        }

        [AllowAnonymous]
        public IActionResult SignUp(string returnUrl)
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await _users.CreateUser(new User
            {
                Username = model.Username,
                Password = model.Password,
                Role = Constants.RoleNames.User,
            });

            if (res.Errors?.Any() ?? false)
            {
                foreach (var e in res.Errors)
                    ModelState.AddModelError("", e.Message);
                return View(model);
            }

            return Redirect(model.ReturnUrl ?? "/");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookie");
            return Redirect("/");
        }

        public async Task<IActionResult> AddTestOrder()
        {
            var userId = Guid.Parse(User.Claims.SingleOrDefault(cl => cl.Type == USER_ID_FIELD).Value);

            var p1 = new Product
            {
                Id = Constants.TestGuids.product1,
                Name = "Cheese",
                Price = 250,
            };

            var p2 = new Product
            {
                Id = Constants.TestGuids.product2,
                Name = "Bread",
                Price = 40,
            };

            var p3 = new Product
            {
                Id = Constants.TestGuids.product3,
                Name = "Butter",
                Price = 100,
            };

            var o = new Order
            {
                Products = new List<Product> { p1, p2, p3 }
            };

            var res =  await _orders.AddOrder(o, userId);

            if (res.Errors?.Any() ?? false)
            {
                foreach (var e in res.Errors)
                    ModelState.AddModelError("", e.Message);
            }

            return Redirect("/Home/Index");
        }
    }
}
