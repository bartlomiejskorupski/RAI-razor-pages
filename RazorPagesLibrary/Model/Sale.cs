namespace RazorPagesLibrary.Model;

public class Sale
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public ICollection<SaleUnit> SaleUnits { get; set; } = null!;
    public DateTime SaleDate { get; set; }

}
