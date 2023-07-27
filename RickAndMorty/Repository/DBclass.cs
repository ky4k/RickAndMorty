using Microsoft.EntityFrameworkCore;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using System.Collections;
using System.Net.WebSockets;

namespace RickAndMorty.Operations
{
    public class DBclass/* : IDataOperation*/
    {
        private string location_url = "https://rickandmortyapi.com/api/location";
        private string episode_url = "https://rickandmortyapi.com/api/episode";
        private string character_url = "https://rickandmortyapi.com/api/character";

        ApplicationContext ap;
        public DBclass(ApplicationContext ap)
        {
            this.ap = ap;
        }

        //public async Task UpdateData()
        //{
        //    Requester<Character> character_requester = new Requester<Character>();
        //    Requester<Location> location_requester = new Requester<Location>();
        //    Requester<Episode> episode_requester = new Requester<Episode>();

        //    var character_response = await character_requester.GetResponseAsync(character_url);
        //    var location_response = await location_requester.GetResponseAsync(location_url);
        //    var episode_response = await episode_requester.GetResponseAsync(episode_url);

        //    foreach (var character in character_response)
        //    {
        //        var existingCharacter = ap.Characters.FirstOrDefault(c => c.id == character.id);
        //        if (existingCharacter != null)
        //        {
        //            existingCharacter.name = character.name;
        //            existingCharacter.status = character.status;
        //            existingCharacter.species = character.species;
        //            existingCharacter.type = character.type;
        //            existingCharacter.gender = character.gender;
        //            existingCharacter.origin = character.origin;
        //            existingCharacter.location = character.location;
        //            existingCharacter.image = character.image;
        //            existingCharacter.url = character.url;
        //            existingCharacter.created = character.created;
        //            existingCharacter.EpisodesList.Clear();
        //        }
        //        else
        //        {
        //            ap.Characters.Add(character);
        //        }
        //        foreach (var chracter_episode in character.episode)
        //        {
        //            foreach (var episode in episode_response)
        //            {
        //                var existingEpisode = ap.Episodes.FirstOrDefault(e => e.id == episode.id);
        //                if (existingEpisode != null)
        //                {
        //                    existingEpisode.name = episode.name;
        //                    existingEpisode.air_date = episode.air_date;
        //                    existingEpisode.episode = episode.episode;
        //                    existingEpisode.url = episode.url;
        //                    existingEpisode.created = episode.created;
        //                    existingEpisode.CharactersList.Clear();
        //                }
        //                else
        //                {
        //                    ap.Episodes.Add(episode);
        //                }

        //                if (chracter_episode == episode.url)
        //                {
        //                    creating many-to - many connection
        //                    character.EpisodesList.Add(episode);
        //                }
        //            }
        //            foreach (var location in location_response)
        //            {
        //                var existingLocation = ap.Locations.FirstOrDefault(l => l.id == location.id);
        //                if (existingLocation != null)
        //                {
        //                    existingLocation.name = location.name;
        //                    existingLocation.type = location.type;
        //                    existingLocation.dimension = location.dimension;
        //                    existingLocation.url = location.url;
        //                    existingLocation.created = location.created;
        //                    existingLocation.Characters.Clear();
        //                }
        //                else
        //                {
        //                    ap.Locations.Add(location);
        //                }
        //                foreach (var residents in location.residents)
        //                {
        //                    if (residents == character.url)
        //                    {
        //                        character.LocationId = location.id;
        //                        character.LocationList = location;
        //                        location.Characters.Add(character);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    await ap.SaveChangesAsync();
        //}


        //WORK!
        //    public async Task UpdateData()
        //    {
        //        ICharacterRequester character_requester = new ICharacterRequester();
        //        ILocationRequester location_requester = new ILocationRequester();
        //        IEpisodeRequester episode_requester = new IEpisodeRequester();

        //        var character_response = await character_requester.GetResponseAsync(character_url);
        //        var location_response = await location_requester.GetResponseAsync(location_url);
        //        var episode_response = await episode_requester.GetResponseAsync(episode_url);

        //        foreach (var character in character_response)
        //        {
        //            var existingCharacter = ap.Characters.FirstOrDefault(c => c.id == character.id);
        //            if (existingCharacter != null)
        //            {
        //                existingCharacter.name = character.name;
        //                existingCharacter.status = character.status;
        //                existingCharacter.species = character.species;
        //                existingCharacter.type = character.type;
        //                existingCharacter.gender = character.gender;
        //                existingCharacter.origin = character.origin;
        //                existingCharacter.location = character.location;
        //                existingCharacter.image = character.image;
        //                existingCharacter.url = character.url;
        //                existingCharacter.created = character.created;
        //            }
        //            else
        //            {
        //                ap.Characters.Add(character);
        //            }

        //        }
        //        foreach (var location in location_response)
        //        {
        //            var existingLocation = ap.Locations.FirstOrDefault(l => l.id == location.id);
        //            if (existingLocation != null)
        //            {
        //                existingLocation.name = location.name;
        //                existingLocation.type = location.type;
        //                existingLocation.dimension = location.dimension;
        //                existingLocation.url = location.url;
        //                existingLocation.created = location.created;
        //            }
        //            else
        //            {
        //                ap.Locations.Add(location);
        //            }
        //        }

        //        foreach (var episode in episode_response)
        //        {
        //            var existingEpisode = ap.Episodes.FirstOrDefault(e => e.id == episode.id);
        //            if (existingEpisode != null)
        //            {
        //                existingEpisode.name = episode.name;
        //                existingEpisode.air_date = episode.air_date;
        //                existingEpisode.episode = episode.episode;
        //                existingEpisode.url = episode.url;
        //                existingEpisode.created = episode.created;
        //            }
        //            else
        //            {
        //                ap.Episodes.Add(episode);
        //            }
        //        }
        //        //much relationship
        //        foreach (var character in character_response)
        //        {
        //            foreach (var chracter_episode in character.episode)
        //            {
        //                foreach (var episode in episode_response)
        //                {
        //                    if (chracter_episode == episode.url)
        //                    {
        //                        //creating many-to-many connection
        //                        character.EpisodesList.Add(episode);
        //                    }
        //                }
        //                foreach (var location in location_response)
        //                {
        //                    foreach (var residents in location.residents)
        //                    {
        //                        if (residents == character.url)
        //                        {
        //                            character.LocationId = location.id;
        //                            character.LocationList = location;
        //                            location.Characters.Add(character);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        await ap.SaveChangesAsync();
        //    }

    }
}
