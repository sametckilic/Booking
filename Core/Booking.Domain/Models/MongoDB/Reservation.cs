using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Models.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Booking.Application.Models.MongoDB;

/// <summary>
/// Represents a 'Reservation' object stored in MongoDB
/// Inherist common props from MongoDbBaseEntity 
/// Implementing IEntity interface
/// </summary>
public class Reservation : Document
{
    
    public string UserId { get; set; }
    public Guid RoomId { get; set; }
    public Guid HotelId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set;}
    public DateTime CreateDate { get; set; }



}
