namespace FormWise.WebApi.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }

        public static Result<T> Success(T data, string message = "", int statusCode = 200)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                StatusCode = statusCode
            };
        }

        public static Result<T> EmptySuccess(string message = "", int statusCode = 200)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static Result<T> Fail(string message, int statusCode = 400, List<string>? errors = null)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors,
                StatusCode = statusCode
            };
        }

        public static Result<T> Fail(List<string> errors, int statusCode = 400)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = "One or more errors occurred.",
                Errors = errors,
                StatusCode = statusCode
            };
        }
    }
}
