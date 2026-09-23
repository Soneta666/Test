using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IHallReservationsService
    {
        Task<IEnumerable<HallReservationDTO>> GetAll();
        Task<HallReservationDTO?> GetById(uint id);
        Task<decimal> Create(HallReservationDTO dto);
        Task Update(HallReservationDTO dto);
        Task Delete(uint id);

        Task<IEnumerable<HallReservationDTO>> GetSchedule(DateTime date);
    }
}
