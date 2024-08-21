using MongoDB.Driver;
using BookApi.Model.BookModel;

namespace BookApi
{
    public class MongoDbContext
    {

        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB:ConnectionString"));
            _database = client.GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName"));
        }

        public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books");
    }

}

