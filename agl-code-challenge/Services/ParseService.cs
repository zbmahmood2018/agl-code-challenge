using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using agl_code_challenge.Configuration;
using agl_code_challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace agl_code_challenge.Services
{
    /// <summary>
    /// Parse Service Implementation
    /// </summary>
    public class ParseService : IParser
    {
        private readonly IHttpClient _service;
        private readonly AppSettings _config;
        private readonly ILogger _logger;

        public ParseService(IHttpClient service, IOptions<AppSettings> config, ILogger<ParseService> logger)
        {
            _service = service;
            _config = config.Value;
            _logger = logger;
        }

        /// <summary>
        /// Parse Requested response into object
        /// </summary>
        /// <typeparam name="T">Parsable Object</typeparam>
        /// <returns>List of parsed object</returns>
        /// <exception cref="DataParseException">When either no or invalid content present</exception>
        /// <exception cref="HttpRequestException">When unable to retrieve data from Uri</exception>
        public async Task<List<T>> ParseData<T>()
        {
            var result = await _service.GetAsync(_config.UrlPath);
            try
            {
                if (string.IsNullOrEmpty(result.Content))
                {
                    throw new Exception("Empty content");
                }
                return JsonConvert.DeserializeObject<List<T>>(result.Content);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new DataParseException("Unable to Parse Data");
            }
        }
    }
}