using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace CBO.Common.Core
{
    public class CboCoreContext : IDisposable
    {
        public CboCoreContext(CboContext hostContext, CboContext? callerContext, [CallerMemberName] string callerName = "")
        {
            operation = callerName;

            HostContext = hostContext;
            CallerContext = callerContext;
        }

        private readonly string? operation;

        public string? Operation
        {
            get
            {
                return operation;
            }
        }

        public CboContext HostContext
        {
            get; set;
        }

        public CboContext? CallerContext
        {
            get; set;
        }

        [JsonIgnore]
        public string? AuditableUser
        {
            get
            {
                return CallerContext?.AuditableUser;
            }
        }

        [JsonIgnore]
        public string? AuditableAppName
        {
            get
            {
                if (!string.IsNullOrEmpty(CallerContext?.AuditableAppName))
                {
                    return $"{CallerContext!.AuditableAppName} through {HostContext.AuditableAppName}";
                }

                return HostContext.AuditableAppName;
            }
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }
}
