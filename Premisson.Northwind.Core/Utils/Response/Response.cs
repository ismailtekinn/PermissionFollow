using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Core.Utils.Response
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public Response(bool isSucces, string message)
        {
            IsSuccess = isSucces;
            Message = message;
        }
        public Response(bool isSuccess, T data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }
        public Response(bool isSucces, string message ,T data)
        {
            IsSuccess = isSucces;
            Message = message;
            Data = data;

        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
