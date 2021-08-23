namespace AHTG.Hospital.Logic
{
    /// <summary>
    /// represents the list of results in increasing order of severity
    /// </summary>
    public enum ResultType
    {
        Ok = 0,
        Info = Ok + 1,
        Warning = Info +1,
        Error = Warning+1,
        Critical = Error + 1
    }
}
