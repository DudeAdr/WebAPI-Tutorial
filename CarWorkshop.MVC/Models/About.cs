namespace CarWorkshop.MVC.Models
{
    public class About
    {
        public string? Tile { get; set; }
        public string? Description { get; set; }
        public List<string>? Tags { get; set; } = new List<string>();
    }
}
