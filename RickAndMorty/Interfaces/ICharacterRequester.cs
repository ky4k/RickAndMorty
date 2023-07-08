using RickAndMorty.Models;

namespace RickAndMorty.Interfaces
{
    public interface ICharacterRequester
    {
        Task<List<Character>> GetAllCharacters();
        Task<List<Character>> GetCharactersByIDlist(List<int> listID);
        Task<Character> GetCharacter(int id);
        Task<Character> GetCharacter(string name);
        Task<Character> GetCharacterStatus(string name, string status);
        Task<Character> GetCharacteGender(string name, string gender);


    }
}
