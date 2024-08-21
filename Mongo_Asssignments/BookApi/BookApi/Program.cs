
using MongoDB.Driver;
using BookApi.Model.BookModel;

namespace BookApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));

            // Add MongoDB context
            builder.Services.AddSingleton<MongoDbContext>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           // app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

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
    
}
