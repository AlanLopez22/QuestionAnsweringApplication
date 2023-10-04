using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionAnsweringApplication.Domain;

namespace QuestionAnsweringApplication.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(256);
            builder.HasIndex(h => h.UserName);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(256);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);

            builder.HasMany(p => p.Questions)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Answers)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
