using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SiteGenerator.Domain.Entities
{
    public class Website
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("age")]
        public string Age { get; set; }
    }
}
