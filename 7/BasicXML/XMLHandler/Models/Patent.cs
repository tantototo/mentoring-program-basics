namespace XMLHandler.Models;

public class Patent : Polygraphy
{
    public string Author { get; set; }
    public DateOnly? PublicationDate { get; set; }
    public DateOnly? RequestDate { get; set; }
    public string? RegistrationNumber { get; set; }
}