using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace TaosPerformanceAPI.Security
{
    public class TokenProviderOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; }
        public SigningCredentials signingCredentials { get; set; }
        public Func<Task<string>> NonceGenerator { get; set; }

        public TokenProviderOptions(IConfiguration configuration)
        {
            Issuer = configuration["Tokens:Issuer"];
            Audience = configuration["Tokens:Audience"];
            Expiration = TimeSpan.FromMinutes(180);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:SecurityKey"]));
            signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            NonceGenerator = new Func<Task<string>>(() => Task.FromResult(Guid.NewGuid().ToString()));
        }
    }
}
