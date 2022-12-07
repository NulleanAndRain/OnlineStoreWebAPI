using Nullean.OnlineStore.DalInterfaceProducts;
using Nullean.OnlineStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullean.OnlineStore.ProductsDaoEF
{
    public class ProductsDaoEF : IProductsDao
    {
        public Task<Response<IEnumerable<Order>>> GetUserOrders(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
