using CBO.Common;
using CBO.Core.Orders.Service;
using Microsoft.AspNetCore.Mvc;

namespace CBO.Core.Orders.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : OrdersManagementController
{

    public OrdersController
    (
    ILogger<OrdersController> logger,
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
    public async Task<bool> Order()
    {
        //using var context = new CboOperationContext(this.hostContext, this.CallerContext);

        //return await service.GetOrder(context);

        return true;
    }
}

