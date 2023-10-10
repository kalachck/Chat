using System.Reflection;
using AspNetChat.Business.Services;
using AspNetChat.Business.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetChat.Business.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
