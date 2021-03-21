using HRM.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Data
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<EmployeeContext>()
            //    .AddDefaultTokenProviders();

            return services;
        }
    }
}
