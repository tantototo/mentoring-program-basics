using MongoDB.Bson;
using MongoDB.Driver;
using TestMongo.Console.Infra.Context;
using TestMongo.Console.Models;

namespace TestMongo.Console.Infra
{
    public class BookRepository(DbContext dbContext)
    {
        private readonly DbContext _dbContext = dbContext;

        public List<Book> GetAll() => _dbContext.Books.Find(Builders<Book>.Filter.Empty).ToList();

        public void AddStartData()
        {
            if (GetAll().Count > 0)
                return;

            var books = new List<Book>()
            {
                new (){ Id = 1, Name = "Hobit", Author = "Tolkien", Count = 5, Genres = { "fantasy" }, Year = 2014 },
                new (){ Id = 2, Name = "Lord of the rings", Author = "Tolkien", Count = 3, Genres = { "fantasy" }, Year = 2015 },
                new (){ Id = 3, Name = "Kolobok", Count = 10, Genres = { "kids" }, Year = 2000 },
                new (){ Id = 4, Name = "Repka", Count = 11, Genres = { "kids" }, Year = 2000 },
                new (){ Id = 5, Name = "Dyadya Stiopa", Author = "Mihalkov", Count = 1, Genres = { "kids" }, Year = 2001 }
            };
            
            _dbContext.Books.InsertMany(books);
        }

        public (int, List<string>) GetMoreThenOneBooks()
        {
            var books = _dbContext.Books.Find(b => b.Count > 1)
                .SortBy(b => b.Name)
                .Limit(3)
                .ToList();

            return ( books.Sum(b => b.Count), 
                     books.Select(b => b.Name).ToList() );
        }

        public Book? GetBookWithMaxCount() => GetAll().OrderByDescending(b => b.Count).FirstOrDefault();

        public Book? GetBookWithMinCount() => GetAll().OrderBy(b => b.Count).FirstOrDefault();

        public List<string> GetAuthors() =>
            GetAll().Where(b => b.Author is not null).Select(b => b.Author).Distinct().ToList();

        public List<Book> GetBooksWithoutAuthors() =>
            _dbContext.Books.Find(b => string.IsNullOrEmpty(b.Author)).ToList();

        public void AddMoreBook(int count) =>
            _dbContext.Books.UpdateMany(new BsonDocument(), Builders<Book>.Update.Inc(x => x.Count, count));

        public void UpdateGenre(string curGenre, string newGenre) =>
            _dbContext.Books.UpdateMany(x => x.Genres.Contains(curGenre) && !x.Genres.Contains(newGenre),
                Builders<Book>.Update.Push(x => x.Genres, newGenre));

        public void DeleteWithCountLessThen(int count) => 
            _dbContext.Books.DeleteMany(b => b.Count < count);

        public void DeleteAll() => _dbContext.Books.DeleteMany(new BsonDocument());
    }
}
