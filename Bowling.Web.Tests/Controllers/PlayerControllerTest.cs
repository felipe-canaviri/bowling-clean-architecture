using AutoMapper;
using Bowling.Core.Entities;
using Bowling.Core.Interfaces.Services;
using Bowling.Web.Controllers;
using Bowling.Web.Mappers;
using Bowling.Web.Models;
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
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProfilesMapper());
            });
            _mapper = mappingConfig.CreateMapper();
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
