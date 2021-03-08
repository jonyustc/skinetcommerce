using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();
        
        public Specification(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }

        public Specification()
        {
        }

        protected void AddIncludes(Expression<Func<T, object>> spec)
        {
            Includes.Add(spec);
        }

        
    }
}