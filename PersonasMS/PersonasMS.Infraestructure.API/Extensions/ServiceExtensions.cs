using AutoMapper.Data;
using PersonasMS.Infraestructure.API.Automapper;

namespace PersonasMS.Infraestructure.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IConfiguration Configuration { get; private set; }

        public static IConfiguration RegisterConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
            return Configuration;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services) => services.AddAutoMapper(cfg =>
        {
            cfg.AddDataReaderMapping();
        }, typeof(MappingProfile));
    }
}
