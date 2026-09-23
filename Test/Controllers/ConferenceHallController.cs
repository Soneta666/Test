using Core.DTOs;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceHallController : ControllerBase
    {
        private readonly IConferenceHallsService conferenceHallsService;
        public ConferenceHallController(
            IConferenceHallsService conferenceHallsService)
        {
            this.conferenceHallsService = conferenceHallsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await conferenceHallsService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] uint id)
        {
            var item = await conferenceHallsService.GetById(id);
            if (item == null) return NotFound();

            return Ok(item);
        }



        [HttpGet("/search")]
        public async Task<IActionResult> Search([FromQuery] AvailableHallDTO dto)
        {
            var item = await conferenceHallsService.GetAvailable(dto);
            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConferenceHallDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            await conferenceHallsService.Create(dto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ConferenceHallDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            await conferenceHallsService.Update(dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] uint id)
        {
            await conferenceHallsService.Delete(id);

            return Ok();
        }
    }
}
