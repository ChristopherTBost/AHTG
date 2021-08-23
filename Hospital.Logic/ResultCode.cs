namespace AHTG.Hospital.Logic
{
    /// <summary>
    /// static list of application specific result codes associated with ResultTypes
    /// </summary>
    /// <remarks>
    /// using the +1 pattern makes reordering/reorganzing the codes easy.
    /// but persisting them is not safe
    /// </remarks>
    public static class ResultCode
    {
        /// <summary>
        /// No code
        /// </summary>
        public const int NONE = 0;
        /// <summary>
        /// Attempt to perform an invalid operation aka NotImplementedException
        /// </summary>
        public const int OPERATION_INVALID = NONE + 1;
        /// <summary>
        /// attempt to perform an operation that the user can't perform or the
        /// state of the object prevents it
        /// </summary>
        public const int OPERATION_NOT_PERMITTED = OPERATION_INVALID + 1;
        /// <summary>
        /// the object necessary for the operation could not be found e.g. ArgumentNullException
        /// </summary>
        public const int OBJECT_NOT_FOUND = OPERATION_NOT_PERMITTED + 1;
    }
}
