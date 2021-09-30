
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebStore.Controllers;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
   [TestClass]
   public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        { 
            //A-A-A
            #region Arrange
            var configuration_mock = new Mock<IConfiguration>();

            var product_data_mock = new Mock<IProductData>();
            var controller = new HomeController(configuration_mock.Object);
            product_data_mock
                .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(Enumerable.Empty<Product>());

            #endregion

            #region Act
            var result = controller.Index(product_data_mock.Object);
            #endregion

            #region Assert
            Assert.IsType<ViewResult>(result);
            #endregion
        }
        [TestMethod]
        public void SecondAction_Returns_View_Correct_Content()
        {
            const string expected_result = "123";

            var configuration_mock = new Mock<IConfiguration>();
            configuration_mock
                .Setup(c => c["Greetings"])
                .Returns(expected_result);

            var product_data_mock = new Mock<IProductData>();
   
            var controller = new HomeController(configuration_mock.Object);


            var result = controller.SecondAction();

            var content_result = Assert.IsType<ContentResult>(result);

            var actual_result = content_result.Content;

            Assert.Equal(expected_result, actual_result);
        }
        [TestMethod]
        public void Blog_Returns_View()
        {

            var configuration_mock = new Mock<IConfiguration>();

            var product_data_mock = new Mock<IProductData>();
           
            var controller = new HomeController(configuration_mock.Object);


            var result = controller.Blog();

            Assert.IsType<ViewResult>(result);

        }
    }
}
