using AspNetChat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetChat.DataAccess.Context.Configurations
{
    public class MessageConfigurations : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.ChatId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Chat)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData
            (
                new Message { Id = 1, Content = "Hello from User1 in Chat1", UserId = 1, ChatId = 1 },
                new Message { Id = 2, Content = "Hello from User2 in Chat2", UserId = 2, ChatId = 2}
            );
        }
    }
}
