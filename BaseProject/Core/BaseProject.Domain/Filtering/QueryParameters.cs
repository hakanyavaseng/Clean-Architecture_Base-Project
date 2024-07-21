using BaseProject.Domain.Enums;

namespace BaseProject.Domain.Filtering
{
    public class QueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; }
        public bool SortDescending { get; set; }
        public Dictionary<string, FilterParameters> Filters { get; set; } = new Dictionary<string, FilterParameters>();
    }

    public class FilterParameters
    {
        public object Value { get; set; }
        public MatchMode MatchMode { get; set; }
    }
}
