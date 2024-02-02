using MatchingGame.Model;

namespace MatchingGame.Tests
{

    [TestClass]
    public class MultiCharBoardGeneratorTests
    {
        private MultiCharBoardGenerator boardGenerator;

        [TestInitialize]
        public void Init()
        {
            boardGenerator = new MultiCharBoardGenerator();
        }

        [TestMethod]
        public void GenerateCardValue_For1_ActualCardHeightEqualsToConst()
        {
            string[] cardValue = boardGenerator.GenerateCardValue(5, 'A');
            Assert.AreEqual(MultiCharBoardGenerator.CARD_LINES_NUMBER, cardValue.Length);
        }

        [TestMethod]
        public void GenerateCardValue_For1_ReturnsCorrectPattern()
        {
            string[] expectedCardValue =
            [
                " _________",
                "|1        |",
                "|         |",
                "|    H    |",
                "|         |",
                "|        1|",
                "|_________|"
            ];
            string[] generatedCardValue = boardGenerator.GenerateCardValue(1, 'H');
            CollectionAssert.AreEqual(expectedCardValue, generatedCardValue);
        }

        [TestMethod]
        public void GenerateCardValue_For2_ReturnsCorrectPattern()
        {
            string[] expectedCardValue =
            [
                " _________",
                "|2        |",
                "|      P  |",
                "|         |",
                "|  P      |",
                "|        2|",
                "|_________|"
            ];
            string[] generatedCardValue = boardGenerator.GenerateCardValue(2, 'P');
            CollectionAssert.AreEqual(expectedCardValue, generatedCardValue);
        }

        [TestMethod]
        public void GenerateCardValue_For3_ReturnsCorrectPattern()
        {
            string[] expectedCardValue =
            [
                " _________",
                "|3        |",
                "|      ♦  |",
                "|    ♦    |",
                "|  ♦      |",
                "|        3|",
                "|_________|"
            ];
            string[] generatedCardValue = boardGenerator.GenerateCardValue(3, '♦');
            CollectionAssert.AreEqual(expectedCardValue, generatedCardValue);
        }

        [TestMethod]
        public void GenerateCardValue_For4_ReturnsCorrectPattern()
        {
            string[] expectedCardValue =
            [
                " _________",
                "|4        |",
                "|  ♣   ♣  |",
                "|         |",
                "|  ♣   ♣  |",
                "|        4|",
                "|_________|"
            ];
            string[] generatedCardValue = boardGenerator.GenerateCardValue(4, '♣');
            CollectionAssert.AreEqual(expectedCardValue, generatedCardValue);
        }

        [TestMethod]
        public void GenerateCardValue_For5_ReturnsCorrectPattern()
        {
            string[] expectedCardValue =
            [
                " _________",
                "|5        |",
                "|  X   X  |",
                "|    X    |",
                "|  X   X  |",
                "|        5|",
                "|_________|"
            ];
            string[] generatedCardValue = boardGenerator.GenerateCardValue(5, 'X');
            CollectionAssert.AreEqual(expectedCardValue, generatedCardValue);
        }

        [TestMethod]
        public void GenerateCardValue_For6_ReturnsCorrectPattern()
        {
            string[] expectedCardValue =
            [
                " _________",
                "|6        |",
                "|  ♠   ♠  |",
                "|  ♠   ♠  |",
                "|  ♠   ♠  |",
                "|        6|",
                "|_________|"
            ];
            string[] generatedCardValue = boardGenerator.GenerateCardValue(6, '♠');
            CollectionAssert.AreEqual(expectedCardValue, generatedCardValue);
        }

        [TestMethod]
        public void GenerateCardValue_For7_ReturnsCorrectPattern()
        {
            string[] expectedCardValue =
            [
                " _________",
                "|7        |",
                "|  H   H  |",
                "|  H H H  |",
                "|  H   H  |",
                "|        7|",
                "|_________|"
            ];
            string[] generatedCardValue = boardGenerator.GenerateCardValue(7, 'H');
            CollectionAssert.AreEqual(expectedCardValue, generatedCardValue);
        }

        [TestMethod]
        public void GenerateCardValue_For8_ReturnsCorrectPattern()
        {
            string[] expectedCardValue =
            [
                " _________",
                "|8        |",
                "|  P P P  |",
                "|  P   P  |",
                "|  P P P  |",
                "|        8|",
                "|_________|"
            ];
            string[] generatedCardValue = boardGenerator.GenerateCardValue(8, 'P');
            CollectionAssert.AreEqual(expectedCardValue, generatedCardValue);
        }

        [TestMethod]
        public void GenerateCardValue_For9_ReturnsCorrectPattern()
        {
            string[] expectedCardValue =
            [
                " _________",
                "|9        |",
                "|  ♦ ♦ ♦  |",
                "|  ♦ ♦ ♦  |",
                "|  ♦ ♦ ♦  |",
                "|        9|",
                "|_________|"
            ];
            string[] generatedCardValue = boardGenerator.GenerateCardValue(9, '♦');
            CollectionAssert.AreEqual(expectedCardValue, generatedCardValue);
        }
    }
}
