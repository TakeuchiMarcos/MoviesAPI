﻿// <auto-generated />
using System;
using FilmesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MoviesAPI.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20230803201048_Initial Create")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FilmesAPI.Models.Adress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Complement")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("longtext");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("neighborhood")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("FilmesAPI.Models.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AdressId")
                        .IsUnique();

                    b.ToTable("Cinemas");
                });

            modelBuilder.Entity("FilmesAPI.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("FilmesAPI.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<int>("MaxSeats")
                        .HasColumnType("int");

                    b.Property<int>("OcupiedSeats")
                        .HasColumnType("int");

                    b.HasKey("Id", "CinemaId");

                    b.HasIndex("CinemaId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("FilmesAPI.Models.Session", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SessionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("MovieId", "CinemaId", "RoomId", "SessionDate");

                    b.HasAlternateKey("Id");

                    b.HasIndex("RoomId", "CinemaId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("FilmesAPI.Models.Cinema", b =>
                {
                    b.HasOne("FilmesAPI.Models.Adress", "Adress")
                        .WithOne("Cinema")
                        .HasForeignKey("FilmesAPI.Models.Cinema", "AdressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adress");
                });

            modelBuilder.Entity("FilmesAPI.Models.Room", b =>
                {
                    b.HasOne("FilmesAPI.Models.Cinema", "Cinema")
                        .WithMany("Rooms")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("FilmesAPI.Models.Session", b =>
                {
                    b.HasOne("FilmesAPI.Models.Movie", "Movie")
                        .WithMany("Sessions")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmesAPI.Models.Room", "Room")
                        .WithMany("Sessions")
                        .HasForeignKey("RoomId", "CinemaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("FilmesAPI.Models.Adress", b =>
                {
                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("FilmesAPI.Models.Cinema", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("FilmesAPI.Models.Movie", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("FilmesAPI.Models.Room", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
