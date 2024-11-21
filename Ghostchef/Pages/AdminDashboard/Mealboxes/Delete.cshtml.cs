
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Mealboxes
{
    public class DeleteModel : PageModel
    {
        private readonly GhostchefContext _context;

        public DeleteModel(GhostchefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mealbox Mealbox { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealbox = await _context.Mealbox.FirstOrDefaultAsync(m => m.Id == id);

            if (mealbox == null)
            {
                return NotFound();
            }
            else
            {
                Mealbox = mealbox;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealbox = await _context.Mealbox.FindAsync(id);
            if (mealbox != null)
            {
                Mealbox = mealbox;
                _context.Mealbox.Remove(Mealbox);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
