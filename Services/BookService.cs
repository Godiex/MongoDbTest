using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbApi.Models;

namespace MongoDbApi.Services;

public class BookService
{
    private readonly IMongoCollection<Book> _booksCollection;
    private readonly IMongoCollection<BookSimple> _booksTestCollection;

    public BookService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _booksCollection = mongoDatabase.GetCollection<Book>(
            databaseSettings.Value.CollectionBook);
        
        _booksTestCollection = mongoDatabase.GetCollection<BookSimple>(
            databaseSettings.Value.CollectionBook);
    }
    
    public async Task<List<Book>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Book?> GetAsync(string id) =>
        await _booksCollection.Find(x => x._Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book newBook) =>
        await _booksCollection.InsertOneAsync(newBook);
    
    public async Task CreateWithoutSomeAttributesAsync(BookSimple newBook) =>
        await _booksTestCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Book updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x._Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x._Id == id);
}