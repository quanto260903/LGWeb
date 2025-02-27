﻿using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); 
        Task<IEnumerable<T>> GetAllAsync(); 
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression); 
        Task AddAsync(T entity); 
        Task AddRangeAsync(IEnumerable<T> entities); 
        Task RemoveAsync(T entity); 
        Task RemoveRangeAsync(IEnumerable<T> entities); 
        Task UpdateAsync(T entity);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    }
}
