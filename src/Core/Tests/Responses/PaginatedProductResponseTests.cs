using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Responses.Products;

namespace Core.Tests.Responses.Products
{
    [TestClass]
    public class PaginatedProductResponseTests
    {
        [TestMethod]
        public void ProductListResponse_ShouldInitializePropertiesCorrectly()
        {
            var productListResponse = new ProductListResponse();

            Assert.IsNotNull(productListResponse.Categories, "Categories should be initialized to an empty list.");
            Assert.AreEqual(0, productListResponse.Categories.Count, "Categories should initially be empty.");

            Assert.IsNull(productListResponse.Products, "Products should be null by default.");
        }

        [TestMethod]
        public void ProductListResponse_ShouldHandleProductAssignmentCorrectly()
        {
            var productListResponse = new ProductListResponse();
            var productResponse = new ProductResponse { Id = 1, Title = "Test Product" };

            productListResponse.Products = new List<ProductResponse> { productResponse };

            Assert.IsNotNull(productListResponse.Products, "Products should not be null after assignment.");
            Assert.AreEqual(1, productListResponse.Products.Count, "Products should contain one item.");
            Assert.AreEqual("Test Product", productListResponse.Products.First().Title, "The product title should match.");
        }
    }
}
