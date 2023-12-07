using MediatR;
using Core.Responses.Products;

namespace Core.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
