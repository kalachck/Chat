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

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Surname)
                .IsRequired(false)
                .HasMaxLength(30);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasData
            (
                new User { Id = 1, Name = "Сергей", Surname = "Kalachev" },
                new User { Id = 2, Name = "Gleb", Surname = "Ponteleev" }
            );
        }
    }
}
