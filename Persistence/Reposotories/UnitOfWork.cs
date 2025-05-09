using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Reposotories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //Get Type Name
            var typeName = typeof(TEntity).Name; //Get Type Name of TEntity

            //Dic<string , object>  ===> string : string key  [name of entity] , object : object from generic repository of TEntity
            //                                            Product              ,         GenericRepository<product>        
            if(_repositories.ContainsKey(typeName))
            {
                return (IGenericRepository<TEntity, TKey>) _repositories[typeName]; //cast to IGenericRepository
            }
            else
            {
                //create Object
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                //store Object in Dictionary
                //_repositories.Add(typeName, repo); //add to dictionary
                _repositories[typeName] = repo; //add to dictionary
                //return Object
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync(); //number of rows affected
        }
    }
}
