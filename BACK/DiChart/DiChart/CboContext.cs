using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using Enums = CBO.DigitalChart.API.Models.Enums;


namespace CBO.DigitalChart.API
{
    public class CboContext
    {
        [JsonConstructor]
        public CboContext(Enums.Host host, string? application = null, string? version = null)
        {
            Host = host;

            if (application != null)
            {
                _appName = application;
            }

            if (version != null)
            {
                _version = version;
            }

            if (_appName == null || _version == null)
            {
                try
                {
                    var assemblyName = Assembly.GetEntryAssembly()?.GetName();

                    _autoName = assemblyName?.Name;
                    _version = assemblyName?.Version?.ToString(3);
                }
                catch
                {
                    ; // ignore intentionally
                }
            }
        }

        public CboContext(CboContext other)
            : this(other.Host, other._appName, other._version)
        {
        }

        public Enums.Host Host
        {
            get;
        }

        private readonly string? _appName;
        private readonly string? _autoName;

        public string Application
        {
            get
            {
                return _appName ?? _autoName ?? "Unknown";
            }
        }

        private readonly string? _version;

        public string? Version
        {
            get
            {
                return _version;
            }
        }

        public string? AuditableAppName
        {
            get
            {
                if (_version != null)
                {
                    return $"{Host} {Application} v{_version}";
                }

                return $"{Host} {Application}";
            }
        }

        public string? AuditableUser
        {
            get; set;
        }

        #region serialization

        public string ToBase64()
        {
            return Convert.ToBase64String
            (
                Encoding.UTF8.GetBytes
                (
                    JsonConvert.SerializeObject(this, _jsonSetting)
                )
            );
        }

        public static CboContext? FromBase64(string base64String)
        {
            return JsonConvert.DeserializeObject<CboContext>
            (
                Encoding.UTF8.GetString
                (
                    Convert.FromBase64String(base64String)
                ),
                _jsonSetting
            );
        }

        private static readonly JsonSerializerSettings _jsonSetting = new() { TypeNameHandling = TypeNameHandling.All };

        #endregion serialization
    }
}
