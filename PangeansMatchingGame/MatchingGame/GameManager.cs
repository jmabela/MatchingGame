using MatchingGame.Interfaces;
using MatchingGame.Model;

namespace MatchingGame
{
    public class GameManager
	{
        /// <summary>
        /// The input provider
        /// </summary>
        private IInputProvider inputProvider;

        /// <summary>
        /// The output provider
        /// </summary>
        private IOutputProvider outputProvider;

        private IBoardGenerator boardGenerator;

        private static readonly ConsoleColor PRIMARY_COLOR = ConsoleColor.Blue;

        public GameMode GameMode { get; private set; }
        public Board Board { get; private set; }
       
        public List<Player> Players { get; private set; }
        public Player CurrentPlayer { get; private set; }   

        public GameManager() : this(new ConsoleInputProvider(), new ConsoleOutputProvider(),
            new MultiCharBoardGenerator())
        {

        }

        public GameManager(IInputProvider inputProvider, IOutputProvider outputProvider,
            IBoardGenerator boardGenerator)
        {
            if (inputProvider == null)
                throw new ArgumentNullException(nameof(inputProvider));
            if (outputProvider == null)
                throw new ArgumentNullException(nameof(outputProvider));
            if (boardGenerator == null)
                throw new ArgumentNullException(nameof(boardGenerator));

            this.inputProvider = inputProvider;
            this.outputProvider = outputProvider;
            this.boardGenerator = boardGenerator;
        }

        public void StartGame()
        {
            outputProvider.SetColor((int)PRIMARY_COLOR);

            InitGame();

            outputProvider.WriteLine("Press enter to start the game...");

            while (true)
            {
                var notGuessedCards = Board.Filter(CardState.NotGuessed);
                // if we have just two cards left, we they will be the last pair
                if (notGuessedCards.Count == 2)
                {
                    foreach (var card in notGuessedCards)
                    {
                        card.State = CardState.Guessing;
                    }
                }
                else
                {
                    OpenCard(); // select first card
                    OpenCard(); // select second card
                }
                
                CheckMatch();

                if (Board.Filter(CardState.NotGuessed).Count == 0)
                {
                    outputProvider.WriteLine("Game over");
                    ShowWinner();
                    outputProvider.WriteLine("Press 1 to keep playing or any other key to exit the game");
                    var decision = inputProvider.Read();
                    if (decision == "1")
                    {
                        InitGame();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void ShowWinner()
        {
            var maxScore = Players.Max(p => p.Score);
            var winnersNames = string.Join(", ", Players.Where(p => p.Score == maxScore).Select(p => p.Name));

            outputProvider.WriteLine($"The winner is {winnersNames} with {maxScore} points!");
        }   

        private void InitGame()
        {
            outputProvider.Clear();

            outputProvider.WriteLine("\t \t \t Welcome to Matching Game!\n");

            InitPlayers();

            GameMode = SelectGameMode();

            Board = boardGenerator.GenerateBoard((int)GameMode);

            outputProvider.Log(Board.ToString());

            RevealAllCards();
        }

        private void RevealAllCards()
        {
            outputProvider.Clear();
            DrawBoard();
            for (int row = 1; row <= Board.Size; row++)
            {
                for (int col = 1; col <= Board.Size; col++)
                {
                    Board[row, col].State = CardState.NotGuessed;
                }
            }
            outputProvider.SetColor((int)PRIMARY_COLOR);
            outputProvider.WriteLine("\nPress enter to begin the game");
            inputProvider.Read();
        }


        public void InitPlayers()
        {
            // if we already have players, reset score/tries for them (in case they want to play again)
            if (Players != null && Players.Count > 0)
            {
                foreach (var player in Players)
                {
                    player.Reset();
                }
                CurrentPlayer = Players[0];
                return;
            }
            
            // otherwie, initialize players
            int numPlayers = 0;
            while (numPlayers <= 0)
            {
                outputProvider.WriteLine("How many people are playing?");
                var numPlayersStr = inputProvider.Read();
                if (!int.TryParse(numPlayersStr, out numPlayers) || numPlayers == 0)
                {
                    outputProvider.WriteLine("The input string provided is invalid!");
                }
            }

            Players = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
            {
                string curPlayerName = "";
                while (curPlayerName.Length == 0)
                {
                    outputProvider.WriteLine($"\nPlease enter the name of player {i + 1}: ");
                    curPlayerName = inputProvider.Read();
                }
                Players.Add(new Player(curPlayerName));
            }
            CurrentPlayer = Players[0];
        }

        public GameMode SelectGameMode()
        {
            outputProvider.WriteLine("\nPlease select game mode");
            outputProvider.WriteLine("1 - Easy \t" +
                                    "2 - Medium \t" +
                                    "3 - Hard \t");
            var input = inputProvider.Read();
            if (input != null && input.All(char.IsDigit))
            {
                switch (input)
                {
                    case "1":
                        return GameMode.Easy;

                    case "2":
                        return GameMode.Medium;

                    case "3":
                        return GameMode.Hard;

                    default:
                        outputProvider.WriteLine("Please choose a valid selection");
                        return SelectGameMode();

                }
            }
            else
            {
                outputProvider.WriteLine("Please choose a valid selection");
                return SelectGameMode();
            }
        }

        public void Redraw()
        {
            outputProvider.Clear();
            DrawPlayerInfo();
            DrawBoard();
        }

        public void OpenCard()
        {
            Redraw();

            int limit = Board.Size + 1;
            outputProvider.SetColor((int)PRIMARY_COLOR);
            outputProvider.WriteLine("\nChoose your card, select row and column (please use x,y format): ");

            var rowAndColumn = inputProvider.Read();
            var split = rowAndColumn.Split(",");

            if (split.Length != 2)
            {
                outputProvider.WriteLine("Please select a valid coordinate");
                inputProvider.Read();
                OpenCard();
                return;
            }

            int.TryParse(split[0].Trim(), out int row);
            int.TryParse(split[1].Trim(), out int column);


            if ((row > 0) && (row < limit) && (column > 0) && (column < limit))
            {
                Card card = Board[row, column];
                if (card.State == CardState.NotGuessed)
                {
                    card.State = CardState.Guessing;
                }
                else
                {
                    outputProvider.WriteLine("You already matched that card!");
                    inputProvider.Read();
                    OpenCard();
                }
            }
            else
            {
                outputProvider.WriteLine("Please select a valid coordinate");
                inputProvider.Read();
                OpenCard();
            }
        }

        public void CheckMatch()
        {
            List<Card> guessingCards = Board.Filter(CardState.Guessing);
            bool match = guessingCards[0].Code == guessingCards[1].Code;

            // Update number of tries and score for player before redrawing
            CurrentPlayer.NumberOfTries++;
            if (match)
            {
                CurrentPlayer.Score++;
            }

            Redraw();

            // Check if user guessed correctly, update card states 
            if (match)
            {
                
                foreach (Card card in guessingCards)
                {
                    card.State = CardState.Guessed;
                }
                outputProvider.WriteLine($"\nYou discovered {guessingCards[0].Name}!"); 
            }
            else
            {
                outputProvider.WriteLine("\nYou guessed incorrectly!");
                foreach (Card card in guessingCards)
                {
                    card.State = CardState.NotGuessed;
                }
            }

            outputProvider.WriteLine("Push enter to continue...");
            inputProvider.Read();

            if (!match)
            {
                // switch to next player if user guessed incorrectly
                NextPlayer();
            }
        }

        private void NextPlayer()
        {
            CurrentPlayer = Players[(Players.IndexOf(CurrentPlayer) + 1) % Players.Count];
        }

        public void DrawPlayerInfo()
        {
            outputProvider.WriteLine($"Current player: {CurrentPlayer.Name}\nScore: {CurrentPlayer.Score}" + 
                $"\nTries: {CurrentPlayer.NumberOfTries}\n");
        }

        public void DrawBoard()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToArray();
            List<string> lines = boardGenerator.GetLinesForOutput(Board);

            // get the height of a card, all cards have the same height
            int cardHeight = Board[1, 1].Value.Length;

            int colorIndex = 6;
            int lineIndex = 0;
            foreach (var line in lines)
            {
                if ((lineIndex % cardHeight) == 0)
                    colorIndex = 6;


                outputProvider.SetColor((int)colors[colorIndex]);
                outputProvider.WriteLine(line);

                if (colorIndex == 7)
                    colorIndex = 5;

                lineIndex++;
                colorIndex++;
            }
        }
    }
}
