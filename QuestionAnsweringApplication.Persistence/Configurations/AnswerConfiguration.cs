using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionAnsweringApplication.Domain;

namespace QuestionAnsweringApplication.Persistence.Configurations
{
    internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.AnswerText).IsRequired();
            builder.Property(p => p.QuestionId).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt).IsRequired();

            builder.HasMany(p => p.Tags)
                .WithOne(x => x.Answer)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
