using AutoMapper;
using Core.Queries.Products;
using Core.Responses.Products;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http;
using Core.Entities.Products;
using Microsoft.Extensions.Options;
using Core.Utilities;

namespace Core.RequestHandlers.Products
{
    public class GetProductByIdRequestHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);
        private readonly string? _productsApiUrl;

        public GetProductByIdRequestHandler(IMapper mapper, IHttpClientFactory httpClientFactory, 
            IMemoryCache cache, IOptions<ApiSettings> apiSettings)
        {
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient();
            _cache = cache;
            _productsApiUrl = apiSettings.Value.ProductsApiUrl;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"Product_{request.Id}";

            if (_cache.TryGetValue(cacheKey, out ProductResponse? cachedProduct))
            {
                return cachedProduct ?? new ProductResponse();
            }

            var url = $"{_productsApiUrl}/{request.Id}";
            var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var product = JsonConvert.DeserializeObject<Product>(content);

            var productResponse = _mapper.Map<ProductResponse>(product);

            _cache.Set(cacheKey, productResponse, _cacheDuration);

            return productResponse;
        }
    }
}
