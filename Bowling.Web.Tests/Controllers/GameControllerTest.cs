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
    public class GameControllerTest
    {
        private readonly Mock<IGameService> _mockGameService;
        private static IMapper _mapper;
        public GameControllerTest()
        {
            _mockGameService = new Mock<IGameService>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProfilesMapper());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async Task ShouldReturnOkWhenGettingAllGames()
        {
            var games = GenerateGames();
            _mockGameService.Setup(x => x.FindAll()).ReturnsAsync(games);
            var controller = new GameController(_mockGameService.Object, _mapper);

            var result = await controller.GetAll() as OkObjectResult;
            var gamesResult = result.Value as IEnumerable<GameModel>;

            gamesResult.Should().NotBeNullOrEmpty();
            gamesResult.Count().Should().Be(8);
            gamesResult.First().GetType().Should().Be(typeof(GameModel));
        }

        [Fact]
        public async Task GivenAGameIdShouldReturnOneGame()
        {
            var gameModel = new Game
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                Status = (int)GameStatus.New
            };
            _mockGameService.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(gameModel);
            var controller = new GameController(_mockGameService.Object, _mapper);

            var result = await controller.Get(2) as OkObjectResult;
            var gameResult = result.Value as GameModel;
            gameResult.Should().NotBeNull();
            gameResult.Id.Should().Be(2);
        }

        [Fact]
        public async Task ShouldCreateAGameWithNoParameters()
        {
            var gameModel = new Game
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                Status = (int)GameStatus.New
            };
            _mockGameService.Setup(x => x.Save(It.IsAny<Game>())).ReturnsAsync(gameModel);
            _mockGameService.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(gameModel);
            var controller = new GameController(_mockGameService.Object, _mapper);

            var result = await controller.Create() as OkObjectResult;
            var gameResult = result.Value as GameModel;
            gameResult.Should().NotBeNull();
            gameResult.Id.Should().Be(2);
            gameResult.Status.Should().Be(0);
        }

        [Fact]
        public async Task ShouldUpdateAnExistingGame()
        {
            var newGame = new Game
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                Status = (int)GameStatus.New
            };
            var updatedGame = new Game { Id = 1, Status = (int)GameStatus.InProgress };
            _mockGameService.Setup(x => x.Save(It.IsAny<Game>())).ReturnsAsync(newGame);
            _mockGameService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Game>())).ReturnsAsync(updatedGame);

            var controller = new GameController(_mockGameService.Object, _mapper);

            var result = await controller.Create() as OkObjectResult;
            var newGameModel = result.Value as GameModel;

            // Update the new model with new values
            newGameModel.Status = GameStatus.InProgress;
            result = await controller.Update(1, newGameModel) as OkObjectResult;
            var updatedModel = result.Value as GameModel;

            updatedModel.Should().NotBeNull();
            updatedModel.Id.Should().Be(1);
            updatedModel.Status.Should().Be(GameStatus.InProgress);
        }

        private IEnumerable<Game> GenerateGames()
        {
            return new List<Game>()
            {
                new Game
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    Status = (int)GameStatus.InProgress
                },
                new Game
                {
                    Id = 2,
                    CreatedAt = DateTime.Now,
                    Status = (int)GameStatus.New
                },
                new Game
                {
                    Id = 3,
                    CreatedAt = DateTime.Now,
                    Status = (int)GameStatus.InProgress
                },
                new Game
                {
                    Id = 4,
                    CreatedAt = DateTime.Now,
                    Status = (int)GameStatus.Completed
                },
                new Game
                {
                    Id = 5,
                    CreatedAt = DateTime.Now,
                    Status = (int)GameStatus.InProgress
                },
                new Game
                {
                    Id = 6,
                    CreatedAt = DateTime.Now,
                    Status = (int)GameStatus.New
                },
                new Game
                {
                    Id = 7,
                    CreatedAt = DateTime.Now,
                    Status = (int)GameStatus.Completed
                },
                new Game
                {
                    Id = 8,
                    CreatedAt = DateTime.Now,
                    Status = (int)GameStatus.InProgress
                }
            };
        }
    }
}
