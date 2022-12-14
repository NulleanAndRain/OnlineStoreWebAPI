using Nullean.OnlineStore.DalInterfaceUsers;
using Nullean.OnlineStore.EFContext;
using Nullean.OnlineStore.Entities;
using Microsoft.EntityFrameworkCore;

using UserModel = Nullean.OnlineStore.Entities.User;
using OrderModel = Nullean.OnlineStore.Entities.Order;
using ProductModel = Nullean.OnlineStore.Entities.Product;

using UserEF = Nullean.OnlineStore.EFContext.EfEntities.User;

namespace Nullean.OnlineStore.UserDaoEF
{
    public class UserDaoEF : IUserDao
    {
        private readonly AppDbContext _ctx;

        public UserDaoEF(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Response> CreateUser(UserModel user)
        {
            var response = new Response();
            try
            {
                var u = new UserEF
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Role = user.Role
                };
                await _ctx.AddAsync(u);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Errors = new List<Error>()
                {
                    new Error
                    {
                        Message = ex.Message
                    }
                };
            }
            return response;
        }

        public async Task<Response<UserModel>> GetUserByName(string username)
        {
            var response = new Response<UserModel>();
            try
            {
                var user = await _ctx.Users
                    .Where(u => u.Username == username)
                    .Select(u => new UserModel
                    {
                        Id = u.UserId,
                        Username = u.Username,
                        Password = u.Password,
                        Role = u.Role
                    })
                    .FirstOrDefaultAsync();
                response.ResponseBody = user;
            }
            catch (Exception ex)
            {
                response.Errors = new List<Error>()
                {
                    new Error
                    {
                        Message = ex.Message
                    }
                };
            }
            return response;
        }

        public async Task<Response<UserDetailed>> GetUserDetials(Guid Id)
        {
            var response = new Response<UserDetailed>();
            try
            {
                var user = await _ctx.Users
                    //.Include(u => u.Orders)
                    //.Include(u => u.Orders.Select(o => o.Products))
                    .Where(u => u.UserId == Id)
                    .Select(u => new UserDetailed
                    {
                        Id = u.UserId,
                        Orders = u.Orders.Select(o => new OrderModel
                        {
                            Id = o.OrderId,
                            Products = o.Products.Select(p => new ProductModel
                            {
                                Id = p.ProductId,
                                Price = p.Price,
                                Name = p.Name
                            })
                        }),
                        Username = u.Username,
                        Password = u.Password
                    })
                    .SingleOrDefaultAsync();
                response.ResponseBody = user;
            }
            catch (Exception ex)
            {
                response.Errors = new List<Error>()
                {
                    new Error
                    {
                        Message = ex.Message
                    }
                };
            }
            return response;
        }
    }
}
