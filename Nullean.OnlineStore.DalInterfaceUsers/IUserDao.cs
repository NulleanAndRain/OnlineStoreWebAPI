using Nullean.OnlineStore.Entities;

namespace Nullean.OnlineStore.DalInterfaceUsers
{
    public interface IUserDao
    {
        public Task<Response> LoginUser(string username, string password);

        public Task<Response> CreateUser(User user);

        public Task<Response<UserDetailed>> GetUserDetials(string username);
    }
}
