using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Spesifications;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class ConferenceHallsService : IConferenceHallsService
    {
        private readonly IRepository<ConferenceHall> repo;
        private readonly IRepository<Service> serviceRepo;
        private readonly IRepository<HallReservation> hallRepo;
        private readonly IMapper mapper;

        public ConferenceHallsService(IRepository<ConferenceHall> repo,
            IRepository<Service> serviceRepo,
            IRepository<HallReservation> hallRepo,
            IMapper mapper)
        {
            this.repo = repo;
            this.serviceRepo = serviceRepo;
            this.hallRepo = hallRepo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ConferenceHallDTO>> GetAll()
        {
            var result = await repo.GetListBySpec(new ConferenceHalls.GetAll());

            return mapper.Map<IEnumerable<ConferenceHallDTO>>(result);
        }

        public async Task<ConferenceHallDTO?> GetById(uint id)
        {
            if (id < 0) return null;

            var result = await repo.GetItemBySpec(new ConferenceHalls.ById(id));

            return mapper.Map<ConferenceHallDTO>(result);
        }

        public async Task<ConferenceHallDTO?> Create(ConferenceHallDTO dto)
        {
            var hall = mapper.Map<ConferenceHall>(dto);

            hall.Services = new List<Service>();

            foreach (var serviceDto in dto.Services ?? [])
            {
                var service = await serviceRepo.GetById(serviceDto.Id);

                if (service != null)
                {
                    hall.Services.Add(service);
                }
            }

            var result = await repo.Insert(hall);
            await repo.Save();

            return mapper.Map<ConferenceHallDTO>(result);
        }

        public async Task Update(ConferenceHallDTO dto)
        {
            var hall = (await repo.GetListBySpec(new ConferenceHalls.ById(dto.Id))).FirstOrDefault();
            if (hall == null)
                throw new Exception("Conference hall not found.");

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
            if (await repo.GetItemBySpec(new ConferenceHalls.ById(id)) == null) return;

            await repo.Delete(id);
            await repo.Save();
        }


        public async Task<IEnumerable<ConferenceHallDTO>> GetAvailable(AvailableHallDTO dto)
        {
            // Знаходимо бронювання на цей час
            var reservations = await hallRepo.GetListBySpec(new AvailableHalls(dto.Date, dto.During));

            // Id залів, які вже зайняті
            var reservedHallIds = reservations
                .Select(x => x.ConferenceHallId)
                .ToHashSet();

            // Знаходимо всі зали потрібної місткості
            var halls = await repo.GetListBySpec(new ConferenceHalls.ByCapacity(dto.Capacity));

            // Прибираємо зайняті
            var availableHalls = halls
                .Where(x => !reservedHallIds.Contains(x.Id));

            return mapper.Map<IEnumerable<ConferenceHallDTO>>(availableHalls);
        }
    }
}
