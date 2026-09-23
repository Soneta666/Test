using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallReservationController : ControllerBase
    {
        private readonly IHallReservationsService hallReservationsService;
        public HallReservationController(
            IHallReservationsService hallReservationsService)
        {
            this.hallReservationsService = hallReservationsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await hallReservationsService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] uint id)
        {
            var item = await hallReservationsService.GetById(id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HallReservationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await hallReservationsService.Create(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] HallReservationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            await hallReservationsService.Update(dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] uint id)
        {
            await hallReservationsService.Delete(id);

            return Ok();
        }


        [HttpGet("schedule")]
        public async Task<IActionResult> GetSchedule([FromQuery] DateTime date)
        {
            var reservations = await hallReservationsService.GetSchedule(date);

            return Ok(reservations);
        }

    }
}
