namespace MatchingGame.Model
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int NumberOfTries { get; set; }
        public Player(string name)
        {
            Name = name;
        }

        public void Reset()
        {
            Score = 0;
            NumberOfTries = 0;
        }
    }
}