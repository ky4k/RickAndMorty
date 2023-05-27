using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace RickAndMorty.Models
{
    public class Location
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? dimension { get; set; }
        public List<string>? residents { get; set; }
        public string? url { get; set; }
        public string? created { get; set; }
    }
}

