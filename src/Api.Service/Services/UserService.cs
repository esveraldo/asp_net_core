using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities.User;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;
        protected readonly IDisposable _disposable;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<UserCreateDTO>> GetAll()
        {
            var ListEntity = await _repository.Select();
            return _mapper.Map<IEnumerable<UserCreateDTO>>(ListEntity);
        } 
        public async Task<UserCreateDTO> Get(Guid id)
        {
            var entity =  await _repository.Select(id);
            return _mapper.Map<UserCreateDTO>(entity) ?? new UserCreateDTO();
        }

        public async Task<UserCreateResultDTO> Post(UserCreateDTO user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.Post(entity);

            return _mapper.Map<UserCreateResultDTO>(result); 
        }

        public async Task<UserUpdateResultDTO> Put(UserUpdateDTO user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.Put(entity);

            return _mapper.Map<UserUpdateResultDTO>(result);
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