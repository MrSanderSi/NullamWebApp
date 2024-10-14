using NullamWebApp.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullamWebApp.Data.Models;

public class ParticipantEntry
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public Guid? ParticipantPersonId { get; set; }
    public Guid? ParticipantCompanyId { get; set; }
    public PaymentType PaymentType { get; set; }
    public string? AdditionalInfo { get; set; }
    public int ParticipantCount { get; set; }
    public virtual Event Event { get; set; }
    public virtual ParticipantPerson? Person { get; set; }
    public virtual ParticipantCompany? Company { get; set; }
}
