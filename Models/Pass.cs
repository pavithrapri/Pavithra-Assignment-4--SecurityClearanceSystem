namespace VisitorSecurityClearance.Models
{
    public class Pass
    {
        public string Id { get; set; }
        public string VisitorId { get; set; }
        public string PassCode { get; set; }
        public DateTime GeneratedTime { get; set; }
    }
}
