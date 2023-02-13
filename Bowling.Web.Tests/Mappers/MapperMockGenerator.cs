using AutoMapper;
using Bowling.Web.Mappers;

namespace Bowling.Web.Tests.Mappers
{
    public class MapperMockGenerator
    {
        public static IMapper CreateMockMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProfilesMapper());
            });
            return mappingConfig.CreateMapper();
        }
    }
}
