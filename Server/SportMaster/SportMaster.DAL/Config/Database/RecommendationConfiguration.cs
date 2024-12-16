using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Config.Database;

public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
{
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.RecommendationText).IsRequired().HasMaxLength(1000);
        builder.HasOne<User>().WithMany().HasForeignKey(r => r.UserId);
    }
}