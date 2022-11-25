using System.Net;

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
