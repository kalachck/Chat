using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Newtonsoft.Json;

namespace AspNetChat.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
        }

        public static void AddNewtonsoftJson(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }
    }
}
