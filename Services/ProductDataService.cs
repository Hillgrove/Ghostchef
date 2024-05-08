using Ghostchef.Models;

namespace Ghostchef.Services
{
    public class ProductDataService : IProductDataService
    {
        private Dictionary<int, Product> _products;

        public ProductDataService()
        {
            _products = new Dictionary<int, Product>();

            Create(new Product(
                "Oksecuvette",
                "200gram bøf af oksecuvette 150gram svampesauce, 100gram grøntsager og 150gram ristede kartofler",
                135,
                new List<Ingredient> { Ingredient.Mel, Ingredient.Sukker },
                new List<Diet> { Diet.Veganer, Diet.Glutenfri },
                new List<Allergy> { Allergy.Nødder }
            ));
            
            Create(new Product(
                "Laks",
                "200gram laks 150 gram kartofler 100 gram grøntsager samt 100 gram dild sauce",
                95,
                new List<Ingredient> { Ingredient.Mel, Ingredient.Salt },
                new List<Diet> { Diet.Ingen },
                new List<Allergy> { Allergy.Skaldyr }
            ));
            
            Create(new Product(
                "Lasagne",
                "Ovnklar Lasagne 1200 gram",
                160,
                new List<Ingredient> { Ingredient.Mel, Ingredient.Salt },
                new List<Diet> { Diet.Ingen },
                new List<Allergy> { Allergy.Ingen },
                false
            ));
        }
        
        public int Create(Product product)
        {
            product.Id = NextId();
            _products[product.Id] = product;

            return product.Id;
        }

        public List<Product> GetAll()
        {
            return _products.Values.ToList();
        }

        public Product? Read(int id)
        {
            return _products.ContainsKey(id) ? _products[id] : null;
        }

        public bool Update(int id, Product product)
        {
            Product? existingProduct = Read(id);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Update(product);

            return true;
        }

        public bool Delete(int id)
        {
            return _products.Remove(id);
        }


        #region Helper Functions
        private int NextId() => _products.Keys.DefaultIfEmpty(0).Max() + 1;
        #endregion
    }
}
