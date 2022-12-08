using Nullean.OnlineStore.BllInterfaceProducts;
using Nullean.OnlineStore.DalInterfaceProducts;
using Nullean.OnlineStore.Entities;

namespace ProductsLogic
{
    public class OrderLogic : IOrderBll
    {
        private readonly IProductsDao _dao;

        public OrderLogic(IProductsDao dao)
        {
            _dao = dao;
        }

        public async Task<Response<IEnumerable<Order>>> GetUserOrders(Guid id)
        {
            var response = new Response<IEnumerable<Order>>();
            try
            {
                var res = await _dao.GetUserOrders(id);
                if (res.Errors?.Any() ?? false)
                {
                    response.Errors = res.Errors;
                }
                else
                {
                    var orders = res.ResponseBody;
                    if (orders != null)
                    {
                        orders = orders.Select(o =>
                        {
                            o.TotalCount = o.Products.Count();
                            o.TotalPrice = o.Products.Sum(p => p.Price);
                            return o;
                        }).ToList();
                    }
                    response.ResponseBody = orders;
                }
            }
            catch (Exception ex)
            {
                var e = new Error
                {
                    Message = ex.Message
                };
                if (response.Errors?.Any() ?? false)
                {
                    response.Errors.Add(e);
                }
                else
                {
                    response.Errors = new List<Error>
                    {
                        e
                    };
                }
            }
            return response;
        }
    }
}
