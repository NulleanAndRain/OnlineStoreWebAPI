using Nullean.OnlineStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullean.OnlineStore.BllInterfaceProducts
{
    internal interface IOrderBll
    {
        public Task<Response<IEnumerable<Order>>> GetUserOrders(Guid id);
    }
}
