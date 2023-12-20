using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibrary.Model;

public class WaterType
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public ICollection<Water> Waters { get; set; } = null!;
}
