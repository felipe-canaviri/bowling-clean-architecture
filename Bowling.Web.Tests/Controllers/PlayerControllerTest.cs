using AutoMapper;
using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Services;
using Bowling.Web.Controllers;
using Bowling.Web.Models;
using Bowling.Web.Tests.Mappers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Bowling.Web.Tests.Controllers
{
    public class PlayerControllerTest
    {
        private readonly Mock<IPlayerService> _mockService;
        private static IMapper _mapper;

        public PlayerControllerTest()
        {
            _mockService = new Mock<IPlayerService>();
            _mapper = MapperMockGenerator.CreateMockMapper();
        }

        [Fact]
        public async Task ShouldReturnOkWhenGettingAllPlayers()
        {
            var games = GeneratePlayers();
            _mockService.Setup(x => x.FindAll()).ReturnsAsync(games);
            var controller = new PlayerController(_mapper, _mockService.Object);

            var result = await controller.GetAll() as OkObjectResult;
            var gamesResult = result.Value as IEnumerable<PlayerModel>;

            gamesResult.Should().NotBeNullOrEmpty();
            gamesResult.Count().Should().Be(5);
            gamesResult.First().GetType().Should().Be(typeof(PlayerModel));
        }

        [Fact]
        public async Task GivenAPlayerIdShouldReturnOneGame()
        {
            var playerModel = new Player
            {
                Id = 2,
                GameId = 1,
                Name = "Test",
            };
            _mockService.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(playerModel);
            var controller = new PlayerController(_mapper, _mockService.Object);

            var result = await controller.Get(2) as OkObjectResult;
            var playerResult = result.Value as PlayerModel;
            playerResult.Should().NotBeNull();
            playerResult.Id.Should().Be(2);
            playerResult.Name.Should().Be("Test");
        }

        [Fact]
        public async Task GivenValidDataShouldCreateAPlayer()
        {
            var player = new Player
            {
                Id = 2,
                GameId = 1,
                Name = "Test",
            };
            _mockService.Setup(x => x.Save(It.IsAny<Player>())).ReturnsAsync(player);
            _mockService.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(player);
            var controller = new PlayerController(_mapper, _mockService.Object);

            var result = await controller.Create(new PlayerModel()) as OkObjectResult;
            var playerResult = result.Value as PlayerModel;
            playerResult.Should().NotBeNull();
            playerResult.Id.Should().Be(2);
            playerResult.GameId.Should().Be(1);
            playerResult.Name.Should().Be("Test");
        }

        [Fact]
        public async Task ShouldUpdateAnExistingPlayer()
        {
            var newPlayer = new Player
            {
                Id = 1,
                GameId = 1,
                Name = "Test"                
            };
            var updatedPlayer = new Player { Id = 1, GameId = 1, Name = "Bart" };
            _mockService.Setup(x => x.Save(It.IsAny<Player>())).ReturnsAsync(newPlayer);
            _mockService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Player>())).ReturnsAsync(updatedPlayer);

            var controller = new PlayerController(_mapper, _mockService.Object);

            var result = await controller.Create(new PlayerModel()) as OkObjectResult;
            var newPlayerModel = result.Value as PlayerModel;

            // Update the new model with new values
            newPlayerModel.Name = "Bart";
            result = await controller.Update(1, newPlayerModel) as OkObjectResult;
            var updatedModel = result.Value as PlayerModel;

            updatedModel.Should().NotBeNull();
            updatedModel.Id.Should().Be(1);
            updatedModel.GameId.Should().Be(1);
            updatedModel.Name.Should().Be("Bart");
        }

        private IEnumerable<Player> GeneratePlayers()
        {
            return new List<Player>() {
                new Player() { 
                    Id = 1,
                    GameId = 1,
                    Name = "Bart"
                },
                new Player() {
                    Id = 2,
                    GameId = 1,
                    Name = "Lisa"
                },
                new Player() {
                    Id = 3,
                    GameId = 1,
                    Name = "Maggie"
                },
                new Player() {
                    Id = 4,
                    GameId = 1,
                    Name = "Marge"
                },
                new Player() {
                    Id = 5,
                    GameId = 1,
                    Name = "Homer"
                }
            };
        }
    }
}
