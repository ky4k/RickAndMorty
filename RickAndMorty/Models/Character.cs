using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Models
{
    public class Character
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? status { get; set; }
        public string? species { get; set; }
        public string? type { get; set; }
        public string? gender { get; set; }
        public CharacterOrigin? origin { get; set; }
        public CharacterLocation? location { get; set; }
        public string? image { get; set; }
        public List<Episode>? episode { get; set; }//Take a Episodes' links 

        //[ForeignKey("EpisodeAndLocationIntoKey")]
        public string? url { get; set; }//Foreign key
        public string? created { get; set; }
    }
}
