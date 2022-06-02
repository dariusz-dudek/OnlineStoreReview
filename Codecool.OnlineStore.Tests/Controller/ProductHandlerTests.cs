using Codecool.OnlineStore.Controller;
using Codecool.OnlineStore.Data.DAL;
using Codecool.OnlineStore.Data.Entities;
using Codecool.OnlineStore.Factories;
using Codecool.OnlineStore.Utils;
using Codecool.OnlineStore.Views.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Codecool.OnlineStore.Tests.Controller
{
    [TestFixture]
    public class ProductHandlerTests
    {
        private IFactory<UnitOfWork> subFactory;
        private IInputSystem subInputSystem;
        private IMenuDisplay subMenuDisplay;

        [SetUp]
        public void SetUp()
        {
            this.subFactory = Substitute.For<IFactory<UnitOfWork>>();
            this.subInputSystem = Substitute.For<IInputSystem>();
            this.subMenuDisplay = Substitute.For<IMenuDisplay>();
        }

        private ProductHandler CreateProductHandler()
        {
            return new ProductHandler(
                this.subFactory,
                this.subInputSystem,
                this.subMenuDisplay);
        }




        [Test]
        public void EditProduct_IfProductIsNull_ThrowNullReferenceException()
        {
            // Arrange
            var productHandler = this.CreateProductHandler();

            // Act


            // Assert
            Assert.Throws<NullReferenceException>(() => productHandler.EditProduct());
        }

        [Test]
        public void EditName_IfProductIsNull_ThrowNullReferenceException()
        {
            // Arrange
            var productHandler = this.CreateProductHandler();
            Product product = null;

            // Act


            // Assert
            Assert.Throws<NullReferenceException>(() => productHandler.EditName(product));
        }

        [Test]
        public void EditDescription_IfProductIsNull_ThrowNullReferenceException()
        {
            // Arrange
            var productHandler = this.CreateProductHandler();
            Product product = null;

            // Act
            ;

            // Assert
            Assert.Throws<NullReferenceException>(() => productHandler.EditDescription(product));
        }

        [Test]
        public void EditPrice_IfProductIsNull_ThrowNullReferenceException()
        {
            // Arrange
            var productHandler = this.CreateProductHandler();
            Product product = null;

            // Act


            // Assert
            Assert.Throws<NullReferenceException>(() => productHandler.EditPrice(product));
        }

        [Test]
        public void EditAmount_IfProductIsNull_ThrowNullReferenceException()
        {
            // Arrange
            var productHandler = this.CreateProductHandler();
            Product product = null;

            // Act


            // Assert
            Assert.Throws<NullReferenceException>(() => productHandler.EditAmount(product));
        }

        [Test]
        public void ChangeActiveState_IfProductIsNull_ThrowNullReferenceException()
        {
            // Arrange
            var productHandler = this.CreateProductHandler();
            Product product = null;
            bool state = false;

            // Act


            // Assert
            Assert.Throws<NullReferenceException>(() => productHandler.ChangeActiveState(
                product,
                state));
        }

        [Test]
        public void ChangeActiveState_IfStateIsTrue_ShoudlReturnTrue()
        {
            // Arrange
            var productHandler = this.CreateProductHandler();
            Product product = new Product() { IsAvailable = false };
            bool state = true;

            // Act
            productHandler.ChangeActiveState(product, state);

            // Assert
            Assert.IsTrue(product.IsAvailable);
        }


    }
}
