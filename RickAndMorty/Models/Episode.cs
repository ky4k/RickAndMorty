using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Models
{
    public class Episode
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? air_date { get; set; }
        public string? episode { get; set; }
        public List<Character>? characters { get; set; } //Take characters' links

        //[ForeignKey("CharacterInfoKey")]
        public string? url { get; set; }
        public string? created { get; set; }

    }
}
