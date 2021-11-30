using System;
using System.Collections.Generic;

namespace Inflow.Shared.Abstractions.Auth;

public class JsonWebToken
{
    public string AccessToken { get; set; }
    public long Expiry { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public IDictionary<string, IEnumerable<string>> Claims { get; set; }
}