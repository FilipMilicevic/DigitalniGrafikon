using CBO.Common;
using CBO.Core.Orders.Service;
using Microsoft.AspNetCore.Mvc;

namespace CBO.Core.Orders.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VehiclesController : OrdersManagementController
    {

       public VehiclesController
       (
           ILogger<VehiclesController> logger,
           IHttpContextAccessor httpContextAccessor,
           IOrdersManagementService service,
           CboContext hostContext
       )
       : base(logger, httpContextAccessor, service, hostContext)
        {

        }


        [HttpGet]
        [Route("[action]")]
        //[Authorize]
        public bool Vehicle()
        {
            return true;
        }
    }

