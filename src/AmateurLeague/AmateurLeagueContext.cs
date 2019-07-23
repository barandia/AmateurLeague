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
                e.HasKey(s => s.Id).HasName("PK_Sports");
                e.Property(s => s.Id).ValueGeneratedOnAdd();
                e.Property(s => s.Name).IsRequired().HasMaxLength(100);
                e.Property(s => s.GenderType).IsRequired();
            });
            modelBuilder.Entity<Sport>().HasData(new Sport{ Id = "1", Name = "Basketball", Type = SportGenderTypes.Men},
                                                new Sport { Id = "2", Name = "Basketball", Type = SportGenderTypes.Women },
                                                new Sport { Id = "3", Name = "Basketball", Type = SportGenderTypes.Coed },
                                                new Sport { Id = "4", Name = "Baseball", Type = SportGenderTypes.Men },
                                                new Sport { Id = "5", Name = "Baseball", Type = SportGenderTypes.Women },
                                                new Sport { Id = "6", Name = "Baseball", Type = SportGenderTypes.Coed },
                                                new Sport { Id = "7", Name = "Soccer", Type = SportGenderTypes.Men },
                                                new Sport { Id = "8", Name = "Soccer", Type = SportGenderTypes.Women },
                                                new Sport { Id = "9", Name = "Soccer", Type = SportGenderTypes.Coed },
                                                new Sport { Id = "10", Name = "Flag Football", Type = SportGenderTypes.Men },
                                                new Sport { Id = "11", Name = "Flag Football", Type = SportGenderTypes.Women },
                                                new Sport { Id = "12", Name = "Flag Football", Type = SportGenderTypes.Coed });
;            

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
