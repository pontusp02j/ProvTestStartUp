using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using Core.Queries.Products;
using Core.Responses.Products;
using Core.RequestHandlers.Products;
using System.Threading.Tasks;
using System.Threading;
using Core.Entities.Products;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Core.Utilities;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Core.UnitTests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _fakeResponse;

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _fakeResponse = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_fakeResponse);
        }
    }

    [TestClass]
    public class GetProductByIdRequestHandlerTests
    {
        private Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        private Mock<IOptions<ApiSettings>> _mockApiSettings = new Mock<IOptions<ApiSettings>>();
        private HttpClient _httpClient = new HttpClient();

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
        }

        [TestMethod]
        public async Task GetProductById_ReturnsProductResponse()
        {
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();

            var product = new Product
            {
                Id = 1,
                Title = "Test Product",
                Price = 100,
                Description = "Test Description",
                Category = "Test Category",
                Image = "TestImage.jpg",
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

            var expectedProductResponse = new ProductResponse
            {
                Id = product.Id,
                Title = product.Title,
            };

            _mockMapper.Setup(m => m.Map<ProductResponse>(It.IsAny<Product>()))
                        .Returns(expectedProductResponse);

            var cache = new MemoryCache(new MemoryCacheOptions());
            var cacheKey = $"Product_{1}";

            var cachedProduct = new ProductResponse
            {
                Id = product.Id,
                Title = product.Title,
            };

            cache.Set(cacheKey, cachedProduct, TimeSpan.FromMinutes(30));

            var handler = new GetProductByIdRequestHandler(_mockMapper.Object, _mockHttpClientFactory.Object, cache, _mockApiSettings.Object);
            var request = new GetProductByIdQuery(1);

            var result = await handler.Handle(request, CancellationToken.None);

            stopwatch.Stop();

            var timeLimit = TimeSpan.FromSeconds(1);

            Assert.IsTrue(stopwatch.Elapsed <= timeLimit, $"Execution Time exceeded {timeLimit.TotalSeconds} seconds");

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedProductResponse.Id, result.Id);
            Assert.AreEqual(expectedProductResponse.Title, result.Title);
        }
    }
}
