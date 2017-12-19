using System;

namespace agl_code_challenge.Models
{
    public class DataParseException: Exception
    {
        public DataParseException(string message)
            : base(message)
        {
        }
    }
}
