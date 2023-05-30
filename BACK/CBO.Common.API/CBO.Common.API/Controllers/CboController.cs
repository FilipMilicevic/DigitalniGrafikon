using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CBO.Common.API.Controllers
{
    public class CboController : ControllerBase
    {
        public const string UserNameHeader = "X-User";
        public const string AppNameHeader = "X-App";
        public const string CallerContextHeader = "X-CallerContext";

        protected readonly ILogger<CboController> logger;
        protected readonly IHttpContextAccessor httpContextAccessor;

        protected CboController(ILogger<CboController> logger,
                IHttpContextAccessor httpContextAccessor,
                CboContext hostContext)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
            this.hostContext = hostContext;
        }

        protected CboContext hostContext;

        /// <summary>
        /// OAuth client credentials flow used to identify the calling module
        /// </summary>
        protected string? ClientId
        {
            get
            {
                var x = this.ControllerContext?.HttpContext?.User?.Claims.ToList();

                var clientId = this.ControllerContext?.HttpContext?.User?.Claims?
                    .FirstOrDefault(c => c.Type == "sub")?.Value;

                return clientId;
            }
        }

        protected CboContext? CallerContext
        {
            get
            {
                CboContext? result = null;

                string? headerValue = this.HttpContext?.Request?.Headers[CallerContextHeader];
                if (headerValue != null)
                {
                    result = CboContext.FromBase64(headerValue);
                }

                return result;
            }
        }
    }
}
