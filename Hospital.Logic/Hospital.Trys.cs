using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Collections.Generic;

namespace AHTG.Hospital.Logic
{
    using OM = AHTG.Hospital.ObjectModel;

    public partial class Hospital
    {

        /*
         * the following operations perform basic CRUD checks (logic) for operations on business objects.
         * 
         * other operations would implement other bussiness operations and likely have side effects
         * and caution should be taken in which "type" of operations call the "other type" of operation to
         * avoid unintentional loops.
         */

        /// <summary>
        /// Reads all <seealso cref="OM.Entities.Hospital"/>s the user has access to
        /// </summary>
        /// <param name="repository">the repository to read from</param>
        /// <param name="hospitals">the enerable of <seealso cref="OM.Entities.Hospital"/>s</param>
        /// <returns>the <see cref="LogicResultCollection"/> of the operation</returns>
        public static LogicResultCollection TryReadAll(OM.HospitalRepository repository, out IEnumerable<OM.Entities.Hospital> hospitals)
        {
            hospitals = from h in repository.Hospitals.ToList()
                        where CanRead(h)
                        select h;

            return LogicResultCollection.Default;
        }

        /// <summary>
        /// Attempt to create <paramref name="hospital"  /> and add it to the <paramref name="repository"/>
        /// </summary>
        /// <param name="repository">the repository to add to</param>
        /// <param name="hospital">the <see cref="OM.Entities.Hospital"/> to add.</param>
        /// <returns>the <see cref="LogicResultCollection"/> of the operation</returns>
        public static LogicResultCollection TryCreate(OM.HospitalRepository repository, OM.Entities.Hospital hospital) 
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (hospital == null) throw new ArgumentNullException(nameof(hospital));

            if (!CanCreate(hospital))
                return new LogicResultCollection(new[] { new LogicResult(ResultType.Error,ResultCode.OPERATION_NOT_PERMITTED) });

            repository.Add(hospital);
            
            return LogicResultCollection.Default;
        }

        /// <summary>
        /// Attempt to read a single <paramref name="hospital"  />from the <paramref name="repository"/>
        /// </summary>
        /// <param name="repository">the repository to read from</param>
        /// <param name="hospitalKey">the primary key of the hospital to read</param>
        /// <param name="hospital">the <see cref="OM.Entities.Hospital"/> read.</param>
        /// <returns>the <see cref="LogicResultCollection"/> of the operation</returns>
        public static LogicResultCollection TryRead(OM.HospitalRepository repository, int hospitalKey,out OM.Entities.Hospital hospital) {
            
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            hospital = repository.Hospitals.FirstOrDefault(_ => _.Id == hospitalKey);

            if (hospital == null || !CanRead(hospital))
                return new LogicResultCollection(new[] { new LogicResult(ResultType.Error, ResultCode.OPERATION_NOT_PERMITTED) });


            return hospital == null ? new LogicResultCollection( new LogicResult(ResultType.Warning, ResultCode.OBJECT_NOT_FOUND) ) : LogicResultCollection.Default;
        }

        /// <summary>
        /// Attempt to update as single <paramref name="hospital"  /> already contained in the repository <paramref name="repository"/>
        /// </summary>
        /// <param name="repository">the repository update</param>
        /// <param name="hospital">the <see cref="OM.Entities.Hospital"/> to update.</param>
        /// <returns>the <see cref="LogicResultCollection"/> of the operation</returns>
        public static LogicResultCollection TryUpdate(OM.HospitalRepository repository, OM.Entities.Hospital hospital) {
            
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            
            if (hospital == null) throw new ArgumentNullException(nameof(hospital));

            if (!CanUpdate(hospital))
                return new LogicResultCollection(new[] { new LogicResult(ResultType.Error, ResultCode.OPERATION_NOT_PERMITTED) });

            var ctxEntity = repository.Hospitals.FirstOrDefault(_ => _.Id == hospital.Id);

            if (ctxEntity == null)
                return new LogicResultCollection(new[] { new LogicResult(ResultType.Error, ResultCode.OBJECT_NOT_FOUND) });

            ctxEntity.Title = hospital.Title;

            repository.Update(ctxEntity);

            return LogicResultCollection.Default;
        }

        /// <summary>
        /// Attempt to delete a single <paramref name="hospital"  /> from the <paramref name="repository"/>
        /// </summary>
        /// <param name="repository">the repository to delete from</param>
        /// <param name="hospital">the <see cref="OM.Entities.Hospital"/> to delete.</param>
        /// <returns>the <see cref="LogicResultCollection"/> of the operation</returns>
        public static LogicResultCollection TryDelete(OM.HospitalRepository repository, int hospitalKey) {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            if (!TryRead(repository, hospitalKey, out var hospital))
                return new LogicResultCollection(new LogicResult(ResultType.Warning, ResultCode.OBJECT_NOT_FOUND));

            if (!CanDelete(hospital))
                return new LogicResultCollection(new[] { new LogicResult(ResultType.Error, ResultCode.OPERATION_NOT_PERMITTED) });

            repository.Delete(hospital);

            return LogicResultCollection.Default;
        }
    }
}
