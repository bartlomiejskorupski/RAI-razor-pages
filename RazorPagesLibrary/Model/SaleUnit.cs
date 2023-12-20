
using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibrary.Model;

public class SaleUnit
{
    public int Id { get; set; }
    [Required]
    public int SaleId { get; set; }
    public Sale Sale { get; set; } = null!;
    [Required]
    public int WaterId { get; set; }
    public Water Water { get; set; } = null!;
    [Required]

    [Range(0, int.MaxValue)]

    public int Count { get; set; }

}
