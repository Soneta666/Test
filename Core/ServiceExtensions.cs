using Core.Entities;
using Core.Interfaces;
using Core.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Core
{
    public static class ServiceExtensions
    {
        public static void AddMapster(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, MapsterMapper.Mapper>();
        }
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IConferenceHallsService, ConferenceHallsService>();
            services.AddScoped<IHallReservationsService, HallReservationsService>();
            services.AddScoped<IServicesService, ServicesService>();
        }
    }
}
