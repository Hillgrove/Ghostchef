using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GC_Subscription.Models
{
    public class Product
    {
        // Product Fields
        public int Id { get; set; }

        [Required(ErrorMessage ="Angiv venligst et navn til retten")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Angiv venligst en beskrivelse af retten")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Angiv venligst et beløb")]
        [Range(1, int.MaxValue, ErrorMessage = "Angiv venligst et beløb")]
        
        public int Price { get; set; }
        public bool InStock { get; set; } = true;
        public string? ImageUrl { get; set; }
        public DateTime LastEdited { get; set; }

        // Relational Fields
        public ICollection<Mealbox> Mealboxes { get; } = [];
        public ICollection<Allergy> Allergies { get; set; } = [];
        public ICollection<Diet> Diets { get; set; } = [];
        
    }
}
