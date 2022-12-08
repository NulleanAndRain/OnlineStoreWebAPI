using Nullean.OnlineStore.Entities;

namespace Nullean.OnlineStore.DalInterfaceProducts
{
    public interface IProductsDao
    {
        public Task<Response<IEnumerable<Order>>> GetUserOrders(Guid id);
        public Task<Response> AddOrder(Order order, Guid userId);
    }
}
