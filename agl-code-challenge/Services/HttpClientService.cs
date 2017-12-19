using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using agl_code_challenge.Models;
using Microsoft.Extensions.Logging;

namespace agl_code_challenge.Services
{
    /// <summary>
    /// Http Client service implementation
    /// </summary>
    public class HttpClientService : IHttpClient
    {
        private readonly ILogger _logger;

        public HttpClientService(ILogger<HttpClientService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get Async Request
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns>Http Response Message</returns>
        /// <exception cref="HttpRequestException">When unable to retrieve data from Uri</exception>
        public async Task<HttpApiResponse> GetAsync(string requestUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    var response = await client.GetAsync(requestUrl);

                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception($"{response.StatusCode}");

                    return new HttpApiResponse
                    {
                        StatusCode = response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new HttpRequestException("Unable to Retrieve data");
            }
        }
    }
}