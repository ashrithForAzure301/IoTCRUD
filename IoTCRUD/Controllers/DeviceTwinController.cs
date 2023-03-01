using IoTCRUD.Models;
using IoTCRUD.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoTCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTwinController : ControllerBase
    {
        private readonly DeviceTwinRepository repository;

        public DeviceTwinController(DeviceTwinRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("reported")]
        public async Task<IActionResult> UpdateReportedProperties([FromBody] DeviceTwin model, string deviceId)
        {
            var twin = await repository.UpdateReportedPropertiesAsync(model, deviceId);
            return Ok(twin);
        }

        [HttpPost("desired")]
        public async Task<IActionResult> UpdateDesiredProperties([FromBody] DeviceTwin model, string deviceId)
        {
            var twin = await repository.UpdateDesiredPropertiesAsync(model, deviceId);
            return Ok(twin);
        }
    }
}
