using Bowling.Core.Entities;

namespace Bowling.Web.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public GameStatus Status { get; set; }
    }
}
