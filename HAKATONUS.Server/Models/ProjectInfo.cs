namespace HAKATONUS.Server.Models
{
    public class ProjectInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Views { get; set; }
        public int Comments { get; set; }
        public string ImageUrl { get; set; }
        public string CompanyType { get; set; }
        public string Services { get; set; }
        public int Quality { get; set; }
        public int Conditions { get; set; }
        public int IndustryCoverage { get; set; }
    }
}
