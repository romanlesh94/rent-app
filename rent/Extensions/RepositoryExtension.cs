﻿using Microsoft.Extensions.DependencyInjection;
using PersonApi.Repository;

namespace PersonApi.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
