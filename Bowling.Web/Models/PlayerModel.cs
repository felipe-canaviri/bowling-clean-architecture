using Bowling.Core.Entities;

namespace Bowling.Web.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Score { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
    }
}
