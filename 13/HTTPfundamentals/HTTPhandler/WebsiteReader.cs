using HtmlAgilityPack;

namespace HTTPhandler
{
    public class WebsiteReader
    {
        private readonly string _url;
        private readonly HttpResponseMessage _response;

        public WebsiteReader(string url)
        {
            _url = url;
            _response = ConnectAsync().Result;
        }

        private async Task<HttpResponseMessage> ConnectAsync()
        {
            ArgumentNullException.ThrowIfNullOrEmpty(_url);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(_url);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString());

            return response;
        }

        public async Task<IEnumerable<string>> Read()
        {
            var content = await _response.Content.ReadAsStringAsync();

            var linkElements = new List<string>() { "link", "img", "a", };
            var linkAttributes = new List<string>() { "src", "href" };

            var document = new HtmlDocument();
            document.LoadHtml(content);
            var links = document.DocumentNode.Descendants()
                .Where(x => linkElements.Any(le => le == x.Name))
                .SelectMany(l => l.Attributes)
                .Where(att => linkAttributes.Any(la => la == att.Name) && !att.Value.StartsWith("#"))
                .Select(att => att.Value)
                .Distinct();
            
            return links.Select(l => GenerateLink(l));
        }

        public async Task Save(string filePath)
        {
            var cgStream = await _response.Content.ReadAsStreamAsync();
            var fileStream = File.Open(filePath, FileMode.Create);
            await cgStream.CopyToAsync(fileStream);
        }

        private string GenerateLink(string link)
        {
            var http = "https";

            return link.StartsWith("//") ? $"{http}:" + link :
                   link.StartsWith($"/") ? $"{_url}{link}" :
                   link.StartsWith(http) ? link : $"{_url}/{link}";
        }
    }
}
