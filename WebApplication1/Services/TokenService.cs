using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Entities;
using WebApplication1.Data;

namespace WebApplication1.Services
{
    public class TokenService

    {
        private IConfiguration Configuration { get; }

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("ApiKey").Value);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);

        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claims.AddClaim(new Claim("Username", user.Name));

            return claims;

        }

        public bool ValidateLogin(User request, DataContext _dataContext) {
            List<User> users = _dataContext.user.ToList();

            foreach(User u in users){
                if (u.Name == request.Name && u.Email == request.Email && u.Password == request.Password){
                    return true;
                }
            }

            return false;
        }
    }
}
