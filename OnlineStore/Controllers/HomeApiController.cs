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
    [ApiController]
    [Route("api/[controller]")]
    public class HomeApiController : Controller
    {
        private readonly IUserBll _users;
        private readonly IOrderBll _orders;

        private const string USER_ID_FIELD = "user_id";

        public HomeApiController(IUserBll users, IOrderBll orders)
        {
            _users = users;
            _orders = orders;
        }

        [HttpGet("/GetUserInfo")]
        [Authorize(Policy = Constants.RoleNames.Admin)]
        public async Task<Response<UserModel>> GetUserInfo([FromHeader] Guid id)
        {
            var res = await _users.GetUserDetials(id);

            if (res.Errors?.Any() ?? false)
            {
                await LogOut();
            }
            var user = res.ResponseBody;

            UserModel model = null;
            if (user != null)
            {
                model = new UserModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Orders = user.Orders,
                };
            }

            return new Response<UserModel>
            {
                Errors = res.Errors,
                ResponseBody = model
            };
        }

        [HttpGet("/GetFullUserInfo")]
        [Authorize(Policy = Constants.RoleNames.Admin)]
        public async Task<Response<UserModelDetailed>> GetFullUserInfo([FromHeader]Guid id)
        {
            var res = await _users.GetUserDetials(id);

            if (res.Errors?.Any() ?? false)
            {
                await LogOut();
            }
            var user = res.ResponseBody;

            UserModelDetailed model = null;
            if (user != null)
            {
                model = new UserModelDetailed
                {
                    Id = user.Id,
                    Username = user.Username,
                    Orders = user.Orders,
                    OrdersCount = user.TotalOrdersCount,
                    OrdersTotalPrice = user.TotalOrdersPrice
                };
            }

            return new Response<UserModelDetailed>
            {
                Errors = res.Errors,
                ResponseBody = model
            };
        }

        [AllowAnonymous]
        [HttpPost("/LogIn")]
        public async Task<Response<UserModel>> LogIn([FromBody]LogInModel model)
        {
            var response = new Response<UserModel>();
            var res = await _users.LoginUser(model.Username, model.Password);

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

            response.Errors = res.Errors;
            response.ResponseBody = new UserModel
            {
                Id = user.Id,
                Username = user.Username,
            };

            return response;
        }

        [AllowAnonymous]
        [HttpPost("/SignUp")]
        public async Task<Response> SignUp([FromBody]SignUpModel model)
        {
            var res = await _users.CreateUser(new User
            {
                Username = model.Username,
                Password = model.Password,
                Role = Constants.RoleNames.User,
            });

            return res;
        }

        [HttpPost("/LogOut")]
        public async Task LogOut()
        {
            await HttpContext.SignOutAsync("Cookie");
        }

        [HttpPost("/AddTestOrder")]
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
