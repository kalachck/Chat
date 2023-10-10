using AspNetChat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetChat.DataAccess.Context.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasIndex(x => x.UserName)
                .IsUnique();

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Surname)
                .IsRequired(false)
                .HasMaxLength(30);

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
