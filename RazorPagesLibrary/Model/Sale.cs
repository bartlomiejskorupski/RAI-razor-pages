using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesLibrary.Model;

public class Sale
{
    public int Id { get; set; }
    [Required]
    [DisplayName("Client")]
    public string EmployeeName { get; set; } = null!;
    public ICollection<SaleUnit> SaleUnits { get; set; } = null!;
    [Required]
    public DateTime SaleDate { get; set; }
    [NotMapped]
    public int TotalCount => SaleUnits?.Aggregate(0, (acc, unit) => acc + unit?.Count ?? 0) ?? 0;
    [NotMapped]
    public string DisplayName => $"{EmployeeName} {SaleDate}";
}
