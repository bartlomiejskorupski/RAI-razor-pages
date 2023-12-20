
using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibrary.Model;

public class Pallet
{
    public int Id { get; set; }

    [Range(0, int.MaxValue)]
    public int Count { get; set; }
    public int? WaterId { get; set; }
    public Water Water { get; set; } = null!;
    public int DeliveryId { get; set; }
    public Delivery Delivery { get; set; } = null!;

}
