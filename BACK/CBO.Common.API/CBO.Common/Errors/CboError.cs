
namespace CBO.Common.Errors
{
    public class CboError
    {
        public ErrorCode ErrorCode
        {
            get; set;
        }

        public List<string> AdditionalInfo
        {
            get;
            set;
        }
        = new List<string>();

        public override string ToString()
        {
            if (AdditionalInfo.Count == 0)
            {
                return string.Format("Error: {0}", ErrorCode.ToString());
            }

            return string.Format("Error: {0}; {1}", ErrorCode.ToString(), string.Join(", ", AdditionalInfo));
        }
    }
}
