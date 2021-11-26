namespace WebChat.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WebChat.Entities.Model;

    public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaims");
            builder.HasKey(roleClaim => roleClaim.Id);

            builder
                .HasOne(roleClaim => roleClaim.Role)
                .WithMany(role => role.RoleClaims)
                .HasForeignKey(roleClaim => roleClaim.RoleId);
        }
    }
}
