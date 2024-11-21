
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Diets
{
    public class DeleteModel : PageModel
    {
        private readonly GhostchefContext _context;

        public DeleteModel(GhostchefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Diet Diet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diet = await _context.Diet.FirstOrDefaultAsync(m => m.Id == id);

            if (diet == null)
            {
                return NotFound();
            }
            else
            {
                Diet = diet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diet = await _context.Diet.FindAsync(id);
            if (diet != null)
            {
                Diet = diet;
                _context.Diet.Remove(Diet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
