using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim14HCI.Model
{
    class SerbiaRailwayContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Train> trains { get; set; }
        public DbSet<Station> stations { get; set; }
        public DbSet<LinkedStation> linkedStations { get; set; }
        public DbSet<TrainLine> trainLines { get; set; }
        public DbSet<OnWayStation> onWayStations { get; set; }
        public DbSet<Departure> departures { get; set; }
        public DbSet<Ticket> tickets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["tim14HCI"].ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>().HasKey(u => u.UserID);

            modelBuilder.Entity<User>()
                .HasMany<Ticket>(u => u.Tickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            //Train
            modelBuilder.Entity<Train>().HasKey(t => t.TrainID);

            modelBuilder.Entity<Train>()
                .HasMany<TrainLine>(t => t.TrainLines)
                .WithOne(tl => tl.Train)
                .HasForeignKey(tl => tl.TrainID)
                .OnDelete(DeleteBehavior.Cascade);

            //Station
            modelBuilder.Entity<Station>().HasKey(s => s.StationID);

            //Linked Station
            modelBuilder.Entity<LinkedStation>().HasKey(k => new { k.Station1ID, k.Station2ID });

            modelBuilder.Entity<LinkedStation>()
                .HasOne<Station>(ls => ls.Station1)
                .WithMany(s => s.stations1)
                .HasForeignKey(ls => ls.Station1ID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LinkedStation>()
                .HasOne<Station>(ls => ls.Station2)
                .WithMany(s => s.stations2)
                .HasForeignKey(ls => ls.Station2ID)
                .OnDelete(DeleteBehavior.NoAction);


            //TrainLine
            modelBuilder.Entity<TrainLine>().HasKey(tl => tl.TrainLineID);

            modelBuilder.Entity<TrainLine>()
                .HasOne<Train>(tl => tl.Train)
                .WithMany(t => t.TrainLines)
                .HasForeignKey(t => t.TrainID);

            modelBuilder.Entity<TrainLine>()
                .HasOne<Station>(tl => tl.StartStation);


            //OnWayStation
            modelBuilder.Entity<OnWayStation>().HasKey(ow => ow.OnWayStationID);

            modelBuilder.Entity<OnWayStation>()
                .HasOne<TrainLine>(ow => ow.TrainLine)
                .WithMany(tl => tl.OnWayStations)
                .HasForeignKey(ow => ow.TrainLineID)
                .OnDelete(DeleteBehavior.NoAction);

            /*
            modelBuilder.Entity<OnWayStation>()
                .HasOne<TrainLine>(ow => ow.TrainLine)
                .WithMany(tl => tl.OnWayStations)
                .HasForeignKey(ow => ow.TrainLineID)
                .OnDelete(DeleteBehavior.NoAction);
            */

            modelBuilder.Entity<OnWayStation>().HasOne<Station>(ows => ows.Station);


            //Departure
            modelBuilder.Entity<Departure>().HasKey(dep => dep.DepartureID);

            modelBuilder.Entity<Departure>()
                .HasOne<TrainLine>(dep => dep.TrainLine)
                .WithMany(tl => tl.Departures)
                .HasForeignKey(dep => dep.TrainLineID);

            //Ticket
            modelBuilder.Entity<Ticket>().HasKey(t => t.TicketID);


            modelBuilder.Entity<Ticket>()
                .HasOne<User>(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserID);

            modelBuilder.Entity<Ticket>()
                .HasOne<Departure>(t => t.Departure)
                .WithMany(dep => dep.Tickets)
                .HasForeignKey(t => t.DepartureID);


            //SEED

            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, FirstName= "Mladen", LastName= "Gajic" , Password="mladen123", Email="mg00@gmail.com", Phone="05603051", UserRole=UserRole.MANAGER, Tickets=null},
                new User { UserID = 2, FirstName = "Nikola", LastName = "Stepic", Password = "aaa", Email = "ns00@gmail.com", Phone ="123432", UserRole = UserRole.MANAGER, Tickets = null },
                new User { UserID = 3, FirstName = "Jovan", LastName = "Jovanovic", Password = "bbb", Email = "jj00@gmail.com", Phone = "214t5654", UserRole = UserRole.CLIENT, Tickets = null },
                new User { UserID = 4, FirstName = "Djordje", LastName = "Tomic", Password = "ccc", Email = "dt00@gmail.com", Phone = "+381064428108", UserRole = UserRole.CLIENT, Tickets = null }
                );

            modelBuilder.Entity<Train>().HasData(
                new Train { TrainID = 1, Name="Soko", Capacity= 120, MaxSpeed=160 , TrainLines=null},
                new Train { TrainID = 2, Name = "Cira", Capacity = 50, MaxSpeed = 60, TrainLines = null },
                new Train { TrainID = 3, Name = "Krsko", Capacity = 100, MaxSpeed = 100, TrainLines = null },
                new Train { TrainID = 4, Name = "Soko II", Capacity = 200, MaxSpeed = 200, TrainLines = null }
                );

            modelBuilder.Entity<Station>().HasData(
                new Station { StationID = 1, Name = "Beograd", stations1 = null , stations2 =null , position_x =85, position_y=95},
                new Station { StationID = 2, Name = "Novi Sad", stations1 = null, stations2 = null , position_x = 30, position_y= 50},
                new Station { StationID = 3, Name = "Subotica", stations1 = null, stations2 = null , position_x = 40, position_y = 10},
                new Station { StationID = 4, Name = "Zrenjanin", stations1 = null, stations2 = null , position_x = 70 , position_y = 60},
                new Station { StationID = 5, Name = "Niš", stations1 = null, stations2 = null , position_x = 130, position_y= 160},
                new Station { StationID = 6, Name = "Leskovac", stations1 = null, stations2 = null , position_x = 160, position_y= 230}
                );

            modelBuilder.Entity<LinkedStation>().HasData(
                new LinkedStation { Station1ID = 1, Station2ID = 2 },
                new LinkedStation { Station1ID = 1, Station2ID = 3 },
                new LinkedStation { Station1ID = 1, Station2ID = 5 },
                new LinkedStation { Station1ID = 2, Station2ID = 3 },
                new LinkedStation { Station1ID = 5, Station2ID = 6 }
                );


            modelBuilder.Entity<TrainLine>().HasData(
                new TrainLine() { TrainLineID = 1, StartStationID = 1, EndStationID = 1, TrainID = 1 }
                );


            modelBuilder.Entity<OnWayStation>().HasData(
                new OnWayStation() { OnWayStationID = 1, Price = 10, Time = 10, StationID = 1, StationOrder = 1, isEndStation = false, TrainLineID = 1 },
                new OnWayStation() { OnWayStationID = 2, Price = 10, Time = 10, StationID = 2, StationOrder = 2, isEndStation = true, TrainLineID = 1 }
                );

            modelBuilder.Entity<Departure>().HasData(
                new Departure() { DepartureID = 1, StartTime = new DateTime(2022,6,8,12,30,0), TrainLineID = 1 },
                new Departure() { DepartureID = 2, StartTime = new DateTime(2022, 6, 8, 15, 40, 0), TrainLineID = 1 },
                new Departure() { DepartureID = 3, StartTime = new DateTime(2022, 6, 8, 20, 15, 0), TrainLineID = 1 }
                );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket() { TicketID = 1 , DepartureID =1 , ForReservation =false , UserID = 1 }
                );
        }
    }
}
