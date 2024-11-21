
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;


namespace GC_Subscription.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly GhostchefContext _context;
        public IList<Product> Product { get; set; } = default!;
        
        public IndexModel(GhostchefContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Product = await _context.Product
                                    .Include(p => p.Allergies)
                                    .Include(p => p.Diets)
                                    .ToListAsync();
        }
    }
}
