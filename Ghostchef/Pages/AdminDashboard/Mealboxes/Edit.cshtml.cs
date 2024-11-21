
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Mealboxes
{
    public class EditModel : PageModel
    {
        private readonly GhostchefContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public List<Product> AvailableProducts { get; set; } = default!;

        [BindProperty]
        public Mealbox Mealbox { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedProductIds { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }


        public EditModel(GhostchefContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealbox =  await _context.Mealbox.Include(m => m.Products)
                                                 .FirstOrDefaultAsync(m => m.Id == id);

            if (mealbox == null)
            {
                return NotFound();
            }
            Mealbox = mealbox;

            AvailableProducts = await _context.Product.ToListAsync();
            
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get current mealbox from DB along with its products
            var existingMealbox = await _context.Mealbox
                                                .Include(m => m.Products)
                                                .FirstOrDefaultAsync(m => m.Id == Mealbox.Id);

            
            if (existingMealbox != null)
            {
                // Update mealbox properties
                existingMealbox.Name = Mealbox.Name;
                existingMealbox.Description = Mealbox.Description;
                existingMealbox.Price = Mealbox.Price;
                existingMealbox.LastEdited = DateTime.Now;
                existingMealbox.Theme = Mealbox.Theme;
                existingMealbox.DateFrom = Mealbox.DateFrom;
                existingMealbox.DateTo = Mealbox.DateTo;

                // Update associated products
                if (SelectedProductIds != null)
                {
                    existingMealbox.Products.Clear(); // Clear existing associations
                    foreach (var productId in SelectedProductIds)
                    {
                        var product = await _context.Product.FindAsync(productId);
                        if (product != null)
                        {
                            existingMealbox.Products.Add(product);
                        }
                    }
                }

                // Run image processing
                await ProcessImageAsync(existingMealbox);
            }

            // Tries to save changes to DB
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealboxExists(Mealbox.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        #region Private Helper Functions
        private bool MealboxExists(int id)
        {
            return _context.Mealbox.Any(e => e.Id == id);
        }

        private async Task ProcessImageAsync(Mealbox existingMealbox)
        {
            if (Image != null && Image.Length > 0)
            {
                var folderName = "images";
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var filePath = Path.Combine(uploadDir, uniqueFileName);

                // Save image to server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                // Update ImageUrl property
                existingMealbox.ImageUrl = $"/{folderName}/" + uniqueFileName;
            }
        }
        #endregion
    }
}
