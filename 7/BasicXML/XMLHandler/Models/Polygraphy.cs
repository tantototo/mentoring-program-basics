using System.ComponentModel.DataAnnotations;

namespace XMLHandler.Models;

public abstract class Polygraphy
{
    [Required(ErrorMessage = "Id is required")]
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    public string? Location { get; set; }
    public long? PageCount { get; set; }
    public string? Note { get; set; }
}