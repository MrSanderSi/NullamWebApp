using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullamWebApp.Data.Models;

public class ParticipantCompany
{
    public Guid Id { get; set; }
    public required string CompanyName { get; set; }
    public int RegistryCode { get; set; }
    public virtual IEnumerable<ParticipantEntry>? ParticipantEntry { get; set; }
}
