namespace Coffee.Shared
{
    public class ResponseResult<T> : ResponseResult
    {
        public ResponseResult(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public ResponseResult(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        public T Data { get; set; }
    }

    public class ResponseResult
    {
        public ResponseResult(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public ResponseResult()
        {
        }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}
