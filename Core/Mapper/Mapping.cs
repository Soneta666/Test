using Core.DTOs;
using Core.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Mapper
{
    public class Mapping
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<ConferenceHall, ConferenceHallDTO>
                .NewConfig()
                .Map(dest => dest.Services, src => src.Services);
            TypeAdapterConfig<ConferenceHallDTO, ConferenceHall>
                .NewConfig()
                .Map(dest => dest.Services, src => src.Services);

            TypeAdapterConfig<HallReservation, HallReservationDTO>
                .NewConfig()
                .Map(dest => dest.ConferenceHall, src => src.ConferenceHall)
                .Map(dest => dest.Services, src => src.Services);
            TypeAdapterConfig<HallReservationDTO, HallReservation>
                .NewConfig()
                .Map(dest => dest.ConferenceHall, src => src.ConferenceHall)
                .Map(dest => dest.Services, src => src.Services);

            TypeAdapterConfig<Service, ServiceDTO>
                .NewConfig();
            TypeAdapterConfig<ServiceDTO, Service>
                .NewConfig();
        }
    }
}
