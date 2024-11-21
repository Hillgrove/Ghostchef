using System.ComponentModel.DataAnnotations;

namespace GC_Subscription.Models
{
    public class Mealbox
    {
        // Product Fields
        public int Id { get; set; }

        [Required(ErrorMessage = "Angiv venligst et navn til måltidskassen")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Angiv venligst en beskrivelse af måltidskassen")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Angiv venligst et beløb")]
        [Range(1, int.MaxValue, ErrorMessage = "Angiv venligst et beløb")]
        public int Price { get; set; }

        public string? ImageUrl { get; set; }
        public string? Theme { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime LastEdited {  get; set; }

        // Relational Fields
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<Subscription> Subscriptions { get; set; } = [];
    }
}
