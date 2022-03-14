namespace MongoDbApi.Models;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CollectionName { get; set; } = null!;
    
    public string CollectionUser { get; set; } = null!;
    public string CollectionLoan { get; set; } = null!;
    public string CollectionBook { get; set; } = null!;
}