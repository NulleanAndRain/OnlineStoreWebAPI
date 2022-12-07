using Nullean.OnlineStore.DalInterfaceUsers;
using Nullean.OnlineStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullean.OnlineStore.UserDaoEF
{
    public class UserDaoEF : IUserDao
    {
        public Task<Response> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UserDetailed>> GetUserDetials(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Response> LoginUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
