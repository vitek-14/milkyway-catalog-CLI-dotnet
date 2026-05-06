using Microsoft.EntityFrameworkCore;
using SpaceCatalog.Domain;

namespace SpaceCatalog.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<StarSystem> StarSystems { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Nebula> Nebulae { get; set; }
        public DbSet<Exoplanet> Exoplanets { get; set; }
        public DbSet<StarExoplanet> StarExoplanets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StarSystem>()
                .OwnsOne(starSystem => starSystem.Coordinates);

            modelBuilder.Entity<Nebula>()
                .OwnsOne(nebula => nebula.Coordinates);

            modelBuilder.Entity<Exoplanet>()
                .HasOne(exoplanet => exoplanet.StarSystem)
                .WithMany(starSystem => starSystem.Exoplanets)
                .HasForeignKey(exoplanet => exoplanet.StarSystemId)
                .IsRequired();

            modelBuilder.Entity<Star>()
                .HasOne(star => star.StarSystem)
                .WithMany(starSystem => starSystem.Stars)
                .HasForeignKey(star => star.StarSystemId);

            modelBuilder.Entity<Star>()
                .HasOne(star => star.Nebula)
                .WithMany(nebula => nebula.Stars)
                .HasForeignKey(star => star.NebulaId);

            modelBuilder.Entity<StarExoplanet>()
                .HasKey(starExoplanet => new
                {
                    starExoplanet.StarId,
                    starExoplanet.ExoplanetId
                });

            modelBuilder.Entity<StarExoplanet>()
                .HasOne(starExoplanet => starExoplanet.Star)
                .WithMany(star => star.StarExoplanets)
                .HasForeignKey(starExoplanet => starExoplanet.StarId)
                .IsRequired();

            modelBuilder.Entity<StarExoplanet>()
                .HasOne(starExoplanet => starExoplanet.Exoplanet)
                .WithMany(exoplanet => exoplanet.StarExoplanets)
                .HasForeignKey(starExoplanet => starExoplanet.ExoplanetId)
                .IsRequired();
        }
    }
}
