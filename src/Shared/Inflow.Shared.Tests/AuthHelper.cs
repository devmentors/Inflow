using System;
using System.Collections.Generic;
using Inflow.Shared.Infrastructure.Auth;
using Inflow.Shared.Infrastructure.Time;

namespace Inflow.Shared.Tests
{
    public static class AuthHelper
    {
        private static readonly AuthManager AuthManager;

        static AuthHelper()
        {
            var options = OptionsHelper.GetOptions<AuthOptions>("auth");
            AuthManager = new AuthManager(options, new UtcClock());
        }

        public static string GenerateJwt(Guid userId, string role = null, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null)
            => AuthManager.CreateToken(userId, role, audience, claims).AccessToken;
    }
}