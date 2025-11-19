using AutoMapper;
using HelLanhShop.Application.Common.Helpers;
using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IGenericRepository<T> _repo => _unitOfWork.GenericRepository<T>();
        public GenericService(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<TDto>> SearchAsync<TDto>(BaseFilter filter)
        {
            var query = _repo.Query();

            query = FilterEngine.ApplyFilters(query, filter.Rules);

            // Sorting

            var paged = await _repo.GetPagedAsync(query, filter.PageIndex, filter.PageSize, filter.SortField, filter.SortDir);

            var dtos = _mapper.Map<List<TDto>>(paged.Data);

            return PagedResult<TDto>.Success(
                dtos,
                paged.PageIndex,
                paged.PageSize,
                paged.TotalPages
            );
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
