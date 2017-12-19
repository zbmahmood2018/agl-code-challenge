using System.Net.Http;
using System.Threading.Tasks;
using agl_code_challenge.Models;

namespace agl_code_challenge.Services
{
    /// <summary>
    /// Http Client interface
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Get Async Request
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns>Http Response Message</returns>
        /// <exception cref="HttpRequestException">When unable to retrieve data from Uri</exception>
        Task<HttpApiResponse> GetAsync(string requestUrl);
    }
}