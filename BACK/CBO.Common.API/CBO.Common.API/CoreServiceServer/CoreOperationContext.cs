using CBO.Common.Core;
using CBO.Common.Errors;
using System.Runtime.CompilerServices;

namespace CBO.Common.API.CoreServiceServer
{
    public class CoreOperationContext : IDisposable
    {
        public CoreOperationContext(CboOperationContext operationContext, [CallerMemberName] string callerName = "")
        {
            this.Context = operationContext;
            this.AuditableMethod = callerName;
            this.CboErrorSet = new CboErrorSet();
        }

        public CboOperationContext Context
        {
            get; set;
        }

        public string? AuditableMethod
        {

            get; set;
        }

        public string? AuditableUser
        {
            get
            {
                return this.Context?.AuditableUser;
            }
        }

        public string? AuditableAppName
        {
            get
            {
                return this.Context?.AuditableAppName;
            }
        }

        #region error handling

        protected CboErrorSet CboErrorSet
        {
            get; set;
        }

        public void Error(ErrorCode errorCode, params object[] additionalInfo)
        {
            this.CboErrorSet.AddError(errorCode, additionalInfo);
        }

        public void Error(object fieldValue, ErrorCode errorCode, [CallerArgumentExpression("fieldValue")] string fieldArgumentName = "Uknown", params object[] additionalInfo)
        {
            string fieldName = fieldArgumentName.Split('.').Last();
            this.CboErrorSet.AddFieldError(fieldName, fieldValue, errorCode, additionalInfo);
        }

        public void FatalError(ErrorCode errorCode, params object[] additionalInfo)
        {
            this.CboErrorSet.AddError(errorCode, additionalInfo);
            this.ThrowCboCoreException();
        }

        public void FatalError(object fieldValue, ErrorCode errorCode, [CallerArgumentExpression("fieldValue")] string fieldArgumentName = "Uknown", params object[] additionalInfo)
        {
            string fieldName = fieldArgumentName.Split('.').Last();
            this.CboErrorSet.AddFieldError(fieldName, fieldValue, errorCode, additionalInfo);
            this.ThrowCboCoreException();
        }

        public bool HasErrors
        {
            get
            {
                return this.CboErrorSet.HasErrors;
            }
        }

        public void AbortIfThereAreErrors()
        {
            if (HasErrors)
            {
                ThrowCboCoreException();
            }
        }

        private void ThrowCboCoreException()
        {
            throw new CboCoreException(this.Context, this.CboErrorSet, string.Format("An error occured in operation {0}", AuditableMethod));
        }

        #endregion error handling

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }
}
