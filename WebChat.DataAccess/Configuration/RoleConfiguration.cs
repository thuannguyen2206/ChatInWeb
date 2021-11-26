using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Status).HasDefaultValue(true);
        }
    }
}
