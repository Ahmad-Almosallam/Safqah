using System;

namespace Safqah.Auth.Models
{
    public sealed class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
