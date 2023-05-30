using System.Runtime.CompilerServices;

namespace CBO.Common
{
    public class CboOperationContext : IDisposable
    {
        public CboOperationContext(CboContext hostContext, CboContext? callerContext, [CallerMemberName] string callerName = "")
        {
            HostContext = hostContext;
            CallerContext = callerContext;
            Operation = callerName;
        }

        public string? Operation
        {
            get; set;
        }

        public CboContext HostContext
        {
            get; set;
        }

        public CboContext? CallerContext
        {
            get; set;
        }

        public string? AuditableUser
        {
            get
            {
                return this.CallerContext?.AuditableUser;
            }
        }

        public string? AuditableAppName
        {
            get
            {
                if (!String.IsNullOrEmpty(this.CallerContext?.AuditableAppName))
                {
                    return $"{this.CallerContext!.AuditableAppName} through {this.HostContext.AuditableAppName}";
                }

                return this.HostContext.AuditableAppName;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
