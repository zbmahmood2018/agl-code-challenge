using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using agl_code_challenge.Models;

namespace agl_code_challenge.Services
{
    /// <summary>
    /// People service interface
    /// </summary>
    public interface IPeopleService
    {
        /// <summary>
        /// Get List of Cats Grouped by their owner's gender
        /// </summary>
        /// <returns>Key value pair of owner gender and cats name list</returns>
        /// <exception cref="DataParseException">When either no or invalid content present</exception>
        /// <exception cref="HttpRequestException">When unable to retrieve data from Uri</exception>
        Task<Dictionary<string, List<string>>> GetCatListByOwnerGender();
    }
}