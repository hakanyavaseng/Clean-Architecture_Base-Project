namespace BaseProject.Domain.Utils
{
    public class ExceptionHandlingModel
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public string Url { get; set; }
        public IDictionary<string, object> Extensions { get; init; } = new Dictionary<string, object>();
    }
}
