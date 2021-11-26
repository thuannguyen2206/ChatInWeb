﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebChat.DataAccess.EF;

namespace WebChat.DataAccess.Migrations
{
    [DbContext(typeof(WebChatDbContext))]
    [Migration("20210517104212_DeleteNotiType")]
    partial class DeleteNotiType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<Guid>");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("87e68219-5095-4363-89a4-704d87f7f71e"),
                            RoleId = new Guid("443d58a9-0d4d-4e60-b94e-64a496dacc65")
                        },
                        new
                        {
                            UserId = new Guid("e1b62ba2-ea51-4c27-a9f8-7e65901f395a"),
                            RoleId = new Guid("24426be2-1dd4-4f73-8e55-0a263982f901")
                        });
                });

            modelBuilder.Entity("WebChat.Entities.Model.CallType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("LastModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CallTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2021, 5, 17, 17, 42, 11, 768, DateTimeKind.Local).AddTicks(7865),
                            Description = "Gọi điện thoại",
                            IsActive = true,
                            Name = "Voice Call"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2021, 5, 17, 17, 42, 11, 768, DateTimeKind.Local).AddTicks(9957),
                            Description = "Gọi video",
                            IsActive = true,
                            Name = "Video Call"
                        });
                });

            modelBuilder.Entity("WebChat.Entities.Model.CallUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CallTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SenderCallId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("TimeStart")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeStop")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CallTypeId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SenderCallId");

                    b.ToTable("CallUsers");
                });

            modelBuilder.Entity("WebChat.Entities.Model.ChatGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 5, 17, 17, 42, 11, 699, DateTimeKind.Local).AddTicks(9484));

                    b.Property<DateTime?>("LastModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ChatGroups");
                });

            modelBuilder.Entity("WebChat.Entities.Model.ChatLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AttachedFiles")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("CallId")
                        .HasColumnType("int");

                    b.Property<Guid>("ChatGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 5, 17, 17, 42, 11, 704, DateTimeKind.Local).AddTicks(1915));

                    b.Property<DateTime?>("LastModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChatGroupId");

                    b.HasIndex("SenderId");

                    b.ToTable("ChatLogs");
                });

            modelBuilder.Entity("WebChat.Entities.Model.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FriendId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsBlock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("WebChat.Entities.Model.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Content")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ReceiverGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TypeNotification")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("WebChat.Entities.Model.NotificationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 5, 17, 17, 42, 11, 708, DateTimeKind.Local).AddTicks(306));

                    b.Property<bool>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NotificationId")
                        .HasColumnType("int");

                    b.Property<Guid?>("ReceiverUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("TimeRead")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.ToTable("NotificationUsers");
                });

            modelBuilder.Entity("WebChat.Entities.Model.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("443d58a9-0d4d-4e60-b94e-64a496dacc65"),
                            ConcurrencyStamp = "0b7db0ac-5cd0-48da-97af-9b096220cacb",
                            Description = "Adminitrator role",
                            Name = "ADMIN",
                            NormalizedName = "ADMIN",
                            Status = false
                        },
                        new
                        {
                            Id = new Guid("24426be2-1dd4-4f73-8e55-0a263982f901"),
                            ConcurrencyStamp = "52299c04-2679-4d29-af65-6764364489da",
                            Description = "User role",
                            Name = "USER",
                            NormalizedName = "USER",
                            Status = false
                        });
                });

            modelBuilder.Entity("WebChat.Entities.Model.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("WebChat.Entities.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AvatarLink")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 5, 17, 17, 42, 11, 715, DateTimeKind.Local).AddTicks(9709));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsLocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("87e68219-5095-4363-89a4-704d87f7f71e"),
                            AccessFailedCount = 0,
                            Address = "Ho Chi Minh, Viet Nam",
                            ConcurrencyStamp = "e0f78c97-f62d-4703-9fd5-ca12a2156d86",
                            DateCreate = new DateTime(2021, 5, 17, 17, 42, 11, 900, DateTimeKind.Local).AddTicks(4437),
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Thuan",
                            IsAdmin = true,
                            IsLocked = false,
                            LastName = "Nguyen",
                            LockoutEnabled = false,
                            NormalizedEmail = "admin@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEG5ECHifp5oorQ74njpTqTZlX48tlYCYFC1C1RUOrEftOs47WI6uz1eVXjqD4jYjnw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            Status = true,
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("e1b62ba2-ea51-4c27-a9f8-7e65901f395a"),
                            AccessFailedCount = 0,
                            Address = "New York, US",
                            ConcurrencyStamp = "110df08d-4025-4ad8-bd97-d4d01410020b",
                            DateCreate = new DateTime(2021, 5, 17, 17, 42, 11, 907, DateTimeKind.Local).AddTicks(5923),
                            Email = "user@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Jack",
                            IsAdmin = false,
                            IsLocked = false,
                            LastName = "Robinson",
                            LockoutEnabled = false,
                            NormalizedEmail = "user@gmail.com",
                            NormalizedUserName = "user",
                            PasswordHash = "AQAAAAEAACcQAAAAEKS5ExabDwr0P1Bf+/AuhhwnaVBnwq/T/YdoFggDLCaHew+O/XgAoHU0HMMRqa+TQA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            Status = true,
                            TwoFactorEnabled = false,
                            UserName = "user"
                        });
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserConnection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ConnectionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 5, 17, 17, 42, 11, 713, DateTimeKind.Local).AddTicks(4585));

                    b.Property<string>("IPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserConnections");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserInChatGroup", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserInChatGroups");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>");

                    b.Property<Guid?>("RoleId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.HasDiscriminator().HasValue("UserRole");
                });

            modelBuilder.Entity("WebChat.Entities.Model.CallUser", b =>
                {
                    b.HasOne("WebChat.Entities.Model.CallType", "CallType")
                        .WithMany("CallUsers")
                        .HasForeignKey("CallTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebChat.Entities.Model.ChatGroup", "ChatGroup")
                        .WithMany("CallUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebChat.Entities.Model.User", "User")
                        .WithMany("CallUsers")
                        .HasForeignKey("SenderCallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CallType");

                    b.Navigation("ChatGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebChat.Entities.Model.ChatLog", b =>
                {
                    b.HasOne("WebChat.Entities.Model.ChatGroup", "ChatGroup")
                        .WithMany("ChatLogs")
                        .HasForeignKey("ChatGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebChat.Entities.Model.User", "User")
                        .WithMany("ChatLogs")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebChat.Entities.Model.Contact", b =>
                {
                    b.HasOne("WebChat.Entities.Model.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebChat.Entities.Model.NotificationUser", b =>
                {
                    b.HasOne("WebChat.Entities.Model.Notification", "Notification")
                        .WithMany("NotificationUsers")
                        .HasForeignKey("NotificationId");

                    b.Navigation("Notification");
                });

            modelBuilder.Entity("WebChat.Entities.Model.RoleClaim", b =>
                {
                    b.HasOne("WebChat.Entities.Model.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserClaim", b =>
                {
                    b.HasOne("WebChat.Entities.Model.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserInChatGroup", b =>
                {
                    b.HasOne("WebChat.Entities.Model.ChatGroup", "ChatGroup")
                        .WithMany("UserInChatGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebChat.Entities.Model.User", "User")
                        .WithMany("UserInChatGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserLogin", b =>
                {
                    b.HasOne("WebChat.Entities.Model.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserToken", b =>
                {
                    b.HasOne("WebChat.Entities.Model.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebChat.Entities.Model.UserRole", b =>
                {
                    b.HasOne("WebChat.Entities.Model.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId1");

                    b.HasOne("WebChat.Entities.Model.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId1");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebChat.Entities.Model.CallType", b =>
                {
                    b.Navigation("CallUsers");
                });

            modelBuilder.Entity("WebChat.Entities.Model.ChatGroup", b =>
                {
                    b.Navigation("CallUsers");

                    b.Navigation("ChatLogs");

                    b.Navigation("UserInChatGroups");
                });

            modelBuilder.Entity("WebChat.Entities.Model.Notification", b =>
                {
                    b.Navigation("NotificationUsers");
                });

            modelBuilder.Entity("WebChat.Entities.Model.Role", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WebChat.Entities.Model.User", b =>
                {
                    b.Navigation("CallUsers");

                    b.Navigation("ChatLogs");

                    b.Navigation("Contacts");

                    b.Navigation("UserClaims");

                    b.Navigation("UserInChatGroups");

                    b.Navigation("UserLogins");

                    b.Navigation("UserRoles");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
