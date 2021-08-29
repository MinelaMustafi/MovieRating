using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using MovieRating.DAL;
using MovieRating.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating.API.Authentication
{
    public class AccessHandler
    {
        protected UnitOfWork Unit;

        public AccessHandler(UnitOfWork unit)
        {
            Unit = unit;
        }

        public async Task<User> Check(string username, string password)
        {
            if (username == "token")
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(password).Payload.ToArray();
                if (TokenExpired(int.Parse(token.FirstOrDefault(c => c.Key == "exp").Value.ToString()))) return null;
                int id = int.Parse(token.FirstOrDefault(c => c.Key == "sub").Value.ToString());
                return (await Unit.Users.Get(id));
            }
            else
            {
                User user = (await Unit.Users.Get(u => u.Username == username)).FirstOrDefault();
                string pwd = username.HashWith(password);
                if (user == null || user.Password != pwd) user = null;
                return user;
            }
        }

        public string GetToken(User user, bool remember)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Startup.Configuration["SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("sub", user.Id.ToString()),
                    new Claim("username", user.Username),
                }),
                Expires = remember ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateJwtSecurityToken(tokenDescriptor));
        }


        public AuthenticationTicket CheckToken(string parameter, string scheme)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(parameter).Payload.ToArray();
            if (TokenExpired(int.Parse(token.FirstOrDefault(c => c.Key == "exp").Value.ToString()))) return null;
            var claims = new[]
            {
                new Claim("id", token.FirstOrDefault(c => c.Key == "sub").Value.ToString()),
                new Claim("username", token.FirstOrDefault(c=>c.Key=="username").Value.ToString())
            };
            CurrentUser.Id = int.Parse(claims[0].Value);
            CurrentUser.Username = claims[1].Value;
            ClaimsIdentity identity = new ClaimsIdentity(claims, scheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, scheme);
            return ticket;
        }

        public bool TokenExpired(int seconds)
        {
            DateTime valid = new DateTime(1970, 1, 1).AddSeconds(seconds);
            return (valid < DateTime.UtcNow);
        }
    }
}
