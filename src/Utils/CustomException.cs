using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user.src.Utils
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public CustomException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public static CustomException NotFound(string message = "Item not found")
        {
            return new CustomException(404, message);
        }

        public static CustomException BadRequest(string message = "Bad request. Please check your request")
        {
            return new CustomException(400, message);
        }

        public static CustomException UnAuthorized(string message = "Unauthorized. Please log in")
        {
            return new CustomException(401, message);
        }


        // admin
        public static CustomException Forbidden(string message = "Forbidden. The user does not have access rights to the content")
        {
            return new CustomException(403, message);
        }

        public static CustomException InternalError(string message = "Internal server error")
        {
            return new CustomException(500, message);
        }
        // public static CustomException InvalidData(string message = "Invalid data")
        // {
        //     return new CustomException(412, message);
        // }
    }
}