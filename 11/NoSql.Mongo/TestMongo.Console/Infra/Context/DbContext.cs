using MongoDB.Driver;
using TestMongo.Console.Models;

namespace TestMongo.Console.Infra.Context
{
    public class DbContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public DbContext(string connectionString, string dbName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(dbName);
        }

        public IMongoCollection<Book> Books => _database.GetCollection<Book>("books");

    }
}
