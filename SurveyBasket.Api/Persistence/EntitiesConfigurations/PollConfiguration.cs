
namespace SurveyBasket.Api.Persistence.EntitiesConfigurations;

public class PollConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.HasIndex(x => x.Title).IsUnique();
        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Summary).HasMaxLength(1500);
        builder.Property(x => x.StartsAt).IsRequired();
        builder.Property(x => x.EndsAt).IsRequired();
        builder.Property(x => x.isPublished).IsRequired();
    }
}
