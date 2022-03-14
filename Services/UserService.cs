using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbApi.Models;

namespace MongoDbApi.Services;

public class UserService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            databaseSettings.Value.CollectionUser);
    }
    
    public async Task<List<User>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _usersCollection.Find(x => x._Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser)
    {
        var existUser = await ExistUser(newUser.Name);
        if(!existUser)
            await _usersCollection.InsertOneAsync(newUser);
    }

    public async Task UpdateAsync(string id, User updatedUser)
    {
        var existUser = await ExistUser(updatedUser.Name);
        if(existUser)
            await _usersCollection.ReplaceOneAsync(x => x._Id == id, updatedUser);
    }

    public async Task RemoveAsync(string id)
    {
        await _usersCollection.DeleteOneAsync(x => x._Id == id);
    }
    
    private async Task<bool> ExistUser(string userName)
    {
        return await _usersCollection.Find(x => x.Name == userName).AnyAsync();
    }
}