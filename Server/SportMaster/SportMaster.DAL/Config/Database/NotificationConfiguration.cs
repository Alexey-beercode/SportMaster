﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Config.Database;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Message).IsRequired().HasMaxLength(500);
        builder.HasOne<User>().WithMany().HasForeignKey(n => n.UserId);
    }
}