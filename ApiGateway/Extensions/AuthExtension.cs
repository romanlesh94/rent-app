using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace ApiGateway.Extensions
{
    public static class AuthExtension
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration Configuration)
        {
            var authenticationProviderKey = "Bearer";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(authenticationProviderKey, options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["TokenValidationParameters:ValidIssuer"],

                    ValidAudience = Configuration["TokenValidationParameters:ValidAudience"],

                    ValidateLifetime = Convert.ToBoolean(Configuration["TokenValidationParameters:ValidateLifetime"]),

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenValidationParameters:IssuerSigningKey"])),

                    ValidateIssuerSigningKey = Convert.ToBoolean(Configuration["TokenValidationParameters:ValidateIssuerSigningKey"]),
                };
            });
        }
    }
}
