using NUnit.Framework;
using Snake.Models;

namespace Snake.Tests.ModelsTests
{
    class DirectionTests
    {
        /// <summary>
        /// Проверяет метод IsDirectionOk на некорректных данных
        /// </summary>
        [Test]
        public void IncorrectDirectionTest()
        {
            SnakeDirection direction = new SnakeDirection() { Direction = (EnumDirection)111 };
            Assert.False(direction.IsDirectionOk());
        }
        
        /// <summary>
        /// Проверяет метод IsDirectionOk на корретных данных
        /// </summary>
        [TestCase(EnumDirection.Left)]
        [TestCase(EnumDirection.Right)]
        [TestCase(EnumDirection.Top)]
        [TestCase(EnumDirection.Bottom)]
        public void CorrectDirectionTest(EnumDirection dir)
        {
            SnakeDirection direction = new SnakeDirection() { Direction = dir };
            Assert.True(direction.IsDirectionOk());
        }
        
        /// <summary>
        /// Проверяет метод IsDirectionsContrary на обратных направлениях
        /// </summary>
        [TestCase(EnumDirection.Left, EnumDirection.Right)]
        [TestCase(EnumDirection.Top, EnumDirection.Bottom)]
        public void ContraryDirectionTest(EnumDirection firstDir, EnumDirection contraryDir)
        {
            SnakeDirection direction = new SnakeDirection() { Direction = firstDir };
            SnakeDirection contraryDirection = new SnakeDirection() { Direction = contraryDir };
            Assert.True(direction.IsDirectionsContrary(contraryDirection));
            Assert.True(contraryDirection.IsDirectionsContrary(direction));
        }
        
        /// <summary>
        /// Проверяет метод IsDirectionsContrary на одинаковых направлениях
        /// </summary>
        [Test]
        public void SameDirectionTest()
        {
            SnakeDirection direction = new SnakeDirection() { Direction = EnumDirection.Left };
            Assert.False(direction.IsDirectionsContrary(direction));
        }
        
        /// <summary>
        /// Проверяет метод IsDirectionsContrary на разных, не обратных направлениях
        /// </summary>
        [TestCase(EnumDirection.Top, EnumDirection.Left)]
        [TestCase(EnumDirection.Top, EnumDirection.Right)]
        [TestCase(EnumDirection.Left, EnumDirection.Bottom)]
        [TestCase(EnumDirection.Right, EnumDirection.Bottom)]
        public void NonContraryDirectionTest(EnumDirection firstDir, EnumDirection nonContraryDir)
        {
            SnakeDirection direction = new SnakeDirection() { Direction = firstDir };
            SnakeDirection nonContraryDirection = new SnakeDirection() { Direction = nonContraryDir };
            Assert.False(direction.IsDirectionsContrary(nonContraryDirection));
            Assert.False(nonContraryDirection.IsDirectionsContrary(direction));
        }
    }
}
