using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.Property(x => x.IsBlock).HasDefaultValue(false);
            builder.HasOne(x => x.User).WithMany(x => x.Contacts).HasForeignKey(x => x.UserId);
        }
    }
}
