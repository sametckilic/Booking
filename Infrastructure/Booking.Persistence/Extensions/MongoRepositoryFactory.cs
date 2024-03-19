using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Documents;
using Booking.Application.Extensions;
using Booking.Application.Interfaces.Repositories.Factory;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Domain.Models.SQLServer;
using Booking.Persistence.MongoDbConfigurations.Models;
using Booking.Persistence.Repositories;
using Microsoft.Extensions.Configuration;

namespace Booking.Persistence.Extensions
{
    public class MongoRepositoryFactory : IMongoRepositoryFactory
    {
        private readonly MongoDbSettings _configuration;

        public MongoRepositoryFactory(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("MongoDB").Get<MongoDbSettings>();
        }

        public object CreateRepository<T>() where T : class , new()
        {
            var collectionName = GetCollectionNameFromType(typeof(T));
            return Activator.CreateInstance(typeof(MongoRepository<>).MakeGenericType(typeof(T)), collectionName,
                  _configuration);

        }

        private string GetCollectionNameFromType(Type entityType)
        {
            string collectionName;

            Attribute attr = Attribute.GetCustomAttribute(entityType, typeof(CollectionNameAttribute));

            if (attr != null)
            {
                collectionName = ((CollectionNameAttribute)attr).Name;
            }
            else
            {
                if (typeof(Domain.Models.MongoDB.Document).IsAssignableFrom(entityType))
                {
                    while (entityType?.BaseType != typeof(Domain.Models.MongoDB.Document))
                    {
                        entityType = entityType?.BaseType;
                    }
                }

                collectionName = entityType.Name;
            }

            return collectionName;
        }

        public IMongoRepository<T> GetRepo<T>() where T : class, new()
        {
            return CreateRepository<T>() as IMongoRepository<T>;
        }
    }
}
