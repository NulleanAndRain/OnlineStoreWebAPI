using Nullean.OnlineStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullean.OnlineStore.DalInterfaceProducts
{
    internal interface IOrderDao
    {
        public Task<Response<IEnumerable<Order>>> GetUserOrders(Guid id);
    }
}
