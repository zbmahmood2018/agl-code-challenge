using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using agl_code_challenge.Models;

namespace agl_code_challenge.Services
{
    /// <summary>
    /// People Service Implementation
    /// </summary>
    public class PeopleService : IPeopleService
    {
        private readonly IParser _parser;

        public PeopleService(IParser parser)
        {
            _parser = parser;
        }

        /// <summary>
        /// Get List of Cats Grouped by their owner's gender
        /// </summary>
        /// <returns>Key value pair of owner gender and cats name list</returns>
        /// <exception cref="DataParseException">When either no or invalid content present</exception>
        /// <exception cref="HttpRequestException">When unable to retrieve data from Uri</exception>
        public async Task<Dictionary<string, List<string>>> GetCatListByOwnerGender()
        {
            var result = await _parser.ParseData<Person>();

            if (result != null && result.Any())
            {
                return result.GroupBy(p => p.Gender, p => p.Pets,
                        (key, pets) => new
                        {
                            Gender = key,
                            List = pets.Where(po => po != null)
                                .SelectMany(pt => pt)
                                .Where(t => t.Type.ToLower() == "cat")
                                .Select(c => c.Name)
                                .OrderBy(n => n)
                                .ToList()
                        })
                    .ToDictionary(x => x.Gender, x => x.List);
            }
            throw new DataParseException("No data found");
        }
    }
}