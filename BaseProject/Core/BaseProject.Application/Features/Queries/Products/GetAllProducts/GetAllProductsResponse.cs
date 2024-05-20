namespace BaseProject.Application.Features.Queries.Products.GetAllProducts
{
    public record GetAllProductsResponse
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public Guid BrandId { get; set; }
    }
}
