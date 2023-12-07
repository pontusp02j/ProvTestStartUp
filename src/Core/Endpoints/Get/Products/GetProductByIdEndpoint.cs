using FastEndpoints;
using Core.Queries.Products;
using Core.Responses.Products;
using MediatR;

namespace Core.Endpoints.Products
{
    public class GetProductByIdEndpoint : Endpoint<GetProductByIdQuery, ProductResponse>
    {
        private readonly IMediator _mediator;
        public GetProductByIdEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("api/products/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetProductByIdQuery req, CancellationToken ct)
        {
            var product = await _mediator.Send(req, ct);
            await SendAsync(product, StatusCodes.Status200OK, ct);
        }
    }
}
