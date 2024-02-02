using System.Text;


namespace MatchingGame.Model
{
    /// <summary>
    /// Ths MultiCharBoardGenerator class, generates a board with cards that consist
    /// of multiple ascii/utf-8 characters
    /// </summary>
    public class MultiCharBoardGenerator : IBoardGenerator
    {
        public static readonly char[] UTF_SYMBOLS = ['★', '♥', '♦', '♣', '♠', '☺', '♞', '☀', '☼', '♛', '▼', '▲'];
        public static readonly int CARD_LINES_NUMBER = 7;
        public static readonly string[] NOT_GUESSED_CARD_VALUE =
        [
            " _________",
            "|≥≤≥≤≥≤≥≤≥|",
            "|≥≤≥≤≥≤≥≤≥|",
            "|≥≤≥≤≥≤≥≤≥|",
            "|≥≤≥≤≥≤≥≤≥|",
            "|≥≤≥≤≥≤≥≤≥|",
            "|_________|"
        ];
        public static readonly string[] GUESSED_CARD_VALUE =
        [
            " _________",
            "|         |",
            "|         |",
            "|         |",
            "|         |",
            "|         |",
            "|_________|"
        ];

        private Random symbolsRandom;
        private Random numbersRandom;
        private Random cardsRandom;
        public MultiCharBoardGenerator()
        {
            symbolsRandom = new Random();
            numbersRandom = new Random();
            cardsRandom = new Random();
        }

        /// <summary>
        /// Randomly selects a card value from the available symbols and numbers
        /// and fill the board with these cards (each card has a duplicate).
        /// Cards are shuffled before being placed on the board.
        /// </summary>
        public Board GenerateBoard(int size)
        {
            var board = new Board(size);

            int uniqueCardsCount = board.Size * board.Size / 2;

            var cards = new List<Card>();

            for (int i = 0; i < uniqueCardsCount; i++)
            {
                bool isCardExists = false;
                char symbol = ' ';
                int n = 0;
                string cardName = "";
                do
                {
                    symbol = UTF_SYMBOLS[symbolsRandom.Next(0, UTF_SYMBOLS.Length)];
                    n = numbersRandom.Next(1, 9);
                    cardName = $"{n}{symbol}";
                    isCardExists = cards.Select(c => c.Name).Contains(cardName);
                }
                while (isCardExists);

                var cardValue = GenerateCardValue(n, symbol);
                var card = new Card(i + 1, cardValue, cardName);
                // adding two cards with the same value
                cards.Add(card);
                cards.Add((Card)card.Clone());
            }
            Shuffle(cards);

            for (int row = 1; row <= board.Size; row++)
            {
                for (int col = 1; col <= board.Size; col++)
                {
                    board[row, col] = cards[(row - 1)* board.Size + col - 1];
                }
            }
            return board;
        }

        /// <summary>
        /// Generates a card value with the given number and symbol.
        /// The number of symbols in the card value depends on the given number.
        /// Symbols are placed in specific pattern on 3x3 grid.
        /// </summary>
        public string[] GenerateCardValue(int n, char symbol)
        {
            var symbolGrid = new char[,]
                {
                    {
                        n >= 4 && n <= 9 ? symbol : ' ',
                        n >= 8 && n <=9 ? symbol : ' ',
                        n >= 2 && n <= 9 ? symbol : ' ',
                    },
                    {
                        n >= 6 && n <= 9 ? symbol : ' ',
                        n <= 9 && n % 2 > 0 ? symbol : ' ',
                        n >= 6 && n<= 9 ? symbol : ' ',
                    },
                    {
                        n >= 2 && n <= 9 ? symbol : ' ',
                        n >= 8 && n <= 9 ? symbol : ' ',
                        n >= 4 && n<= 9 ? symbol : ' ',
                    }

                };
            return
            [
                    " _________",
                    $"|{n}        |",
                    $"|  {symbolGrid[0,0]} {symbolGrid[0,1]} {symbolGrid[0,2]}  |",
                    $"|  {symbolGrid[1,0]} {symbolGrid[1,1]} {symbolGrid[1,2]}  |",
                    $"|  {symbolGrid[2,0]} {symbolGrid[2,1]} {symbolGrid[2,2]}  |",
                    $"|        {n}|",
                    "|_________|"
            ];
        }

        public List<string> GetLinesForOutput(Board board)
        {
            var result = new List<string>();
            for (int row = 1; row <= board.Size; row++)
            {
                for (int cli = 0; cli < CARD_LINES_NUMBER; cli++)
                {
                    var sb = new StringBuilder();
                    for (int col = 1; col <= board.Size; col++)
                    {
                        var card = board[row, col];
                        var cardLine = "";
                        switch (card.State)
                        {
                            case CardState.NotGuessed:
                                if (cli == NOT_GUESSED_CARD_VALUE.Length / 2)
                                {
                                    cardLine = $"|≥≤({row},{col})≥≤|";
                                }
                                else
                                {
                                    cardLine = NOT_GUESSED_CARD_VALUE[cli];
                                }
                                break;
                            case CardState.Guessed:
                                cardLine = GUESSED_CARD_VALUE[cli];
                                break;
                            case CardState.Guessing:
                                cardLine = card.Value[cli];
                                break;
                            default:
                                break;
                        }
                        
                        sb.Append(cardLine).Append('\t');
                    }
                    result.Add(sb.ToString());
                }
            }
            return result;
        }

        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = cardsRandom.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}
