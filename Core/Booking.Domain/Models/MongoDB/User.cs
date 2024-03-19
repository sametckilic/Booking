using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Models.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Booking.Application.Models.MongoDB;
/// <summary>
/// Represents a 'User' object stored in MongoDB
/// Inherist common props from MongoDbBaseEntity 
/// Implementing IEntity interface
/// </summary>
public class User : Document
{
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; }
}
