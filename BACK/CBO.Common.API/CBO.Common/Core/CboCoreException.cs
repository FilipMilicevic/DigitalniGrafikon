using CBO.Common.Errors;

namespace CBO.Common.Core
{
    public class CboCoreException : CboException
    {
        public CboOperationContext CboOperationContext
        {
            get; set;
        }

        public CboCoreException(CboOperationContext context, CboErrorSet errorSet, string? message = null)
           : base(errorSet, message)
        {
            CboOperationContext = context;
        }
    }
}
