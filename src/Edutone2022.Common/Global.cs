using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Edutone2022.Common
{
    public static class Global
    {
        public static readonly SymmetricSecurityKey KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sOmerand0meString!"));

        public static readonly string SITE_ADMIN_ACCOUNT = "siteadmin";
        public static class Roles
        {
            public const string ADMIN = "admin";
            public const string USER = "user";
        }
    }
}
