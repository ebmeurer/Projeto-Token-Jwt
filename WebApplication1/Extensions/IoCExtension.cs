using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Services;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

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

        public static void ConnectDb(this IServiceCollection service, IConfiguration Configuration)
        {
            string stringDeConexao = Configuration.GetConnectionString("conexaoMySQL");

            service.AddDbContext<DataContext>(opt => opt.UseMySql(stringDeConexao, ServerVersion.AutoDetect(stringDeConexao)));
        }
    }


}
