using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.Content).HasMaxLength(500);
        }
    }
}
