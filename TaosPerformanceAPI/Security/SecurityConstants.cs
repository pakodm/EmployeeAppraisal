namespace TaosPerformanceAPI.Security
{
    public class SecurityConstants
    {

        private const string saltKey = "725ea2aa173ed23a241727eeaf04b5de5d3b6e5a72cf499f68c8c89cc31607db";

        public static string SaltKey => saltKey;

        public enum userRole
        {
            Admin = 1,
            Manager = 2,
            Basic = 3,
            Owner = 4
        }

        public struct Jwt035Claims
        {
            public const string UserId = "userId";
            public const string UserName = "userName";
            public const string UserRolId = "userRoleId";
            public const string CompanyId = "companyId";
        }
    }
}
