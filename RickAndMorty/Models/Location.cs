using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace RickAndMorty.Models
{
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? dimension { get; set; }
        [NotMapped]
        public List<string>? residents { get; set; }
        public string? url { get; set; }
        public string? created { get; set; }
        //one-to-many [Location-Character]
        public List<Character>? Characters { get; set; } = new List<Character>();
    }
}

