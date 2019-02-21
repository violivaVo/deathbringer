using Microsoft.AspNetCore.Authentication;

namespace iCubed.Ragnarok.Api.Middlewares
{
    /// <summary>
    /// Options for Basic Authentication
    /// </summary>
    public class BasicAuthenticationOptions: AuthenticationSchemeOptions
    {
        /// <summary>
        /// Schema name
        /// </summary>
        public static string Scheme = "Basic";
    }
}
