using BaseProject.Application.Interfaces.Repositories.Common;
using BaseProject.Domain.Entities;
using MediatR;

namespace BaseProject.Application.Features.Queries.Products.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, IList<GetAllProductsResponse>>
    {
        private readonly IRepositoryManager repositoryManager;
        public GetAllProductsHandler(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }
        public async Task<IList<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var reponse = await repositoryManager.GetReadRepository<Product>().GetWithFilterAsync(request.QueryParameters);
            return reponse.Select(x => new GetAllProductsResponse
            {
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                BrandId = x.BrandId,
                Discount = x.Discount,
            }).ToList();
        }
    }
}
