using Microsoft.EntityFrameworkCore;
using ConfHall.Domain.Entities;
namespace ConfHall.Domain.Data
{
    using ConfHall.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;

    public partial class ConfHallDBContext : IdentityDbContext
         <
         User,
         Role,
         Guid,
         IdentityUserClaim<Guid>,
         IdentityUserRole<Guid>,
         IdentityUserLogin<Guid>,
         IdentityRoleClaim<Guid>,
         IdentityUserToken<Guid>
         >
    {
        private readonly IPasswordHasher<User> hashingService;


        public ConfHallDBContext(DbContextOptions<ConfHallDBContext> options, IPasswordHasher<User> hashingService)
           : base(options)
        {
            this.hashingService = hashingService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
            });

            modelBuilder.Entity<HallFeature>().HasOne(hf => hf.Hall).WithMany(h => h.HallFeatures).HasForeignKey(hf=>hf.HallId);
            modelBuilder.Entity<HallFeature>().HasOne(hf => hf.Feature).WithMany(h => h.HallFeatures).HasForeignKey(hf=>hf.FeatureId);
            
            modelBuilder.Seed(hashingService);
        }

        public DbSet<ConfHall.Domain.Entities.Customer> Customer { get; set; }
        public DbSet<ConfHall.Domain.Entities.Hall> Hall { get; set; }
        public DbSet<ConfHall.Domain.Entities.Reservation> Reservation { get; set; }
        public DbSet<ConfHall.Domain.Entities.HallFeature> HallFeature { get; set; }
    }
}
