using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
         Task<IEnumerable<TEntity>> Select();
         Task<TEntity> Select(Guid id);
         Task<TEntity> Post(TEntity entity);
         Task<TEntity> Put(TEntity entity);
         Task<bool> Delete(Guid id);
         Task<bool> Exist(Guid id);
         Task SaveChanges();
    }
}