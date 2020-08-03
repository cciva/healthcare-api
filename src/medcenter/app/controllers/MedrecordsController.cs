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
    [Route("api/v{version:apiVersion}/[controller]")]    
    public class MedrecordsController : MedController
    {
        public MedrecordsController(IConfiguration conf)
            : base(conf)
        {

        }

        // only nurses and doctor are able to read patient's medical
        // record
        // for the assesment needs, this action only returns simple string
        // it could be easily modified to return complex object
        // in real-world scenario
        [HttpGet("{patientId}")]
        [Authorize("fetch:medrecords")]
        public async Task<IActionResult> Fetch(string patientId)
        {
            return await Task.FromResult(Ok("record"));
        }

        // only doctors are able to modify patients medical record
        // for the assesment needs, this action only returns boolean
        // it could be easily modified to return complex object
        // in real-world scenario
        [HttpPut("{recordId}")]
        [Authorize("modify:medrecords")]
        public async Task<IActionResult> Update(string recordId, [FromBody] MedicalRecord record)
        {
            return await Task.FromResult(Ok(true));
        }
    } 
}