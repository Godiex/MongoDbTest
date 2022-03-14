using System.Reflection.Metadata.Ecma335;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbApi.Models;

public class Loan
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _Id { get; set; }
    public string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LoanDetail LoanDetail { get; set; }
    public List<string> Books { get; set; }
}

public class LoanDetail
{
    public string State { get; set; }
    public string Ubication { get; set; }
}