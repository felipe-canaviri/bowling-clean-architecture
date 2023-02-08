namespace Bowling.Core.Entities
{
    public class Scores
    {
        public Scores() { }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
