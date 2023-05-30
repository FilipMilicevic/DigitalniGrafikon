
using Newtonsoft.Json;

namespace CBO.Common.Errors
{
    public class CboErrorSet
    {
        public CboErrorSet()
        {
        }

        public List<CboError> Errors
        {
            get; set;
        }
        = new List<CboError>();

        [JsonIgnore]
        public bool HasErrors
        {
            get
            {
                return this.Errors.Count > 0;
            }
        }

        public void ThrowOnError()
        {
            if (this.Errors.Count > 0)
            {

            }
        }

        public void AddError(ErrorCode errorCode, params object[] additionalInfo)
        {
            this.Errors.Add
            (
                 new CboError
                 {
                     ErrorCode = errorCode,
                     AdditionalInfo = Convert(additionalInfo)
                 }
            );
        }

        public void AddFieldError(string fieldName, object fieldValue, ErrorCode errorCode, params object[] additionalInfo)
        {
            this.Errors.Add
            (
                 new CboFieldError
                 {
                     ErrorCode = errorCode,
                     FieldName = fieldName,
                     InvalidValue = fieldValue,
                     AdditionalInfo = Convert(additionalInfo)
                 }
            );
        }

        public static List<string> Convert(object[] additionalInfo)
        {
            List<string> info = new List<string>();

            if (additionalInfo != null && additionalInfo.Length > 0)
            {
                for (int i = 0; i < additionalInfo.Length; i++)
                {
                    object additional = additionalInfo[i];

                    info.Add(Convert(additional));
                }
            }

            return info;
        }

        public static string Convert(object something)
        {
            if (something == null)
            {
                return string.Empty;
            }
            else if (something is string @string)
            {
                return @string;
            }
            else if (something is decimal @decimal)
            {
                return $"{@decimal:0.00}$";
            }
            else
            {
                return something.ToString()!;
            }
        }
    }
}
