namespace MatchingGame.Model
{
    /// <summary>
    /// Ths IBoardGenerator interface, generates a board and formats it for output
    /// </summary>
    public interface IBoardGenerator
    {
        /// <summary>
        /// Returns a new board of size [size, size] filled with randomly generated cards
        /// </summary>
        Board GenerateBoard(int size);

        /// <summary>
        /// Formats the board for output
        /// </summary>
        List<string> GetLinesForOutput(Board board);
    }
}
