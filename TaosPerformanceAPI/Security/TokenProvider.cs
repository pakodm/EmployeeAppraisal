using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TaosPerformanceAPI.DAL;
using TaosPerformanceAPI.Models;
using static TaosPerformanceAPI.Resources.Resources;
using static TaosPerformanceAPI.Security.SecurityConstants;


namespace TaosPerformanceAPI.Security
{
    public class TokenProvider
    {
        private readonly TaosPerformanceDB _taosDB;
        private readonly TokenProviderOptions _options;

        public TokenProvider(IRepositoryWrapper repositoryWrapper, IConfiguration configuration)
        {
            _options = new TokenProviderOptions(configuration);
            ThrowIfInvalidOptions(_options);
            _taosDB = repositoryWrapper.taosDB;
        }

        public async Task<(string AccessToken, int ExpiresIn)> TokenAuth(HttpContext context, Usuarios user)
        {
            if (!context.Request.Path.Equals("/api/auth", StringComparison.Ordinal))
            {
                context.Response.StatusCode = 400;
                throw new Exception(LOGIN_ERROR_URL);
            }
            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                throw new Exception(LOGIN_ERROR_METHOD);
            }
            var token = await GenerateToken(context, user);

            return token;
        }

        private async Task<(string AccessToken, int ExpiresIn)> GenerateToken(HttpContext context, Usuarios user )
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, await _options.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64),
                new Claim(Jwt035Claims.UserId, user.Id),
                new Claim(Jwt035Claims.UserName, user.NombreCompleto),
                new Claim(Jwt035Claims.UserRolId, user.RolUsuario.ToString()),
                new Claim(Jwt035Claims.CompanyId, user.EmpresaUsuario.ToString())
            };

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.signingCredentials
            );
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);

            return (encodedJWT, (int)_options.Expiration.TotalSeconds);
        }

        private void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (String.IsNullOrEmpty(options.Issuer)) throw new ArgumentNullException(nameof(TokenProviderOptions.Issuer));
            if (String.IsNullOrEmpty(options.Audience)) throw new ArgumentNullException(nameof(TokenProviderOptions.Audience));
            if (options.Expiration == TimeSpan.Zero) throw new ArgumentException("Expiration must be greater than 0");
            if (options.signingCredentials == null) throw new ArgumentNullException(nameof(TokenProviderOptions.signingCredentials));
            if (options.NonceGenerator == null) throw new ArgumentNullException(nameof(TokenProviderOptions.NonceGenerator));
        }

        public long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
    }
}
