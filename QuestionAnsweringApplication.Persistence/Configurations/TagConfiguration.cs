using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionAnsweringApplication.Domain;

namespace QuestionAnsweringApplication.Persistence.Configurations
{
    internal class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.Name).IsRequired();

            builder.HasMany(p => p.Questions)
                .WithOne(x => x.Tag)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Answers)
                .WithOne(x => x.Tag)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
