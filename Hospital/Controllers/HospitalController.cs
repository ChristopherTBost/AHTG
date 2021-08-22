using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHTG.Hospital.Web.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class HospitalController : ControllerBase
    {
        ObjectModel.HospitalRepository hospitalRepository;
        public HospitalController(ObjectModel.HospitalRepository hospitalRepository) : base()
        {
            this.hospitalRepository = hospitalRepository;
        }

        [HttpGet]
        public IEnumerable<ObjectModel.Entities.Hospital> Get()
        {
            if (Logic.Hospital.TryReadAll(hospitalRepository, out var hospital))
                return hospital;

            return Enumerable.Empty<ObjectModel.Entities.Hospital>();
        }

        [HttpGet]
        public IEnumerable<ObjectModel.Entities.Hospital> Index() => Get();

        [HttpGet]
        public ObjectModel.Entities.Hospital Read(int hospitalId)
        {
            if (Logic.Hospital.TryRead(hospitalRepository, hospitalId, out var hospital))
                return hospital;

            return new ObjectModel.Entities.Hospital();
        }

        [HttpPost]
        public ObjectModel.Entities.Hospital Create(ObjectModel.Entities.Hospital hospital)
        {
            if (Logic.Hospital.TryCreate(hospitalRepository, hospital))
                return hospital;

            return new ObjectModel.Entities.Hospital();
        }

        [HttpPost]
        public ObjectModel.Entities.Hospital Update(ObjectModel.Entities.Hospital hospital)
        {
            if (Logic.Hospital.TryUpdate(hospitalRepository, hospital))
                return hospital;

            return new ObjectModel.Entities.Hospital();
        }

        [HttpPost]
        public ObjectModel.Entities.Hospital Delete(ObjectModel.Entities.Hospital hospital)
        {
            if (Logic.Hospital.TryDelete(hospitalRepository, hospital.Id))
                return hospital;

            return new ObjectModel.Entities.Hospital();
        }
    }
}
