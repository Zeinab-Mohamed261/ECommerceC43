using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        //Generate Repositorty
        IGenericRepository<TEntity, TKey> GetRepository<TEntity , TKey>() where TEntity : BaseEntity<TKey>;

        //Save Changes
        Task<int> SaveChangesAsync(); //number of rows affected
    }
}
