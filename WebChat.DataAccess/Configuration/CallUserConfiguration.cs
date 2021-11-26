using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Configuration
{
    public class CallUserConfiguration : IEntityTypeConfiguration<CallUser>
    {
        public void Configure(EntityTypeBuilder<CallUser> builder)
        {
            builder.ToTable("CallUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.HasOne(x => x.ChatGroup).WithMany(x => x.CallUsers).HasForeignKey(x => x.GroupId);
            builder.HasOne(x => x.User).WithMany(x => x.CallUsers).HasForeignKey(x => x.SenderCallId);
        }
    }
}
