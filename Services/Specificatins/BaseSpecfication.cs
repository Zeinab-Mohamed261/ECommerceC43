using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specificatins
{
    public class BaseSpecfication<TEntity,T> : ISpecefications<TEntity ,T> where TEntity : BaseEntity<T>
    {
        public BaseSpecfication(Expression<Func<TEntity, bool>>? criteria) 
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria // where conditions(null => GetAll , with Value => GetById)
        {
            get;
            private set;
        }

        

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];// this is intialize to no refer to null

        protected void AddInclude(Expression<Func<TEntity, object>> include)
            => IncludeExpression.Add(include);
    }
}
