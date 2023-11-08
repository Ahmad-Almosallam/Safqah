using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Safqah.Shared.BaseRepository
{
    public interface IRepository<T, TKey, TDbContext> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                string includeProperties = "");
        T GetBy(Expression<Func<T, bool>> filter = null, string includeProperties = "",
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(TKey id);
        bool IsNameExists(Expression<Func<T, bool>> filter);
        int Count();
        void UpdateRange(List<T> entity);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                string includeProperties = "");
        Task<T> GetByAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "",
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(TKey id);
        Task<bool> IsNameExistsAsync(Expression<Func<T, bool>> filter);
        Task<int> CountAsync();
        Task UpdateRangeAsync(List<T> entity);
    }
}