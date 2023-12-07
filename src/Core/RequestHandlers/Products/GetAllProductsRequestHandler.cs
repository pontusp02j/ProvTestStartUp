using AutoMapper;
using Core.Queries.Products;
using Core.Responses.Products;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using Core.Entities.Products;
using Microsoft.Extensions.Options;
using Core.Utilities;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Core.RequestHandlers.Products
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsQuery, ProductListResponse>
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);
        private readonly string? _productsApiUrl;

        public GetAllProductsRequestHandler(IMapper mapper, IHttpClientFactory httpClientFactory, 
            IMemoryCache cache, IOptions<ApiSettings> apiSettings)
        {
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient();
            _cache = cache;
            _productsApiUrl = apiSettings.Value.ProductsApiUrl;
        }

        public async Task<ProductListResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = GenerateCacheKey(request);
            if (TryGetFromCache(cacheKey, out ProductListResponse? cachedResponse))
            {
                return cachedResponse ?? new ProductListResponse();
            }

            var allProducts = await FetchAllProducts(cancellationToken);
            var filteredProducts = FilterProducts(allProducts, request);
            var paginatedProducts = PaginateProducts(filteredProducts, request);
            var productResponses = MapProducts(paginatedProducts);

            var response = new ProductListResponse
            {
                Products = productResponses,
                TotalCount = filteredProducts.Count,
                Categories = GetUniqueCategories(allProducts)
            };

            CacheResponse(cacheKey, response);
            return response;
        }

        private async Task<List<Product>> FetchAllProducts(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(_productsApiUrl, cancellationToken);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<List<Product>>(content) ?? new List<Product>();
        }

        private string GenerateCacheKey(GetAllProductsQuery request)
        {
            return $"Products_{request.MinPrice}_{request.MaxPrice}_{request.Category}_{request.PageNumber}_{request.PageSize}";
        }

        private bool TryGetFromCache(string cacheKey, out ProductListResponse? cachedResponse)
        {
            return _cache.TryGetValue(cacheKey, out cachedResponse);
        }

        private void CacheResponse(string cacheKey, ProductListResponse response)
        {
            _cache.Set(cacheKey, response, _cacheDuration);
        }

        private List<Product> FilterProducts(List<Product> products, GetAllProductsQuery request)
        {
            if (request.MinPrice.HasValue)
                products = products.Where(p => p.Price >= request.MinPrice.Value).ToList();
            if (request.MaxPrice.HasValue)
                products = products.Where(p => p.Price <= request.MaxPrice.Value).ToList();
            if (!string.IsNullOrWhiteSpace(request.Category))
                products = products.Where(p => p.Category.Equals(request.Category, StringComparison.OrdinalIgnoreCase)).ToList();
            
            return products;
        }

        private List<Product> PaginateProducts(List<Product> products, GetAllProductsQuery request)
        {
            return products
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
        }

        private List<ProductResponse> MapProducts(List<Product> products)
        {
            return products
                .Select(product =>
                {
                    var productResponse = _mapper.Map<ProductResponse>(product);

                    if (productResponse != null)
                    {
                        productResponse.Description = string.Empty;
                        productResponse.Rating = null;
                    }

                    return productResponse;
                })
                .Where(productResponse => productResponse != null)
                .ToList()!;
        }




        private List<string> GetUniqueCategories(List<Product> products)
        {
            return products.Select(p => p.Category).Distinct().ToList();
        }
    }
}