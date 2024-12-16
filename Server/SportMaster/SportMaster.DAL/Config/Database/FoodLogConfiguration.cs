using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Config.Database;

public class FoodLogConfiguration : IEntityTypeConfiguration<FoodLog>
{
    public void Configure(EntityTypeBuilder<FoodLog> builder)
    {
        builder.HasKey(fl => fl.Id);
        builder.Property(fl => fl.FoodName).IsRequired().HasMaxLength(200);
        builder.Property(fl => fl.Calories).IsRequired();
        builder.HasOne<User>().WithMany().HasForeignKey(fl => fl.UserId);
    }
}