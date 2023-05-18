using Microsoft.EntityFrameworkCore;

namespace RickAndMorty.Models
{
    [Owned]
    public class CharacterOrigin
    {
        public string? name { get; set; }
        public string? url { get; set; }
    }
}
