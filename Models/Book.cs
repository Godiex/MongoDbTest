using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbApi.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string Page { get; set; }
    public Author Author { get; set; }
    public Editorial Editorial { get; set; }
    public Category Category { get; set; }
    
    
}

public class Author
{
    public string Code { get; set; }
    public string Name { get; set; }
}

public class Editorial
{
    public string Code { get; set; }
    public string Name { get; set; }
}

public class Category
{
    public string Code { get; set; }
    public string Name { get; set; }
}