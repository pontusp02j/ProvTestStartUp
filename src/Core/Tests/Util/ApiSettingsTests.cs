using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Utilities;

namespace Core.Tests.Util
{
    [TestClass]
    public class ApiSettingsTests
    {
        [TestMethod]
        public void ProductsApiUrl_ShouldNotBeNullOrEmpty()
        {
            var apiSettings = new ApiSettings();

            apiSettings.ProductsApiUrl = "https://fakestoreapi.com/products";

            Assert.IsNotNull(apiSettings.ProductsApiUrl);
            Assert.AreNotEqual(string.Empty, apiSettings.ProductsApiUrl);
        }
    }
}
