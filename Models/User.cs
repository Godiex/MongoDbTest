using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbApi.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _Id { get; set; }
    public string Name { get; set; }
    public Direction Direction { get; set; }
}

public class Direction
{
    public string Country { get; set; }
    public string Department { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
}