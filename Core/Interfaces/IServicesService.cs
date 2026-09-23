using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IServicesService
    {
        Task<IEnumerable<ServiceDTO>> GetAll();
        Task<ServiceDTO?> GetById(uint id);
        Task<ServiceDTO?> Create(ServiceDTO dto);
        Task Update(ServiceDTO dto);
        Task Delete(uint id);
    }
}
