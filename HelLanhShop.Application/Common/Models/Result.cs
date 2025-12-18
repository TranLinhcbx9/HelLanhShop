   using HelLanhShop.Application.Common.Enums;
namespace HelLanhShop.Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public ErrorType ErrorType { get; }
        public ErrorCode ErrorCode { get; }

        protected Result(bool isSuccess, string? error, ErrorType errorType, ErrorCode errorCode)
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorType = errorType;
            ErrorCode = errorCode;
        }

        public static Result Success()
            => new(true, null, ErrorType.None, ErrorCode.NONE);

        public static Result Failure(string error, ErrorType errorType, ErrorCode errorCode)
            => new(false, error, errorType, errorCode);
    }
    public class Result<T> : Result
    {
            public T? Data { get; }
            protected Result(bool isSuccess, T? data, string? error, ErrorType errorType, ErrorCode errorCode) : base(isSuccess, error, errorType, errorCode)
            {
                Data = data;
            }

            public static Result<T> Success(T data) => new(true, data, null, ErrorType.None, ErrorCode.NONE);
            public static Result<T> Failure(string error, ErrorType errorType, ErrorCode errorCode) => new(false, default, error, errorType, errorCode);
    }
    public class PagedResult<T> : Result<IEnumerable<T>>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalItems { get; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasNext => PageIndex < TotalPages;
        public bool HasPrevious => PageIndex > 1;

        // Add a constructor that calls the base constructor
        private PagedResult(
        bool isSuccess,
        IEnumerable<T>? data,
        string? error,
        ErrorType errorType,
        ErrorCode errorCode,
        int pageIndex,
        int pageSize,
        int totalItems
    ) : base(isSuccess, data, error, errorType, errorCode)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        // factory methods for success/failure
        public static PagedResult<T> Success(IEnumerable<T> data, int pageIndex, int pageSize, int totalItems)
            => new PagedResult<T>(true, data, null, ErrorType.None, ErrorCode.NONE, pageIndex, pageSize, totalItems);

        public static PagedResult<T> Failure(string error, ErrorType errorType, ErrorCode errorCode)
            => new PagedResult<T>(false, null, error, errorType, errorCode, 0, 0, 0);
    }

}
