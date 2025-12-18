using HelLanhShop.Application.Common.Enums;
using Volo.Abp.ExceptionHandling;

namespace HelLanhShop.API.Common.ApiResponses
{
    public class ApiResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public ErrorType ErrorType { get; set; }
        public ErrorCode ErrorCode { get; set; }
        protected ApiResponse(bool success, string message, ErrorType errorType, ErrorCode errorCode) 
        {
            Success = success;
            Message = message;
            ErrorType = errorType;
            ErrorCode = errorCode;
        }
        public static ApiResponse Ok(string? message = null)
            => new(true, message ?? "", ErrorType.None, ErrorCode.NONE);

        public static ApiResponse Fail(string message, ErrorType errorType, ErrorCode errorCode )
            => new(false, message, errorType, errorCode);
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }
        protected ApiResponse(bool success, T? data, string message, ErrorType errorType, ErrorCode errorCode) : base(success, message, errorType, errorCode)
        {
            Data = data;
        }
        public static ApiResponse<T> Ok(T data, string? message = null)
            => new(true, data, message ?? "", ErrorType.None, ErrorCode.NONE);

        public static ApiResponse<T> Fail(string message, T? data, ErrorType errorType,ErrorCode errorCode )
            => new(false, data, message, errorType, errorCode);
    }

    

    public class PagedResponse<T> : ApiResponse<IEnumerable<T>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasNext => PageIndex < TotalPages;
        public bool HasPrevious => PageIndex > 1;
        protected PagedResponse(bool success, IEnumerable<T> data, int pageIndex, int pageSize, int totalItems, string message, ErrorType errorType, ErrorCode errorCode)
            : base(success, data, message, errorType, errorCode)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
        public static PagedResponse<T> Create(IEnumerable<T> data, int page, int pageSize, int totalItems, string message, ErrorType errorType, ErrorCode errorCode)
            => new(true, data, page, pageSize, totalItems, message, errorType, errorCode);
        public static PagedResponse<T> Fail(string message, ErrorType errorType, ErrorCode errorCode, int page = 1, int pageSize = 0, int totalItems = 0)
        => new(false, Enumerable.Empty<T>(), page, pageSize, totalItems, message, errorType, errorCode);
    }


}
