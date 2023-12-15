using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using Core.Queries.Products;
using Core.Responses.Products;
using Core.RequestHandlers.Products;
using Core.Entities.Products;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Core.Utilities;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using Core.Tests.Util;

namespace Core.UnitTests
{

    [TestClass]
    public class GetAllProductsRequestHandlerTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<IOptions<ApiSettings>> _mockApiSettings;
        private HttpClient _httpClient;

        public GetAllProductsRequestHandlerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockApiSettings = new Mock<IOptions<ApiSettings>>();
            _httpClient = new HttpClient();
        }


        [TestInitialize]
        public void Initialize()
        {
            _mockMapper = new Mock<IMapper>();
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockApiSettings = new Mock<IOptions<ApiSettings>>();

            var product = new Product
            {
                Id = 1,
                Title = "Test Product",
                Price = 9.99,
                Description = "This is a test product description.",
                Category = "Test Category",
                Image = "test-image.jpg",
                Rating = new Rating()
                {
                    Rate = 3,
                    Count = 20
                }
            };

            var responseContent = JsonConvert.SerializeObject(product);
            var fakeResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
            };

            var mockHandler = new MockHttpMessageHandler(fakeResponseMessage);
            _httpClient = new HttpClient(mockHandler);

            _mockApiSettings.Setup(x => x.Value).Returns(new ApiSettings { ProductsApiUrl = "http://fakeapi.com" });
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductResponse>();
            });
            _mockMapper.Setup(x => x.ConfigurationProvider).Returns(configuration);
        }

        [TestMethod]
        public async Task GetAllProducts_ReturnsProductListResponse()
        {
            var product1 = new Product
            {
                Id = 1,
                Title = "Test Product 1",
                Price = 9.99,
                Description = "Description 1",
                Category = "Category 1",
                Image = "test-image1.jpg",
                Rating = new Rating()
                {
                    Rate = 3,
                    Count = 20
                }
            };

            var product2 = new Product
            {
                Id = 2,
                Title = "Test Product 2",
                Price = 19.99,
                Description = "Description 2",
                Category = "Category 2",
                Image = "test-image2.jpg",
                Rating = new Rating()
                {
                    Rate = 4,
                    Count = 15
                }
            };

            var products = new List<Product> { product1, product2 };
            var cache = new MemoryCache(new MemoryCacheOptions());
            var responseContent = JsonConvert.SerializeObject(products);
            var fakeResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
            };

            var mockHandler = new MockHttpMessageHandler(fakeResponseMessage);
            var httpClient = new HttpClient(mockHandler);

            _mockApiSettings.Setup(x => x.Value).Returns(new ApiSettings { ProductsApiUrl = "https://fakestoreapi.com/products" });
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var handler = new GetAllProductsRequestHandler(_mockMapper.Object, _mockHttpClientFactory.Object, cache, _mockApiSettings.Object);
            var request = new GetAllProductsQuery();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = await handler.Handle(request, CancellationToken.None);

            stopwatch.Stop();

            Assert.IsNotNull(result);
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 1000, "Execution time exceeded 1 second");
        }

    }
}
