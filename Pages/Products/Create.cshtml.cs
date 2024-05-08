using Ghostchef.Services;
using Ghostchef.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ghostchef.Pages.Products
{
    public class CreateModel : PageModel
    {
        private IProductDataService _productDataService;

        [BindProperty]
        public Product Data { get; set; }

        public Dictionary<Ingredient, bool> IngredientChoices { get; set; } = new Dictionary<Ingredient, bool>();
        public Dictionary<Diet, bool> DietChoices { get; set; } = new Dictionary<Diet, bool>();
        public Dictionary<Allergy, bool> AllergyChoices { get; set; } = new Dictionary<Allergy, bool>();

        public CreateModel(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }

        public void OnGet()
        {
            InitializeChoices();
        }

        public IActionResult OnPost(string[] Ingredients, string[] Diets, string[] Allergies)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Data.InStock = true;
            Data.Ingredients = Ingredients.Select(i => Enum.Parse<Ingredient>(i)).ToList();
            Data.Diets = Diets.Select(d => Enum.Parse<Diet>(d)).ToList();
            Data.Allergies = Allergies.Select(a => Enum.Parse<Allergy>(a)).ToList();

            _productDataService.Create(Data);

            return RedirectToPage("All");
        }

        #region Helper Functions
        private void InitializeChoices()
        {
            IngredientChoices = Enum.GetValues(typeof(Ingredient)).Cast<Ingredient>().ToDictionary(ingredient => ingredient, ingredient => false);
            DietChoices = Enum.GetValues(typeof(Diet)).Cast<Diet>().ToDictionary(diet => diet, diet => false);
            AllergyChoices = Enum.GetValues(typeof(Allergy)).Cast<Allergy>().ToDictionary(allergy => allergy, allergy => false);
        }
        #endregion
    }
}
