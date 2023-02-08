using Bowling.Core.Entities;
using Bowling.Core.Interfaces;
using Bowling.Core.Interfaces.Services;

namespace Bowling.Services
{
    public class TurnService : ITurnService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TurnService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Turn> FindById(int id)
        {
            return await _unitOfWork.TurnRepository.GetByIdAsync(id);
        }

        public async Task<Turn> Save(Turn newTurn)
        {
            await _unitOfWork.TurnRepository.AddAsync(newTurn);
            await _unitOfWork.CommitAsync();

            return newTurn;
        }

        public async Task<Turn> Update(int turnToBeUpdatedId, Turn newTurnValues)
        {
            Turn turn = await _unitOfWork.TurnRepository.GetByIdAsync(turnToBeUpdatedId);

            if (turn == null)
                throw new ArgumentException("Invalid turn ID while updating");

            turn.FirstThrowing = newTurnValues.FirstThrowing;
            turn.SecondThrowing = newTurnValues.SecondThrowing;
            turn.ThirdThrowing = newTurnValues.ThirdThrowing;
            turn.TurnNumber = newTurnValues.TurnNumber;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.TurnRepository.GetByIdAsync(turnToBeUpdatedId);
        }
    }
}
