using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Configuration
{
    public class ChatGroupConfiguration : IEntityTypeConfiguration<ChatGroup>
    {
        public void Configure(EntityTypeBuilder<ChatGroup> builder)
        {
            builder.ToTable("ChatGroups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Name).HasMaxLength(100);
        }
    }
}
