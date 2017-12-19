using System.Collections.Generic;
using System.Threading.Tasks;
using agl_code_challenge.Models;

namespace agl_code_challenge.Services
{
    /// <summary>
    /// Data Parser Interface
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Parse data generic method
        /// </summary>
        /// <typeparam name="T">Parsable object</typeparam>
        /// <returns>List of parsed object</returns>
        /// <exception cref="DataParseException">When either no or invalid content present</exception>
        Task<List<T>> ParseData<T>();
    }
}