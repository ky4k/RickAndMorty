using RickAndMorty.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Data
{
    public class CharacterEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string? name { get; set; }
        public string? status { get; set; }
        public string? species { get; set; }
        public string? type { get; set; }
        public string? gender { get; set; }
        public CharacterOrigin? origin { get; set; }
        public CharacterLocation? location { get; set; }
        public string? image { get; set; }
        public string? url { get; set; }
        public string? created { get; set; }
        //many-to-many[Chracter - Episode]
        public List<EpisodeEntity> EpisodeEntitys { get; set; }
        //one-to-many[Chracter-Location]
        public int LocationEntityId { get; set; }
        public LocationEntity LocationEntity { get; set; }
    }
}
