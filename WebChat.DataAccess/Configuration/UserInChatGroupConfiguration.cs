using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Configuration
{
    public class UserInChatGroupConfiguration : IEntityTypeConfiguration<UserInChatGroup>
    {
        public void Configure(EntityTypeBuilder<UserInChatGroup> builder)
        {
            builder.ToTable("UserInChatGroups");
            builder.HasKey(x => new { x.UserId, x.GroupId });

            builder.HasOne(u => u.User).WithMany(ug => ug.UserInChatGroups)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(gr => gr.ChatGroup).WithMany(ug => ug.UserInChatGroups)
                .HasForeignKey(x => x.GroupId);
        }
    }
}
