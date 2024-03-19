using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Caching;
using Booking.Application.Features;
using Booking.Application.Interfaces;
using Booking.Application.Interfaces.Managers;
using Booking.Application.Interfaces.Queue;
using Booking.Application.Interfaces.Repositories.EntityFramework;
using Booking.Application.Interfaces.Repositories.Factory;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Infrastructure.Contexts;
using Booking.Persistence.Caching;
using Booking.Persistence.Queue;
using Booking.Persistence.Repositories.EntityFramework;
using Booking.Persistence.Repositories.MongoDb;
using Booking.Persistence.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Booking.Persistence.Extensions
{
    /// <summary>
    /// Adds infrastructure related services for provide IServiceCollection
    /// </summary>
    public static class Registration
    {
        public static IServiceCollection AddInfrastrucutreRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            var connStr = configuration.GetConnectionString("SQLConnectionString");

            services.AddDbContext<BookingSQLServerDbContext>(conf =>
            {
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }, ServiceLifetime.Singleton);


            services.AddSingleton<ITokenHandler, TokenHandler>();

            // Redis Connection

            var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));

            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddSingleton<ICacheService, RedisCacheService>();



            services.AddSingleton<IBillingPaymentRepository, BillingPaymentRepository>();
            services.AddSingleton<IHotelRepository, HotelRepository>();
            services.AddSingleton<IHotelRoomRepository, HotelRoomRepository>();
            services.AddSingleton<IHotelRoomPriceRepository, HotelRoomPriceRepository>();
            //services.AddSingleton<IUserRepository, UserRepository>();
            //services.AddSingleton<IReservationRepository, ReservationRepository>();

            services.AddSingleton<IReservationManager, ReservationService>();
            services.AddSingleton<IUserManager, UserService>();
            services.AddSingleton<IHotelManager, HotelService>();
            services.AddSingleton<IBillingManager, BillingService>();
            services.AddSingleton<IAuthManager, AuthService>(); 
            



            services.AddSingleton<IMongoRepositoryFactory, MongoRepositoryFactory>();

            services.AddTransient<IRabbitmqFactory, RabbitmqFactory>();


            return services;
        }
    }
}
