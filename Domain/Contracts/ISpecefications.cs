using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecefications<TEntity,T> where TEntity : BaseEntity<T>
    {
        public Expression<Func<TEntity,bool>> Criteria { get;} //p => p.id == 1
        public List<Expression<Func<TEntity,object>>> IncludeExpression { get; }


    }
}
