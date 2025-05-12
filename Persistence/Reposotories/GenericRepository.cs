using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Reposotories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public  async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);  //added
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            //Without Specification[GetAll , GetById]  => miss (Open Closed Principle)
            //if(_dbContext.Set<TEntity>() is Product)
            //{
            //     _dbContext.Set<Product>().Include(t => t.ProductBrand).ToList();
            //}
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)

        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);  //deleted
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity); //modified
        }
    }
}
