using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesLibrary.Model;

public class Ion
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Symbol { get; set; } = null!;
    [DisplayName("Content [g/L]")]
    public double Content { get; set; }
    [Required]
    public IonType Type { get; set; }
    public ICollection<Water> Waters { get; set; } = null!;
    [NotMapped]
    public string DisplayName => $"{Name}, {Symbol}, {Content} g/L";
}
