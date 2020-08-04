using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MedCenter.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class DonationsController : MedController
    {
        public DonationsController(IConfiguration conf)
            : base(conf)
        {

        }
        // this is public endpoint allowing everyone to inform
        // themselves about donating blood or procedures about
        // donating organs
        // for the assesment needs, this action only returns simple string
        // it could be easily modified to return complex object
        // in real-world scenario
        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            return await Task.FromResult(Ok("info"));
        }
        
        // this is public endpoing and everyone can apply for 
        // blood donation
        // for the assesment needs, this action only returns simple string
        // it could be easily modified to return complex object
        // in real-world scenario
        [HttpPost("apply/{type:donationType}")]
        public async Task<IActionResult> Apply(DonationType type,
                                               [FromBody] Person applier)
        {
            // if we haven't seen this patient yet, open 
            // new medical record for them and register
            // to the donation program upon which they will
            // receive application code as reference
            return await Task.FromResult(Ok("code"));
        }
    } 
}