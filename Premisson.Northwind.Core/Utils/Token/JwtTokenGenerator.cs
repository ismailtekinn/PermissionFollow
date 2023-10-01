using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using Premisson.Northwind.Entities.Concreate;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Premisson.Northwind.Core.Utils.Token
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.Now.AddDays(int.Parse(_configuration["JwtExpireDays"]));
            Claim[] claims = new Claim[]
           {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
           };
            var token = new JwtSecurityToken(_configuration["JwtIssuer"], _configuration["JwtAudience"], claims, null, expire, cred);
            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;
        }
    }
}
