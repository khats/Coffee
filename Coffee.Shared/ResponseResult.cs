using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Shared
{
    public class ResponseResult<T> : ResponseResult
    {
        public ResponseResult(string errorMessage)
        {
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
            ErrorMessage = errorMessage;
        }

        public ResponseResult()
        {
        }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}
