using System.Diagnostics;
using TravelAPI.Models;
using TravelAPI.Controllers;
using TravelAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace TravelAPI.DBContexts
{
    public class TravelDbContext : DbContext
    {
        // Define DbSet properties for your models
        public DbSet<Voyage> Voyages { get; set; } = null!;
        public DbSet<Activites> Activities { get; set; } = null!;
        public DbSet<ActivityType> ActivityType { get; set; } = null!;
        public DbSet<Budget> Budget { get; set; } = null!;
        public DbSet<Comments> Comments { get; set; } = null!;
        public DbSet<Destination> Destination { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

        // Constructor for the DbContext
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        {
            // Créez une liste de destinations par défaut à ajouter à la base de données
            var defaultDestinations = new[]
            {
                new Destination { Id = Guid.NewGuid(), Country = "Guadeloupe" },
                new Destination { Id = Guid.NewGuid(), Country = "Italy" },
                new Destination { Id = Guid.NewGuid(), Country = "Spain" },
                new Destination { Id = Guid.NewGuid(), Country = "Germany" },
                new Destination { Id = Guid.NewGuid(), Country = "Canada" },
                new Destination { Id = Guid.NewGuid(), Country = "Netherlands" },
                new Destination { Id = Guid.NewGuid(), Country = "Thailand" },
                new Destination { Id = Guid.NewGuid(), Country = "United States" },
                new Destination { Id = Guid.NewGuid(), Country = "Switzerland" },
                new Destination { Id = Guid.NewGuid(), Country = "Sweden" },
                new Destination { Id = Guid.NewGuid(), Country = "Russia" },
                new Destination { Id = Guid.NewGuid(), Country = "Mexico" },
                new Destination { Id = Guid.NewGuid(), Country = "India" },
                new Destination { Id = Guid.NewGuid(), Country = "China" },
                new Destination { Id = Guid.NewGuid(), Country = "Brazil" },
                new Destination { Id = Guid.NewGuid(), Country = "Australia" },
                new Destination { Id = Guid.NewGuid(), Country = "Japan" },
                new Destination { Id = Guid.NewGuid(), Country = "Greece" },
                new Destination { Id = Guid.NewGuid(), Country = "France" },
                new Destination { Id = Guid.NewGuid(), Country = "South Africa" }
            };

            // Ajoutez les destinations par défaut à la base de données si elle est vide
            if (!Destination.Any())
            {
                Destination.AddRange(defaultDestinations);
                SaveChanges();
            }

            var defaultActivityTypes = new[]
            {
                new ActivityType { Id = Guid.NewGuid(), Name = "Hiking" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Sightseeing" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Swimming" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Visit" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Diving" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Climbing" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Tasting" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Safari" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Courses" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Paragliding" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Yoga" },
                new ActivityType { Id = Guid.NewGuid(), Name = "Stroll" }
            };

            // Ajoutez les types d'activités par défaut à la base de données si elle est vide
            if (!ActivityType.Any())
            {
                ActivityType.AddRange(defaultActivityTypes);
                SaveChanges();
            }
        }

        // Add any additional configuration or overrides here
        // (Optional, if needed)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define a many-to-many relationship between Voyages and Activities

            modelBuilder.Entity<Voyage>() // Define configuration for the Voyages entity
                .HasMany(v => v.Activities) // A Voyage has many Activities
                .WithMany(a => a.Voyages)    // An Activity can belong to many Voyages
                .UsingEntity(j => j.ToTable("VoyageActivities")); // Define a junction table named "VoyageActivities"

            modelBuilder.Entity<Activites>()
                .HasOne(a => a.ActivityType) // Une Activites a un ActivityType
                .WithMany() // Un ActivityType peut avoir plusieurs Activites
                .HasForeignKey(a => a.ActivityTypeId); // Clé étrangère dans Activites pointant vers l'ID de ActivityType

        }
    }
}
