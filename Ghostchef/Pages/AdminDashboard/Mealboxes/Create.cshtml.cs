
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GC_Subscription.Data;
using GC_Subscription.Models;


namespace GC_Subscription.Pages.Mealboxes
{
    public class CreateModel : PageModel
    {
        private readonly GhostchefContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public List<Product> AvailableProducts { get; set; }

        [BindProperty]
        public Mealbox Mealbox { get; set; }

        [BindProperty]
        public List<int> SelectedProductIds { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }


        public CreateModel(GhostchefContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            AvailableProducts = await _context.Product.ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Run image processing
            await ProcessImageAsync();

            // Insert products into relationtabel
            var selectedProductIds = Request.Form["SelectedProductIds"];

            // Iterate through the selected product IDs and associate them with the mealbox
            foreach (var productId in selectedProductIds)
            {
                var product = await _context.Product.FindAsync(int.Parse(productId));
                
                if (product != null)
                {
                    Mealbox.Products.Add(product);
                }
            }

            // Save date
            Mealbox.LastEdited = DateTime.Now;

            // Save Mealbox
            _context.Mealbox.Add(Mealbox);
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

                Mealbox.ImageUrl = $"/{folderName}/" + uniqueFileName;
            }
        }
        #endregion
    }
}
