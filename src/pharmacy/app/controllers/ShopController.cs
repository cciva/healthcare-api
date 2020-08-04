using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts;

namespace Pharmacy
{
    [Route("[controller]")]
    public class ShopController : Controller
    {
        // purchase needed medicaments only with 
        // therapy id
        [HttpPost("buy/{therapyId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Buy([FromQuery] string therapyId)
        {
            return await Task.FromResult(Ok());
        }

        [HttpPost("buy")]
        [AllowAnonymous]
        public async Task<IActionResult> Buy([FromBody] ShopRq rq)
        {
            return await Task.FromResult(Ok());
        }
    }
}

