using Microsoft.EntityFrameworkCore;

namespace dotnetAssignment.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<TouristPlace> Tourists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TouristPlace>().HasData(
                new TouristPlace { Id = 1, Address = "Cox's Bazar", Name = "Cox bazar Sea Beach", Rating = 3.5, Type=PlaceType.Beach },
                new TouristPlace { Id = 2, Address = "Khulna", Name = "SundarBan", Rating = 4, Type=PlaceType.Landmark },
                new TouristPlace { Id = 3, Address = "Lalbag", Name = "Lalbag Fort", Rating = 3, Type=PlaceType.Landmark },
                new TouristPlace { Id = 4, Address = "Cumilla", Name = "Mohasthan Gor", Rating = 4.2, Type=PlaceType.Hills });
        }
    }
}
