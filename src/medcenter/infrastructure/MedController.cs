using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net;
using NLog;

namespace MedCenter.V1
{
    public abstract class MedController : Controller
    {
        private readonly ILogger _logger = null;

        protected MedController(IConfiguration conf)
        {
            _logger = LogManager.GetLogger(this.GetType().Name);
            Config = conf;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            LogActivity(context.RouteData);
            base.OnActionExecuted(context);
        }

        // protected Task<IActionResult> Fail(HttpStatusCode code, string fmt, params object[] args)
        // {
        //     return Task.FromResult(
        //         new ContentResult
        //         {
        //             Content = string.Format(fmt, args),
        //             ContentType = "text/plain",
        //             StatusCode = (int)code
        //         }
        //     );
        //     //return Task.FromResult(new HttpResponseMessage(code, string.Format(fmt, args)));
        // }

        // protected Task<IActionResult> Success<T>(T data)
        // {
        //     return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Ok));
        // }

        private void LogActivity(RouteData route)
        {
            Logger.Info("[*] executed {0} -> {1}", 
                route.Values["controller"], 
                route.Values["action"]);
        }

        protected IConfiguration Config { get; }
        protected ILogger Logger 
        { 
            get { return _logger; }
        }
    }
}