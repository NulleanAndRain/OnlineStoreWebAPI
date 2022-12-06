using Nullean.OnlineStore.Entities;

namespace Nullean.OnlineStore.BllInterfaceUsers
{
    public interface IUserBll
    {
        public Task<Response> LoginUser(string username, string password);

        public Task<Response> CreateUser(User user);

        public Task<Response<UserDetailed>> GetUserDetials(string username);
    }
}
