using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

using TaosPerformanceAPI.DAL;
using static TaosPerformanceAPI.Security.SecurityConstants;
using static TaosPerformanceAPI.Resources.Resources;

namespace TaosPerformanceAPI.Security
{
    public class SecurityMiddleware
    {
        private readonly TaosPerformanceDB _taosDB;
        private readonly TokenProvider _tokenProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SecurityMiddleware(IRepositoryWrapper repositoryWrapper, TokenProvider tokenProvider, IHttpContextAccessor httpContextAccessor)
        {
            _taosDB = repositoryWrapper.taosDB;
            _tokenProvider = tokenProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual string GetValueFromClaim(string claimName)
        {
            try
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    return _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == claimName).FirstOrDefault()?.Value;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public string GetJWT()
        {
            try
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    return _httpContextAccessor.HttpContext.GetTokenAsync("taosPM").Result;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public void ClearSessionCollection()
        {
            _session.Clear();
        }

        public async Task<object> Login(string userName, string pwd)
        {
            var user = _taosDB.GetUser(userName, pwd);
            if (user == null)
            {
                throw new Exception(LOGIN_ERROR_USER_NOT_FOUND);
            }
            else if (!user.ClaveUsuario.Equals(SHA256.EncodeString(SaltKey + SHA256.EncodeString(pwd))))
            {
                throw new Exception(LOGIN_ERROR_USER_NOT_FOUND);
            }
            else if (!user.Activo)
            {
                throw new Exception(LOGIN_ERROR_USER_NOT_ACTIVE);
            }
            if (String.IsNullOrEmpty(user.UltimoAcceso.ToString()))
            {
                user.UltimoAcceso = DateTime.UtcNow;
            }
            if (String.IsNullOrEmpty(user.NombreCompleto))
            {
                user.NombreCompleto = "Sin Nombre";
            }
            var (AccessToken, ExpiresIn) = await _tokenProvider.TokenAuth(_httpContextAccessor.HttpContext, user);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("taosPM", AccessToken);

            return new
            {
                Token = new { AccessToken, ExpiresIn},
                User = new { IdUsuario = user.Id, user.NombreCompleto, user.UltimoAcceso }
            };
        }
    }
}
