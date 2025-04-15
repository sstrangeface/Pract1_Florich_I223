using Yagnov_I212.DBmodel;

namespace Yagnov_I212.Services
{
    public interface IAuthService
    {
        Users Authenticate(string login, string pass);
    }
}