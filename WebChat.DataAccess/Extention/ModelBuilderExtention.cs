using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.Utilities.Enums;
using WebChat.Entities.Model;

namespace WebChat.DataAccess.Extention
{
    public static class ModelBuilderExtention
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            var roleUserId = new Guid("24426BE2-1DD4-4F73-8E55-0A263982F901");
            var roleAdminId = new Guid("443D58A9-0D4D-4E60-B94E-64A496DACC65");
            var adminId = new Guid("87E68219-5095-4363-89A4-704D87F7F71E");
            var userId = new Guid("E1B62BA2-EA51-4C27-A9F8-7E65901F395A");
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = roleAdminId,
                    Name = "ADMIN",
                    NormalizedName = "ADMIN",
                    Description = "Adminitrator role"
                },
                new Role
                {
                    Id = roleUserId,
                    Name = "USER",
                    NormalizedName = "USER",
                    Description = "User role"
                }
                );

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Thuan",
                    LastName = "Nguyen",
                    Address = "Ho Chi Minh, Viet Nam",
                    DateCreate = DateTime.Now,
                    IsAdmin = true,
                    IsLocked = false,
                    Status = true
                },
                new User
                {
                    Id = userId,
                    UserName = "user",
                    NormalizedUserName = "user",
                    Email = "user@gmail.com",
                    NormalizedEmail = "user@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Jack",
                    LastName = "Robinson",
                    Address = "New York, US",
                    DateCreate = DateTime.Now,
                    IsAdmin = false,
                    IsLocked = false,
                    Status = true
                });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = roleAdminId,
                    UserId = adminId
                },
                new IdentityUserRole<Guid>
                {
                    RoleId = roleUserId,
                    UserId = userId
                });

        }
    }
}
