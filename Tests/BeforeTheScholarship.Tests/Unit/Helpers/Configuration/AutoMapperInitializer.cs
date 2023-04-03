using AutoMapper;

namespace BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
public static class AutoMapperInitializer
{
    public static IMapper Initialize()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("beforethescholarship."));
        var mapperConfiguration = new MapperConfiguration(opt => opt.AddMaps(assemblies));

        var mapper = mapperConfiguration.CreateMapper();

        return mapper;
    }
}
