using CBO.Common;
using CBO.Core.Orders.DataAccess;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CBO.Core.Orders.Service
{
    public class AccountsCoreOperationContext : CBO.Common.API.CoreServiceServer.CoreOperationContext
    {
        public AccountsCoreOperationContext(CboOperationContext operationContext, IOrdersDataAccess dataAccess, [CallerMemberName] string callerName = "")
            : base(operationContext, callerName)
        {
            this.Context = operationContext;
            this.DataAccess = dataAccess;

            var featureMapping = (typeof(IOrdersManagementService)).GetMethod(callerName)?.GetCustomAttributes<ApplicationAuthorization>().FirstOrDefault();

            if (featureMapping == null || Context.CallerContext == null)
            {
                return;
            }

            var matchingRoles = DataAccess.GetMatchingRoles(featureMapping.FeatureCode.ToString(), Context.CallerContext!.Application);

            matchingRoles.Wait();

            if (matchingRoles.Result.Count == 0)
            {
                this.FatalError(ErrorCode.NotAuthorizedToUseFeature, featureMapping.FeatureCode.ToString());
            }
        }

        public IOrdersDataAccess DataAccess
        {
            get; set;
        }
    }
}
