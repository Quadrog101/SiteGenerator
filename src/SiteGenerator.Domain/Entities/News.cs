using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SiteGenerator.Domain.Entities
{
    public class News
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("alias")]
        public string Alias { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("created_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; }
    }
}
