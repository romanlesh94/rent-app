﻿// <auto-generated />
using System;
using HouseApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HouseApi.Migrations
{
    [DbContext(typeof(HouseDbContext))]
    [Migration("20230928170424_UpdatedBooking")]
    partial class UpdatedBooking
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HouseApi.Models.Booking.HouseBooking", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("date");

                    b.Property<long>("GuestId")
                        .HasColumnType("bigint");

                    b.Property<long>("HouseId")
                        .HasColumnType("bigint");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("HouseApi.Models.House", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Rules")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("HouseApi.Models.HousePropertyMapping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("HouseId")
                        .HasColumnType("bigint");

                    b.Property<long>("PropertyId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("HousePropertyMappings");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            HouseId = 1L,
                            PropertyId = 2L
                        },
                        new
                        {
                            Id = 2L,
                            HouseId = 1L,
                            PropertyId = 3L
                        },
                        new
                        {
                            Id = 3L,
                            HouseId = 1L,
                            PropertyId = 5L
                        },
                        new
                        {
                            Id = 4L,
                            HouseId = 2L,
                            PropertyId = 2L
                        },
                        new
                        {
                            Id = 5L,
                            HouseId = 2L,
                            PropertyId = 13L
                        },
                        new
                        {
                            Id = 6L,
                            HouseId = 2L,
                            PropertyId = 11L
                        },
                        new
                        {
                            Id = 7L,
                            HouseId = 3L,
                            PropertyId = 10L
                        },
                        new
                        {
                            Id = 8L,
                            HouseId = 3L,
                            PropertyId = 9L
                        },
                        new
                        {
                            Id = 9L,
                            HouseId = 3L,
                            PropertyId = 7L
                        });
                });

            modelBuilder.Entity("HouseApi.Models.Property", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("PropertyText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HouseProperties");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            PropertyText = "Free Wi-Fi"
                        },
                        new
                        {
                            Id = 2L,
                            PropertyText = "King-size bed"
                        },
                        new
                        {
                            Id = 3L,
                            PropertyText = "Free parking"
                        },
                        new
                        {
                            Id = 4L,
                            PropertyText = "Breakfast included"
                        },
                        new
                        {
                            Id = 5L,
                            PropertyText = "Pet-friendly"
                        },
                        new
                        {
                            Id = 6L,
                            PropertyText = "Non-smoking"
                        },
                        new
                        {
                            Id = 7L,
                            PropertyText = "Swimming pool"
                        },
                        new
                        {
                            Id = 8L,
                            PropertyText = "Safe for children"
                        },
                        new
                        {
                            Id = 9L,
                            PropertyText = "Transfer from the airport"
                        },
                        new
                        {
                            Id = 10L,
                            PropertyText = "Beautiful view"
                        },
                        new
                        {
                            Id = 11L,
                            PropertyText = "BBQ"
                        },
                        new
                        {
                            Id = 12L,
                            PropertyText = "Balcony"
                        },
                        new
                        {
                            Id = 13L,
                            PropertyText = "Outside area"
                        },
                        new
                        {
                            Id = 14L,
                            PropertyText = "TV"
                        },
                        new
                        {
                            Id = 15L,
                            PropertyText = "Washing machine"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
