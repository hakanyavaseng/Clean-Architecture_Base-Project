using BaseProject.Domain.Filtering;
using MediatR;

namespace BaseProject.Application.Features.Queries.Products.GetAllProducts
{
    public record GetAllProductsRequest : IRequest<IList<GetAllProductsResponse>>
    {
        public List<QueryFilter>? Filters { get; set; }
    }
}
