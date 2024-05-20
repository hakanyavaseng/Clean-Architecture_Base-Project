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
            var products = await repositoryManager.GetReadRepository<Product>().GetAllAsync();

            List<GetAllProductsResponse> responses = new List<GetAllProductsResponse>();
            foreach (var product in products)
            {
                responses.Add(new GetAllProductsResponse()
                {
                    Title = product.Title,
                    Price = product.Price,
                    Discount = product.Discount,
                    Description = product.Description,
                    BrandId = product.BrandId,
                });
            }
            return responses;
        }
    }
}
