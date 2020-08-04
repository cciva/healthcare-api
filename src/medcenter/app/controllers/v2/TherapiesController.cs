using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http;


// Version 2 of medcenter API brings some interesting
// concepts - now we are using MediatR library to
// coordinate all tasks and workload between
// components. There is also a command/query pattern (a form of CQRS)
// in order to separate responsibilities (read and write operations)
namespace MedCenter.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/[controller]")]    
    public class TherapiesController : MedController
    {
        IMediator _coordinator = null;

        public TherapiesController(IConfiguration conf, IMediator mediator)
            : base(conf)
        {
            _coordinator = mediator;
        }

        // patients (*their own* therapy data exclusively), 
        // doctors and users can get information about therapies
        [HttpGet("{patientId}")]
        [Authorize("read:therapy")]
        public async Task<IActionResult> GetInfo(string patientId, 
                                                [FromQuery] string doctorId,
                                                [FromQuery] DateTime from,
                                                [FromQuery] DateTime to)
        {
            TherapiesQuery query = new TherapiesQuery()
            {
                PatientId = patientId,
                DoctorId = doctorId
            };

            IEnumerable<Therapy> data = await Coordinator.Send(query);
            if(data == null || data.Count() == 0)
            {
                return BadRequest(string.Format("No therapies found with for patient {0} treated by doctor {1}", patientId, doctorId));
            }

            return await Task.FromResult(Ok(data));
        }

        // only doctors can create patient's therapy
        [HttpPost]
        [Authorize("create:therapy")]
        public async Task<IActionResult> CreateTherapy([FromBody] Therapy data)
        {
            // to make things simple, we will reuse request object
            // and pass it around the pipeline. Alternatively, there
            // should be separately defined request and response model
            // (so caller can use them) and internal model only used inside api 
            CreateTherapyCommand cmd = new CreateTherapyCommand
            {
                Therapy = data
            };

            Therapy therapy = await Coordinator.Send(cmd);
            if(therapy == null)
                return await Task.FromResult(StatusCode(StatusCodes.Status500InternalServerError));

            return await Task.FromResult(Ok(therapy));
        }

        protected IMediator Coordinator
        {
            get { return _coordinator; }
        }
    } 
}