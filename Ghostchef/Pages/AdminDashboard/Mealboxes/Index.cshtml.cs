
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;


namespace GC_Subscription.Pages.Mealboxes
{
    public class IndexModel : PageModel
    {
        private readonly GhostchefContext _context;

        public IList<Mealbox> Mealbox { get; set; } = default!;

        public IndexModel(GhostchefContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            // Retrieves Mealbox items and their associated products, diets, and allergies
            Mealbox = await _context.Mealbox
                                    .Include(m => m.Products)
                                    .ThenInclude(p => p.Diets)
                                    .Include(m => m.Products)
                                    .ThenInclude(p => p.Allergies)
                                    .Distinct()
                                    .ToListAsync();
        }
    }
}