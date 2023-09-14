using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RickAndMorty.Controllers;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using RickAndMorty.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace RickAndMortyTests
{
    //This test vived all type exeptions which be realezed in CharacteHttpRepository.
    //Also, this test vived get corected data from API in last test.
    [TestClass]
    public class CharacteHttpRepositoryArgumentExeptionTest
    {
        [TestMethod]
        public async Task GetByIDlist_InvalidInput_ThrowsException()
        {
            // Arrange
            var httpClient = new HttpClient();
            var characterRepository = new CharacteHttpRepository(httpClient);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => characterRepository.GetByIDlist(new List<int> { 0, 1 }));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => characterRepository.GetByIDlist(new List<int> { }));
            await Assert.ThrowsExceptionAsync<JsonSerializationException>(() => characterRepository.GetByIDlist(new List<int> { -1 }));
            await Assert.ThrowsExceptionAsync<JsonSerializationException>(() => characterRepository.GetByIDlist(new List<int> { -1 }));
        }
        [TestMethod]
        public async Task GetID_InvalidInput_ThrowsException()
        {
            // Arrange
            var httpClient = new HttpClient();
            var characterRepository = new CharacteHttpRepository(httpClient);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetID(0));
        }
        [TestMethod]
        public async Task GetByName_InvalidInput_ThrowsException()
        {
            // Arrange
            var httpClient = new HttpClient();
            var characterRepository = new CharacteHttpRepository(httpClient);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetByName(null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetByName(""));
        }
        [TestMethod]
        public async Task GetCharacterStatus_InvalidInput_ThrowsException()
        {
            // Arrange
            string name = "Rick";
            string status = "Alive";
            var httpClient = new HttpClient();
            var characterRepository = new CharacteHttpRepository(httpClient);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterStatus(null, status));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterStatus(name, null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterStatus(null, null));
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => characterRepository.GetCharacterStatus("Dvsv", "fsdf"));
        }
        [TestMethod]
        public async Task GetCharacterBySpecies_InvalidInput_ThrowsException()
        {
            // Arrange
            var httpClient = new HttpClient();
            var characterRepository = new CharacteHttpRepository(httpClient);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterBySpecies(null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterBySpecies(""));
        }
        [TestMethod]
        public async Task GetCharacterByType_InvalidInput_ThrowsException()
        {
            // Arrange
            var httpClient = new HttpClient();
            var characterRepository = new CharacteHttpRepository(httpClient);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterByType(null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterByType(""));
        }
        [TestMethod]
        public async Task GetCharacteGender_InvalidInput_ThrowsException()
        {
            // Arrange
            string name = "Rick";
            string gender = "Male";
            var httpClient = new HttpClient();
            var characterRepository = new CharacteHttpRepository(httpClient);

            // Act & Assert
            var res = characterRepository.GetID(1);
            Assert.IsInstanceOfType(res.Result, typeof(Character));
            Assert.AreEqual(res.Id, 1); //View coorect data
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterStatus(null, gender));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterStatus(name, null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => characterRepository.GetCharacterStatus(null, null));
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => characterRepository.GetCharacterStatus("Dvsv", "fsdf"));
        }
    }
}

