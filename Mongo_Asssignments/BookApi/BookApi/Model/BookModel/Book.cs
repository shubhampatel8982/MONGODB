using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookApi.Model.BookModel
{
    public class Book
    {
        
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
