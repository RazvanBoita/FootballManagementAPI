﻿// <auto-generated />
using System;
using FootballMgm.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FootballMgm.Api.Migrations
{
    [DbContext(typeof(FootballDbContext))]
    partial class FootballDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FootballMgm.Api.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("XpYears")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId")
                        .IsUnique()
                        .HasFilter("[TeamId] IS NOT NULL");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.CoachRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("XpYears")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CoachRequests");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.Footballer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("PreferredFoot")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("ShirtNumber")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Footballers");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.FootballerRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("PrefferedFoot")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("ShirtNumber")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FootballerRequests");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.Admin", b =>
                {
                    b.HasOne("FootballMgm.Api.Models.AppUser", "User")
                        .WithOne()
                        .HasForeignKey("FootballMgm.Api.Models.Admin", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.Coach", b =>
                {
                    b.HasOne("FootballMgm.Api.Models.Team", "Team")
                        .WithOne("Coach")
                        .HasForeignKey("FootballMgm.Api.Models.Coach", "TeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballMgm.Api.Models.AppUser", "User")
                        .WithOne()
                        .HasForeignKey("FootballMgm.Api.Models.Coach", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.CoachRequest", b =>
                {
                    b.HasOne("FootballMgm.Api.Models.AppUser", "User")
                        .WithMany("CoachRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.Footballer", b =>
                {
                    b.HasOne("FootballMgm.Api.Models.Team", "Team")
                        .WithMany("Footballers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballMgm.Api.Models.AppUser", "User")
                        .WithOne()
                        .HasForeignKey("FootballMgm.Api.Models.Footballer", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.FootballerRequest", b =>
                {
                    b.HasOne("FootballMgm.Api.Models.AppUser", "User")
                        .WithMany("FootballerRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.AppUser", b =>
                {
                    b.Navigation("CoachRequests");

                    b.Navigation("FootballerRequests");
                });

            modelBuilder.Entity("FootballMgm.Api.Models.Team", b =>
                {
                    b.Navigation("Coach")
                        .IsRequired();

                    b.Navigation("Footballers");
                });
#pragma warning restore 612, 618
        }
    }
}
