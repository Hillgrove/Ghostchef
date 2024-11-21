
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;


namespace GC_Subscription.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly GhostchefContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public List<Allergy>? AvailableAllergies { get; set; }
        public List<Diet>? AvailableDiets { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public List<int>? SelectedAllergyIds { get; set; }

        [BindProperty]
        public List<int>? SelectedDietIds { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }


        public CreateModel(GhostchefContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync()
        {
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

            // Process image upload
            await ProcessImageAsync();


            // Associate selected allergies with the product
            if (SelectedAllergyIds != null)
            {
                Product.Allergies = new List<Allergy>();
                foreach (int allergyId in SelectedAllergyIds)
                {
                    var allergy = await _context.Allergy.FindAsync(allergyId);
                    if (allergy != null)
                    {
                        Product.Allergies.Add(allergy);
                    }
                }
            }

            // Associate selected diets with the product
            if (SelectedDietIds != null)
            {
                Product.Diets = new List<Diet>();
                foreach (int dietId in SelectedDietIds)
                {
                    var diet = await _context.Diet.FindAsync(dietId);
                    if (diet != null)
                    {
                        Product.Diets.Add(diet);
                    }
                }
            }

            // Save date
            Product.LastEdited = DateTime.Now;

            // Save Product
            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        #region Private Helper Functions
        private async Task ProcessImageAsync()
        {
            if (Image != null && Image.Length > 0)
            {
                var folderName = "images";
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

                // Ensure the directory exists, create it if not
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // Combine the directory and filename to get the full path
                var filePath = Path.Combine(uploadDir, uniqueFileName);

                // Save the uploaded image to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                Product.ImageUrl = $"/{folderName}/" + uniqueFileName;
            }
        }
        #endregion
    }
}