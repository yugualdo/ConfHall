namespace ConfHall.Domain.Data
{
    using ConfHall.Domain.Entities;
    using ConfHall.Enums;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder, IPasswordHasher<User> hashingService)
        {
            #region User and Role
            User user = new User
            {
                Id = Guid.NewGuid(),
                AccessFailedCount = 3,
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                IsActive = true,
                EmailConfirmed = true,
                Email = "admin@confhall.com",
                NormalizedEmail = "admin@confhall.com".ToUpper(),
                NormalizedUserName = "admin".ToUpper(),
                PhoneNumber = "+5(555)55555",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                UserName = "admin",
                FirstName ="admin",
                LastName = "admin"
            };
            user.PasswordHash = hashingService.HashPassword(user, "admin");

            modelBuilder.Entity<User>().HasData(user);

            Role role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "admin",
                NormalizedName = "admin".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            modelBuilder.Entity<Role>().HasData(role);
            #endregion

            #region Hall
            Hall hall = new Hall
            {
                Id = Guid.NewGuid(),
                Name = "Imperial",
                Description= "Big hall",
                HallType = HallType.EmptyRoom
            };
            modelBuilder.Entity<Hall>().HasData(hall);
            #endregion
        }
    }
}
