using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Master.Entity;
using Master.Entity.Dto.Infra;

namespace Master.Controller.Helper
{
    public class HelperJwtComposer
    {
        [NonAction]
        public string ComposeTokenForSession(DtoAuthenticatedUser user)
        {
            const string sUser = "user";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(LocalNetwork.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(sUser, JsonSerializer.Serialize(user)),
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = LocalNetwork.Issuer,
                Audience = LocalNetwork.Audience,
                SigningCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(key),
                                        SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}