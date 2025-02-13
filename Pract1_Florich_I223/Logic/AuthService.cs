using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract1_Florich_I223.DBmodel;
using Pract1_Florich_I223.Model;

namespace Pract1_Florich_I223.Logic
{
    public class AuthService : IAuthService
    {
        private List<User> _users;
        private ShopDBEntities dbContext; // Делаем dbContext полем класса

        public AuthService()
        {
            dbContext = new ShopDBEntities(); // Инициализируем dbContext в конструкторе
        }

        public bool CheckData(string login, string pass)
        {
            // Ищем пользователя по логину и паролю
            var user = dbContext.Users.FirstOrDefault(u => u.Login == login && u.Pass == pass);

            // Если пользователь найден и пароль совпадает, возвращаем true
            if (user != null && user.Pass == pass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}