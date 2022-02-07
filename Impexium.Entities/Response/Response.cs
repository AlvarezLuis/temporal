using System;
using System.Collections.Generic;
using System.Text;

namespace Impexium.Entities.Response
{
    public class Response
    {
        public Response()
        {
            Error = new List<string>();
        }
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public List<string> Error { get; set; }

        public static Response BuildResponse(int statusCode, object data = null)
        {
            return BuildResponse(statusCode, new List<string>(), data);
        }
        public static Response BuildResponse(int statusCode, List<string> errorMessages, object data = null)
        {
            return new Response()
            {
                StatusCode = statusCode,
                Data = data,
                Error = errorMessages
            };
        }
    }
}
