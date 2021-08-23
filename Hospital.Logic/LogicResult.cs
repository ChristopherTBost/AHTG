using System;

namespace AHTG.Hospital.Logic
{
    /// <summary>
    /// Represents a result of performing a business logic operation
    /// </summary>
    public class LogicResult
    {
        /// <summary>
        /// for default parameters, i loath literal strings
        /// </summary>
        const string EMPTY_INFO = "";

        /// <summary>
        /// default LogicResults is Ok with no Code or Info
        /// </summary>
        public static readonly LogicResult Default = new LogicResult();

        /// <summary>
        /// the Type of the LogicResult
        /// </summary>
        public ResultType Type { get; private set; } = ResultType.Ok;

        /// <summary>
        /// a Type specific code assocatied with <see cref="Type"/>
        /// </summary>
        public int Code { get; private set; } = ResultCode.NONE;

        /// <summary>
        /// extra info associated with the result
        /// </summary>
        public string Info { get; private set; }

        internal LogicResult(ResultType type = ResultType.Ok, int code = ResultCode.NONE, string info = EMPTY_INFO)
            : base()
        {
            if (type == ResultType.Ok && code != ResultCode.NONE)
                throw new LogicException($"A {nameof(ResultType)} of {nameof(ResultType.Ok)} must have a matching {nameof(Code)} of {nameof(ResultCode.NONE)}", new ArgumentException($"{nameof(type)} and {nameof(code)} mismatch."));

            this.Type = type;
            this.Code = code;
            this.Info = info;
        }
    }
}
