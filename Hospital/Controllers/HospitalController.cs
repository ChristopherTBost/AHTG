using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHTG.Hospital.Web.Controllers
{
    /// <summary>
    /// Single controller to handle CRUD ops.
    /// Added an "Index" to provide a list/start point.
    /// 
    /// Each API call is basically
    /// 1. Get the Entity from the Body (Framework Provided)
    /// 2. Call a static function in Logic which actually performs the work.
    /// 3. Check status and map to something the client code knows how to deal with.
    /// 4. If success SaveChanges
    /// 
    /// I tend to do data wrangling in the controller and business rule implementation in the Logic.
    /// IMHO this tends to facilitate better automated testing.
    /// 
    /// Personally been throwing around ideas dealing with errors -vs- successful API calls.
    /// But didn't try and implement for this exercise.
    /// 
    /// e.g.
    /// class Result {
    ///     public int Code {get;}
    ///     public string Message {get;}
    ///     public object ResultData {get;}
    /// }
    /// </summary>
    public class HospitalController : ControllerBase
    {
        /// <summary>
        /// reference to the repository provided by the constructor via injection
        /// </summary>
        ObjectModel.HospitalRepository hospitalRepository;

        /// <summary>
        /// "default" constructor requiring the <see cref="ObjectModel.HospitalRepository"/> service
        /// </summary>
        /// <param name="hospitalRepository">the repository serivce</param>
        /// <exception cref="ArgumentNullException"> if <param name="hospitalRepository"/> is null </exception>
        public HospitalController(ObjectModel.HospitalRepository hospitalRepository) : base()
        {
            this.hospitalRepository = hospitalRepository ?? throw new ArgumentNullException(nameof(hospitalRepository));
        }

        /// <summary>
        /// reads all Hospitals the users can read
        /// </summary>
        /// <returns>list of <see cref="ObjectModel.Entities.Hospital"/>s the user can read</returns>
        [HttpGet]
        public IEnumerable<ObjectModel.Entities.Hospital> Index() 
        {
            if (Logic.Hospital.TryReadAll(hospitalRepository, out var hospitals))
                return hospitals;

            return Enumerable.Empty<ObjectModel.Entities.Hospital>();
        }

        /// <summary>
        /// attempts to read a single Hospital by Id
        /// </summary>
        /// <param name="hospitalId">the primary key of the <see cref="ObjectModel.Entities.Hospital"/>.</param>
        /// <returns>the hospital with the matchin key</returns>
        [HttpGet]
        public ObjectModel.Entities.Hospital Read(int hospitalId)
        {
            if (Logic.Hospital.TryRead(hospitalRepository, hospitalId, out var hospital))
                return hospital;

            Response.StatusCode = 404;

            return new ObjectModel.Entities.Hospital();
        }

        /// <summary>
        /// creates a new Hospital entity and saves it to the data store.
        /// </summary>
        /// <param name="hospital">the new hospital</param>
        /// <returns>the hospital with default values applied</returns>
        /// <remarks>NOTE: the primary key is ignored, so subsequent calls utilizing the return value will create new objects</remarks>
        [HttpPost]
        public ObjectModel.Entities.Hospital Create([FromBody]ObjectModel.Entities.Hospital hospital)
        {
            var result = Logic.Hospital.TryCreate(hospitalRepository, hospital);

            if (result.Any(_ => _.Code == Logic.ResultCode.OPERATION_NOT_PERMITTED))
                Response.StatusCode = StatusCodes.Status403Forbidden;

            hospitalRepository.SaveChanges();

            return hospital;
        }

        /// <summary>
        /// the Hospital entity to update
        /// </summary>
        /// <param name="hospital">a hospital</param>
        /// <returns>the updated hospital</returns>
        [HttpPost]
        public ObjectModel.Entities.Hospital Update([FromBody]ObjectModel.Entities.Hospital hospital)
        {
            var result = Logic.Hospital.TryUpdate(hospitalRepository, hospital);

            if (result.Any(_ => _.Code == Logic.ResultCode.OPERATION_NOT_PERMITTED))
                Response.StatusCode = StatusCodes.Status403Forbidden;

            hospitalRepository.SaveChanges();

            return hospital;
        }

        /// <summary>
        /// deletes a Hospital entity from the store
        /// </summary>
        /// <param name="hospital">the hospital to be deleted</param>
        /// <returns>the deleted entity</returns>
        /// <remarks>this could easily be just the primary key</remarks>
        [HttpPost]
        public ObjectModel.Entities.Hospital Delete([FromBody]ObjectModel.Entities.Hospital hospital)
        {
            var result = Logic.Hospital.TryDelete(hospitalRepository, hospital.Id);

            if (result.Any(_ => _.Code == Logic.ResultCode.OPERATION_NOT_PERMITTED))
                Response.StatusCode = StatusCodes.Status403Forbidden;

            hospitalRepository.SaveChanges();

            return hospital;
        }
    }
}
