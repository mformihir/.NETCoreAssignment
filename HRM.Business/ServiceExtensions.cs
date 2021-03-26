using HRM.Business.Automapper;
using HRM.Data;
using HRM.Data.Interface;
using HRM.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRM.Business
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataServices(configuration);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            return services;
        }
    }
}
