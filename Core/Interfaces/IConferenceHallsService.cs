using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IConferenceHallsService
    {
        Task<IEnumerable<ConferenceHallDTO>> GetAll();
        Task<ConferenceHallDTO?> GetById(uint id);
        Task<ConferenceHallDTO?> Create(ConferenceHallDTO dto);
        Task Update(ConferenceHallDTO dto);
        Task Delete(uint id);

        Task<IEnumerable<ConferenceHallDTO>> GetAvailable(AvailableHallDTO dto);
    }
}
