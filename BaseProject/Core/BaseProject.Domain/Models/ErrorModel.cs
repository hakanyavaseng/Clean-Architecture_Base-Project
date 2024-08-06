namespace BaseProject.Domain.Models
{
    public record ErrorModel
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
