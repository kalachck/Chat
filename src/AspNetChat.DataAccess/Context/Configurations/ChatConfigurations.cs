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

            builder.HasIndex(x => x.ChatName)
                .IsUnique();

            builder.Property(x => x.ChatName)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Chats)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
