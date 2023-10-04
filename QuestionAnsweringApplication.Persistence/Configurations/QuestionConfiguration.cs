using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionAnsweringApplication.Domain;

namespace QuestionAnsweringApplication.Persistence.Configurations
{
    internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.QuestionText).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt).IsRequired();

            builder.HasMany(p => p.Tags)
                .WithOne(x => x.Question)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
