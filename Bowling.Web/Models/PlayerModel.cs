
namespace Bowling.Web.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int GameId { get; set; }        
        public List<TurnModel>? Turns { get; set; }
    }
}
