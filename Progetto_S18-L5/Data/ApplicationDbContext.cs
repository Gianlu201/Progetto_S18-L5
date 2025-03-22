using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Progetto_S18_L5.Models;

namespace Progetto_S18_L5.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<
            ApplicationUser,
            ApplicationRole,
            string,
            IdentityUserClaim<string>,
            ApplicationUserRole,
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>,
            IdentityUserToken<string>
        >
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura la relazione tra ApplicationUserRole e ApplicationUser
            modelBuilder
                .Entity<ApplicationUserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.ApplicationUserRole)
                .HasForeignKey(ur => ur.UserId);

            // Configura la relazione tra ApplicationUserRole e ApplicationRole
            modelBuilder
                .Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.ApplicationUserRole)
                .HasForeignKey(ur => ur.RoleId);

            // Configura la relazione tra Room e RoomType
            modelBuilder
                .Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId);

            // Configura la relazione tra Reservation e Room
            modelBuilder
                .Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(r => r.RoomId);

            // Configura la relazione tra Reservation e ApplicationUser
            modelBuilder
                .Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.ClientId);

            // Configura la relazione tra Reservation e ApplicationUser per la proprietà EmployeeId
            modelBuilder
                .Entity<Reservation>()
                .HasOne(r => r.Employee)
                .WithMany(u => u.OperatorReservations)
                .HasForeignKey(r => r.EmployeeId);

            modelBuilder
                .Entity<Reservation>()
                .HasOne(r => r.Employee)
                .WithMany(u => u.OperatorReservations)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configura valore di default al campo CheckIn di Reservation
            modelBuilder
                .Entity<Reservation>()
                .Property(r => r.CheckIn)
                .HasDefaultValueSql("GETDATE()");

            // Inserisce valori nella tabella RoomTypes
            modelBuilder
                .Entity<RoomType>()
                .HasData(
                    new RoomType()
                    {
                        RoomTypeId = Guid.Parse("6c5b01f7-d730-437c-a712-92e4c706cebe"),
                        RoomTypeName = "Twin room",
                        MaxOccupancy = 2,
                    },
                    new RoomType()
                    {
                        RoomTypeId = Guid.Parse("759b2d69-57a9-4284-9850-b771ab1b662c"),
                        RoomTypeName = "Three-bed room",
                        MaxOccupancy = 3,
                    },
                    new RoomType()
                    {
                        RoomTypeId = Guid.Parse("336d4f29-603c-4962-b81a-a11e994136fa"),
                        RoomTypeName = "Four-bed room",
                        MaxOccupancy = 4,
                    }
                );

            // Inserisce valori nella tabella dei Ruoli
            modelBuilder
                .Entity<ApplicationRole>()
                .HasData(
                    new ApplicationRole()
                    {
                        Id = "8d64359a-fda6-4096-b40d-f1375775244d",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                    },
                    new ApplicationRole()
                    {
                        Id = "25b30e78-3e7c-49cd-b682-19c58159e3f1",
                        Name = "Employee",
                        NormalizedName = "EMPLOYEE",
                    },
                    new ApplicationRole()
                    {
                        Id = "849b8726-44b3-434b-9b18-48a4e8d4e9dd",
                        Name = "Manager",
                        NormalizedName = "MANAGER",
                    }
                );

            // Inserisce valori nella tabella Rooms
            modelBuilder
                .Entity<Room>()
                .HasData(
                    new Room()
                    {
                        RoomId = Guid.Parse("b599f97b-2c0e-4817-b3d8-f6dd81ea46ad"),
                        RoomNumber = "11",
                        RoomTypeId = Guid.Parse("6c5b01f7-d730-437c-a712-92e4c706cebe"),
                        Price = 60,
                        IsAvailable = true,
                    },
                    new Room()
                    {
                        RoomId = Guid.Parse("e44cb2e9-45e8-439b-b492-8d3afe124f46"),
                        RoomNumber = "12",
                        RoomTypeId = Guid.Parse("6c5b01f7-d730-437c-a712-92e4c706cebe"),
                        Price = 65,
                        IsAvailable = true,
                    },
                    new Room()
                    {
                        RoomId = Guid.Parse("6e0d4dad-5363-4beb-97b6-78b7113741d9"),
                        RoomNumber = "13",
                        RoomTypeId = Guid.Parse("759b2d69-57a9-4284-9850-b771ab1b662c"),
                        Price = 80,
                        IsAvailable = true,
                    },
                    new Room()
                    {
                        RoomId = Guid.Parse("3cbaf41b-b7d6-4812-8579-3851a6960770"),
                        RoomNumber = "14",
                        RoomTypeId = Guid.Parse("759b2d69-57a9-4284-9850-b771ab1b662c"),
                        Price = 83,
                        IsAvailable = true,
                    },
                    new Room()
                    {
                        RoomId = Guid.Parse("462a9f3b-d36a-477d-93d3-ccb0aadc42dd"),
                        RoomNumber = "15",
                        RoomTypeId = Guid.Parse("336d4f29-603c-4962-b81a-a11e994136fa"),
                        Price = 100,
                        IsAvailable = true,
                    }
                );

            // Inserisce valori nella tabella ApplicationUser
            modelBuilder
                .Entity<ApplicationUser>()
                .HasData(
                    new ApplicationUser()
                    {
                        Id = "55e83e62-2057-45b0-82fe-33a4cba69a2e",
                        FirstName = "Admin",
                        LastName = "Admin",
                        Email = "admin@example.com",
                        UserName = "admin@example.com",
                        NormalizedUserName = "ADMIN@EXAMPLE.COM",
                        NormalizedEmail = "ADMIN@EXAMPLE.COM",
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEPTjFiaYaGtq8tsslxnhffNqhCeoVvpygVnS8vRbrx/pI2O2Nb7Q75iDvT4ZIQWV4g==",
                        PhoneNumber = "0000000000",
                    },
                    new ApplicationUser()
                    {
                        Id = "7f11db70-49f5-4c66-bad3-51085c2bd27a",
                        FirstName = "Mario",
                        LastName = "Mario",
                        Email = "mario.mario@examople.com",
                        UserName = "mario.mario@example.com",
                        NormalizedUserName = "MARIO.MARIO@EXAMPLE.COM",
                        NormalizedEmail = "MARIO.MARIO@EXAMPLE.COM",
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEKaVk2PilFpBF+5mhGmCiGOtIF+qHjjpf0Z4ukKkpAnff1/s2WJ/UiFQh4aZ9iP2YQ==",
                        PhoneNumber = "1111111111",
                    },
                    new ApplicationUser()
                    {
                        Id = "766609fc-a1bd-4ca8-bc3b-8167dd9ba0f2",
                        FirstName = "Luigi",
                        LastName = "Mario",
                        Email = "luigi.mario@example.com",
                        UserName = "luigi.mario@example.com",
                        NormalizedUserName = "LUIGI.MARIO@EXAMPLE.COM",
                        NormalizedEmail = "LUIGI.MARIO@EXAMPLE.COM",
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEHyzjydlPHBKYr6FC7KflthqGK/GbH+NZI8pY+a4rzNrqB7yy7z2HO+fuvlBfxjk5w==",
                        PhoneNumber = "2222222222",
                    }
                );

            // Inserisce valori nella tabella ApplicationUserRoles
            modelBuilder
                .Entity<ApplicationUserRole>()
                .HasData(
                    new ApplicationUserRole()
                    {
                        UserRoleId = "ffaec466-8d00-4538-9d77-3c9b13c01178",
                        UserId = "55e83e62-2057-45b0-82fe-33a4cba69a2e",
                        RoleId = "8d64359a-fda6-4096-b40d-f1375775244d",
                    },
                    new ApplicationUserRole()
                    {
                        UserRoleId = "4ac90c70-f29d-4af0-a914-f7235c851ad4",
                        UserId = "7f11db70-49f5-4c66-bad3-51085c2bd27a",
                        RoleId = "849b8726-44b3-434b-9b18-48a4e8d4e9dd",
                    },
                    new ApplicationUserRole()
                    {
                        UserRoleId = "499bc9f4-93e0-445a-9d20-352521f46992",
                        UserId = "766609fc-a1bd-4ca8-bc3b-8167dd9ba0f2",
                        RoleId = "25b30e78-3e7c-49cd-b682-19c58159e3f1",
                    }
                );
        }
    }
}
