using System.Reflection;
using AspNetChat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetChat.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<UserChat> UserChats { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
