namespace ContosoUniversity.Models
{
    public class Membership
    {
        public int MemberID { get; set; }
        public int Student1ID { get; set; }
        public int Student2ID { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }

        // Navigation properties
        public Student Student1 { get; set; }
        public Student Student2 { get; set; }

    }

}
