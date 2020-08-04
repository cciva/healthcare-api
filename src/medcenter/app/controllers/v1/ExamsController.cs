using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace MedCenter.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]    
    public class ExamsController : MedController
    {
        public ExamsController(IConfiguration conf)
            : base(conf)
        {

        }

        // only authenticated users can get information 
        // about *their own* medical exams exclusively
        [HttpGet("{patientId}")]
        [Authorize("read:exams")]
        public async Task<IActionResult> GetInfo(string patientId, 
                                                [FromQuery] DateTime from,
                                                [FromQuery] DateTime to)
        {
            Patient p;
            if(!Find<Patient>(patientId, out p))
                return await Task.FromResult(
                        NotFound(string.Format("patient {0} not found", patientId))
                );

            return await Task.FromResult(
                Ok(Enumerable.Empty<ExamInfo>())
            );
        }

        // only authenticated users can arrange medical exam
        [HttpPost("arrange")]
        [Authorize("arrange:exams")]
        public async Task<IActionResult> Arrange([FromBody] ExamRq rq)
        {
            Doctor d;
            if(!Find<Doctor>(rq.DoctorId, out d))
                return await Task.FromResult(
                        NotFound(string.Format("doctor {0} does not work at {1}", rq.DoctorId, rq.At))
                );

            // upon successfull examination schedule,
            // return new alphanumeric code as reference to the user (patient)
            return await Task.FromResult(
                Ok("exam1")
            );
        }

        // only operators can modify/reschedule exams
        // only authenticated users can arrange medical exam
        [HttpPost("reschedule/{code}")]
        [Authorize("arrange:exams")]
        public async Task<IActionResult> Reschedule(string code, [FromBody] ExamRq rq)
        {
            ExamInfo e;
            if(!Find<ExamInfo>(code, out e))
                return await Task.FromResult( 
                        NotFound(string.Format("exam {0} was never scheduled", code))
                );

            // upon successfull reschedule/update,
            // return new alphanumeric code as reference to the user (patient)
            return await Task.FromResult(Ok("exam1"));
        }

        // only operator in medical center can cancel exam
        [HttpDelete("cancel/{code}")]
        [Authorize("cancel:exams")]
        public async Task<IActionResult> Cancel(string code)
        {
            ExamInfo e;
            if(!Find<ExamInfo>(code, out e))
                return await Task.FromResult(
                        NotFound(string.Format("exam {0} was never scheduled", code))
                );

            return await Task.FromResult(Ok(true));
        }
        
        // this should be the storage layer code
        // later on in V2, when we decide to improve our API for instance
        // this will be replaced with mediator pattern 
        private bool Find<T>(string id, out T obj) where T : new()
        {
            obj = new T();
            return true;
        }
    } 
}