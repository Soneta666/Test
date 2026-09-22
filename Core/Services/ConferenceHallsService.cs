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
    public class ConferenceHallsService : IConferenceHallsService
    {
        private readonly IRepository<ConferenceHall> repo;
        private readonly IMapper mapper;

        public ConferenceHallsService(IRepository<ConferenceHall> repo, IMapper mapper)
        {
            this.repo = repo;
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
            var result = await repo.Insert(mapper.Map<ConferenceHall>(dto));
            await repo.Save();
            return mapper.Map<ConferenceHallDTO>(result);
        }

        public async Task Update(ConferenceHallDTO dto)
        {
            await repo.Update(mapper.Map<ConferenceHall>(dto));
            await repo.Save();
        }

        public async Task Delete(uint id)
        {
            if (await repo.GetItemBySpec(new ConferenceHalls.ById(id)) == null) return;

            await repo.Delete(id);
            await repo.Save();
        }
    }
}
