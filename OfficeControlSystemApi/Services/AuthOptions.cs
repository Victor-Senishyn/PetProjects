using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OfficeControlSystemApi.Services
{
    public class AuthOptions
    {
        public string ISSUER { get; }
        public string AUDIENCE { get; }
        public string KEY { get; }

        public AuthOptions(IConfiguration configuration)
        {
            ISSUER = configuration["AuthOptions:ISSUER"]!;
            AUDIENCE = configuration["AuthOptions:AUDIENCE"]!;
            KEY = configuration["AuthOptions:KEY"]!;
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
