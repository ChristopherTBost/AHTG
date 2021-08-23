using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHTG.Hospital.Web.Controllers
{
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
        /// <returns></returns>
        [HttpGet]
        public ObjectModel.Entities.Hospital Read(int hospitalId)
        {
            if (Logic.Hospital.TryRead(hospitalRepository, hospitalId, out var hospital))
                return hospital;

            Response.StatusCode = 404;

            return new ObjectModel.Entities.Hospital();
        }

        [HttpPost]
        public ObjectModel.Entities.Hospital Create([FromBody]ObjectModel.Entities.Hospital hospital)
        {
            var result = Logic.Hospital.TryCreate(hospitalRepository, hospital);

            if (result.Any(_ => _.Code == Logic.ResultCode.OPERATION_NOT_PERMITTED))
                Response.StatusCode = StatusCodes.Status403Forbidden;

            hospitalRepository.SaveChanges();

            return hospital;
        }

        [HttpPost]
        public ObjectModel.Entities.Hospital Update([FromBody]ObjectModel.Entities.Hospital hospital)
        {
            var result = Logic.Hospital.TryUpdate(hospitalRepository, hospital);

            if (result.Any(_ => _.Code == Logic.ResultCode.OPERATION_NOT_PERMITTED))
                Response.StatusCode = StatusCodes.Status403Forbidden;

            hospitalRepository.SaveChanges();

            return hospital;
        }

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
