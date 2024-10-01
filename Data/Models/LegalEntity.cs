using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;

public class LegalEntity
{
    public Guid Id { get; set; }

    public required string FullName { get; set; }
}
