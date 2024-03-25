using HTTPhandler;

var sitePath = "https://ru.wikipedia.org/";
var filePath = @"C:\Users\Tkacheva\Downloads\iii\new.html";

var reader = new WebsiteReader(sitePath);
await reader.Read();
await reader.Save(filePath);