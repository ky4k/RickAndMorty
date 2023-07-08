using RickAndMorty.Models;

namespace RickAndMorty.Interfaces
{
    public interface IEpisodeRequester
    {
        Task<List<Episode>> GetAllEpisodes();
        Task<List<Episode>> GetEpisodesByIDlist(List<int> listID);
        Task<Episode> GetEpisode(int id);
        Task<Episode> GetEpisode(string name);
    }
}
