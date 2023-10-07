using AspNetChat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetChat.DataAccess.Context.Configurations
{
    public class ChatConfigurations : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ChatName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Chats)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData
            (
                new Chat { Id = 1, ChatName = "Chat1", CreatedByUserId = 1 },
                new Chat { Id = 2, ChatName = "Chat2", CreatedByUserId = 2 }
            );
        }
    }
}
