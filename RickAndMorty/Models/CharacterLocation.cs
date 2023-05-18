using Microsoft.EntityFrameworkCore;

namespace RickAndMorty.Models
{
    [Owned]
    public class CharacterLocation
    {
        public string? name { get; set; }
        public string? url { get; set; }
    }
}
