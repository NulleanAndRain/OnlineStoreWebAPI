﻿namespace Nullean.OnlineStore.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }

    public class UserDetailed : User
    {
        public decimal TotalOrdersPrice { get; set; }
        public int TotalOrdersCount { get; set; }
    }
}
