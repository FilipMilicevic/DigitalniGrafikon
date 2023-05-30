using CBO.Common.API.Controllers;

namespace CBO.Common.API.CoreServiceClient
{
    public abstract class CoreCboServiceClientBase : ICoreCboServiceClient
    {
        public CboContext? CallerContext
        {
            get; set;
        }

        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();

            if (this.CallerContext != null)
            {
                msg.Headers.Add(CboController.CallerContextHeader, this.CallerContext.ToBase64());
            }

            return Task.FromResult(msg);
        }
    }
}
