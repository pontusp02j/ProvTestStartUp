using Core.Queries.Products;
using FastEndpoints;
using MediatR;
using Core.Responses.Products;

namespace Core.Endpoints.Get.Products.GetAllProducts
{
    public class GetAllProductsEndpoint : Endpoint<GetAllProductsQuery, ProductListResponse>
    {
        private readonly IMediator _mediator;
        public GetAllProductsEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("api/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllProductsQuery req, CancellationToken ct)
        {
            var products = await _mediator.Send(req);
            await SendAsync(products, StatusCodes.Status200OK, ct);
        }
    }
}
