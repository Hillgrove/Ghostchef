
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Allergies
{
    public class IndexModel : PageModel
    {
        private readonly GhostchefContext _context;

        public IndexModel(GhostchefContext context)
        {
            _context = context;
        }

        public IList<Allergy> Allergy { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Allergy = await _context.Allergy.ToListAsync();
        }
    }
}
