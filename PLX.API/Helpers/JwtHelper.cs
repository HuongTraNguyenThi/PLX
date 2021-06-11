using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PLX.API.Helpers
{
    public class JwtHelper
    {
        public static JwtSecurityToken ValidateToken(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken;
        }

        public static string GenerateToken(JwtConfig jwtConfig, IDictionary<string, object> userInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claimMap = new Dictionary<string, object>();
            claimMap.Add(JwtRegisteredClaimNames.Sub, jwtConfig.Subject);
            claimMap.Add(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claimMap.Add(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString());
            userInfo.ToList().ForEach(claim =>
            {
                claimMap.Add(claim.Key, claim.Value);
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = jwtConfig.Issuer,
                Audience = jwtConfig.Audience,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredentials,
                Claims = claimMap
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}