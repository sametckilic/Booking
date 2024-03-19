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
    /// Represents the EF Core configuration for the 'BillingPayment' entity.
    /// Implements IEntityTypeConfiguration interface to define entity's schema
    /// </summary>
    public class BillingPaymentEntityConfiguration : IEntityTypeConfiguration<BillingPayment>
    {
        public void Configure(EntityTypeBuilder<BillingPayment> builder)
        {

            builder.ToTable("billingpayments", BookingSQLServerDbContext.DEFAULT_SCHEMA);

            builder.HasKey(e => e.Id);

            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.CreateDate).ValueGeneratedOnAdd();

            builder.HasOne(i => i.Hotel)
                .WithMany(i => i.BillingPayments)
                .HasForeignKey(i => i.HotelId);



        }
    }
}
