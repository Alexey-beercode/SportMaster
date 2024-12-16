using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Config.Database;

public class CustomGoalConfiguration : IEntityTypeConfiguration<CustomGoal>
{
    public void Configure(EntityTypeBuilder<CustomGoal> builder)
    {
        builder.HasKey(cg => cg.Id);
        builder.Property(cg => cg.GoalName).IsRequired().HasMaxLength(200);
        builder.HasOne<User>().WithMany().HasForeignKey(cg => cg.UserId);
    }
}