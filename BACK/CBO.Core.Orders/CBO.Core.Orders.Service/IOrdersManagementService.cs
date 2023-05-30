using CBO.Common;

namespace CBO.Core.Orders.Service
{
    public interface IOrdersManagementService
    {

        Task<bool> GetOrder(CboOperationContext context);
    }
}
