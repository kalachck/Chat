using AspNetChat.DataAccess.Context;
using AspNetChat.DataAccess.Repositories.Abstract;
using AspNetChat.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetChat.DataAccess.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        public static void AddDataAccessDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserChatRepository, UserChatRepository>();
        }

        public static void MigrateDatabase(this IServiceCollection services)
        {
            services.BuildServiceProvider().CreateScope().ServiceProvider
                .GetRequiredService<DatabaseContext>().Database.Migrate();
        }
    }
}
