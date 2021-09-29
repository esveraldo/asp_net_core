using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> Select()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public async Task<TEntity> Select(Guid id)
        {
            try
            {
                return await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public async Task<TEntity> Post(TEntity entity)
        {
            try
            {
                if(entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();

                entity.CreatedAt = DateTime.UtcNow;

                _dbSet.Add(entity);
                await SaveChanges();
            }
            catch (Exception e)
            {
                
                throw e;
            }

            return entity;
        }

        public async Task<TEntity> Put(TEntity entity)
        {
            try
            {
                var Result = await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));

                if(Result == null)
                    return null;

                entity.UpdateAt = DateTime.UtcNow;
                entity.CreatedAt = Result.CreatedAt;

                _context.Entry(Result).CurrentValues.SetValues(entity);
                await SaveChanges();
            }
            catch (Exception e)
            {
                
                throw e;
            }

            return entity;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var Result = await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

                if(Result == null)
                    return false;

                _dbSet.Remove(Result);
                await SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public async Task<bool> Exist(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id.Equals(id));
        }
    }
}