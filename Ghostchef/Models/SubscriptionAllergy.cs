namespace GC_Subscription.Models
{
    public class SubscriptionAllergy
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }

    }
}
