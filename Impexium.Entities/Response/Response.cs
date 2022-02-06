using System;
using System.Collections.Generic;
using System.Text;

namespace Impexium.Entities.Response
{
    public class Response
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public string Error { get; set; }
    }
}
