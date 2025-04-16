#pragma warning disable CS8618

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Classes
{
    internal class AuthenticationResponse
    {
        public string platformType { get; set; }
        public string ticket { get; set; }
        public object twoFactorAuthenticationTicket { get; set; }
        public string profileId { get; set; }
        public string userId { get; set; }
        public string nameOnPlatform { get; set; }
        public string environment { get; set; }
        public DateTime expiration { get; set; }
        public string spaceId { get; set; }
        public string clientIp { get; set; }
        public string clientIpCountry { get; set; }
        public DateTime serverTime { get; set; }
        public string sessionId { get; set; }
        public string sessionKey { get; set; }
        public object rememberMeTicket { get; set; }
    }
}
