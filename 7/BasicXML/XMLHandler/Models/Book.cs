using System.ComponentModel.DataAnnotations;

namespace XMLHandler.Models;

public class Book : Polygraphy
{
    [Required]
    public string Author { get; set; }
    public string Publisher { get; set; }
    public long? Year { get; set; }
    public string? ISBN { get; set; }
}