using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Spesifications;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Spesifications.HallReservations;

namespace Core.Services
{
    public class HallReservationsService : IHallReservationsService
    {
        private readonly IRepository<HallReservation> repo;
        private readonly IRepository<Service> serviceRepo;
        private readonly IRepository<ConferenceHall> hallRepo;
        private readonly IMapper mapper;

        public HallReservationsService(IRepository<HallReservation> repo, 
            IRepository<Service> serviceRepo,
            IRepository<ConferenceHall> hallRepo,
            IMapper mapper)
        {
            this.repo = repo;
            this.serviceRepo = serviceRepo;
            this.hallRepo = hallRepo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<HallReservationDTO>> GetAll()
        {
            var result = await repo.GetListBySpec(new HallReservations.GetAll());

            return mapper.Map<IEnumerable<HallReservationDTO>>(result);
        }

        public async Task<HallReservationDTO?> GetById(uint id)
        {
            if (id < 0) return null;

            var result = await repo.GetItemBySpec(new HallReservations.ById(id));

            return mapper.Map<HallReservationDTO>(result);
        }

        public async Task<decimal> Create(HallReservationDTO dto)
        {
            decimal cost = 0;

            var hall = mapper.Map<HallReservation>(dto);

            hall.Services = new List<Service>();

            foreach (var serviceDto in dto.Services ?? [])
            {
                var service = await serviceRepo.GetById(serviceDto.Id);

                if (service != null)
                {
                    hall.Services.Add(service);
                    cost += service.Cost;
                }
            }

            var result = await repo.Insert(hall);
            await repo.Save();

            cost += CalculateRentalCost(
                (await hallRepo.GetById(dto.ConferenceHallId)).BaseRentalCost,
                dto.Date,
                dto.During);

            return cost;

        }

        public async Task Update(HallReservationDTO dto)
        {
            var hall = (await repo.GetListBySpec(new HallReservations.ById(dto.Id))).FirstOrDefault();
            if (hall == null)
                throw new Exception("Hall reservation not found.");

            mapper.Map(dto, hall);

            var services = new List<Service>();

            foreach (var serviceDto in dto.Services ?? [])
            {
                var service = await serviceRepo.GetById(serviceDto.Id);

                if (service != null)
                    services.Add(service);
            }

            // Оновлюємо many-to-many
            hall.Services.Clear();

            foreach (var service in services)
            {
                hall.Services.Add(service);
            }

            await repo.Save();
        }

        public async Task Delete(uint id)
        {
            if (await repo.GetItemBySpec(new HallReservations.ById(id)) == null) return;

            await repo.Delete(id);
            await repo.Save();
        }

        public async Task<IEnumerable<HallReservationDTO>> GetSchedule(DateTime date)
        {
            var spec = new HallReservationsByDateSpecification(date);

            var reservations = await repo.GetListBySpec(spec);

            return mapper.Map<IEnumerable<HallReservationDTO>>(reservations);
        }

        //Метод для розрахунку вартості
        private decimal CalculateRentalCost(
            uint baseCost,
            DateTime startDate,
            uint duration)
        {
            decimal total = 0;

            for (int i = 0; i < duration; i++)
            {
                var currentHour = startDate.AddHours(i).TimeOfDay;

                decimal coefficient;

                if (currentHour >= TimeSpan.FromHours(12) &&
                    currentHour < TimeSpan.FromHours(14))
                {
                    // Пікові години +15%
                    coefficient = 1.15m;
                }
                else if (currentHour >= TimeSpan.FromHours(18) &&
                         currentHour < TimeSpan.FromHours(23))
                {
                    // Вечірні години -20%
                    coefficient = 0.80m;
                }
                else if (currentHour >= TimeSpan.FromHours(6) &&
                         currentHour < TimeSpan.FromHours(9))
                {
                    // Ранкові години -10%
                    coefficient = 0.90m;
                }
                else
                {
                    // Стандартні години
                    coefficient = 1.00m;
                }

                total += baseCost * coefficient;
            }

            return total;
        }
    }
}
