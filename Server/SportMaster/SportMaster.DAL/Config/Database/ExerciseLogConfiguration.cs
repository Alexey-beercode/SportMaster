using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Config.Database;

public class ExerciseLogConfiguration : IEntityTypeConfiguration<ExerciseLog>
{
    public void Configure(EntityTypeBuilder<ExerciseLog> builder)
    {
        builder.HasKey(el => el.Id);
        builder.Property(el => el.ExerciseType).IsRequired().HasMaxLength(200);
        builder.Property(el => el.Duration).IsRequired();
        builder.HasOne<User>().WithMany().HasForeignKey(el => el.UserId);
    }
}