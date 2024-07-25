namespace BaseProject.Application.Interfaces.Mapper
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source, string? ignore = null);
        IList<TDestination> Map<TSource, TDestination>(IList<TSource> source, string? ignore = null);
        TDestination Map<TDestination>(object source, string? ignore = null);
        IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null);
    }
}
