using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Safqah.Shared.BaseRepository
{
    public class Repository<T, TKey, TDbContext> : IRepository<T, TKey, TDbContext> 
        where T : class 
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        public Repository(TDbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();

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
                return query.ToList();
            }
        }
        public T GetBy(Expression<Func<T, bool>> filter = null, string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            query = query.Where(filter);
            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            return query.FirstOrDefault();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
        public bool IsNameExists(Expression<Func<T, bool>> filter)
        {
            var query = _context.Set<T>().Where(filter);
            var count = query.Count();
            return count != 0;
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public void UpdateRange(List<T> entity)
        {
            _context.Set<T>().UpdateRange(entity);
            _context.SaveChanges();
        }

        

        public void Delete(TKey id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();

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
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        

        
        public async Task<T> GetByAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            query = query.Where(filter);
            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => _context.Set<T>().Update(entity));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => _context.Set<T>().Remove(entity));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TKey id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            await Task.Run(() => _context.Set<T>().Remove(entity));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsNameExistsAsync(Expression<Func<T, bool>> filter)
        {
            var count = await _context.Set<T>().Where(filter).CountAsync();
            return count != 0;
        }

        

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task UpdateRangeAsync(List<T> entity)
        {
            await Task.Run(() => _context.Set<T>().UpdateRange(entity));
            await _context.SaveChangesAsync();
        }

    }
}