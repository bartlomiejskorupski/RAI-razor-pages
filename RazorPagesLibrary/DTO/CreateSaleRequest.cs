using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPagesLibrary.DTO;

public class CreateSaleRequest
{
    public string ClientName { get; set; } = null!;
    public IList<SaleUnitDTO> SaleUnits { get; set; } = new List<SaleUnitDTO>();
}
