using Nullean.OnlineStore.Entities;

namespace Nullean.OnlineStore.OnlineStore.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }

    public class UserModelDetailed : UserModel
    {
        public int OrdersCount { get; set; }
        public decimal OrdersTotalPrice { get; set; }
    }
}
