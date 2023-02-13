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
    public class TurnControllerTest
    {
        private readonly Mock<ITurnService> _mockService;
        private static IMapper _mapper;

        public TurnControllerTest()
        {
            _mockService = new Mock<ITurnService>();
            _mapper = MapperMockGenerator.CreateMockMapper();
        }

        [Fact]
        public async Task GivenATurnIdShouldReturnOneTurnModel()
        {
            var turn = new Turn
            {
                Id = 2,
                PlayerId = 1,
                FirstThrowing = 6,
                SecondThrowing = 1,
                ThirdThrowing = 0,
                TurnNumber = 2,
            };
            _mockService.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(turn);
            var controller = new TurnController(_mockService.Object, _mapper);

            var result = await controller.Get(2) as OkObjectResult;
            var turnResult = result.Value as TurnModel;
            turnResult.Should().NotBeNull();
            turnResult.Id.Should().Be(2);
            turnResult.FirstThrowing.Should().Be(6);
            turnResult.SecondThrowing.Should().Be(1);
            turnResult.ThirdThrowing.Should().Be(0);
        }

        [Fact]
        public async Task GivenValidDataShouldCreateATurn()
        {
            var turn = new Turn
            {
                Id = 1,
                PlayerId = 1,                
                FirstThrowing = 5,
                SecondThrowing = 5,
                ThirdThrowing = 0,
                TurnNumber = 1,
            };
            _mockService.Setup(x => x.Save(It.IsAny<Turn>())).ReturnsAsync(turn);
            _mockService.Setup(x => x.FindById(It.IsAny<int>())).ReturnsAsync(turn);
            var controller = new TurnController(_mockService.Object, _mapper);

            var result = await controller.Create(new TurnModel()) as OkObjectResult;
            var turnResult = result.Value as TurnModel;
            turnResult.Should().NotBeNull();
            turnResult.Id.Should().Be(1);
            turnResult.FirstThrowing.Should().Be(5);
            turnResult.SecondThrowing.Should().Be(5);
            turnResult.ThirdThrowing.Should().Be(0);
            turnResult.TurnNumber.Should().Be(1);
        }

        [Fact]
        public async Task ShouldUpdateAnExistingTurn()
        {
            var newTurn = new Turn
            {
                Id = 1,
                PlayerId = 1,
                FirstThrowing = 5,
                SecondThrowing = 0,
                ThirdThrowing = 0,
                TurnNumber = 1,
            };
            var updatedPlayer = new Turn 
            { 
                Id = 1, 
                PlayerId = 1,
                FirstThrowing = 5,
                SecondThrowing = 4,
                ThirdThrowing = 0,
                TurnNumber = 1,
            };
            _mockService.Setup(x => x.Save(It.IsAny<Turn>())).ReturnsAsync(newTurn);
            _mockService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Turn>())).ReturnsAsync(updatedPlayer);

            var controller = new TurnController(_mockService.Object, _mapper);

            var result = await controller.Create(new TurnModel()) as OkObjectResult;
            var newTurnModel = result.Value as TurnModel;

            // Update the new model with new values
            newTurnModel.SecondThrowing = 4;
            result = await controller.Update(1, newTurnModel) as OkObjectResult;
            var updatedModel = result.Value as TurnModel;

            updatedModel.Should().NotBeNull();
            updatedModel.Id.Should().Be(1);
            updatedModel.FirstThrowing.Should().Be(5);
            updatedModel.SecondThrowing.Should().Be(4);
        }

        private IEnumerable<Turn> GenerateTurns()
        {
            /*
             * Return a same game turns from 2 players with three turns each
             */
            return new List<Turn>() 
            { 
                new Turn
                { 
                    Id = 1, 
                    PlayerId = 1,
                    FirstThrowing = 5,
                    SecondThrowing = 5,
                    ThirdThrowing = 0,
                    TurnNumber = 1,
                },
                new Turn
                {
                    Id = 2,
                    PlayerId = 1,
                    FirstThrowing = 6,
                    SecondThrowing = 1,
                    ThirdThrowing = 0,
                    TurnNumber = 2,
                },
                new Turn
                {
                    Id = 3,
                    PlayerId = 1,
                    FirstThrowing = 7,
                    SecondThrowing = 0,
                    ThirdThrowing = 0,
                    TurnNumber = 3,
                },
                new Turn
                {
                    Id = 4,
                    PlayerId = 2,
                    FirstThrowing = 0,
                    SecondThrowing = 6,
                    ThirdThrowing = 0,
                    TurnNumber = 1,
                },
                new Turn
                {
                    Id = 5,
                    PlayerId = 2,
                    FirstThrowing = 8,
                    SecondThrowing = 1,
                    ThirdThrowing = 0,
                    TurnNumber = 2,
                },
                new Turn
                {
                    Id = 6,
                    PlayerId = 2,
                    FirstThrowing = 10,
                    SecondThrowing = 6,
                    ThirdThrowing = 0,
                    TurnNumber = 3,
                },
            };
        }
    }
}
