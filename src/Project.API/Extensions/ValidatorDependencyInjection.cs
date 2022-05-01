using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Project.API;

namespace Project.Api.Extensions
{
    public static class ValidatorDependencyInjection
    {
        /// <summary>
        /// Adds validators to dependency injection.
        /// Add and remove unnecessary parameters if needed.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidatorsToDependencyInjection(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            return services;
        }
    }
}
