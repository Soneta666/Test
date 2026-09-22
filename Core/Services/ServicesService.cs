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
    public class ServicesService : IServicesService
    {
        private readonly IRepository<Service> repo;
        private readonly IMapper mapper;

        public ServicesService(IRepository<Service> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDTO>> GetAll()
        {
            var result = await repo.GetAll();

            return mapper.Map<IEnumerable<ServiceDTO>>(result);
        }

        public async Task<ServiceDTO?> GetById(uint id)
        {
            if (id < 0) return null;

            var result = await repo.GetItemBySpec(new Spesifications.Services.ById(id));

            return mapper.Map<ServiceDTO>(result);
        }

        public async Task<ServiceDTO?> Create(ServiceDTO dto)
        {
            var result = await repo.Insert(mapper.Map<Service>(dto));
            await repo.Save();
            return mapper.Map<ServiceDTO>(result);
        }

        public async Task Update(ServiceDTO dto)
        {
            await repo.Update(mapper.Map<Service>(dto));
            await repo.Save();
        }

        public async Task Delete(uint id)
        {
            if (await repo.GetItemBySpec(new Spesifications.Services.ById(id)) == null) return;

            await repo.Delete(id);
            await repo.Save();
        }
    }
}
