using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Results
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }

        public static ResponseModel<T> Error(int statusCode, string errors, string errorMessage = null)
        {
            if (errorMessage == null)
                return new ResponseModel<T> { StatusCode = statusCode, Errors = errors };

            return new ResponseModel<T> { StatusCode = statusCode, Message = errorMessage, Errors = errors };
        }
        public static ResponseModel<T> Success(T data, string message = null)
        {
            if (message != null)
                return new ResponseModel<T> { StatusCode = 200, Data = data, Message = message };

            return new ResponseModel<T> { StatusCode = 200, Data = data };
        }
    }
}
