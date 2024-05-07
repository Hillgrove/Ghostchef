using Ghostchef.Models;

namespace Ghostchef.Services
{
    public class MealDataService : IMealDataService
    {
        private List<Meal> _meals = new List<Meal>
        {
            new Meal(
                1,
                "Oksecuvette",
                "200gram bøf af oksecuvette 150gram svampesauce, 100gram grøntsager og 150gram ristede kartofler",
                new List<Ingredient> { Ingredient.Mel, Ingredient.Sukker },
                new List<Diet> { Diet.Veganer, Diet.Glutenfri },
                new List<Allergy> { Allergy.Nødder }
                ),
            new Meal(
                2,
                "Laks",
                "200gram laks 150 gram kartofler 100 gram grøntsager samt 100 gram dild sauce",
                new List<Ingredient> { Ingredient.Mel, Ingredient.Salt },
                new List<Diet> { Diet.Ingen },
                new List<Allergy> { Allergy.Skaldyr }
                ),
            new Meal(
                3,
                "Lasagne",
                "Ovnklar Lasagne 1200 gram",
                new List<Ingredient> { Ingredient.Mel, Ingredient.Salt },
                new List<Diet> { Diet.Ingen },
                new List<Allergy> { Allergy.Ingen },
                false
            )
        };

        public List<Meal> GetAll()
        {
            return _meals;
        }
    }
}
