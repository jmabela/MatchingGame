using MatchingGame.Interfaces;
using MatchingGame.Model;
using Moq;

namespace MatchingGame.Tests
{
    [TestClass]
    public class GameManagerTests
    {
        private Mock<IInputProvider> mockInputProvider = new Mock<IInputProvider>(MockBehavior.Strict);
        private Mock<IOutputProvider> mockOutputProvider = new Mock<IOutputProvider>(MockBehavior.Strict);

        private GameManager gm;

        [TestInitialize]
        public void Init()
        {
            mockOutputProvider.Setup(x => x.WriteLine(It.IsAny<string>())).Verifiable();

            gm = new GameManager(mockInputProvider.Object,
                mockOutputProvider.Object, new MultiCharBoardGenerator());
        }

        [TestMethod]
        public void SelectGameMode_ForUserInput1_ReturnsGameModeToEasy()
        {
            mockInputProvider.Setup(x => x.Read()).Returns("1");

            GameMode selectedGameMode = gm.SelectGameMode();

            Assert.AreEqual(GameMode.Easy, selectedGameMode);
        }

        [TestMethod]
        public void SelectGameMode_ForUserInput2_ReturnsGameModeToMedium()
        {
            mockInputProvider.Setup(x => x.Read()).Returns("2");
            GameMode selectedGameMode = gm.SelectGameMode();

            Assert.AreEqual(GameMode.Medium, selectedGameMode);
        }

        [TestMethod]
        public void SelectGameMode_ForUserInput3_ReturnsGameModeToHard()
        {
            mockInputProvider.Setup(x => x.Read()).Returns("3");
            GameMode selectedGameMode = gm.SelectGameMode();

            Assert.AreEqual(GameMode.Hard, selectedGameMode);
        }

        [TestMethod]
        public void InitPlayers_ForUserInput2_CreatesTwoPlayers()
        {
            mockInputProvider.SetupSequence(x => x.Read())
                .Returns("2")
                .Returns("John")
                .Returns("Doe");
            gm.InitPlayers();

            Assert.AreEqual(2, gm.Players.Count);
        }
    }
}