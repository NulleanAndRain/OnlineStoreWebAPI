﻿using Nullean.OnlineStore.Entities;

namespace Nullean.OnlineStore.BllInterfaceProducts
{
    public interface IOrderBll
    {
        public Task<Response<IEnumerable<Order>>> GetUserOrders(Guid id);
    }
}
