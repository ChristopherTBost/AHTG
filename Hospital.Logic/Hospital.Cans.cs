using System;
using System.Collections.Generic;
using System.Text;

namespace AHTG.Hospital.Logic
{
    using OM = AHTG.Hospital.ObjectModel;

    /// <summary>
    /// most business objects i work with tend to have numerous "Can" operations;
    /// therefore, separating them into thier own partial helps organize.
    /// </summary>
    static partial class Hospital
    {
        /*
         * The following are a collection of checks that could be security based or logic based.
         * 
         * To support the operation logic calling multple business rules it retuns LogicResultCollection
         * 
         * NOTE : Care must be taken at design time, to determine whether "Can" operations will call other
         *        Logic operations or whether Logic checks in include calling the "Can" opeations. Loops
         *        can hide whithout careful review
         */

        /// <summary>
        /// Checks  business rules for creating the <paramref name="hospital"/>.
        /// </summary>
        /// <param name="hospital">the hospital to check</param>
        /// <returns>true if the <paramref name="hospital"/> is readable by the current user</returns>
        public static LogicResultCollection CanCreate(OM.Entities.Hospital hospital) =>
            hospital != null ? LogicResultCollection.Default : new LogicResultCollection(new LogicResult(ResultType.Warning, ResultCode.OPERATION_NOT_PERMITTED));

        /// <summary>
        /// Checks  business rules for creating the <paramref name="hospital"/>.
        /// </summary>
        /// <param name="hospital">the hospital to check</param>
        /// <returns>true if the <paramref name="hospital"/> is creatable by the current user</returns>
        public static LogicResultCollection CanRead(OM.Entities.Hospital hospital) =>
            hospital != null ? LogicResultCollection.Default : new LogicResultCollection(new LogicResult(ResultType.Warning, ResultCode.OPERATION_NOT_PERMITTED));

        /// <summary>
        /// Checks business rules for updating the <paramref name="hospital"/>.
        /// </summary>
        /// <param name="hospital">the hospital to check</param>
        /// <returns>true if the <paramref name="hospital"/> is updateable by the current user</returns>
        public static LogicResultCollection CanUpdate(OM.Entities.Hospital hospital) =>
            hospital != null ? LogicResultCollection.Default : new LogicResultCollection(new LogicResult(ResultType.Warning, ResultCode.OPERATION_NOT_PERMITTED));

        /// <summary>
        /// Checks  business rules for deleting the <paramref name="hospital"/>.
        /// </summary>
        /// <param name="hospital">the hospital to check</param>
        /// <returns>true if the <paramref name="hospital"/> is deleable by the current user</returns>
        public static LogicResultCollection CanDelete(OM.Entities.Hospital hospital) =>
            hospital != null ? LogicResultCollection.Default : new LogicResultCollection(new LogicResult(ResultType.Warning, ResultCode.OPERATION_NOT_PERMITTED));

    }
}
