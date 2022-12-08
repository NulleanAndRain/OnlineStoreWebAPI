using Microsoft.EntityFrameworkCore;
using Nullean.OnlineStore.DalInterfaceProducts;
using Nullean.OnlineStore.EFContext;
using Nullean.OnlineStore.Entities;

using OrderModel = Nullean.OnlineStore.Entities.Order;
using ProductModel = Nullean.OnlineStore.Entities.Product;

namespace Nullean.OnlineStore.ProductsDaoEF
{
    public class ProductsDaoEF : IProductsDao
    {
        private readonly AppDbContext _ctx;

        public ProductsDaoEF(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Response<IEnumerable<Order>>> GetUserOrders(Guid id)
        {
            var response = new Response<IEnumerable<Order>>();
            try
            {
                var orders = _ctx.Orders
                    .Include(o => o.Products)
                    .Where(o => o.UserId == id)
                    .Select(o => new OrderModel
                    {
                        Id = o.OrderId,
                        Products = o.Products.Select(p => new ProductModel
                        {
                            Id = p.ProductId,
                            Name = p.Name,
                            Price = p.Price
                        }).ToList()
                    });
                response.ResponseBody = await orders.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Errors = new List<Error>()
                {
                    new Error
                    {
                        Message = ex.Message,
                    }
                };
            }
            return response;
        }
    }
}
