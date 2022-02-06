using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace WebApplication2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ModelTransaction> Transacciones { get; set; } = null;
        public DbSet<ModelCurrency> Currencies { get; set; } = null;

    }
}
