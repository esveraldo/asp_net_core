using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities.User;
using Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dbset;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _dbset = context.Set<UserEntity>();
        }
        public async Task<UserEntity> FindByLogin(string email, string senha)
        {
           return await _dbset.FirstOrDefaultAsync(e => e.Email.Equals(email) && e.Senha.Equals(senha));
        }
    }
}