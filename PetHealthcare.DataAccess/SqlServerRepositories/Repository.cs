using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;

namespace PetHealthcare.DataAccess.SqlServerRepositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) query = query.Where(filter);
            if(includeProperties != null)
            {
                foreach(var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            if (orderby != null) return await orderby(query).ToListAsync();
            Debug.WriteLine($"*** Start *** SQL QUERY STRING || Repository<T> || GetAllAsync(): ");                   // VS Does this already
            Debug.WriteLine(query!.ToQueryString());
            Debug.WriteLine($"*** End *** SQL QUERY STRING || Repository<T> || GetAllAsync(): ");
            return await query.ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            Debug.WriteLine($"*** Start *** SQL QUERY STRING || Repository<T> || GetFirstOrDefaultAsync(): ");      // VS Does this already
            Debug.WriteLine(query!.ToQueryString());
            Debug.WriteLine($"*** End *** SQL QUERY STRING || Repository<T> || GetFirstOrDefaultAsync(): ");
            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }
        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
        }
        public async Task RemoveByIdAsync(int id)
        {
            T? entityToRemove = await dbSet.FindAsync(id);
            if (entityToRemove != null) dbSet.Remove(entityToRemove);
        }
    }
}
