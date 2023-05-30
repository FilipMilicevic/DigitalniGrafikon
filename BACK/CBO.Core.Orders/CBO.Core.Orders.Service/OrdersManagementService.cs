using CBO.Common;
using Serilog;

namespace CBO.Core.Orders.Service
{
    public class OrdersManagementService : IOrdersManagementService
    {

        private readonly IOrdersManagementService _dataAccess;
        private readonly ILogger _logger;

        public OrdersManagementService(IOrdersManagementService dataAccess)
        {
            _dataAccess = dataAccess;
            _logger = Log.Logger.ForContext(typeof(OrdersManagementService));
        }

        public async Task<bool> GetOrder(CboOperationContext context)
        {
            return true;
        
        }
    }
}