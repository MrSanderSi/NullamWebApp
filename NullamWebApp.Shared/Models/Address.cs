using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullamWebApp.Shared.Models;

public class Address
{
    public string? Country { get; set; }
    public string? County { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? AdditionalLines { get; set; }
    public int PostalCode { get; set; }
}
