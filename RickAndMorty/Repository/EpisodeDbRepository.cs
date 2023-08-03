using Microsoft.EntityFrameworkCore;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;

namespace RickAndMorty.Repository
{
    public class EpisodeDbRepository: IEpisodeDB
    {
        ApplicationContext db;
        public EpisodeDbRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<List<Episode>> GetAllEpisodes()
        {
            List<Episode> episodes = await db.Episodes.ToListAsync();
            if (episodes.Any())
                return episodes;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Episode>> GetEpisodesByIDlist(List<int> listID)
        {
            if (listID == null) throw new ArgumentNullException("list is null");

            var HasNegativeValue = listID.Any(x => x < 0);
            if (HasNegativeValue) throw new ArgumentException("list has negative value");

            List<Episode> episodes = await db.Episodes.Where(c => listID.Contains(c.id)).ToListAsync();

            if (episodes.Any())
                return episodes;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<Episode> GetEpisodeByID(int id)
        {
            if (id < 0) throw new ArgumentException("id must be more then 0");
            var episodes = await db.Episodes.Where(c => c.id == id).FirstOrDefaultAsync();
            if (!(episodes is null))
                return episodes;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Episode>> GetEpisodeByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            List<Episode> episodes = await db.Episodes.Where(c => name.Contains(c.name)).ToListAsync();
            if (episodes.Any())
                return episodes;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Episode>> GetEpisodeByEpisode(string episode)
        {
            if (string.IsNullOrWhiteSpace(episode))
                throw new ArgumentException("Name cannot be empty.", nameof(episode));
            List<Episode> episodes = await db.Episodes.Where(c => episode.Contains(c.episode)).ToListAsync();
            if (episodes.Any())
                return episodes;
            else
                throw new ArgumentNullException("list is null");
        }
    }
}
