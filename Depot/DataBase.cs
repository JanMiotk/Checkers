using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Depot
{
    public class DataBase : DbContext
    {
        private readonly string _path;
        public DbSet<User> Users { get; set; }
        public DbSet<Table> Tables { get; set; }

        public DataBase()
        {
            using (var file = new StreamReader(Directory.GetCurrentDirectory()+@"\appsettings.json"))
            {
                var json = file.ReadToEnd();
                var config = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
                _path = config["ConnectionStrings"]["Sql"];
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_path);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        ID = 1,
                        Email = "janek@wp.pl",
                        Nick = "spacehunter",
                        Name = "Jan",
                        Surname = "Kowalski",
                        Password = "password"
                    },
                    new User
                    {
                        ID = 2,
                        Email = "kasia@wp.pl",
                        Nick = "darkqueen",
                        Name = "Katarzyna",
                        Surname = "Ebut",
                        Password = "password"
                    },
                    new User
                    {
                        ID = 3,
                        Email = "zenek@wp.pl",
                        Nick = "imperator",
                        Name = "Zenon",
                        Surname = "Dacki",
                        Password = "password"
                    },
                    new User
                    {
                        ID = 4,
                        Email = "seba@wp.pl",
                        Nick = "conqueror",
                        Name = "Sebastian",
                        Surname = "Roden",
                        Password = "password"
                    }
            );

            modelBuilder.Entity<Table>()
                .HasData(
                    new Table
                    {
                        ID = 1,
                        Name = "Master",
                        Link = "/Room/Master",
                    },
                    new Table
                    {
                        ID = 2,
                        Name = "Average",
                        Link = "/Room/Average",
                    },
                    new Table
                    {
                        ID = 3,
                        Name = "Novice",
                        Link = "/Room/Novice",
                    },
                    new Table
                    {
                        ID = 4,
                        Name = "Beginner",
                        Link = "/Room/Beginner",
                    }
            );

        }
    }
}
