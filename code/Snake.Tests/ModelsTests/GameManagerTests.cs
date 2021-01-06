using System;
using NUnit.Framework;
using Snake.Models;

namespace Snake.Tests.ModelsTests
{
    class GameManagerTests
    {
        IGameManager gameManager;
        private readonly Size size = new Size(20, 20);
        private const int turnTime = 3000; 

        [SetUp]
        public void Init()
        {
            gameManager = new GameManager();
        }

        [TearDown]
        public void Dispose()
        {
            gameManager = null;
        }

        /// <summary>
        /// Проверяет создание новой доски
        /// </summary>
        [Test]
        [Timeout(5000)] // 5 секунд
        public void CreateNewGameBoardNotEmptyTest()
        {
            Guid resGuid = gameManager.CreateNewGameBoard(size, turnTime);
            
            Assert.AreNotEqual(resGuid, Guid.Empty);
        }

        /// <summary>
        /// Проверяет что две созданные доски имеют разные Guid
        /// </summary>
        [Test]
        [Timeout(10000)] // 5 секунд
        public void TestTwoCreatedBoardsDifferentGuidTest()
        {
            Guid gameGuid1 = gameManager.CreateNewGameBoard(size, turnTime);
            Guid gameGuid2 = gameManager.CreateNewGameBoard(size, turnTime);
            
            Assert.AreNotEqual(gameGuid1, gameGuid2);
        }
        
        /// <summary>
        /// Проверяет получение созданной доски
        /// </summary>
        [Test]
        [Timeout(5000)] // 5 секунд
        public void GetCreatedGameboardNotNullTest()
        {
            Guid gameGuid = gameManager.CreateNewGameBoard(size, turnTime);

            IGameBoard gameBoard = gameManager.GetGameBoard(gameGuid);

            Assert.NotNull(gameBoard);
        }

        /// <summary>
        /// Проверяет что полученные две доски разные
        /// </summary>
        [Test]
        [Timeout(10000)]
        public void TwoGameboardsAreDifferentTest()
        {
            Guid gameGuid1 = gameManager.CreateNewGameBoard(size, turnTime);
            Guid gameGuid2 = gameManager.CreateNewGameBoard(size, turnTime);

            IGameBoard gameBoard1 = gameManager.GetGameBoard(gameGuid1);
            IGameBoard gameBoard2 = gameManager.GetGameBoard(gameGuid2);

            Assert.NotNull(gameBoard1);
            Assert.NotNull(gameBoard2);
            Assert.AreNotEqual(gameBoard1, gameBoard2);
        }

        /// <summary>
        /// Проверяет что после создания второй доски, первая не изменилась
        /// </summary>
        [Test]
        [Timeout(10000)]
        public void AfterSecondGameCreationFirstIsSameTest()
        {
            Guid gameGuid1 = gameManager.CreateNewGameBoard(size, turnTime);
            IGameBoard gameBoard11 = gameManager.GetGameBoard(gameGuid1);

            Guid gameGuid2 = gameManager.CreateNewGameBoard(size, turnTime);

            IGameBoard gameBoard12 = gameManager.GetGameBoard(gameGuid1);
            Assert.AreEqual(gameBoard11, gameBoard12);
        }
    }
}
