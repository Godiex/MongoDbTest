using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbApi.Models;

public class BookSimple
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string Page { get; set; }
    public Author Author { get; set; }
}