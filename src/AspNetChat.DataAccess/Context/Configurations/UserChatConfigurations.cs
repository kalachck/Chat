using AspNetChat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetChat.DataAccess.Context.Configurations
{
    public class UserChatConfigurations : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ChatId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserChats)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Chat)
                .WithMany(x => x.UserChats)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData
            (
                new UserChat { Id = 1, ChatId = 1, UserId = 1 },
                new UserChat { Id = 2, ChatId = 1, UserId = 2 },
                new UserChat { Id = 3, ChatId = 2, UserId = 1 },
                new UserChat { Id = 4, ChatId = 2, UserId = 2 }
            );
        }
    }
}
