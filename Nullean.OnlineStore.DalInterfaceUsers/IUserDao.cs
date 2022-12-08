using Nullean.OnlineStore.Entities;

namespace Nullean.OnlineStore.DalInterfaceUsers
{
    public interface IUserDao
    {
        public Task<Response<User>> GetUserByName(string username);
        public Task<Response> CreateUser(User user);
        public Task<Response<UserDetailed>> GetUserDetials(Guid Id);
    }
}
