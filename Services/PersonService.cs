using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbApi.Models;

namespace MongoDbApi.Services;

public class PersonService
{
    private readonly IMongoCollection<Person> _personsCollection;

    public PersonService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _personsCollection = mongoDatabase.GetCollection<Person>(
            databaseSettings.Value.CollectionName);
    }
    
    public async Task<List<Person>> GetAsync() =>
        await _personsCollection.Find(_ => true).ToListAsync();

    public async Task<Person?> GetAsync(string id) =>
        await _personsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Person newPerson) =>
        await _personsCollection.InsertOneAsync(newPerson);

    public async Task UpdateAsync(string id, Person updatedPerson) =>
        await _personsCollection.ReplaceOneAsync(x => x.Id == id, updatedPerson);

    public async Task RemoveAsync(string id) =>
        await _personsCollection.DeleteOneAsync(x => x.Id == id);
}