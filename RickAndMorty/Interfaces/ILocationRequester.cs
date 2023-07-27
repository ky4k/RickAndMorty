using RickAndMorty.Models;

namespace RickAndMorty.Interfaces
{
    public interface ILocationRequester
    {
        Task<List<Location>> GetAll();
        Task<List<Location>> GetByIDlist(List<int> listID);
        Task<Location> GetByID(int id);
        Task<List<Location>> GetByName(string name);
        Task<List<Location>> GetByType(string type);
        Task<List<Location>> GetByDimension(string dimension);
    }
}
