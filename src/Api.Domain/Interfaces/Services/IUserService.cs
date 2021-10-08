using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities.User;

namespace Api.Domain.Interfaces.Services
{
    public interface IUserService : IDisposable
    {
         Task<IEnumerable<UserCreateDTO>> GetAll();
         Task<UserCreateDTO> Get(Guid id);
         Task<UserCreateResultDTO> Post(UserCreateDTO user);
         Task<UserUpdateResultDTO> Put(UserUpdateDTO user);
         Task<bool> Delete(Guid id);
    }
}