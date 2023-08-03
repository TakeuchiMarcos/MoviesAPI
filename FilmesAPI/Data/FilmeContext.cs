using FilmesAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> opts) : base(opts)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .HasKey(room => new { room.Id, room.CinemaId });
            modelBuilder.Entity<Session>()
                .HasOne(session => session.Room)
                .WithMany(room => room.Sessions)
                .HasForeignKey(session => new { session.RoomId, session.CinemaId })
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Session>()
                .HasKey(session => new { session.MovieId, session.CinemaId, session.RoomId, session.SessionDate });
            modelBuilder.Entity<Session>()
                .HasAlternateKey(session => session.Id);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Session> Sessions { get; set; }

    }
}
