﻿using NUnit.Framework;
using System;
using Microsoft.AspNetCore.Mvc;
using Snake.Models;
using Snake.Controllers;

namespace Snake.Tests.ControllersTests
{
    public class GameControllerTests
    {
        IGameManager gameManager;
        GameController controller;
        Guid curGameGuid;

        [SetUp]
        public void Init()
        {
            gameManager = new GameManager();
            controller = new GameController(gameManager);
            curGameGuid = (Guid)((OkObjectResult)controller.GetGameboard()).Value;
        }

        [TearDown]
        public void Dispose()
        {
            gameManager = null;
            controller = null;
            curGameGuid = Guid.Empty;
        }

        /// <summary>
        /// Проверяет корректный запрос на изменение направления
        /// </summary>
        [Test]
        public void ChangeSnakeDirectionTest()
        {
            EnumDirection newDirection;

            if (gameManager.GetGameBoard(curGameGuid).SnakeDirection != EnumDirection.Left)
                newDirection = EnumDirection.Left;
            else
                newDirection = EnumDirection.Right;

            ActionResult actionResult = controller.PatchDirection(curGameGuid, new SnakeDirection { Direction = newDirection });

            Assert.IsInstanceOf<OkResult>(actionResult);
            Assert.AreEqual(newDirection, gameManager.GetGameBoard(curGameGuid).SnakeDirection);
        }
        
        /// <summary>
        /// Проверяет случай новое_направление == текущее
        /// </summary>
        [Test]
        public void ChangeSnakeDirectionToCurrentTest()
        {
            EnumDirection newDirection = gameManager.GetGameBoard(curGameGuid).SnakeDirection;

            ActionResult actionResult = controller.PatchDirection(curGameGuid, new SnakeDirection { Direction = newDirection });

            Assert.IsInstanceOf<OkResult>(actionResult);
            Assert.AreEqual(newDirection, gameManager.GetGameBoard(curGameGuid).SnakeDirection);
        }
        
        /// <summary>
        /// Проверяет случай некорректного направления в запросе
        /// </summary>
        [Test]
        public void ChangeSnakeToBadDirectionTest()
        {
            ActionResult actionResult = controller.PatchDirection(curGameGuid, new SnakeDirection { Direction = (EnumDirection)111 });

            Assert.IsInstanceOf<BadRequestResult>(actionResult);
        }
        
        /// <summary>
        /// Проверяет случай поворота на 180 градусов
        /// </summary>
        [Test]
        public void ChangeSnakeDirection180Test()
        {
            ActionResult actionResult = controller.PatchDirection(curGameGuid, new SnakeDirection { Direction = EnumDirection.Bottom });

            Assert.IsInstanceOf<BadRequestResult>(actionResult);
        }
        
        /// <summary>
        /// Проверяет соответствие реального состояния игры и возвращаемого в GetGameboard
        /// </summary>
        [Test]
        public void CompareGetGameboardAndActualTest()
        {
            IGameBoard expectedGameBoard = gameManager.GetGameBoard(curGameGuid);

            ActionResult actionResult = controller.GetGameboard(curGameGuid);
            var contentResult = actionResult as OkObjectResult;
            var resultGameBord = (IGameBoard)contentResult.Value;

            Assert.AreEqual(expectedGameBoard, resultGameBord);
        }
    }
}
