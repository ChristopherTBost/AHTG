using System;
using System.Collections.Generic;
using System.Text;

namespace AHTG.Hospital.Logic
{
    public enum ResultType
    {
        Ok,
        Info,
        Warning,
        Error,
        Critical
    }

    public static class ResultCode
    {
        public const int NONE = 0;
        public const int OPERATION_INVALID = NONE + 1;
        public const int OPERATION_NOT_PERMITTED = OPERATION_INVALID + 1;
        public const int OBJECT_NOT_FOUND = OPERATION_NOT_PERMITTED + 1;
    }

    public class LogicResult
    {
        const string EMPTY_INFO = "";

        public static readonly LogicResult Default = new LogicResult();

        public ResultType Type { get; private set; } = ResultType.Ok;

        public int Code { get; private set; } = ResultCode.NONE;

        public string Info { get; private set; }

        internal LogicResult(ResultType type = ResultType.Ok, int code = ResultCode.NONE, string info = EMPTY_INFO)
            : base()
        {
            const string b = "";
            if (type == ResultType.Ok && code != ResultCode.NONE)
                throw new LogicException($"A {nameof(ResultType)} of {nameof(ResultType.Ok)} must have a matching {nameof(Code)} of {nameof(ResultCode.NONE)}", new ArgumentException($"{nameof(type)} and {nameof(code)} mismatch."));

            this.Type = type;
            this.Code = code;
            this.Info = info;
        }
    }

    public class LogicResultCollection : List<LogicResult>
    {
        public static LogicResultCollection Default = new LogicResultCollection();

        public bool IsOk => this.TrueForAll(_ => _.Type == ResultType.Ok);

        public static implicit operator bool(LogicResultCollection logicResults) => logicResults.IsOk;

        internal LogicResultCollection()
            : base() { }

        internal LogicResultCollection(LogicResult result)
            : this(new LogicResult[] {result})
        {

        }
        internal LogicResultCollection(IEnumerable<LogicResult> results)
            : base(results) { }

        internal LogicResultCollection(LogicResultCollection logicResults)
            : base(logicResults) { }
    }
}
