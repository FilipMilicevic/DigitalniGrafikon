using CBO.Common;
using CBO.Common.API.Controllers;
using CBO.Core.Orders.Service;

namespace CBO.Core.Orders.API.Controllers
{
    public class OrdersManagementController : CboController
    {
        protected readonly IOrdersManagementService service;

        public OrdersManagementController
        (
            ILogger<OrdersManagementController> logger,
            IHttpContextAccessor httpContextAccessor,
            IOrdersManagementService service,
            CboContext hostContext
        )
        : base(logger, httpContextAccessor, hostContext)
        {
            this.service = service;
        }
    }
}
