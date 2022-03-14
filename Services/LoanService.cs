using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbApi.Models;

namespace MongoDbApi.Services;

public class LoanService
{
    private readonly IMongoCollection<Loan> _loansCollection;

    public LoanService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _loansCollection = mongoDatabase.GetCollection<Loan>(
            databaseSettings.Value.CollectionLoan);
    }
    
    public async Task<List<Loan>> GetAsync() =>
        await _loansCollection.Find(_ => true).ToListAsync();

    public async Task<Loan?> GetAsync(string id) =>
        await _loansCollection.Find(x => x._Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Loan newloan) =>
        await _loansCollection.InsertOneAsync(newloan);

    public async Task UpdateAsync(string id, Loan updatedloan) =>
        await _loansCollection.ReplaceOneAsync(x => x._Id == id, updatedloan);

    public async Task RemoveAsync(string id) =>
        await _loansCollection.DeleteOneAsync(x => x._Id == id);
}