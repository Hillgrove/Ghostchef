
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Diets
{
    public class IndexModel : PageModel
    {
        private readonly GhostchefContext _context;

        public IndexModel(GhostchefContext context)
        {
            _context = context;
        }

        public IList<Diet> Diet { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Diet = await _context.Diet.ToListAsync();
        }
    }
}
