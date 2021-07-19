using System;

namespace interview.Domain.Options
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }

        public TimeSpan Expires { get; set; }

        public string Issuer { get; set; }
    }
}