using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLayer.Models
{
	public abstract class BaseModel
	{
		[BsonElement("_id")]
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
	}
}
