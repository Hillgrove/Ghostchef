
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Diets
{
    public class CreateModel : PageModel
    {
        private readonly GhostchefContext _context;

        public CreateModel(GhostchefContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Diet Diet { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Diet.Add(Diet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
