using HelLanhShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        //Task SaveChangesAsync();
        IQueryable<T> Query();
        Task<PagedResult<T>> GetPagedAsync(IQueryable<T> query, int pageIndex, int pageSize, string? sortField = null, string? sortDir = "asc");

    }
}
