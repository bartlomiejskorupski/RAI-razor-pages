
using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibrary.Model;

public class SaleUnit
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public Sale Sale { get; set; } = null!;
    public int WaterId { get; set; }
    public Water Water { get; set; } = null!;

    [Range(0, int.MaxValue)]

    public int Count { get; set; }

}
