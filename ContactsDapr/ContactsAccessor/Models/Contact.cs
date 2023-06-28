using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Accessor.Models
{
    public class ContactDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public required string name { get; set; }

        public required string phone { get; set; }
    }

    public class Contact
    {
        public required string Name { get; set; }

        public required string Phone { get; set; }
    }
}
