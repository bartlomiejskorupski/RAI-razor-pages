
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesLibrary.Model;

public class Water
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [DisplayName("Type")]
    public int TypeID { get; set; }
    public WaterType Type { get; set; } = null!;
    [Required]
    [DisplayName("Manufacturer")]
    public int ManufacturerID { get; set; }
    public Company Manufacturer { get; set; } = null!;
    [Required]
    [Range(5D, 9D, ErrorMessage = "Invalid pH. 5 <= pH <= 9")]
    public double pH { get; set; }
    public ICollection<Ion> Ions { get; set; } = null!;
    [NotMapped]
    public ICollection<Ion> Cations => Ions?.Where(i => i.Type == IonType.Cation).ToList()!;
    [NotMapped]
    public ICollection<Ion> Anions => Ions?.Where(i => i.Type == IonType.Anion).ToList()!;

    [Required]
    [DisplayName("Packaging")]
    public int PackagingID { get; set; }
    public Packaging Packaging { get; set; } = null!;
    [Required]
    [DataType(DataType.ImageUrl)]
    public string Picture { get; set; } = null!;
    [NotMapped]
    public string Mineralization
    {
        get
        {

            var ionSum = Ions != null ? Ions.Aggregate(0D, (acc, ion) => acc + ion.Content) : 0D;
            if (ionSum <= 0.05D)
            {
                return "Very low mineralization";
            }
            else if (ionSum <= 0.5D)
            {
                return "Low mineralization";
            }
            else if (ionSum <= 1.5D)
            {
                return "Medium mineralization";
            }
            return "Highly mineralized";
        }
    }
}
