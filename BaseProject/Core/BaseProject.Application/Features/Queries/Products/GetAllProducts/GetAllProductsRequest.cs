using BaseProject.Domain.Filtering;
using MediatR;

namespace BaseProject.Application.Features.Queries.Products.GetAllProducts
{
    public record GetAllProductsRequest : IRequest<IList<GetAllProductsResponse>>
    {
        public QueryParameters QueryParameters { get; set; }
    }
}
