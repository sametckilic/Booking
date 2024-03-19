using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.SQLServer;
using Booking.Domain.Models.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace Booking.Infrastructure.Contexts
{
    /// <summary>
    /// Represents the Entity Framework DbContext for managin entites
    /// </summary>
    public class BookingSQLServerDbContext : DbContext
    {
        // represents the default schema name in the database
        public const string DEFAULT_SCHEMA = "dbo";

        public BookingSQLServerDbContext()
        {
        }
        public BookingSQLServerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<BillingPayment> BillingPayments { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomPrice> HotelRoomPrices { get; set;} 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
 

            if (!optionsBuilder.IsConfigured)
            {
                var connStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SQLConnectionString").ToString();
                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
