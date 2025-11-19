using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Common.Models;
using HelLanhShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly HelLanhDBContext _context;
        public readonly DbSet<T> _dbSet;

        public GenericRepository(HelLanhDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
             _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }
        public async Task<PagedResult<T>> GetPagedAsync(IQueryable<T> query, int pageIndex, int pageSize, string? sortField = null, string? sortDir = "asc")
        {
            if (!string.IsNullOrEmpty(sortField))
            {
                query = sortDir!.ToLower() == "desc"
                    ? query.OrderByDescending(x => EF.Property<object>(x, sortField))
                    : query.OrderBy(x => EF.Property<object>(x, sortField));
            }
            var total = await query.CountAsync();
            var data = await query.Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();


            return PagedResult<T>.Success(data, pageIndex, pageSize, total);
        }
    }
}
