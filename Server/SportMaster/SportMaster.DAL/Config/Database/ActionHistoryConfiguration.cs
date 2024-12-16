using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Config.Database;

public class ActionHistoryConfiguration : IEntityTypeConfiguration<ActionHistory>
{
    public void Configure(EntityTypeBuilder<ActionHistory> builder)
    {
        builder.HasKey(ah => ah.Id);
        builder.Property(ah => ah.Description).HasMaxLength(500);
        builder.HasOne<User>().WithMany().HasForeignKey(ah => ah.UserId);
    }
}