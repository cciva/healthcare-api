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
    public class AdminController : MedController
    {
        public AdminController(IConfiguration conf)
            : base(conf)
        {

        }

        // only administrators have access to
        // these api calls
        [HttpGet("status")]
        //[Authorize("admin:status")]
        public async Task<IActionResult> Status()
        {
            return await Task.FromResult(Ok(true));
        }
    } 
}