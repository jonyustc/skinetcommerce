using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interface;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            // var query = _context.Set<T>().AsQueryable();
            return await GetInclude(spec).ToListAsync();
        }

        public async Task<T> GetEntityBySpec(ISpecification<T> spec)
        {
            // var query = _context.Set<T>().AsQueryable();
            return await GetInclude(spec).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetInclude(ISpecification<T> spec)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            if(spec.Criteria != null) {
                query = query.Where(spec.Criteria);
            }
            query =  spec.Includes.Aggregate(query,(current,include) => current.Include(include));

            return query;
 
            // var dbSet = _context.Set<T>();
            // IQueryable<T> query=null;
            // foreach(var inclue in spec.Includes){
            //    query = dbSet.Include(inclue);
            // }

            // return query ?? dbSet;
        }

        
    }
}