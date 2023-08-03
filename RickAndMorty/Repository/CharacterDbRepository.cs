using Microsoft.EntityFrameworkCore;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using System.Collections.Generic;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RickAndMorty.Repository
{
    public class CharacterDbRepository : ICharacterDB
    {
        ApplicationContext db;
        public CharacterDbRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<List<Character>> GetAll()
        {
            var characters = await db.Characters.ToListAsync();
            if (characters.Any())
                return characters;
            else
                throw new ArgumentNullException("list is null");

        }
        public async Task<List<Character>> GetByIDlist(List<int> listID)
        {
            if (listID == null) throw new ArgumentNullException("list is null");

            var HasNegativeValue = listID.Any(x => x < 0);
            if (HasNegativeValue) throw new ArgumentException("list has negative value");

            List<Character> characters = await db.Characters.Where(c => listID.Contains(c.id)).ToListAsync();

            if (characters.Any())
                return characters;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<Character> GetID(int id)
        {
            if (id < 0) throw new ArgumentException("id must be more then 0");
            var character = await db.Characters.Where(c=>c.id== id).FirstOrDefaultAsync();
            if (!(character is null))
                return character;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Character>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            List<Character> characters = await db.Characters.Where(c=>name.Contains(c.name)).ToListAsync();
            if (characters.Any())
                return characters;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Character>> GetCharacterStatus(string name, string status)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Gender cannot be empty.", nameof(status));
            var characters = db.Characters.Where(c =>c.name==name);
            if (characters.Any())
            {
                characters = characters.Where(c => c.status == status);
                List<Character> result = await characters.ToListAsync();
                return result;
            }   
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Character>> GetCharacterBySpecies(string species)
        {
            if (string.IsNullOrWhiteSpace(species))
                throw new ArgumentException("Name cannot be empty.", nameof(species));
            List<Character> characters = await db.Characters.Where(c => c.species==species).ToListAsync();
            if (characters.Any())
                return characters;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Character>> GetCharacterByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Name cannot be empty.", nameof(type));
            List<Character> characters = await db.Characters.Where(c => c.type == type).ToListAsync();
            if (characters.Any())
                return characters;
            else
                throw new ArgumentNullException("list is null");
        }
        public async Task<List<Character>> GetCharacteGender(string name, string gender)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(gender))
                throw new ArgumentException("Gender cannot be empty.", nameof(gender));
            var characters = db.Characters.Where(c => c.name == name);
            if (characters.Any())
            {
                characters = characters.Where(c => c.gender == gender);
                List<Character> result = await characters.ToListAsync();
                return result;
            }
            else
                throw new ArgumentNullException("list is null");
        }
    }
}
