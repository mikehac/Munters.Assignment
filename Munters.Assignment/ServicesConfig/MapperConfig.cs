using AutoMapper;
using Munters.Assignment.Mapping;

namespace Munters.Assignment.ServicesConfig
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var MappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new EntityiesProfile());
            });
            return MappingConfig;
        }
    }
}
