﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace rent.Extensions
{
    public static class AuthExtension
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["TokenValidationParameters:ValidIssuer"],

                    ValidateIssuer = Convert.ToBoolean(Configuration["TokenValidationParameters:ValidateIssuer"]),

                    ValidateAudience = Convert.ToBoolean(Configuration["TokenValidationParameters:ValidateAudience"]),

                    ValidAudience = Configuration["TokenValidationParameters:ValidAudience"],

                    ValidateLifetime = Convert.ToBoolean(Configuration["TokenValidationParameters:ValidateLifetime"]),

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenValidationParameters:IssuerSigningKey"])),

                    ValidateIssuerSigningKey = Convert.ToBoolean(Configuration["TokenValidationParameters:ValidateIssuerSigningKey"]),
                };
            });
        }
    }
}
