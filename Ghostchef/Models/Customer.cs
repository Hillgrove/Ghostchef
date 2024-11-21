using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GC_Subscription.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id               { get; set; } // PK

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(64)]
        public string Name          { get; set; }

        [Required, EmailAddress]
        [Display(Name = "Email")]
        [StringLength(128)]
        public string Email         { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string? Phone         { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address1      { get; set; }
        
        /*
        public int PostalCodeId { get; set; } // FK for City 3/nf
        ICollection<PostalCode> PostalCo { get; set; }
        */
      
        ICollection<Subscription> Subscriptions { get; set; }

    }
}
