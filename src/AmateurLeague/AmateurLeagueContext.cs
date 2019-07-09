using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmateurLeague
{
    /**
     *
     */ 
    public class AmateurLeagueContext: DbContext
    {
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AmateurLeagueDb;Integrated Security=True;Connect Timeout=30;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>().ToTable("Sports");
            modelBuilder.Entity<Sport>(e =>
            {
                e.HasKey(s => s.Id).HasName("PK_Sports");
                e.Property(s => s.Id).ValueGeneratedOnAdd();
                e.Property(s => s.Name).IsRequired().HasMaxLength(100);
                e.Property(s => s.Type).IsRequired();
            });

            modelBuilder.Entity<League>().ToTable("Leagues");
            modelBuilder.Entity<League>(e => 
            {
                e.HasKey(l => l.Name).HasName("PK_Leagues");
                e.Property(l => l.Name).IsRequired().HasMaxLength(100);

                e.HasOne(l => l.Sport).WithMany(s => s.Leagues).IsRequired();
                e.HasMany(l => l.Teams).WithOne(t => t.League).IsRequired();
            });

            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<Team>(e =>
            {
                e.HasKey(t => t.Name).HasName("PK_Teams");
                e.Property(t => t.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Player>(e =>
            {
                e.HasKey(p => p.EmailAddress).HasName("PK_Players");
                e.Property(p => p.EmailAddress).IsRequired();
                e.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                e.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                e.Property(p => p.Gender).IsRequired();
                e.Property(p => p.DateOfBirth).IsRequired();
            });

            modelBuilder.Entity<TeamPlayer>().ToTable("TeamPlayer");
            modelBuilder.Entity<TeamPlayer>( e =>
            {
                e.HasKey(tp => new { tp.TeamId, tp.PlayerId });
                e.HasOne(tp => tp.Team).WithMany(t => t.TeamPlayers).HasForeignKey(tp => tp.TeamId);
                e.HasOne(tp => tp.Player).WithMany(t => t.TeamPlayers).HasForeignKey(tp => tp.PlayerId);
            });
        }
    }
}
