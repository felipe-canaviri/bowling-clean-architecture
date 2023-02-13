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
    public class ScoreControllerTest
    {
        private readonly Mock<IScoreService> _mockService;

        public ScoreControllerTest()
        {
            _mockService = new Mock<IScoreService>();
        }

        [Fact]
        public async Task GivenPlayerAndGameIdentifiersShouldReturnOkWhenGettingScores()
        {
            var games = GenerateScores();
            _mockService.Setup(x => x.GetScore(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(games);
            var controller = new ScoreController(_mockService.Object);

            var result = await controller.GetScores(1, 1) as OkObjectResult;
            var scoresResult = result.Value as IEnumerable<Scores>;

            scoresResult.Should().NotBeNullOrEmpty();
            scoresResult.Count().Should().Be(1);
            scoresResult.First().GetType().Should().Be(typeof(Scores));
            scoresResult.First().playerid.Should().Be(1);
            scoresResult.First().GameId.Should().Be(1);
            scoresResult.First().Name.Should().Be("Bart");
            scoresResult.First().score.Should().Be(45);
        }
        
        private IEnumerable<Scores> GenerateScores()
        {
            return new List<Scores>()
            {
                new Scores()
                {
                    GameId = 1,
                    playerid = 1,
                    score = 45,
                    Name = "Bart",
                }
            };
        }
    }
}
