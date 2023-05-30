namespace CBO.Common.Errors
{
    public class CboException : Exception
    {
        public CboErrorSet CboErrorSet
        {
            get; set;
        }

        public CboException(CboErrorSet errorSet, string? message = null)
            : base(message)
        {
            CboErrorSet = errorSet;
        }
    }
}
