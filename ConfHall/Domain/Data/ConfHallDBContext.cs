namespace ConfHall.Domain.Data
{
    using ConfHall.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;

    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="hashingService"></param>
        public ConfHallDBContext(DbContextOptions<ConfHallDBContext> options, IPasswordHasher<User> hashingService)
           : base(options)
        {
            this.hashingService = hashingService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
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

            modelBuilder.Entity<HallFeature>().HasKey(hf => new { hf.HallId, hf.FeatureId });
            modelBuilder.Seed(hashingService);
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Hall> Hall { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Reservation> Reservation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<HallFeature> HallFeature { get; set; }
    }
}
