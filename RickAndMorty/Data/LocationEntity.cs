using RickAndMorty.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Data
{
    public class LocationEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? dimension { get; set; }
        //one-to-many[Chracter - Location]
        public List<CharacterEntity>? CharactersEntity { get; set; }
        public string? url { get; set; }
        public string? created { get; set; }
    }
}
