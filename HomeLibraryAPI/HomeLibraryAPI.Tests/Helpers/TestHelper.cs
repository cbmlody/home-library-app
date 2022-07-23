using AutoMapper;

namespace HomeLibraryAPI.Tests.Helpers
{
    internal static class TestHelper
    {
        internal static IMapper InitializeMapper()
        {
            var profile = new MappingProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            return new Mapper(config);
        }
    }
}
