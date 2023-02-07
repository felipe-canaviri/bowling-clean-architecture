using System.Collections.ObjectModel;

namespace Bowling.Core.Entities
{
    public class Game
    {
        public Game() 
        {
            Players = new Collection<Player>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
