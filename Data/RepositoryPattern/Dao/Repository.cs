using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryPattern.Dao
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal DbContext context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async virtual Task<IEnumerable<T>> FindByCriteria(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return await query.ToListAsync<T>();
            }
        }


        public virtual void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public async virtual void Delete(object PK)
        {
            T entity = await dbSet.FindAsync(PK);
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async virtual Task<IEnumerable<T>> FindAll()
        {
            return await dbSet.ToListAsync<T>();
       }

        public async virtual Task<T> FindByPK(object PK) {
            return await dbSet.FindAsync(PK);
        }
        

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
