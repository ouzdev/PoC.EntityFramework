namespace Common.Mapper
{
    public interface IMapper
    {
        TDestination Map<TDestination>(object source);
    }
}
