
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesLibrary.Model;

public class Delivery
{
    public int Id { get; set; }
    [Required]
    public string EmployeeName { get; set; } = null!;
    [Required]
    public int SupplierID { get; set; }
    public Company Supplier { get; set; } = null!;
    public ICollection<Pallet> Pallets { get; set; } = null!;
    [Required]

    [Range(1, int.MaxValue, ErrorMessage = "Has to be a positive integer")]
    public int ItemsPerPallet { get; set; }
    [Required]
    public DateTime DeliveryDate { get; set; }
    [NotMapped]
    public string DisplayName => $"{DeliveryDate}, {Supplier?.Name}, {EmployeeName}, N: {ItemsPerPallet}";
}
