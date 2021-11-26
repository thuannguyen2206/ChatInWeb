using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebChat.DataAccess.Configuration;
using WebChat.DataAccess.Extention;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.EF
{
    public class WebChatDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public WebChatDbContext( DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configuration using fluent API
            modelBuilder.ApplyConfiguration(new CallUserConfiguration());
            modelBuilder.ApplyConfiguration(new ChatGroupConfiguration());
            modelBuilder.ApplyConfiguration(new ChatLogConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserInChatGroupConfiguration());
            modelBuilder.ApplyConfiguration(new UserConnectionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            //modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(x => new { x.UserId, x.RoleId });

            //seeding data
            modelBuilder.Seed();
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<CallUser> CallUsers { get; set; }
        public DbSet<ChatGroup> ChatGroups { get; set; }
        public DbSet<ChatLog> ChatLogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        public DbSet<UserInChatGroup> UserInChatGroups { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<User> Users { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }

    }
}
