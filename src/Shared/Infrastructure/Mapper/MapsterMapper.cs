using Common.Mapper;
using Mapster;

namespace Infrastructure.Mapper
{
    public class MapsterMapper : IMapper
    {
        TDestination IMapper.Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>();
        }
    }
}
