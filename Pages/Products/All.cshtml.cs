using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Ghostchef.Models;

namespace Ghostchef.Pages.Products
{
    public class AllModel : PageModel
    {
        public List<Product> Data { get; } = new List<Product>
        {
            new Product(
                1,
                "Favoritkassen", 
                "Denne m�ltidkasse indeholder 4 af Ghostchefs mest solgte f�rdigretter",
			    new List<Ingredient> { Ingredient.Mel, Ingredient.Sukker },
				new List<Diet> { Diet.Veganer, Diet.Glutenfri },
				new List<Allergy> { Allergy.N�dder }
                ),
            new Product(
                2,
                "Skaldyrskassen",
                "Fantastisk beskrivelse her",
                new List<Ingredient> { Ingredient.Mel, Ingredient.Salt },
                new List<Diet> { Diet.Ingen },
                new List<Allergy> { Allergy.Skaldyr } 
                ),
            new Product(
                3,
				"Ugekassen",
				"Uge kassen leveres altid om s�ndagen inden ugens start.",
				new List<Ingredient> { Ingredient.Mel, Ingredient.Salt },
				new List<Diet> { Diet.Ingen },
				new List<Allergy> { Allergy.Ingen },
                false
				)
        };

        public void OnGet()
        {
        }
    }
}