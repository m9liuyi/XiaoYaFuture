using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceNet.Common
{
    public static class JWTTokenHelper
    {
        public static string Generate(string userName)
        {
            var secret = ConfigurationManager.AppSettings["JWTSecret"];
            var expireMinutes = ConfigurationManager.AppSettings["JWTExpireMinutes"];

            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // or use JwtValidator.UnixEpoch
            var secondsSinceEpoch = Math.Round((now.AddMinutes(int.Parse(expireMinutes)) - unixEpoch).TotalSeconds);
            var payload = new Dictionary<string, object>
                {
                    {"user", userName},
                    {"exp", secondsSinceEpoch}
                };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            return token;
        }

        public static IDictionary<string, object> Decode(string token)
        {
            var secret = ConfigurationManager.AppSettings["JWTSecret"];
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            var jsonObj = decoder.DecodeToObject<IDictionary<string, object>>(token, secret, verify: true);
            return jsonObj;
        }
    }
}
