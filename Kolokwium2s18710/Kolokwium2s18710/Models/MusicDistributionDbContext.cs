using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2s18710.Models
{
    public class MusicDistributionDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }

        public DbSet<Musician> Musicians { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<MusicLabel> MusicLabels { get; set; }

        public DbSet<MusicianTrack> MusicianTracks { get; set; }

        public MusicDistributionDbContext() { }

        public MusicDistributionDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MusicianTrack>()
            .HasKey(e => new { e.IdMusician, e.IdTrack });
        }
    }
}
