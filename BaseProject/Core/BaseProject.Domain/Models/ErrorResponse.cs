namespace BaseProject.Domain.Models
{
    public record ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
