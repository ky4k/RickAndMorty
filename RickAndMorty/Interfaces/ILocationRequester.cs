using RickAndMorty.Models;

namespace RickAndMorty.Interfaces
{
    public interface ILocationRequester
    {
        Task<List<Location>> GetAllLocations();
        Task<List<Location>> GetLocationsByIDlist(List<int> listID);
        Task<Location> GetLocation(int id);
        Task<Location> GetLocation(string name);
    }
}
