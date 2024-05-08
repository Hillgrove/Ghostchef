using System.ComponentModel.DataAnnotations;

namespace Ghostchef.Models
{
	public class Product
	{
		public int Id { get; set; }
        [Required(ErrorMessage = "Der skal angives et navn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Der skal angives en beskrivelse")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Der skal angives en pris")]
        public int? Price { get; set; }
		public bool InStock { get; set; }
        [Required]
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        [Required]
        public List<Diet> Diets { get; set; } = new List<Diet>();
        [Required]
        public List<Allergy> Allergies { get; set; } = new List<Allergy>();


        public Product() { }

        public Product(string name, string description, int price, List<Ingredient> ingredients, List<Diet> diets, List<Allergy> allergies, bool inStock = true)
		{
			Id = 0;
			Name = name;
			Description = description;
			Price = price;
			Ingredients = ingredients;
			Diets = diets;
			Allergies = allergies;
			InStock = inStock;	
		}

		public void Update(Product other)
		{
			Name = other.Name;
			Description = other.Description;
			Price = other.Price;
			Ingredients = other.Ingredients;
			Diets = other.Diets;
			Allergies = other.Allergies;
			InStock = other.InStock;
		}
	}
}
