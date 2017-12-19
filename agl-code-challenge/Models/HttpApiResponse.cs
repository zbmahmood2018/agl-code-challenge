using System.Net;

namespace agl_code_challenge.Models
{
    public class HttpApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
    }
}