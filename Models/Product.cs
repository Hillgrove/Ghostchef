namespace Ghostchef.Models
{
	public class Product
	{
		public int Id { get; }
		public string Name { get; }
		public string Description { get; }
		public bool InStock { get; }
		public List<Ingredient> Ingredients { get; }
		public List<Diet> Diets { get; }
		public List<Allergy> Allergies { get; }

		public Product(int id, string name, string description, List<Ingredient> ingredients, List<Diet> diets, List<Allergy> allergies, bool inStock = true)
		{
			Id = id;
			Name = name;
			Description = description;
			Ingredients = ingredients ?? new List<Ingredient>();
			Diets = diets ?? new List<Diet>();
			Allergies = allergies ?? new List<Allergy>();
			InStock = inStock;	
		}
	}
}
