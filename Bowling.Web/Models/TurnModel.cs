namespace Bowling.Web.Models
{
    public class TurnModel
    {
        public int Id { get; set; }
        public int FirstThrowing { get; set; }
        public int SecondThrowing { get; set; }
        public int ThirdThrowing { get; set; }
        public int TurnNumber { get; set; }

        public int PlayerId { get; set; }
        public int GameId { get; set; }
    }
}
