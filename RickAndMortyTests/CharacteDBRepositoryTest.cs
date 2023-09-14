using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using RickAndMorty.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace RickAndMortyTests
{
    //All tests passed
    [TestClass]
    public class CharacteDBRepositoryTest
    {
        private DbContextOptions<ApplicationContext> _options;
        private ApplicationContext _context;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationContext(_options);
            var characters = new List<Character>
            {
                new Character { id = 1, name = "Character 1",status="alive" },
                new Character { id = 2, name = "Character 2", status="died" },
                new Character { id = 3, name = "Character 3", status = "alive" }
            };

            _context.Characters.AddRange(characters);
            _context.SaveChanges();
        }


        //1) Get All
        [TestMethod]
        public async Task GetAll_ReturnsListOfCharacters()
        {
            //arrange
            var repository = new CharacterDbRepository(_context);
            //act
            var result = await repository.GetAll();
            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }
        [TestMethod]
        public async Task GetAll_EmptyDatabase_ThrowsArgumentNullException()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureDeleted(); 
            }

            // Arrange
            using (var context = new ApplicationContext(_options))
            {
                var repository = new CharacterDbRepository(context);

                // assert
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => repository.GetAll());
            }
        }

        //2) GetByIDlist
        [TestMethod]
        public async Task GetByIDlist_ReturnsListOfCharacters()
        {
            //arrange
            var repository = new CharacterDbRepository(_context);
            //act
            var result = await repository.GetByIDlist(new List<int> { 1, 3 });
            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        public async Task GetByIDlist_EmptyDatabase_ThrowsArgumentNullException()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureDeleted(); 
            }

            // Arrange
            using (var context = new ApplicationContext(_options))
            {
                var repository = new CharacterDbRepository(context);

                // assert
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => repository.GetByIDlist(new List<int> { 1,3}));
            }
        }
        [TestMethod]
        public async Task GetByIDlist_EmptyList_ThrowsArgumentNullException()
        {
            //arrange
            var repository = new CharacterDbRepository(_context);
            //act
            //assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => repository.GetByIDlist(new List<int>()));

        }
        [TestMethod]
        public async Task GetByIDlist_NegativeValue_ThrowsArgumentException()
        {
            //arrange
            var repository = new CharacterDbRepository(_context);
            //act
            //assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => repository.GetByIDlist(new List<int> {1,-2}));

        }

        //3)GetID
        [TestMethod]
        public async Task GetID_ReturnID()
        {
            //arrange
            var repository = new CharacterDbRepository(_context);
            //act
            var result = await repository.GetID(1);
            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.id);
        }
        [TestMethod]
        public async Task GetID_EmptyDatabase_ThrowsArgumentNullException()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureDeleted();
            }

            // Arrange
            using (var context = new ApplicationContext(_options))
            {
                var repository = new CharacterDbRepository(context);

                // assert
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => repository.GetID(1));
            }
        }
        [TestMethod]
        public async Task GetID_NegativeValue_ThrowsArgumentException()
        {
            //arrange
            var repository = new CharacterDbRepository(_context);
            //act
            //assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => repository.GetID(0));
        }

        //GetCharacterStatus
        [TestMethod]
        public async Task GetCharacterStatus_ReturnListOfCharacters()
        {
            //arrange
            var name = "Character 2";
            var status_died = "died";
            var repository = new CharacterDbRepository(_context);
            //act
            var result = await repository.GetCharacterStatus(name, status_died);
            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
        [TestMethod]
        public async Task GetCharacterStatus_EmptyDatabase_ThrowsArgumentNullException()
        {
            //arrange
            var name = "Character 2";
            var status_died = "died";

            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureDeleted();
            }

            // Arrange
            using (var context = new ApplicationContext(_options))
            {
                var repository = new CharacterDbRepository(context);

                // assert
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => repository.GetCharacterStatus(name,status_died));
            }
        }

        [TestMethod]
        public async Task GetCharacterStatus_EmptyDatabase_ThrowsArgumentException()
        {
            //arrange
            var repository = new CharacterDbRepository(_context);
            //act
            //assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => repository.GetCharacterStatus(null,null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => repository.GetCharacterStatus("Character 1", null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => repository.GetCharacterStatus(null, "Alive"));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => repository.GetCharacterStatus("", null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => repository.GetCharacterStatus("Character 1", ""));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => repository.GetCharacterStatus(null, ""));
        }

    }
}
