﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Persistence.MongoDbConfigurations.Models
{
    /// <summary>
    /// Represents MongoDB connection settings for app
    /// </summary>
    public class MongoDbSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
