using BaseProject.Application.Interfaces.Services.Tokens;
using BaseProject.Domain.Models;
using BaseProject.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BaseProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));
            services.AddScoped<ITokenService, TokenService>();

            var tokenSettings = configuration.GetSection("TokenSettings").Get<TokenSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
           {
               opt.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = tokenSettings.ValidateIssuer,
                   ValidateAudience = tokenSettings.ValidateAudience,
                   ValidateLifetime = tokenSettings.ValidateLifetime,
                   ValidIssuer = tokenSettings.ValidIssuer,
                   ValidAudience = tokenSettings.ValidAudience,
                   ValidateIssuerSigningKey = tokenSettings.ValidateIssuerSigningKey,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey)),
                   ClockSkew = TimeSpan.Zero
               };
           });
        }
    }
}
