using System.Threading.Tasks;
using Api.Domain.Entities.User;
using Api.Domain.Interfaces;

namespace Api.Domain.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
         Task<UserEntity> FindByLogin(string email, string senha);
    }
}