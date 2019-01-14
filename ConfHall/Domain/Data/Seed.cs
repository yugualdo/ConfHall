namespace ConfHall.Domain.Data
{
    using ConfHall.Domain.Entities;
    using ConfHall.Enums;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="hashingService"></param>
        public static void Seed(this ModelBuilder modelBuilder, IPasswordHasher<User> hashingService)
        {

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
                FirstName = "admin",
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

            Feature feature = new Feature
            {
                Id = Guid.NewGuid(),
                Name = "Screen"
            };
            modelBuilder.Entity<Feature>().HasData(feature);



            Hall hall = new Hall
            {
                Id = Guid.NewGuid(),
                Name = "Imperial",
                Description = "Big hall",
                HallType = HallType.EmptyRoom
            };
            modelBuilder.Entity<Hall>().HasData(hall);

            //HallFeature hallFeature = new HallFeature
            //{
            //    Id = Guid.NewGuid(),
            //    HallId = hall.Id,
            //    FeatureId = feature.Id
            //};
            //modelBuilder.Entity<HallFeature>().HasData(hallFeature);



            Customer customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Customer 1",
                IdNumber = "12345667890",
                PhoneNumber = "12345679900",
                Balance = 0
            };
            modelBuilder.Entity<Customer>().HasData(customer);

            //Reservation reservation = new Reservation
            //{
            //    Id = Guid.NewGuid(),
            //    Hall= hall,
            //    Customer = customer,
            //    From = DateTime.Now,
            //    To = DateTime.Now.AddHours(3),
            //    Price = 0,
            //    IsConfirmed = false,
            //    IsPaid = false
            //};
            //modelBuilder.Entity<Reservation>().OwnsOne(e => e.Hall);
            //modelBuilder.Entity<Reservation>().OwnsOne(e => e.Customer);
            //modelBuilder.Entity<Reservation>().HasData(reservation);

        }
    }
}
