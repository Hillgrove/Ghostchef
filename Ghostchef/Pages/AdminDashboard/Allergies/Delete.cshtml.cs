
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Allergies
{
    public class DeleteModel : PageModel
    {
        private readonly GhostchefContext _context;

        public DeleteModel(GhostchefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Allergy Allergy { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergy = await _context.Allergy.FirstOrDefaultAsync(m => m.Id == id);

            if (allergy == null)
            {
                return NotFound();
            }
            else
            {
                Allergy = allergy;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergy = await _context.Allergy.FindAsync(id);
            if (allergy != null)
            {
                Allergy = allergy;
                _context.Allergy.Remove(Allergy);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
