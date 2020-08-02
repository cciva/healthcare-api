using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using NLog;

namespace MedCenter.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin")]
    public class AdminController : MedController
    {
        public AdminController(IConfiguration conf)
            : base(conf)
        {

        }

        // only administrators have access to
        // these api calls
        [HttpGet("status")]
        //[Authorize("admin:all")]
        public async Task<IActionResult> GetStatus()
        {
            return await Task.FromResult(Ok(true));
        }
    } 
}