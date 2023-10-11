using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace AspNetChat.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
        }
    }
}
