namespace CBO.Common.API.CoreServiceClient
{
    public interface ICoreCboServiceClient
    {
        CboContext? CallerContext
        {
            get; set;
        }
    }
}
