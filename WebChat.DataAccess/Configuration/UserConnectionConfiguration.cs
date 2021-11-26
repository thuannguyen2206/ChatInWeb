namespace WebChat.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using WebChat.Entities.Model;

    public class UserConnectionConfiguration : IEntityTypeConfiguration<UserConnection>
    {
        public void Configure(EntityTypeBuilder<UserConnection> builder)
        {
            builder.ToTable("UserConnections");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.LastModifyAt).IsRequired(false);
            builder.Property(x => x.ConnectionId).IsRequired(false);
            builder.Property(x => x.IPAddress).IsRequired(false);

        }
    }
}
