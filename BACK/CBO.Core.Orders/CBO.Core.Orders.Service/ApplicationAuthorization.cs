using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBO.Core.Orders.Service
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ApplicationAuthorization : Attribute
    {
        public OperationCode FeatureCode
        {
            get; protected set;
        }

        public ApplicationAuthorization(OperationCode featureCode)
        {
            FeatureCode = featureCode;
        }
    }

}
