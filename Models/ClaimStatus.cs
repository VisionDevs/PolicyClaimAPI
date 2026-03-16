namespace PolicyClaimAPI.Models
{
    public static class ClaimStatus
    {
        public const string Submitted = "Submitted";
        public const string UnderReview = "Under Review";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public const string Paid = "Paid";
        
        public static readonly List<string> AllStatuses = new()
        {
            Submitted, UnderReview, Approved, Rejected, Paid
        };
    }
}