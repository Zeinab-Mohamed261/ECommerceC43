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

        public Expression<Func<TEntity, object>> OrderBy{ get; private set; }

        public Expression<Func<TEntity, object>> orderByDescending 
            {
            get;
            private set;
        }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void ApplyPagination(int pageSize , int pageIndex)
        //                                  5                2
        {
            IsPaginated = true;
            Take = pageSize; //take(5).skip(5)
            Skip = (pageIndex - 1) * pageSize;//1*5 = 5
        }

        //public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
            => IncludeExpression.Add(include);

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
           => OrderBy = orderBy;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> oorderByDescending)
          => orderByDescending = oorderByDescending;
    }
}
