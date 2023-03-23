using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pweb.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //Creare claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.UserData, user.UserName));


            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Pweb:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Pweb:Issuer"],
                configuration["Pweb:Audiemce"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials : credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

 
}
