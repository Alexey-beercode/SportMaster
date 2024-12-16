using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Config.Database;

public class PersonalDataConfiguration : IEntityTypeConfiguration<PersonalData>
{
    public void Configure(EntityTypeBuilder<PersonalData> builder)
    {
        builder.HasKey(pd => pd.Id);
        builder.Property(pd => pd.FieldName).IsRequired().HasMaxLength(100);
        builder.HasOne<User>().WithMany().HasForeignKey(pd => pd.UserId);
    }
}