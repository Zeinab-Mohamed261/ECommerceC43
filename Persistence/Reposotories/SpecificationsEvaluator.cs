using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Reposotories
{
    public static class SpecificationsEvaluator
    {
        //                                                                    TEntity                                     Expression
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey> (IQueryable<TEntity> inputQuery , ISpecefications<TEntity,TKey> specefication)
            where TEntity : BaseEntity<TKey>

        {
            var query = inputQuery; //_dbContext.Set<TEntity>()
            if (specefication.Criteria != null)
                query = query.Where(specefication.Criteria); //_dbContext.Set<TEntity>().where(p => p.brandId ==2 && p=> p.typeId == 3)

            //foreach (var include in specefication.IncludeExpression)
            //{
            //    query = query.Include(include); // _dbContext.Set<TEntity>().Include(t => t.ProductBrand)
            //}
            //_dbContext.Set<TEntity>().where(p => p.id ==1).Include(P => P.ProductBrand).include(P => P.ProductType);

            //Equals To Foreach
            query = specefication.IncludeExpression.Aggregate(query,
                (currentQuery , include) => currentQuery.Include(include));
            //_dbContext.Set<TEntity>().where(p => p.id ==1).Include(P => P.ProductBrand).include(P => P.ProductType);

            return query;
        }
    }
}
