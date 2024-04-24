namespace HAKATONUS.Server.Models
{
    public class Investors
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public string Owner{ get; set; }
        public string SponsorProject{ get; set; }
        public string ProjectCount{ get; set; }
    }
}
