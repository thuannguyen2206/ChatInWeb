using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Configuration
{
    public class ChatLogConfiguration : IEntityTypeConfiguration<ChatLog>
    {
        public void Configure(EntityTypeBuilder<ChatLog> builder)
        {
            builder.ToTable("ChatLogs");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ChatGroup).WithMany(y => y.ChatLogs).HasForeignKey(z => z.ChatGroupId);
            builder.HasOne(x => x.User).WithMany(x => x.ChatLogs).HasForeignKey(x => x.SenderId);

            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.Property(x => x.AttachedFiles).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CallId).IsRequired(false);
            builder.Property(x => x.Content).IsRequired(false);
            builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
        }
    }
}
