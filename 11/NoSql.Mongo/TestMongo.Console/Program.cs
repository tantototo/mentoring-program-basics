using TestMongo.Console.Infra;
using TestMongo.Console.Infra.Context;

var context = new DbContext(@"mongodb://localhost:27017", "newDb");
var rep = new BookRepository(context);

rep.AddStartData();
var task2 = rep.GetMoreThenOneBooks();
var task3max = rep.GetBookWithMaxCount();
var task3min = rep.GetBookWithMinCount();

var task4 = rep.GetAuthors();
var task5 = rep.GetBooksWithoutAuthors();
rep.AddMoreBook(1);
rep.UpdateGenre("fantasy", "favority");
rep.DeleteWithCountLessThen(3);
rep.DeleteAll();
