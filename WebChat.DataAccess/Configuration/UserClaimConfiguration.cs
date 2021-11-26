namespace WebChat.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WebChat.Entities.Model;

    public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("UserClaims");
            builder.HasKey(userClaim => userClaim.Id);

            builder
                .HasOne(userClaim => userClaim.User)
                .WithMany(user => user.UserClaims)
                .HasForeignKey(userClaim => userClaim.UserId);
        }
    }
}
