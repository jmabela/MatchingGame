using MatchingGame.Model;
using System.Text;

namespace MatchingGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}
