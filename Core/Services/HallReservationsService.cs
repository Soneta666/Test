using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Spesifications;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class HallReservationsService : IHallReservationsService
    {
        private readonly IRepository<HallReservation> repo;
        private readonly IMapper mapper;

        public HallReservationsService(IRepository<HallReservation> repo, IMapper mapper)
        {
            this.repo = repo;
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

        public async Task<HallReservationDTO?> Create(HallReservationDTO dto)
        {
            var result = await repo.Insert(mapper.Map<HallReservation>(dto));
            await repo.Save();
            return mapper.Map<HallReservationDTO>(result);
        }

        public async Task Update(HallReservationDTO dto)
        {
            await repo.Update(mapper.Map<HallReservation>(dto));
            await repo.Save();
        }

        public async Task Delete(uint id)
        {
            if (await repo.GetItemBySpec(new HallReservations.ById(id)) == null) return;

            await repo.Delete(id);
            await repo.Save();
        }
    }
}
