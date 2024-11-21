
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;


namespace GC_Subscription.Pages.Buy
{
    public class IndexModel : PageModel
    {
        private readonly GhostchefContext _context;
        public List<Product> AvailableProducts { get; set; } = default!;
        public List<Allergy>? AvailableAllergies { get; set; }
        public List<Diet>? AvailableDiets { get; set; }

        public IndexModel(GhostchefContext context)
        {
            _context = context;
        }

        public IList<Subscription> Subscription { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Subscription = await _context.Subscription
                .Include(s => s.Customer).ToListAsync();

            AvailableProducts = await _context.Product.ToListAsync();
            AvailableAllergies = await _context.Allergy.ToListAsync();
            AvailableDiets = await _context.Diet.ToListAsync();

            return Page();
        }
       

    }
}
