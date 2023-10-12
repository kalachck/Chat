using AspNetChat.Api.Extensions;
using AspNetChat.Api.Hubs;
using AspNetChat.Api.Middlewares;
using AspNetChat.Business.Extensions;
using AspNetChat.DataAccess.Extensions;

namespace AspNetChat.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDatabaseContext(builder.Configuration);
            builder.Services.AddRepositories();

            builder.Services.AddServices();
            builder.Services.AddMappers();

            builder.Services.AddValidators();
            builder.Services.AddNewtonsoftJson();
            builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            if (!builder.Environment.IsEnvironment("Testing"))
            {
                builder.Services.MigrateDatabase();
            }

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<ChatHub>("/chat");

            app.MapControllers();

            app.Run();
        }
    }
}