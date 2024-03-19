using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Models.SQLServer;
using Booking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Persistence.EntityFrameworkConfigurations
{
    /// <summary>
    /// Represents the EF Core configuration for the 'HotelRoomPrice' entity.
    /// Implements IEntityTypeConfiguration interface to define entity's schema
    /// </summary>
    public class HotelRoomPriceEntityConfiguration : IEntityTypeConfiguration<HotelRoomPrice>
    {
        public void Configure(EntityTypeBuilder<HotelRoomPrice> builder)
        {

            builder.ToTable("hotelroomprices", BookingSQLServerDbContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();


            builder.HasOne(e => e.HotelRoom)
                .WithMany(e => e.HotelRoomPrices)
                .HasForeignKey(e => e.HotelRoomId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(e => e.Hotel)
                .WithMany(e => e.HotelRoomPrices)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
