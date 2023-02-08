namespace Bowling.Core.Entities
{
    public class Turn
    {
        public Turn() 
        {
        }
        public int Id { get; set; }
        public int FirstThrowing { get; set; }
        public int SecondThrowing { get; set; }
        public int ThirdThrowing { get; set; }
        public int TurnNumber { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}