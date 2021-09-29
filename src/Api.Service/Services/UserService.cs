using System;
using System.Collections;
using System.Threading.Tasks;
using Api.Domain.Entities.User;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        protected readonly IDisposable _disposable;

        public UserService(IRepository<UserEntity> repository)
        {
            _repository = repository;

        }

        public async Task<IEnumerable> GetAll()
        {
            return await _repository.Select();
        } 
        public async Task<UserEntity> Get(Guid id)
        {
            return await _repository.Select(id);
        }

        public async Task<UserEntity> Post(UserEntity user)
        {
            return await _repository.Post(user); 
        }

        public async Task<UserEntity> Put(UserEntity user)
        {
            return await _repository.Put(user);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}