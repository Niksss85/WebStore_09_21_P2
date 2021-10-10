using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        //private class ConfigurationMoq : IConfiguration
        //{
        //    public IEnumerable<IConfigurationSection> GetChildren() { throw new System.NotImplementedException(); }

        //    public IChangeToken GetReloadToken() { throw new System.NotImplementedException(); }

        //    public IConfigurationSection GetSection(string key) { throw new System.NotImplementedException(); }

        //    public string this[string key] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        //}

        [TestMethod]
        public void Index_Returns_View()
        {
            // A-A-A

            #region Arrange

            var configuration_mock = new Mock<IConfiguration>();
            var product_data_mock = new Mock<IProductData>();

            product_data_mock
               .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
               .Returns(new ProductsPage(Enumerable.Empty<Product>(), 0));

            var controller = new HomeController(configuration_mock.Object);

            #endregion

            #region Act

            var result = controller.Index(product_data_mock.Object);

            #endregion

            #region Assert

            Assert.IsType<ViewResult>(result);

            #endregion
        }

        [TestMethod]
        public void SecondAction_Returns_Correct_Content()
        {
            const string expected_result = "123";

            var configuration_mock = new Mock<IConfiguration>();
            configuration_mock
               .Setup(c => c["Greetings"])
               .Returns(expected_result);

            var controller = new HomeController(configuration_mock.Object);

            var result = controller.SecondAction();

            var content_result = Assert.IsType<ContentResult>(result);
            var actual_result = content_result.Content;
            Assert.Equal(expected_result, actual_result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            // A-A-A

            #region Arrange

            var configuration_mock = new Mock<IConfiguration>();
            var product_data_mock = new Mock<IProductData>();

            var controller = new HomeController(configuration_mock.Object);

            #endregion

            #region Act

            var result = controller.Blog();

            #endregion

            #region Assert

            Assert.IsType<ViewResult>(result);

            #endregion
        }
    }
}