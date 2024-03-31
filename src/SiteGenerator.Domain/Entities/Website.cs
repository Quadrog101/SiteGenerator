using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Options;

namespace SiteGenerator.Domain.Entities
{
    public class Website
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("type")]
        public string Type { get; set; }
        
        [BsonElement("alias")]
        public string Alias { get; set; }

        [BsonElement("ownerId")]
        public long OwnerId { get; set; }
        
        public BsonDocument Data { get; set; }
    }
}
