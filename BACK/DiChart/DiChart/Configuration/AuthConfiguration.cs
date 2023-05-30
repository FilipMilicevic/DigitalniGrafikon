namespace CBO.DigitalChart.API.Configuration
{
    public class AuthConfiguration
    {
        public int AccessTokenLifetime
        {
            get; set;
        }

        public int RefreshTokenLifetime
        {
            get; set;
        }
    }
}
