using HelLanhShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<Result<PagedResult<TDto>>> SearchAsync<TDto>(BaseFilter filter);
    }
}
