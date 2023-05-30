namespace CBO.Common.Errors
{
    public class CboFieldError : CboError
    {
        public string FieldName
        {
            get; set;
        }
        = null!;

        public object InvalidValue
        {
            get; set;
        }
        = null!;

        public override string ToString()
        {
            if (AdditionalInfo.Count == 0)
            {
                return string.Format("Field: {0} error: {1}, invalid value: {2}", FieldName, ErrorCode.ToString(), InvalidValue.ToString());
            }

            return string.Format("Field: {0} error: {1}, invalid value: {2}; {3}", FieldName, ErrorCode.ToString(), InvalidValue.ToString(), string.Join(", ", AdditionalInfo));
        }
    }
}
