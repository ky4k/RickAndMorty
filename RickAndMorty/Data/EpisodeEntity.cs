using RickAndMorty.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Data
{
    public class EpisodeEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string? name { get; set; }
        public string? air_date { get; set; }
        public string? episode { get; set; }
        public string? url { get; set; }
        public string? created { get; set; }
        //many-to-many[Chracter-Episode]
        public List<CharacterEntity> CharacterEntitys { get; set; } = new();
    }
}
