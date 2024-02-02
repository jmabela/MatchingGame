using System.Text;

namespace MatchingGame.Model
{
    public class Board
    {
        private Card[,] cards;

        public int Size { get; private set; }

        public Board(int size)
        {
            Size = size;
            cards = new Card[size, size];
        }

        public Card this[int col, int row]
        {
            get
            {
                return cards[col - 1, row - 1];
            }
            set
            {
                cards[col - 1, row - 1] = value;
            }
        }

        public List<Card> Filter(CardState state)
        {
            return cards.Cast<Card>().Where(c => c.State == state).ToList();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = 1; row <= Size; row++)
            {
                for (int col = 1; col <= Size; col++)
                {
                    sb.Append(this[row, col]).Append('\t');
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
