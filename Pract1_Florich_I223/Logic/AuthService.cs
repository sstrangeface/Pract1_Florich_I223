using System.Data.Entity;
using System.Linq;
using Yagnov_I212.DBmodel;
using Yagnov_I212.Services;

namespace Yagnov_I212.Logic
{
    public class AuthService : IAuthService
    {
        private readonly ShopDBEntities5 dbContext;

        public AuthService()
        {
            dbContext = new ShopDBEntities5();
        }

        public Users Authenticate(string login, string pass)
        {
            return dbContext.Users.Include("Roles")
                                  .FirstOrDefault(u => u.Login == login && u.Pass == pass);
        }
    }
}
