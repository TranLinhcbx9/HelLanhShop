using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelLanhShop;
using HelLanhShop.Application.Common.Enums;
namespace HelLanhShop.Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public ErrorType ErrorType { get; }

        protected Result(bool isSuccess, string? error, ErrorType errorType)
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorType = errorType;
        }

        public static Result Success()
            => new(true, null, ErrorType.None);

        public static Result Failure(string error, ErrorType errorType)
            => new(false, error, errorType);
    }
    public class Result<T> : Result
    {
            public T? Data { get; }
            protected Result(bool isSuccess, T? data, string? error, ErrorType errorType) : base(isSuccess, error, errorType)
            {
                Data = data;
            }

            public static Result<T> Success(T data) => new(true, data, null, ErrorType.None);
            public new static Result<T> Failure(string error, ErrorType errorType) => new(false, default, error, errorType);
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
        private PagedResult(
        bool isSuccess,
        IEnumerable<T>? data,
        string? error,
        ErrorType errorType,
        int pageIndex,
        int pageSize,
        int totalItems
    ) : base(isSuccess, data, error, errorType)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        // factory methods for success/failure
        public static PagedResult<T> Success(IEnumerable<T> data, int pageIndex, int pageSize, int totalItems)
            => new PagedResult<T>(true, data, null, ErrorType.None, pageIndex, pageSize, totalItems);

        public new static PagedResult<T> Failure(string error, ErrorType errorType)
            => new PagedResult<T>(false, null, error, errorType, 0, 0, 0);
    }

}
