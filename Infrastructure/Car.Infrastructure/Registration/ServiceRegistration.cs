using Car.Infrastructure.Abstract;
using Car.Infrastructure.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Car.Infrastructure.Registration
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPhotoService, PhotoService>();
        }
    }
}
