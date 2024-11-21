
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;

namespace GC_Subscription.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly GhostchefContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public List<Allergy> AvailableAllergies { get; set; }
        public List<Diet> AvailableDiets { get; set; }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public List<int>? SelectedAllergyIds { get; set; }

        [BindProperty]
        public List<int>? SelectedDietIds { get; set; }

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

            var product = await _context.Product.Include(p => p.Allergies)
                                                .Include(p => p.Diets)
                                                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            Product = product;

            AvailableAllergies = await _context.Allergy.ToListAsync();
            AvailableDiets = await _context.Diet.ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            // Get current product from DB
            var existingProduct = await _context.Product.Include(p => p.Allergies)
                                                        .Include(p => p.Diets)
                                                        .FirstOrDefaultAsync(p => p.Id == Product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = Product.Name;
                existingProduct.Description = Product.Description;
                existingProduct.Price = Product.Price;
                existingProduct.LastEdited = DateTime.Now;

                // Update selected allergies
                existingProduct.Allergies.Clear();
                if (SelectedAllergyIds != null)
                {
                    foreach (var allergyId in SelectedAllergyIds)
                    {
                        var allergy = await _context.Allergy.FindAsync(allergyId);
                        if (allergy != null)
                        {
                            existingProduct.Allergies.Add(allergy);
                        }
                    }
                }

                // Update selected diets
                existingProduct.Diets.Clear();
                if (SelectedDietIds != null)
                {
                    foreach (var dietId in SelectedDietIds)
                    {
                        var diet = await _context.Diet.FindAsync(dietId);
                        if (diet != null)
                        {
                            existingProduct.Diets.Add(diet);
                        }
                    }
                }

                // Image processing
                await ProcessImageAsync(existingProduct);



                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(Product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        #region Private helper Functions
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private async Task ProcessImageAsync(Product existingProduct)
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
                existingProduct.ImageUrl = $"/{folderName}/" + uniqueFileName;
            }
        }
        #endregion
    }
}

