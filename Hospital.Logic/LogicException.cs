using System;
using System.Collections.Generic;
using System.Text;

namespace AHTG.Hospital.Logic
{
    /// <summary>
    /// Base exception class for exception behavior caught or initiated in the Logic code
    /// </summary>
    public class LogicException : System.Exception
    {
        public LogicException(string message) : base(message)
        {
        }

        public LogicException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
