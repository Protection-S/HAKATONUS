namespace HAKATONUS.Server.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WhoNeed { get; set; }
        public string Description { get; set; }
        public string Instruments { get; set; }
        public string Language { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public DateTime Date { get; set; }
        public string Exp { get; set; }
    }
}
