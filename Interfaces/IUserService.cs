using Models;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user);
        User LoginUser(LogInModel user);
        User ReturnUser(string email);
    }
}