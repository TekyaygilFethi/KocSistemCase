using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Data.Web.Base
{
    public class ResponseObject<T>
    {
        public bool IsSuccess { get; set; } = true;

        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

        public string Message { get; set; } = String.Empty;

        public T Data { get; set; }
    }
}
