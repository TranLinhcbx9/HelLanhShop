namespace HelLanhShop.API.Common.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string? message = null)
            => new() { Success = true, Data = data, Message = message ?? "" };

        public static ApiResponse<T> Fail(string message)
            => new() { Success = false, Message = message, Data = default };
    }

    public class ApiResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";

        public static ApiResponse Ok(string? message = null)
            => new() { Success = true, Message = message ?? "" };

        public static ApiResponse Fail(string message)
            => new() { Success = false, Message = message };
    }

    public class PagedResponse<T> : ApiResponse<IEnumerable<T>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasNext => PageIndex < TotalPages;
        public bool HasPrevious => PageIndex > 1;

        public static PagedResponse<T> Create(IEnumerable<T> data, int page, int pageSize, int totalItems)
            => new() { Success = true, Data = data, PageIndex = page, PageSize = pageSize, TotalItems = totalItems };
    }


}
