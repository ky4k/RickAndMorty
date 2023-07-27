using RickAndMorty.Models;

namespace RickAndMorty.Interfaces
{
    public interface ICharacterRequester
    {
        Task<List<Character>> GetAll();
        Task<List<Character>> GetByIDlist(List<int> listID);
        Task<Character> GetID(int id);
        Task<List<Character>> GetByName(string name);
        Task<List<Character>> GetCharacterStatus(string name, string status);
        Task<List<Character>> GetCharacterBySpecies(string species);
        Task<List<Character>> GetCharacterByType(string type);
        Task<List<Character>> GetCharacteGender(string name, string gender);


    }
}
