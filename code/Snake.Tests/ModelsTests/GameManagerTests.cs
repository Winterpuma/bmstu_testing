using System;
using NUnit.Framework;
using Snake.Models;
using Snake.Tests.DataBuilder;

namespace Snake.Tests.ModelsTests
{
    class GameManagerTests
    {
        IGameManager gameManager;
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
            Size size = new SizeBuilder().Box10x10().Build();

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
            Size size = new SizeBuilder().Box10x10().Build();

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
            Size size = new SizeBuilder().Box10x10().Build();
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
            Size size = new SizeBuilder().Box10x10().Build();
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
            Size size = new SizeBuilder().Box10x10().Build();
            Guid gameGuid1 = gameManager.CreateNewGameBoard(size, turnTime);
            IGameBoard gameBoard11 = gameManager.GetGameBoard(gameGuid1);

            Guid gameGuid2 = gameManager.CreateNewGameBoard(size, turnTime);

            IGameBoard gameBoard12 = gameManager.GetGameBoard(gameGuid1);
            Assert.AreEqual(gameBoard11, gameBoard12);
        }
    }
}
