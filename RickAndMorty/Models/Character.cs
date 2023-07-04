using System.ComponentModel.DataAnnotations.Schema;
using Azure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RickAndMorty.Models
{
    public class Character
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
        [NotMapped]
        public List<string>? episode { get; set; }
        public string? url { get; set; }
        public string? created { get; set; }
        //many-to-many[Character-Episode]
        public List<Episode>? EpisodesList { get; set; }=new List<Episode>();
        //many-to-one[Chracter-Location]
        public int? LocationId { get; set; }
        public Location? LocationList { get; set; }//name was LocationList
    }
}
