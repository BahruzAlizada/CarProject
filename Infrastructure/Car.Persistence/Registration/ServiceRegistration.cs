using Car.Application.Abstract;
using Car.Persistence.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Car.Persistence.Registration
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IMarkaReadRepository,MarkaReadRepository>();
            services.AddScoped<IMarkaWriteRepository, MarkaWriteRepository>();

            services.AddScoped<ICityReadRepository, CityReadRepository>();
            services.AddScoped<ICityWriteRepository, CityWriteRepository>();

            services.AddScoped<IBanReadRepository, BanReadRepository>();
            services.AddScoped<IBanWriteRepository, BanWriteRepository>();

            services.AddScoped<IEngineTypeReadRepository,EngineTypeReadRepository>();
            services.AddScoped<IEngineTypeWriteRepository,EngineTypeWriteRepository>();

            services.AddScoped<IEngineCapacityReadRepository,EngineCapacityReadRepository>();
            services.AddScoped<IEngineCapacityWriteRepository,EngineCapacityWriteRepository>();

            services.AddScoped<IGearBoxReadRepository, GearBoxReadRepository>();
            services.AddScoped<IGearBoxWriteRepository, GearBoxWriteRepository>();

            services.AddScoped<ITransmitterReadRepository, TransmitterReadRepository>();
            services.AddScoped<ITransmitterWriteRepository, TransmitterWriteRepository>();

            services.AddScoped<IYearReadRepository, YearReadRepository>();
            services.AddScoped<IYearWriteRepository,YearWriteRepository>();

            services.AddScoped<ISeatReadRepository, SeatReadRepository>();
            services.AddScoped<ISeatWriteRepository, SeatWriteRepository>();

            services.AddScoped<IOwnerReadRepository, OwnerReadRepository>();
            services.AddScoped<IOwnerWriteRepository, OwnerWriteRepository>();

            services.AddScoped<IPreferenceReadRepository, PreferenceReadRepository>();
            services.AddScoped<IPreferenceWriteRepository, PreferenceWriteRepository>();

            services.AddScoped<IStatusReadRepository,StatusReadRepository>(); 
            services.AddScoped<IStatusWriteRepository,StatusWriteRepository>();

            services.AddScoped<IExteriorColorReadRepository, ExteriorColorReadRepository>();
            services.AddScoped<IExteriorColorWriteRepository, ExteriorColorWriteRepository>();

            services.AddScoped<IInteriorColorReadRepository, InteriorColorReadRepository>();
            services.AddScoped<IInteriorColorWriteRepository, InteriorColorWriteRepository>();

        }
    }
}
