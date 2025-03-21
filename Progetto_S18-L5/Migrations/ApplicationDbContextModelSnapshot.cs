﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Progetto_S18_L5.Data;

#nullable disable

namespace Progetto_S18_L5.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "8d64359a-fda6-4096-b40d-f1375775244d",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "25b30e78-3e7c-49cd-b682-19c58159e3f1",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "849b8726-44b3-434b-9b18-48a4e8d4e9dd",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        });
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "55e83e62-2057-45b0-82fe-33a4cba69a2e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "51c6680e-5cfa-40a8-b200-47d2d8481f65",
                            Email = "admin@example.com",
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN@EXAMPLE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEPTjFiaYaGtq8tsslxnhffNqhCeoVvpygVnS8vRbrx/pI2O2Nb7Q75iDvT4ZIQWV4g==",
                            PhoneNumber = "0000000000",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8a004169-727e-4e5e-8c1e-7ddf3d62c43b",
                            TwoFactorEnabled = false,
                            UserName = "admin@example.com"
                        },
                        new
                        {
                            Id = "7f11db70-49f5-4c66-bad3-51085c2bd27a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a2974c78-f27e-4392-94a7-0db07661c441",
                            Email = "mario.mario@examople.com",
                            EmailConfirmed = false,
                            FirstName = "Mario",
                            LastName = "Mario",
                            LockoutEnabled = false,
                            NormalizedEmail = "MARIO.MARIO@EXAMPLE.COM",
                            NormalizedUserName = "MARIO.MARIO@EXAMPLE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEKaVk2PilFpBF+5mhGmCiGOtIF+qHjjpf0Z4ukKkpAnff1/s2WJ/UiFQh4aZ9iP2YQ==",
                            PhoneNumber = "1111111111",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "46efe68e-4ec9-4d25-bbb1-14e99f07c8e0",
                            TwoFactorEnabled = false,
                            UserName = "mario.mario@example.com"
                        },
                        new
                        {
                            Id = "766609fc-a1bd-4ca8-bc3b-8167dd9ba0f2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a5904b41-0e50-41d4-999e-46b189cd40b4",
                            Email = "luigi.mario@example.com",
                            EmailConfirmed = false,
                            FirstName = "Luigi",
                            LastName = "Mario",
                            LockoutEnabled = false,
                            NormalizedEmail = "LUIGI.MARIO@EXAMPLE.COM",
                            NormalizedUserName = "LUIGI.MARIO@EXAMPLE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHyzjydlPHBKYr6FC7KflthqGK/GbH+NZI8pY+a4rzNrqB7yy7z2HO+fuvlBfxjk5w==",
                            PhoneNumber = "2222222222",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3c79a20a-ad34-405e-9668-298240ce7c49",
                            TwoFactorEnabled = false,
                            UserName = "luigi.mario@example.com"
                        });
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "55e83e62-2057-45b0-82fe-33a4cba69a2e",
                            RoleId = "8d64359a-fda6-4096-b40d-f1375775244d"
                        },
                        new
                        {
                            UserId = "7f11db70-49f5-4c66-bad3-51085c2bd27a",
                            RoleId = "849b8726-44b3-434b-9b18-48a4e8d4e9dd"
                        },
                        new
                        {
                            UserId = "766609fc-a1bd-4ca8-bc3b-8167dd9ba0f2",
                            RoleId = "25b30e78-3e7c-49cd-b682-19c58159e3f1"
                        });
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.Reservation", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CheckIn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.HasKey("ReservationId");

                    b.HasIndex("ClientId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoomTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoomId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            RoomId = new Guid("b599f97b-2c0e-4817-b3d8-f6dd81ea46ad"),
                            IsAvailable = true,
                            Price = 60m,
                            RoomNumber = "11",
                            RoomTypeId = new Guid("6c5b01f7-d730-437c-a712-92e4c706cebe")
                        },
                        new
                        {
                            RoomId = new Guid("e44cb2e9-45e8-439b-b492-8d3afe124f46"),
                            IsAvailable = true,
                            Price = 65m,
                            RoomNumber = "12",
                            RoomTypeId = new Guid("6c5b01f7-d730-437c-a712-92e4c706cebe")
                        },
                        new
                        {
                            RoomId = new Guid("6e0d4dad-5363-4beb-97b6-78b7113741d9"),
                            IsAvailable = true,
                            Price = 80m,
                            RoomNumber = "13",
                            RoomTypeId = new Guid("759b2d69-57a9-4284-9850-b771ab1b662c")
                        },
                        new
                        {
                            RoomId = new Guid("3cbaf41b-b7d6-4812-8579-3851a6960770"),
                            IsAvailable = true,
                            Price = 83m,
                            RoomNumber = "14",
                            RoomTypeId = new Guid("759b2d69-57a9-4284-9850-b771ab1b662c")
                        },
                        new
                        {
                            RoomId = new Guid("462a9f3b-d36a-477d-93d3-ccb0aadc42dd"),
                            IsAvailable = true,
                            Price = 100m,
                            RoomNumber = "15",
                            RoomTypeId = new Guid("336d4f29-603c-4962-b81a-a11e994136fa")
                        });
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.RoomType", b =>
                {
                    b.Property<Guid>("RoomTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaxOccupancy")
                        .HasColumnType("int");

                    b.Property<string>("RoomTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomTypeId");

                    b.ToTable("RoomTypes");

                    b.HasData(
                        new
                        {
                            RoomTypeId = new Guid("6c5b01f7-d730-437c-a712-92e4c706cebe"),
                            MaxOccupancy = 2,
                            RoomTypeName = "Twin room"
                        },
                        new
                        {
                            RoomTypeId = new Guid("759b2d69-57a9-4284-9850-b771ab1b662c"),
                            MaxOccupancy = 3,
                            RoomTypeName = "Three-bed room"
                        },
                        new
                        {
                            RoomTypeId = new Guid("336d4f29-603c-4962-b81a-a11e994136fa"),
                            MaxOccupancy = 4,
                            RoomTypeName = "Four-bed room"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Progetto_S18_L5.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Progetto_S18_L5.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Progetto_S18_L5.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Progetto_S18_L5.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.ApplicationUserRole", b =>
                {
                    b.HasOne("Progetto_S18_L5.Models.ApplicationRole", "Role")
                        .WithMany("ApplicationUserRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Progetto_S18_L5.Models.ApplicationUser", "User")
                        .WithMany("ApplicationUserRole")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.Reservation", b =>
                {
                    b.HasOne("Progetto_S18_L5.Models.ApplicationUser", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Progetto_S18_L5.Models.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.Room", b =>
                {
                    b.HasOne("Progetto_S18_L5.Models.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.ApplicationRole", b =>
                {
                    b.Navigation("ApplicationUserRole");
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.ApplicationUser", b =>
                {
                    b.Navigation("ApplicationUserRole");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.Room", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Progetto_S18_L5.Models.RoomType", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
