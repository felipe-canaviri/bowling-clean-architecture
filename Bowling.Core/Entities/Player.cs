using System.Collections.ObjectModel;

namespace Bowling.Core.Entities
{
    public class Player
    {

        public Player() 
        {
            Turns = new Collection<Turn>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public ICollection<Turn> Turns { get; set; }

    }
}