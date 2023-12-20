
using RazorPagesLibrary.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RazorPagesLibrary.DTO;

public class GetWaterResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public double pH { get; set; }
    public string Packaging { get; set; } = null!;
    public string Picture { get; set; } = null!;
    public string Mineralization { get; set; } = null!;
    public int Stock {  get; set; }

    public static GetWaterResponse FromWater(Water water, int stock)
    {
        return new GetWaterResponse() 
        { 
            Id = water.Id,
            Name = water.Name,
            Type = water.Type.Name,
            Manufacturer = water.Manufacturer.Name,
            pH = water.pH,
            Packaging = water.Packaging.DisplayName,
            Picture = water.Picture,
            Mineralization = water.Mineralization,
            Stock = stock
        };
    }

}
