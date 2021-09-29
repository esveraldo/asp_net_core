using System.Threading.Tasks;
using Api.Domain.Entities.User;

namespace Api.Domain.Interfaces.Services
{
    public interface ILoginService
    {
         Task<object> FindByLogin(UserEntity user);
    }
}