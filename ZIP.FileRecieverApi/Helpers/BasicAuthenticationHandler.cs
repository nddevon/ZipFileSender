using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ZIP.FileRecieverApi.Helpers {
    public class BasicAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions> {
        private readonly IConfiguration _configuration;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration userService)
            : base(options, logger, encoder, clock) {
            _configuration = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                var userNameUserInfo = _configuration.GetSection("UserInfo").GetSection("UserName").Value;
                var passwordUserInfo = _configuration.GetSection("UserInfo").GetSection("Password").Value;

                if (!username.Equals(userNameUserInfo) && !password.Equals(passwordUserInfo))
                    return AuthenticateResult.Fail("Invalid Username or Password");

                var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userNameUserInfo),
                new Claim(ClaimTypes.Name, userNameUserInfo) };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }
}
