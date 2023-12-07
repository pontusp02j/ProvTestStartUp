using MediatR;
using Core.Responses.Products;
using System.ComponentModel.DataAnnotations;

namespace Core.Queries.Products
{
    public class GetAllProductsQuery : IRequest<ProductListResponse>
    {
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string? Category { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;

    }
}
