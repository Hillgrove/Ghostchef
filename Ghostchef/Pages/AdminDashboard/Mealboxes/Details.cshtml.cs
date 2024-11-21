
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;


namespace GC_Subscription.Pages.Mealboxes
{
    public class DetailsModel : PageModel
    {
        private readonly GhostchefContext _context;

        public DetailsModel(GhostchefContext context)
        {
            _context = context;
        }

        public Mealbox Mealbox { get; set; } = default!;
        public List<string> UniqueDiets { get; set; } = new List<string>();
        public List<string> UniqueAllergies { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealbox = await _context.Mealbox
                                        .Include(m => m.Products)
                                        .ThenInclude(p => p.Diets)
                                        .Include(m => m.Products)
                                        .ThenInclude(p => p.Allergies)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (mealbox == null)
            {
                return NotFound();
            }

            Mealbox = mealbox;

            UniqueDiets = mealbox.Products
                                 .SelectMany(p => p.Diets)
                                 .Select(d => d.Name)
                                 .Distinct()
                                 .ToList();

            UniqueAllergies = mealbox.Products
                                     .SelectMany(p => p.Allergies)
                                     .Select(a => a.Name)
                                     .Distinct()
                                     .ToList();

            return Page();
        }
    }
}
