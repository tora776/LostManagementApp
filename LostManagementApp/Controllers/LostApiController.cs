using LostManagementApp.Models;
using LostManagementApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LostManagementApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LostApiController : ControllerBase
    {
        private readonly LostService _lostService;
        public LostApiController(LostService lostService)
        {
            _lostService = lostService;
        }

        [HttpPost("GetLost")]
        public IActionResult GetLost([FromBody] Lost lost)
        {
            if (lost == null)
            {
                return BadRequest("Invalid lost item data.");
            }
            var lostItems = _lostService.GetLost(lost);
            if (lostItems == null || !lostItems.Any())
            {
                return NotFound("No lost items found.");
            }
            return Ok(lostItems);
        }

        [HttpPost("InsertLost")]
        public IActionResult InsertLost([FromBody] Lost lost)
        {
            if (lost == null)
            {
                return BadRequest("Invalid lost item data.");
            }
            _lostService.InsertLost(lost);
            return CreatedAtAction(nameof(GetLost), new { id = lost.LostId }, lost);
        }

        [HttpPut("UpdateLost")]
        public IActionResult UpdateLost([FromBody] Lost lost)
        {
            if (lost == null || lost.LostId <= 0)
            {
                return BadRequest("Invalid lost item data.");
            }
            _lostService.UpdateLost(lost);
            return NoContent();
        }

        [HttpDelete("DeleteLost")]
        public IActionResult DeleteLost([FromBody] Lost lost)
        {
            if (lost == null || lost.LostId <= 0)
            {
                return BadRequest("Invalid lost item data.");
            }
            _lostService.DeleteLost(lost);
            return NoContent();
        }
    }
}
