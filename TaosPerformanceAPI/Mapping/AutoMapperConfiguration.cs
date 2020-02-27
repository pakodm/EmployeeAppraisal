using AutoMapper;

namespace TaosPerformanceAPI.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ModelToVMMappingProfile>();
            });
        }
    }
}
