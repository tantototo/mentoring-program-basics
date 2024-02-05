namespace XMLHandler.Models;

public class Newspaper : Polygraphy
{
    public string Publisher { get; set; }
    public long? Year { get; set; }
    public DateOnly? Date { get; set; }
    public string? ISSN { get; set; }
}