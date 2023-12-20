
using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibrary.Model;

public class Delivery
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public int SupplierID { get; set; }
    public Company Supplier { get; set; } = null!;
    public ICollection<Pallet> Pallets { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int ItemsPerPallet { get; set; }
    public DateTime DeliveryDate { get; set; }
}
