﻿// <auto-generated />
using System;
using AmateurLeague;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AmateurLeague.Migrations
{
    [DbContext(typeof(AmateurLeagueContext))]
    partial class AmateurLeagueContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AmateurLeague.Entity.League", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<string>("SportId")
                        .IsRequired();

                    b.HasKey("Name")
                        .HasName("PK_Leagues");

                    b.HasIndex("SportId");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("AmateurLeague.Entity.Player", b =>
                {
                    b.Property<string>("EmailAddress")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("EmailAddress")
                        .HasName("PK_Players");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("AmateurLeague.Entity.Sport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GenderType")
                        .IsRequired()
                        .HasColumnName("GenderType");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("PK_Sports");

                    b.ToTable("Sports");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            GenderType = "Men",
                            Name = "Basketball"
                        },
                        new
                        {
                            Id = "2",
                            GenderType = "Women",
                            Name = "Basketball"
                        },
                        new
                        {
                            Id = "3",
                            GenderType = "Coed",
                            Name = "Basketball"
                        },
                        new
                        {
                            Id = "4",
                            GenderType = "Men",
                            Name = "Baseball"
                        },
                        new
                        {
                            Id = "5",
                            GenderType = "Women",
                            Name = "Baseball"
                        },
                        new
                        {
                            Id = "6",
                            GenderType = "Coed",
                            Name = "Baseball"
                        },
                        new
                        {
                            Id = "7",
                            GenderType = "Men",
                            Name = "Soccer"
                        },
                        new
                        {
                            Id = "8",
                            GenderType = "Women",
                            Name = "Soccer"
                        },
                        new
                        {
                            Id = "9",
                            GenderType = "Coed",
                            Name = "Soccer"
                        },
                        new
                        {
                            Id = "10",
                            GenderType = "Men",
                            Name = "Flag Football"
                        },
                        new
                        {
                            Id = "11",
                            GenderType = "Women",
                            Name = "Flag Football"
                        },
                        new
                        {
                            Id = "12",
                            GenderType = "Coed",
                            Name = "Flag Football"
                        });
                });

            modelBuilder.Entity("AmateurLeague.Entity.Team", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<string>("LeagueName")
                        .IsRequired();

                    b.HasKey("Name")
                        .HasName("PK_Teams");

                    b.HasIndex("LeagueName");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("AmateurLeague.Entity.TeamPlayer", b =>
                {
                    b.Property<string>("TeamId");

                    b.Property<string>("PlayerId");

                    b.HasKey("TeamId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("TeamPlayer");
                });

            modelBuilder.Entity("AmateurLeague.Entity.League", b =>
                {
                    b.HasOne("AmateurLeague.Entity.Sport", "Sport")
                        .WithMany("Leagues")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AmateurLeague.Entity.Team", b =>
                {
                    b.HasOne("AmateurLeague.Entity.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AmateurLeague.Entity.TeamPlayer", b =>
                {
                    b.HasOne("AmateurLeague.Entity.Player", "Player")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AmateurLeague.Entity.Team", "Team")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
