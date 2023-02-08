namespace Bowling.Web.Models
{
    public class TurnModel
    {
        public int Id { get; set; }
        public int FirstThrowing { get; set; }
        public int SecondThrowing { get; set; }

        public int PlayerId { get; set; }
        public PlayerModel Player { get; set; }
    }
}
