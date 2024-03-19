using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.SQLServer;
using Booking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Persistence.EntityFrameworkConfigurations
{
    /// <summary>
    /// Represents the EF Core configuration for the 'HotelRoom' entity.
    /// Implements IEntityTypeConfiguration interface to define entity's schema
    /// </summary>
    public class HotelRoomEntityConfiguration : IEntityTypeConfiguration<HotelRoom>
    {
        public void Configure(EntityTypeBuilder<HotelRoom> builder)
        {

            builder.ToTable("hotelrooms", BookingSQLServerDbContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.HasOne(i => i.Hotel)
                .WithMany(i => i.HotelRooms)
                .HasForeignKey(i => i.HotelId);
        }
    }
}
