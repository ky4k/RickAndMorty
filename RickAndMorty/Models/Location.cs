namespace RickAndMorty.Models
{
    public class Location
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? dimension { get; set; }
        public List<Character>? characters { get; set; } //Позже можно попробовать изменить на string
        public string? url { get; set; }
        public string? created { get; set; }
    }
}
}
