using RickAndMorty.Models;

namespace RickAndMorty.Interfaces
{
    public interface IEpisodeRequester
    {
        Task<List<Episode>> GetAllEpisodes();
        Task<List<Episode>> GetEpisodesByIDlist(List<int> listID);
        Task<Episode> GetEpisodeByID(int id);
        Task<List<Episode>> GetEpisodeByName(string name);
        Task<List<Episode>> GetEpisodeByEpisode(string episode);
    }
}
