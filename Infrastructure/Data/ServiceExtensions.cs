using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public static class ServiceExtensions
    {
        public static void AddDbContext(this IServiceCollection services, string connStr)
        {
            services.AddDbContext<TestDbContext>(opt => opt.UseSqlServer(connStr));
        }

        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
