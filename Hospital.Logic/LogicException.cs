using System;
using System.Collections.Generic;
using System.Text;

namespace AHTG.Hospital.Logic
{
    public class LogicException : Core.Exception
    {
        public LogicException(string message) : base(message)
        {
        }

        public LogicException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
