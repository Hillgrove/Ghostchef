using System.ComponentModel.DataAnnotations;

namespace GC_Subscription.Models
{
    public class Diet
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Angiv venligst et navn på diæten")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; } = [];
    }
}
