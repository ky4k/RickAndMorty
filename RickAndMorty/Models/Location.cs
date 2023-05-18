namespace RickAndMorty.Models
{
    public class Location
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? dimension { get; set; }
        public List<Character>? residents { get; set; } //Take Characters' links
        public string? url { get; set; }
        public string? created { get; set; }
    }
}

