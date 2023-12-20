using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibrary.Model;

public class Company
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}
