
using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibrary.Model;

public class Pallet
{
    public int Id { get; set; }
    [Required]
    [Range(1, 20, ErrorMessage = "Only values from 1 to 20")]
    public int Count { get; set; }
    [Required]
    public int? WaterId { get; set; }
    public Water Water { get; set; } = null!;
    [Required]
    public int DeliveryId { get; set; }
    public Delivery Delivery { get; set; } = null!;

}
