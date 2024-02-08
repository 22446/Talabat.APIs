using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class TokentServices : ITokenServices
    {
        private readonly IConfiguration configuration;

        public TokentServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> GenerateToken(AppUser appUser)
        {

            var AuthClaim = new List<Claim>()
            {
             new Claim(ClaimTypes.GivenName,appUser.DisplayName),
             new Claim(ClaimTypes.Email,appUser.Email)
            };
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:KEY"]));
            var Token = new JwtSecurityToken(
                issuer: configuration["JWT:Assuer"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:ExpireDate"])),
                claims:AuthClaim,
                signingCredentials: new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
