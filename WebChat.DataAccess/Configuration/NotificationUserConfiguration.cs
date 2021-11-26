using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Configuration
{
    public class NotificationUserConfiguration : IEntityTypeConfiguration<NotificationUser>
    {
        public void Configure(EntityTypeBuilder<NotificationUser> builder)
        {
            builder.ToTable("NotificationUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.Property(x => x.IsRead).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
            builder.HasOne(x => x.Notification).WithMany(y => y.NotificationUsers)
                .HasForeignKey(z => z.NotificationId);
        }
    }
}
