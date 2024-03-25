using HTTPhandler;

var sitePath = "https://ru.wikipedia.org/";
var filePath = @"";

var reader = new WebsiteReader(sitePath);
await reader.Read();
await reader.Save(filePath);