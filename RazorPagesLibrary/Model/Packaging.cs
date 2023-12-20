
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesLibrary.Model;

public class Packaging
{
    public int Id { get; set; }
    [Required]
    public string Type { get; set; } = null!;
    [Required]
    [Range(0, 50.0, MinimumIsExclusive = true)]
    [DisplayName("Capacity [Litres]")]
    public double Capacity { get; set; }
    public ICollection<Water> Waters { get; set; } = null!;
    [NotMapped]
    public string DisplayName => $"{Type} {Capacity}L";
}
