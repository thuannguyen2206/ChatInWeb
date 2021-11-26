using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.FirstName).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.IsAdmin).HasDefaultValue(false);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.IsLocked).HasDefaultValue(false);
            builder.Property(x => x.AvatarLink).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Status).HasDefaultValue(true);
            builder.Property(x => x.DateCreate).HasDefaultValue(DateTime.Now);
        }
    }
}
