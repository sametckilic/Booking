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
    /// Represents the EF Core configuration for the 'Hotel' entity.
    /// Implements IEntityTypeConfiguration interface to define entity's schema
    /// </summary>
    public class HotelEntityConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("hotels", BookingSQLServerDbContext.DEFAULT_SCHEMA);

            builder.HasKey(e => e.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

        }
    }
}
