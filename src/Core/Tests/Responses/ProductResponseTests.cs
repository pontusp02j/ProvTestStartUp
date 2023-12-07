using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Responses.Products;
using Core.Entities.Products;

namespace Core.Tests.Responses.Products
{
    [TestClass]
    public class ProductResponseTests
    {
        [TestMethod]
        public void Rating_ShouldInitializeNewRating_WhenSetToNull()
        {
            var productResponse = new ProductResponse();

            productResponse.Rating = null;

            Assert.IsNotNull(productResponse.Rating, "Rating should not be null.");
            Assert.IsInstanceOfType(productResponse.Rating, typeof(Rating), "Rating should be of type Rating.");
        }
    }
}
