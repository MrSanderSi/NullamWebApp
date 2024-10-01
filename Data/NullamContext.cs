using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace NullamWebApp.Data;

public class NullamContext : DbContext
{
    public DbSet<LegalEntity> LegalEntity { get; set; }

    public NullamContext(DbContextOptions options) : base(options)
    {
    }
}
