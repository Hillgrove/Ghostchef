using Ghostchef.Models;

namespace Ghostchef.Services
{
    public class ProductDataService : IProductDataService
    {
        private List<Product> _products = new List<Product>
        {
            new Product(
                1,
                "Favoritkassen",
                "Denne måltidkasse indeholder 4 af Ghostchefs mest solgte færdigretter",
                new List<Ingredient> { Ingredient.Mel, Ingredient.Sukker },
                new List<Diet> { Diet.Veganer, Diet.Glutenfri },
                new List<Allergy> { Allergy.Nødder }
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
                "Uge kassen leveres altid om søndagen inden ugens start.",
                new List<Ingredient> { Ingredient.Mel, Ingredient.Salt },
                new List<Diet> { Diet.Ingen },
                new List<Allergy> { Allergy.Ingen },
                false
            )
        };

        public List<Product> GetAll()
        {
            return _products;
        }
    }
}
