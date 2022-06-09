﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tim14HCI.Model;

namespace Tim14HCI.Migrations
{
    [DbContext(typeof(SerbiaRailwayContext))]
    partial class SerbiaRailwayContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tim14HCI.Model.Departure", b =>
                {
                    b.Property<int>("DepartureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrainLineID")
                        .HasColumnType("int");

                    b.HasKey("DepartureID");

                    b.HasIndex("TrainLineID");

                    b.ToTable("departures");

                    b.HasData(
                        new
                        {
                            DepartureID = 1,
                            StartTime = new DateTime(2022, 6, 8, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TrainLineID = 1
                        },
                        new
                        {
                            DepartureID = 2,
                            StartTime = new DateTime(2022, 6, 8, 15, 40, 0, 0, DateTimeKind.Unspecified),
                            TrainLineID = 1
                        },
                        new
                        {
                            DepartureID = 3,
                            StartTime = new DateTime(2022, 6, 8, 20, 15, 0, 0, DateTimeKind.Unspecified),
                            TrainLineID = 1
                        },
                        new
                        {
                            DepartureID = 4,
                            StartTime = new DateTime(2022, 6, 14, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TrainLineID = 2
                        },
                        new
                        {
                            DepartureID = 5,
                            StartTime = new DateTime(2022, 6, 14, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            TrainLineID = 2
                        },
                        new
                        {
                            DepartureID = 6,
                            StartTime = new DateTime(2022, 6, 14, 18, 40, 0, 0, DateTimeKind.Unspecified),
                            TrainLineID = 2
                        },
                        new
                        {
                            DepartureID = 7,
                            StartTime = new DateTime(2022, 6, 14, 21, 15, 0, 0, DateTimeKind.Unspecified),
                            TrainLineID = 2
                        },
                        new
                        {
                            DepartureID = 8,
                            StartTime = new DateTime(2022, 6, 15, 15, 40, 0, 0, DateTimeKind.Unspecified),
                            TrainLineID = 2
                        },
                        new
                        {
                            DepartureID = 9,
                            StartTime = new DateTime(2022, 6, 15, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TrainLineID = 2
                        });
                });

            modelBuilder.Entity("Tim14HCI.Model.LinkedStation", b =>
                {
                    b.Property<int>("Station1ID")
                        .HasColumnType("int");

                    b.Property<int>("Station2ID")
                        .HasColumnType("int");

                    b.HasKey("Station1ID", "Station2ID");

                    b.HasIndex("Station2ID");

                    b.ToTable("linkedStations");

                    b.HasData(
                        new
                        {
                            Station1ID = 1,
                            Station2ID = 2
                        },
                        new
                        {
                            Station1ID = 1,
                            Station2ID = 3
                        },
                        new
                        {
                            Station1ID = 1,
                            Station2ID = 5
                        },
                        new
                        {
                            Station1ID = 2,
                            Station2ID = 3
                        },
                        new
                        {
                            Station1ID = 5,
                            Station2ID = 6
                        });
                });

            modelBuilder.Entity("Tim14HCI.Model.OnWayStation", b =>
                {
                    b.Property<int>("OnWayStationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("StationID")
                        .HasColumnType("int");

                    b.Property<int>("StationOrder")
                        .HasColumnType("int");

                    b.Property<float>("Time")
                        .HasColumnType("real");

                    b.Property<int>("TrainLineID")
                        .HasColumnType("int");

                    b.Property<bool>("isEndStation")
                        .HasColumnType("bit");

                    b.HasKey("OnWayStationID");

                    b.HasIndex("StationID");

                    b.HasIndex("TrainLineID");

                    b.ToTable("onWayStations");

                    b.HasData(
                        new
                        {
                            OnWayStationID = 1,
                            Price = 800f,
                            StationID = 2,
                            StationOrder = 1,
                            Time = 30f,
                            TrainLineID = 1,
                            isEndStation = true
                        },
                        new
                        {
                            OnWayStationID = 2,
                            Price = 400f,
                            StationID = 2,
                            StationOrder = 1,
                            Time = 35f,
                            TrainLineID = 2,
                            isEndStation = false
                        },
                        new
                        {
                            OnWayStationID = 3,
                            Price = 550f,
                            StationID = 1,
                            StationOrder = 2,
                            Time = 35f,
                            TrainLineID = 2,
                            isEndStation = false
                        },
                        new
                        {
                            OnWayStationID = 4,
                            Price = 700f,
                            StationID = 5,
                            StationOrder = 3,
                            Time = 50f,
                            TrainLineID = 2,
                            isEndStation = false
                        },
                        new
                        {
                            OnWayStationID = 5,
                            Price = 400f,
                            StationID = 6,
                            StationOrder = 4,
                            Time = 30f,
                            TrainLineID = 2,
                            isEndStation = true
                        });
                });

            modelBuilder.Entity("Tim14HCI.Model.Station", b =>
                {
                    b.Property<int>("StationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("position_x")
                        .HasColumnType("int");

                    b.Property<int>("position_y")
                        .HasColumnType("int");

                    b.HasKey("StationID");

                    b.ToTable("stations");

                    b.HasData(
                        new
                        {
                            StationID = 1,
                            Name = "Beograd",
                            position_x = 85,
                            position_y = 95
                        },
                        new
                        {
                            StationID = 2,
                            Name = "Novi Sad",
                            position_x = 30,
                            position_y = 50
                        },
                        new
                        {
                            StationID = 3,
                            Name = "Subotica",
                            position_x = 40,
                            position_y = 10
                        },
                        new
                        {
                            StationID = 4,
                            Name = "Zrenjanin",
                            position_x = 70,
                            position_y = 60
                        },
                        new
                        {
                            StationID = 5,
                            Name = "Niš",
                            position_x = 130,
                            position_y = 160
                        },
                        new
                        {
                            StationID = 6,
                            Name = "Leskovac",
                            position_x = 160,
                            position_y = 230
                        });
                });

            modelBuilder.Entity("Tim14HCI.Model.Ticket", b =>
                {
                    b.Property<int>("TicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartureID")
                        .HasColumnType("int");

                    b.Property<int>("EndStationID")
                        .HasColumnType("int");

                    b.Property<bool>("ForReservation")
                        .HasColumnType("bit");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Seat")
                        .HasColumnType("int");

                    b.Property<int>("StartStationID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("TicketID");

                    b.HasIndex("DepartureID");

                    b.HasIndex("EndStationID");

                    b.HasIndex("StartStationID");

                    b.HasIndex("UserID");

                    b.ToTable("tickets");

                    b.HasData(
                        new
                        {
                            TicketID = 1,
                            DepartureID = 1,
                            EndStationID = 2,
                            ForReservation = false,
                            Price = 800.0,
                            Seat = 2,
                            StartStationID = 1,
                            UserID = 4
                        },
                        new
                        {
                            TicketID = 2,
                            DepartureID = 4,
                            EndStationID = 2,
                            ForReservation = true,
                            Price = 400.0,
                            Seat = 1,
                            StartStationID = 3,
                            UserID = 3
                        },
                        new
                        {
                            TicketID = 3,
                            DepartureID = 5,
                            EndStationID = 1,
                            ForReservation = false,
                            Price = 950.0,
                            Seat = 2,
                            StartStationID = 3,
                            UserID = 4
                        },
                        new
                        {
                            TicketID = 4,
                            DepartureID = 6,
                            EndStationID = 5,
                            ForReservation = true,
                            Price = 1650.0,
                            Seat = 3,
                            StartStationID = 3,
                            UserID = 3
                        },
                        new
                        {
                            TicketID = 5,
                            DepartureID = 7,
                            EndStationID = 6,
                            ForReservation = false,
                            Price = 2050.0,
                            Seat = 4,
                            StartStationID = 3,
                            UserID = 4
                        },
                        new
                        {
                            TicketID = 6,
                            DepartureID = 8,
                            EndStationID = 2,
                            ForReservation = false,
                            Price = 400.0,
                            Seat = 5,
                            StartStationID = 3,
                            UserID = 3
                        },
                        new
                        {
                            TicketID = 7,
                            DepartureID = 9,
                            EndStationID = 5,
                            ForReservation = true,
                            Price = 1650.0,
                            Seat = 6,
                            StartStationID = 3,
                            UserID = 4
                        },
                        new
                        {
                            TicketID = 8,
                            DepartureID = 4,
                            EndStationID = 1,
                            ForReservation = false,
                            Price = 950.0,
                            Seat = 7,
                            StartStationID = 3,
                            UserID = 3
                        },
                        new
                        {
                            TicketID = 9,
                            DepartureID = 5,
                            EndStationID = 6,
                            ForReservation = false,
                            Price = 2050.0,
                            Seat = 8,
                            StartStationID = 3,
                            UserID = 4
                        },
                        new
                        {
                            TicketID = 10,
                            DepartureID = 6,
                            EndStationID = 2,
                            ForReservation = true,
                            Price = 400.0,
                            Seat = 9,
                            StartStationID = 3,
                            UserID = 3
                        },
                        new
                        {
                            TicketID = 11,
                            DepartureID = 7,
                            EndStationID = 5,
                            ForReservation = true,
                            Price = 1650.0,
                            Seat = 10,
                            StartStationID = 3,
                            UserID = 4
                        },
                        new
                        {
                            TicketID = 12,
                            DepartureID = 8,
                            EndStationID = 1,
                            ForReservation = false,
                            Price = 950.0,
                            Seat = 12,
                            StartStationID = 3,
                            UserID = 3
                        });
                });

            modelBuilder.Entity("Tim14HCI.Model.Train", b =>
                {
                    b.Property<int>("TrainID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<float>("MaxSpeed")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrainID");

                    b.ToTable("trains");

                    b.HasData(
                        new
                        {
                            TrainID = 1,
                            Capacity = 120,
                            MaxSpeed = 160f,
                            Name = "Soko"
                        },
                        new
                        {
                            TrainID = 2,
                            Capacity = 50,
                            MaxSpeed = 60f,
                            Name = "Cira"
                        },
                        new
                        {
                            TrainID = 3,
                            Capacity = 100,
                            MaxSpeed = 100f,
                            Name = "Krsko"
                        },
                        new
                        {
                            TrainID = 4,
                            Capacity = 200,
                            MaxSpeed = 200f,
                            Name = "Soko II"
                        });
                });

            modelBuilder.Entity("Tim14HCI.Model.TrainLine", b =>
                {
                    b.Property<int>("TrainLineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EndStationID")
                        .HasColumnType("int");

                    b.Property<int?>("EndStationOnWayStationID")
                        .HasColumnType("int");

                    b.Property<int>("StartStationID")
                        .HasColumnType("int");

                    b.Property<int>("TrainID")
                        .HasColumnType("int");

                    b.HasKey("TrainLineID");

                    b.HasIndex("EndStationOnWayStationID");

                    b.HasIndex("StartStationID");

                    b.HasIndex("TrainID");

                    b.ToTable("trainLines");

                    b.HasData(
                        new
                        {
                            TrainLineID = 1,
                            EndStationID = 1,
                            StartStationID = 1,
                            TrainID = 1
                        },
                        new
                        {
                            TrainLineID = 2,
                            EndStationID = 6,
                            StartStationID = 3,
                            TrainID = 4
                        });
                });

            modelBuilder.Entity("Tim14HCI.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Email = "mg00@gmail.com",
                            FirstName = "Mladen",
                            LastName = "Gajic",
                            Password = "mladen123",
                            Phone = "05603051",
                            UserRole = 1
                        },
                        new
                        {
                            UserID = 2,
                            Email = "ns00@gmail.com",
                            FirstName = "Nikola",
                            LastName = "Stepic",
                            Password = "aaa",
                            Phone = "123432",
                            UserRole = 1
                        },
                        new
                        {
                            UserID = 3,
                            Email = "jj00@gmail.com",
                            FirstName = "Jovan",
                            LastName = "Jovanovic",
                            Password = "bbb",
                            Phone = "214t5654",
                            UserRole = 0
                        },
                        new
                        {
                            UserID = 4,
                            Email = "dt00@gmail.com",
                            FirstName = "Djordje",
                            LastName = "Tomic",
                            Password = "ccc",
                            Phone = "+381064428108",
                            UserRole = 0
                        });
                });

            modelBuilder.Entity("Tim14HCI.Model.Departure", b =>
                {
                    b.HasOne("Tim14HCI.Model.TrainLine", "TrainLine")
                        .WithMany("Departures")
                        .HasForeignKey("TrainLineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tim14HCI.Model.LinkedStation", b =>
                {
                    b.HasOne("Tim14HCI.Model.Station", "Station1")
                        .WithMany("stations1")
                        .HasForeignKey("Station1ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Tim14HCI.Model.Station", "Station2")
                        .WithMany("stations2")
                        .HasForeignKey("Station2ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Tim14HCI.Model.OnWayStation", b =>
                {
                    b.HasOne("Tim14HCI.Model.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tim14HCI.Model.TrainLine", "TrainLine")
                        .WithMany("OnWayStations")
                        .HasForeignKey("TrainLineID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Tim14HCI.Model.Ticket", b =>
                {
                    b.HasOne("Tim14HCI.Model.Departure", "Departure")
                        .WithMany("Tickets")
                        .HasForeignKey("DepartureID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tim14HCI.Model.Station", "EndStation")
                        .WithMany("TicketsEndStation")
                        .HasForeignKey("EndStationID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Tim14HCI.Model.Station", "StartStation")
                        .WithMany("TicketsStartStation")
                        .HasForeignKey("StartStationID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Tim14HCI.Model.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tim14HCI.Model.TrainLine", b =>
                {
                    b.HasOne("Tim14HCI.Model.OnWayStation", "EndStation")
                        .WithMany()
                        .HasForeignKey("EndStationOnWayStationID");

                    b.HasOne("Tim14HCI.Model.Station", "StartStation")
                        .WithMany()
                        .HasForeignKey("StartStationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tim14HCI.Model.Train", "Train")
                        .WithMany("TrainLines")
                        .HasForeignKey("TrainID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
