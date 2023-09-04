using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Services;

namespace WebApplication1.Extensions
{
    public static class IoCExtension
    {
        public static void AddIoC(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("ApiKey").Value)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });

            service.AddTransient<TokenService>();
        }
    }
}
