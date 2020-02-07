using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TheFleet.Services.Domain;
using TheFleet.Services.Validators;

namespace TheFleet.Services.Dependencies
{
    public static class AppDependencyResolver
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            
        }

        public static void ResolveValidatorsDependencies(this IServiceCollection services)
        {
            services.AddTransient<IValidator<User>, UserValidator>();
        }
    }
}
