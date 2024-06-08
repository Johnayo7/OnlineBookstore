namespace OnlineBookstore.API.DTOs
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ApiResponse<T> Response(bool status, string message, T data)
        {
            return new ApiResponse<T>
            {
                Status = status,
                Message = message,
                Data = data
            };
        }
    }
}
