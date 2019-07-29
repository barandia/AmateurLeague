using AmateurLeague.Entity;
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
                e.HasKey(s => s.SportId);
                e.Property(s => s.SportId).ValueGeneratedOnAdd();
                e.Property(s => s.SportName).IsRequired().HasMaxLength(100);
                e.Property(s => s.GenderType).IsRequired();
            });
            modelBuilder.Entity<Sport>().HasData(new Sport{ SportId = "1", SportName = "Basketball", Type = SportGenderTypes.Men},
                                                new Sport { SportId = "2", SportName = "Basketball", Type = SportGenderTypes.Women },
                                                new Sport { SportId = "3", SportName = "Basketball", Type = SportGenderTypes.Coed },
                                                new Sport { SportId = "4", SportName = "Baseball", Type = SportGenderTypes.Men },
                                                new Sport { SportId = "5", SportName = "Baseball", Type = SportGenderTypes.Women },
                                                new Sport { SportId = "6", SportName = "Baseball", Type = SportGenderTypes.Coed },
                                                new Sport { SportId = "7", SportName = "Soccer", Type = SportGenderTypes.Men },
                                                new Sport { SportId = "8", SportName = "Soccer", Type = SportGenderTypes.Women },
                                                new Sport { SportId = "9", SportName = "Soccer", Type = SportGenderTypes.Coed },
                                                new Sport { SportId = "10", SportName = "Flag Football", Type = SportGenderTypes.Men },
                                                new Sport { SportId = "11", SportName = "Flag Football", Type = SportGenderTypes.Women },
                                                new Sport { SportId = "12", SportName = "Flag Football", Type = SportGenderTypes.Coed });
;            

            modelBuilder.Entity<League>().ToTable("Leagues");
            modelBuilder.Entity<League>(e => 
            {
                e.HasKey(l => l.LeagueId);
                e.Property(l => l.LeagueId).ValueGeneratedOnAdd();
                e.HasIndex(l => l.LeagueName).IsUnique();
                e.Property(l => l.LeagueName).IsRequired().HasMaxLength(100);

                e.HasOne(l => l.Sport).WithMany(s => s.Leagues).IsRequired();
                e.HasMany(l => l.Teams).WithOne(t => t.League).IsRequired();
            });

            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<Team>(e =>
            {
                e.HasKey(t => t.TeamId);
                e.Property(t => t.TeamId).ValueGeneratedOnAdd();
                e.HasIndex(t => t.TeamName).IsUnique();
                e.Property(t => t.TeamName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Player>(e =>
            {
                e.HasKey(p => p.PlayerId);
                e.Property(p => p.PlayerId).ValueGeneratedOnAdd();
                e.HasIndex(p => p.EmailAddress).IsUnique();
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
