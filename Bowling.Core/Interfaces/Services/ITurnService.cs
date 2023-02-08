using Bowling.Core.Entities;

namespace Bowling.Core.Interfaces.Services
{
    public interface ITurnService
    {
        Task<Turn> FindById(int id);
        Task<Turn> Save(Turn newTurn);
        Task<Turn> Update(int turnToBeUpdatedId, Turn newTurnValues);
    }
}
