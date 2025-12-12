using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelLanhShop;
namespace HelLanhShop.Application.Common.Models
{
    public class Result<T>
    {
            public bool IsSuccess { get; }
            public string? Error { get; }
            public T? Data { get; }

            protected Result(bool isSuccess, T? data, string? error)
            {
                IsSuccess = isSuccess;
                Data = data;
                Error = error;
            }

            public static Result<T> Success(T data) => new(true, data, null);
            public static Result<T> Failure(string error) => new(false, default, error);
    }
    public class PagedResult<T> : Result<IEnumerable<T>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasNext => PageIndex < TotalPages;
        public bool HasPrevious => PageIndex > 1;

        // Add a constructor that calls the base constructor
        public PagedResult(bool isSuccess, IEnumerable<T>? data, string? error)
            : base(isSuccess, data, error)
        {
        }

        // Optionally, you can add factory methods for success/failure if needed
        public static PagedResult<T> Success(IEnumerable<T> data, int pageIndex, int pageSize, int totalItems)
            => new PagedResult<T>(true, data, null)
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems
            };

        public new static PagedResult<T> Failure(string error)
            => new PagedResult<T>(false, null, error);
    }

}
