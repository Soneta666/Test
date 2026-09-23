using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServicesService servicesService;
        public ServiceController(
            IServicesService servicesService)
        {
            this.servicesService = servicesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await servicesService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] uint id)
        {
            var item = await servicesService.GetById(id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            await servicesService.Create(dto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ServiceDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            await servicesService.Update(dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] uint id)
        {
            await servicesService.Delete(id);

            return Ok();
        }
    }
}
