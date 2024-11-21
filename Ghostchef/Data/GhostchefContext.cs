
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Models;

namespace GC_Subscription.Data
{
    public class GhostchefContext : DbContext
    {
        public GhostchefContext (DbContextOptions<GhostchefContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Mealbox> Mealbox { get; set; } = default!;
        public DbSet<Diet> Diet { get; set; } = default!;
        public DbSet<Allergy> Allergy { get; set; } = default!;
        public DbSet<Subscription> Subscription { get; set; } = default!;
    }
}
